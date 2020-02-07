#include <ESP8266WiFi.h>
#include "WebSocketClient.h"

const char *ssid = "buzka";
const char *password = "jedominantni";

WebSocketClient ws(true);

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
}

void loop() {

  if(!ws.isConnected())
  {
    ws.connect("192.168.43.169", "/", 5050);
    Serial.println("Connected to WS server!");
  }
  else
  {
    ws.send("lmao");
  }
  
  delay(500);
}
