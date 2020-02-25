import requests
import json

API_KEY = 'zQ76RyFye0Gc8yeS'

class NAGApi:

    Headers = {
        'content-type': 'application/json',
        'X-Api-Key': API_KEY,
        'Accept': 'application/json'
    }

    @staticmethod
    def get_luminosity():
        return requests.get("https://api.nag-iot.zcu.cz/v2/variable/luminosity", headers = NAGApi.Headers).json()

    @staticmethod
    def set_luminosity(data):
        response = requests.put('https://api.nag-iot.zcu.cz/v2/variable/luminosity', headers = NAGApi.Headers, data = json.dumps(data))
        print(json.dumps(data))
        return response.status_code

    @staticmethod
    def set_luminosity_value(value):
        data = NAGApi.get_luminosity()
        data['value'] = value
        return True if NAGApi.set_luminosity(data) == 200 else False

if __name__ == "__main__":
    NAGApi.set_luminosity_value(20)
    response = requests.get("https://api.nag-iot.zcu.cz/v2/variable/luminosity", headers = NAGApi.Headers)
    print(response.json())
    print('setting to 10.0f')
    NAGApi.set_luminosity_value(10)
    response = requests.get("https://api.nag-iot.zcu.cz/v2/variable/luminosity", headers = NAGApi.Headers)
    print(response.json())
