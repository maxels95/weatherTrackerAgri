import requests
from datetime import datetime, timezone

def post_growth_stage(data):
    url = 'http://localhost:5028/api/GrowthStage'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added Growth Stage: {data['stageName']}")
    else:
        print(f"Failed to add Growth Stage {data['stageName']}. Status code: {response.status_code}, Message: {response.text}")

def utc_datetime(year, month, day):
    # Create a datetime object in local time
    local_dt = datetime(year, month, day)
    # Convert local datetime to UTC
    return local_dt.astimezone(timezone.utc)

def main():
    growth_stages = [
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

    for stage in growth_stages:
        post_growth_stage(stage)

if __name__ == "__main__":
    main()