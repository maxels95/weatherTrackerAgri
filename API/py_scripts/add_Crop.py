import requests

def create_crop():
    url = 'http://localhost:5028/api/Crop'
    headers = {'Content-Type': 'application/json'}
    payload = {
        "name": "Cocoa",
        "growthCycles": [2],
        "locations": [18, 19, 20, 21, 22, 23, 24, 25, 26, 27],
        "healthScores": []
    }

    response = requests.post(url, json=payload, headers=headers)
    if response.status_code == 201:
        print("Successfully added the crop.")
    else:
        print(f"Failed to add the crop. Status code: {response.status_code}, Response: {response.text}")

if __name__ == "__main__":
    create_crop()
