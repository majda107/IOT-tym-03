#include <ESP8266WiFi.h>
#include <WebSocketClient.h>

WebSocketClient webSocketClient;
WiFiClient client;



// ====
// CONFIG
// ====


// WIFI SSID
const char *ssid = "buzka";
// WIFI PASSWORD
const char *password = "jedominantni";

char path[] = "/";

// SERVER IP
char host[] = "192.168.43.169";

// ====
// END CONFIG
// ====




void init_vars() {
  // Init console
  Serial.begin(115200);
  Serial.println("Serial started at 115200");
}

void connect_wifi() {
  WiFi.begin(ssid,password);
  int count = 0; 
  while ( (WiFi.status() != WL_CONNECTED) && count < 17) 
  {
      Serial.print(".");  delay(500);  count++;
  }
 
  if (WiFi.status() != WL_CONNECTED)
  { 
     Serial.println("");  Serial.print("Failed to connect to ");  Serial.println(ssid);
     while(1);
  }

  Serial.println(F("[CONNECTED]"));   
  Serial.print("[IP ");  
  Serial.print(WiFi.localIP()); 
  Serial.println("]");
}

void connect_ws() {
  // Connect to the websocket server
  if (client.connect(host, 5050)) {
    Serial.println("Connected");
  } else {
    Serial.println("Connection failed.");
    while(1) {
      // Hang on failure (RESTART)
    }
  }

  webSocketClient.path = path;
  webSocketClient.host = host;

  // Handshake with the server
  webSocketClient.path = path;
  webSocketClient.host = host;
  if (webSocketClient.handshake(client)) {
    Serial.println("Handshake successful");
  } else {
    Serial.println("Handshake failed.");
    while(1) {
      // Hang on failure
    }  
  }
}


void send_data() {
  // measure light
  float lux = random(10000, 100000) / (float)100;
  webSocketClient.sendData("light;" + String(lux, 6));
  
  // measure battery voltage
  int voltage = random(20, 30) / (float)10;
  delay(100);
  float f_voltage = voltage * 0.00615615615;
  webSocketClient.sendData("voltage;" + String(f_voltage, 6));

  //measure temp and humidity
  float humidity = random(50, 90);
  float temperature = random(27000, 36000) / (float)1000;
  
  webSocketClient.sendData("humidity;" + String(humidity, 6));
  webSocketClient.sendData("temperature;" + String(temperature, 6));
}


void setup() {
  init_vars();
  connect_wifi();
  connect_ws();
}



void loop() {
  if (client.connected()) {  
    //webSocketClient.sendData("test lol: " + String(random(0, 100)));
    send_data();
    Serial.println("Data sent... sleeping for 10s");
    ESP.deepSleep(10000000);
  }
  else
  {
    Serial.println("Client not connected... retrying in 5s");
    ESP.deepSleep(5000000);
  }
}
