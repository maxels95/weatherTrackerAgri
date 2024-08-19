import requests

def post_condition_threshold(data):
    url = 'http://localhost:5028/api/ConditionThreshold'
    response = requests.post(url, json=data)
    if response.status_code == 201:
        print(f"Successfully added: {data['Name']}")
    else:
        print(f"Failed to add {data['Name']}. Status code: {response.status_code}, Message: {response.text}")

def main():
    thresholds = [
        {
        "id": 1,
        "name": "Coffee Flowering Adverse Conditions",
        "minTemperature": 10,
        "maxTemperature": 30,
        "mildMinTemp": 25,
        "mildMaxTemp": 29,
        "mildResilienceDuration": 6,
        "moderateMinTemp": 30,
        "moderateMaxTemp": 34,
        "moderateResilienceDuration": 4,
        "severeMinTemp": 35,
        "severeMaxTemp": 38,
        "severeResilienceDuration": 2,
        "extremeMinTemp": 39,
        "extremeMaxTemp": 50,
        "extremeResilienceDuration": 1,
        "optimalHumidity": 70,
        "minHumidity": 50,
        "maxHumidity": 90,
        "minRainfall": 10,
        "maxRainfall": 50,
        "maxWindSpeed": 40
    },
    {
        "id": 2,
        "name": "Coffee Fruit Development Adverse Conditions",
        "minTemperature": 12,
        "maxTemperature": 30,
        "mildMinTemp": 25,
        "mildMaxTemp": 29,
        "mildResilienceDuration": 7,
        "moderateMinTemp": 30,
        "moderateMaxTemp": 34,
        "moderateResilienceDuration": 5,
        "severeMinTemp": 35,
        "severeMaxTemp": 38,
        "severeResilienceDuration": 3,
        "extremeMinTemp": 39,
        "extremeMaxTemp": 50,
        "extremeResilienceDuration": 1,
        "optimalHumidity": 60,
        "minHumidity": 40,
        "maxHumidity": 85,
        "minRainfall": 15,
        "maxRainfall": 45,
        "maxWindSpeed": 35
    },
    {
        "id": 3,
        "name": "Coffee Flowering Optimal Conditions",
        "minTemperature": 18,
        "maxTemperature": 24,
        "mildMinTemp": 0,
        "mildMaxTemp": 0,
        "mildResilienceDuration": 0,
        "moderateMinTemp": 0,
        "moderateMaxTemp": 0,
        "moderateResilienceDuration": 0,
        "severeMinTemp": 0,
        "severeMaxTemp": 0,
        "severeResilienceDuration": 0,
        "extremeMinTemp": 0,
        "extremeMaxTemp": 0,
        "extremeResilienceDuration": 0,
        "optimalHumidity": 75,
        "minHumidity": 60,
        "maxHumidity": 85,
        "minRainfall": 20,
        "maxRainfall": 30,
        "maxWindSpeed": 20
    },
    {
        "id": 4,
        "name": "Coffee Fruit Development Optimal Conditions",
        "minTemperature": 19,
        "maxTemperature": 25,
        "mildMinTemp": 0,
        "mildMaxTemp": 0,
        "mildResilienceDuration": 0,
        "moderateMinTemp": 0,
        "moderateMaxTemp": 0,
        "moderateResilienceDuration": 0,
        "severeMinTemp": 0,
        "severeMaxTemp": 0,
        "severeResilienceDuration": 0,
        "extremeMinTemp": 0,
        "extremeMaxTemp": 0,
        "extremeResilienceDuration": 0,
        "optimalHumidity": 70,
        "minHumidity": 50,
        "maxHumidity": 80,
        "minRainfall": 20,
        "maxRainfall": 35,
        "maxWindSpeed": 25
    }
    ]

    for threshold in thresholds:
        post_condition_threshold(threshold)

if __name__ == "__main__":
    main()
