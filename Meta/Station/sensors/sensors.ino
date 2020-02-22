#include <DHT.h>
DHT dht;

#include <Wire.h>
#include <BH1750.h>
BH1750 lightMeter;


void setup() {
  // Init console
  Serial.begin(74880);
  Serial.println("test");
  
  // init lightmeter
  Wire.begin(D3, D2);
  lightMeter.begin();
  Serial.println(F("BH1750 Test"));
  
  // init temp and humidity sensors
  dht.setup(D1);
}
void loop() {
  // measure light
  float lux = lightMeter.readLightLevel();
  Serial.print("Light: ");
  Serial.print(lux);
  Serial.println(" lx");
  delay(1000);
  
  // measure battery voltage
  int voltage = analogRead(A0);
  delay(100);
  float f_voltage = voltage * 0.00615615615;
  Serial.print("Measured : ");
  Serial.println(voltage);
  Serial.print("Voltage :");
  Serial.println(f_voltage);
  delay(3000);

  //measure temp and humidity
  delay(dht.getMinimumSamplingPeriod()); /* Delay of amount equal to sampling period */
  float humidity = dht.getHumidity();/* Get humidity value */
  float temperature = dht.getTemperature();/* Get temperature value */
  Serial.println(dht.getStatusString());/* Print status of communication */
  Serial.print("Vlhkost: ");
  Serial.println(humidity, 1);
  Serial.print("Teplota: ");
  Serial.println(temperature, 1);
  //deep sleep 4 bat save
  Serial.println("Deep sleep for 10 seconds");
  ESP.deepSleep(10000000);
  //then wake (reset)
}
