#include <DHT.h>

#include <Wire.h>
#include <BH1750.h>

#include <ESP8266WiFi.h>
#include <WebSocketClient.h>


DHT dht;
BH1750 lightMeter;

WebSocketClient webSocketClient;
WiFiClient client;



// ====
// CONFIG
// ====


// WIFI SSID
const char *ssid = "HELLDESK-NOVAJD";
// WIFI PASSWORD
const char *password = "!@#$%^&*()";

char path[] = "/";

// SERVER IP
char host[] = "192.168.137.118";

// ====
// END CONFIG
// ====




void init_vars() {
  // Init console
  Serial.begin(115200);
  Serial.println("Serial started at 115200");
  
  // init lightmeter
  //Wire.begin(D3, D2);
  Wire.begin(3, 2);
  lightMeter.begin();
  Serial.println(F("BH1750 Test"));
  
  // init temp and humidity sensors
  //dht.setup(D1);
  dht.setup(1);
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
  float lux = lightMeter.readLightLevel();
  webSocketClient.sendData("light;" + String(lux, 6));
  
  // measure battery voltage
  int voltage = analogRead(A0);
  delay(100);
  float f_voltage = voltage * 0.00615615615;
  webSocketClient.sendData("voltage;" + String(f_voltage, 6));

  //measure temp and humidity
  delay(dht.getMinimumSamplingPeriod()); /* Delay of amount equal to sampling period */
  float humidity = dht.getHumidity();/* Get humidity value */
  float temperature = dht.getTemperature();/* Get temperature value */
  Serial.println(dht.getStatusString());/* Print status of communication */

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
    delay(10000);
    //ESP.deepSleep(10000000);
  }
  else
  {
    Serial.println("Client not connected... retrying in 5s");
    //ESP.deepSleep(5000000);
    delay(5000);
  }
}
