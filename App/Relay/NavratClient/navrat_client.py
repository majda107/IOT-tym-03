import websocket

class NavratClient:
    def __init__(self):
        self.connection = None

    def start(self):
        self.connection = websocket.create_connection("ws://127.0.0.1:5050")

    # @staticmethod
    # def print_error(error):
    #     print('error: ', error)

    def send_data(self, sensor, data):
        self.connection.send(f'{sensor};{data}')

    def send_name(self, name):
        self.connection.send(f'{name}')


if __name__ == "__main__":
    client = NavratClient()
    client.start()

    print("Sending test data...")
    client.send_data("relay_test", 69)
    print("Data sent...")