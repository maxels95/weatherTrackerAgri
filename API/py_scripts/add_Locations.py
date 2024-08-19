import requests

def add_location(data):
    url = 'http://localhost:5028/api/Location'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added location: {data['name']}")
    else:
        print(f"Failed to add location {data['name']}. Status code: {response.status_code}, Response: {response.text}")

def main():
    locations = [
        {"id": 0, "country": "Côte d'Ivoire", "name": "Soubré, Côte d'Ivoire", "latitude": 5.7874, "longitude": -6.5943},
        {"id": 0, "country": "Ghana", "name": "Kumasi, Ghana", "latitude": 6.6884, "longitude": -1.6244},
        {"id": 0, "country": "Indonesia", "name": "Sulawesi, Indonesia", "latitude": -2.9167, "longitude": 119.6475},
        {"id": 0, "country": "Brazil", "name": "Ilhéus, Brazil", "latitude": -14.793, "longitude": -39.0469},
        {"id": 0, "country": "Ecuador", "name": "Los Ríos, Ecuador", "latitude": -1.3736, "longitude": -79.4597},
        {"id": 0, "country": "Nigeria", "name": "Ondo, Nigeria", "latitude": 7.1000, "longitude": 4.8333},
        {"id": 0, "country": "Peru", "name": "San Martín, Peru", "latitude": -6.4878, "longitude": -76.3692},
        {"id": 0, "country": "Cameroon", "name": "Southwest Region, Cameroon", "latitude": 4.2550, "longitude": 9.2303},
        {"id": 0, "country": "Dominican Republic", "name": "San Francisco de Macorís, Dominican Republic", "latitude": 19.3000, "longitude": -70.2500},
        {"id": 0, "country": "Colombia", "name": "Santander, Colombia", "latitude": 6.6437, "longitude": -73.6547}
    ]

    for location in locations:
        add_location(location)

if __name__ == "__main__":
    main()
