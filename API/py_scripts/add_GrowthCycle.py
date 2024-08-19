import requests
from datetime import datetime, timezone

def post_growth_cycle(data):
    url = 'http://localhost:5028/api/GrowthCycle'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added Growth Cycle: {data['name']}")
    else:
        print(f"Failed to add Growth Cycle {data['name']}. Status code: {response.status_code}, Message: {response.text}")

def utc_datetime(year, month, day, hour, minute):
    return datetime(year, month, day, hour, minute, tzinfo=timezone.utc).isoformat()

def main():
    growth_cycle = {
        "id": 0,  # This should be auto-generated by the database if configured
        "name": "Cocoa Growth Cycle",
        "stages": [
            {
                "id": 3,
                "stageName": "Cocoa Flowering",
                "startDate": "2024-03-01T00:00:00.000Z",
                "endDate": "2024-04-15T00:00:00.000Z",
                "optimalConditions": 7,
                "adverseConditions": 5,
                "resilienceDurationInDays": 7,
                "historicalAdverseImpactScore": 30
            },
            {
                "id": 4,
                "stageName": "Cocoa Pod Development",
                "startDate": "2024-04-16T00:00:00.000Z",
                "endDate": "2024-09-01T00:00:00.000Z",
                "optimalConditions": 8,
                "adverseConditions": 6,
                "resilienceDurationInDays": 10,
                "historicalAdverseImpactScore": 50
            },
            {
                "id": 5,
                "stageName": "Cocoa Maintenance",
                "startDate": "2024-09-02T00:00:00.000Z",
                "endDate": "2024-12-31T00:00:00.000Z",
                "optimalConditions": 10,
                "adverseConditions": 9,
                "resilienceDurationInDays": 14,
                "historicalAdverseImpactScore": 40
            }
        ]
    }

    post_growth_cycle(growth_cycle)

if __name__ == "__main__":
    main()
