#include <ESP8266WiFi.h>
#include <WebSocketClient.h>

const char *ssid = "buzka";
const char *password = "jedominantni";

char path[] = "/";
char host[] = "192.168.43.169";

//const char *ssid = "HELLDESK-NOVAJD";
//const char *password = "!@#$%^&*()";

//char path[] = "/";
//char host[] = "192.168.137.118";

WebSocketClient webSocketClient;
WiFiClient client;

bool connecting;

void setup() {
  Serial.begin(115200);
  Serial.println();
  Serial.println("Serial started at 115200");
  Serial.println();

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

  Serial.println(F("[CONNECTED]"));   Serial.print("[IP ");  Serial.print(WiFi.localIP()); 
  Serial.println("]");

  // Connect to the websocket server
  if (client.connect("192.168.43.169", 5050)) {
    Serial.println("Connected");
  } else {
    Serial.println("Connection failed.");
    while(1) {
      // Hang on failure
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

void loop() {
  Serial.println("Sending...");

  if (client.connected()) {  
    webSocketClient.sendData("test lol: " + String(random(0, 100)));
    Serial.println("Data send...");
  }
  else
  {
    Serial.println("Client not connected...");
  }
  
  delay(5000);
}
