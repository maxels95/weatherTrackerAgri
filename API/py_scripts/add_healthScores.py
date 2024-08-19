import requests
from datetime import datetime, timezone, timedelta

def add_health_scores():
    url = 'http://localhost:5028/api/HealthScore'
    headers = {'Content-Type': 'application/json'}

    crop_id = 4
    location_ids = [18, 19, 20, 21, 22, 23, 24, 25, 26, 27]
    
    # Calculate a date 5 years ago in UTC
    date_5_years_ago = (datetime.now(timezone.utc) - timedelta(days=5*365))

    for location_id in location_ids:
        payload = {
            "id": 0,  # Let the server generate the ID
            "cropId": crop_id,
            "locationId": location_id,
            "date": date_5_years_ago.isoformat(),  # Date is explicitly in UTC
            "score": 0  # You can modify this score if needed
        }

        response = requests.post(url, json=payload, headers=headers)
        if response.status_code == 201:
            print(f"Successfully added HealthScore for location ID: {location_id}")
        else:
            print(f"Failed to add HealthScore for location ID: {location_id}. Status code: {response.status_code}, Response: {response.text}")

if __name__ == "__main__":
    add_health_scores()
