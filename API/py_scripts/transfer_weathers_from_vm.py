import requests
import json
from datetime import datetime, timezone

def get_weathers_from_vm():
    url = 'http://192.168.1.141:5045/api/Weather'   # Adjust to your API endpoint
    response = requests.get(url)

    if response.status_code == 200:
        weathers = response.json()
        return weathers
    else:
        print(f"Failed to fetch weathers. Status code: {response.status_code}, Response: {response.text}")
        return []

def get_all_locations_local():
    url = 'http://localhost:5028/api/Location'  # Adjust the URL to your actual API endpoint
    response = requests.get(url)

    if response.status_code == 200:
        locations = response.json()
        return locations['$values']  # Extract the list of locations
    else:
        print(f"Failed to fetch locations. Status code: {response.status_code}, Response: {response.text}")
        return []

def map_location_ids(vm_weathers, local_locations):
    local_location_map = {(loc['latitude'], loc['longitude']): loc['id'] for loc in local_locations}

    for weather in vm_weathers:
        location = weather['location']

        if isinstance(location, str):
            location = json.loads(location)

        key = (location['latitude'], location['longitude'])

        if key in local_location_map:
            weather['locationId'] = local_location_map[key]
        else:
            print(f"Warning: Location with coordinates {key} not found in local locations.")
    
    return vm_weathers

def convert_to_utc(date_str):
    # Parse the date string and ensure it's in UTC
    date_obj = datetime.fromisoformat(date_str)
    if date_obj.tzinfo is None:
        date_obj = date_obj.replace(tzinfo=timezone.utc)  # Assume UTC if no timezone info is present
    date_obj = date_obj.astimezone(timezone.utc)
    
    # Return the ISO formatted date with milliseconds and 'Z' to indicate UTC
    formatted_date = date_obj.isoformat(timespec='milliseconds').replace('+00:00', 'Z')
    print(f"Converted date: {formatted_date}")
    return formatted_date

def post_weather_data(weather):
    url = 'http://localhost:5028/api/Weather'
    data = {
        "id": 0,  # Let the server generate the ID
        "date": convert_to_utc(weather["date"]),
        "temperature": weather["temperature"],
        "humidity": weather["humidity"],
        "rainfall": weather["rainfall"],
        "windSpeed": weather["windSpeed"],
        "locationId": weather["locationId"]  # Use the mapped location ID
    }
    
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Weather data for {weather['date']} added successfully.")
    else:
        print(f"Failed to add weather data for {weather['date']}. Status code: {response.status_code}, Message: {response.text}")
        print("Failed JSON object:", json.dumps(data, indent=4))

def main():
    # Fetch all weathers from the VM database
    vm_weathers = get_weathers_from_vm()

    # Fetch all local locations
    local_locations = get_all_locations_local()

    # Map location IDs from VM weathers to local locations
    updated_weathers = map_location_ids(vm_weathers, local_locations)

    # Post each weather record to the local API
    if updated_weathers:
        for weather in updated_weathers:
            post_weather_data(weather)
    else:
        print("No weather records found.")

if __name__ == "__main__":
    main()
