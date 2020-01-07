# NAG-IoT
## Návrat

1. Připojení snímače

2. Použité knihovny - sprovoznění I2C sběrnice
	- Na sprovoznění BH1750 snímače, jenž má sběrnici I2C jsmě použili knihovnu smbus, který lze inicializovat příkazem `bus = smbus.SMBus(1)` a následně číst data příkazem `bus.read_i2c_block_data(addr, 0x20)`, kde `addr` je adresa snímače (v našem případě 0x23). 

3. Změna svitu diody
	- Další věc, co jsme potřebovali rozchodit, je změna svitu diody, toho lze dosáhnout přes PWM. PWM lze inicializovat příkazem `GPIO.PWM(36, 100)`. Následnou hodnotu PWM na výstupu GPIO pinu lze měnit příkazem `pwm.ChangeDutyCycle(hodnota)`, nutno však říct, že ne všechny piny tuto funkci podporují. 

4. Sjednocení snímače a regulace svitu diody
	- Poslední, co nám zbývá je tyto dvě funční části programu spojit. Logika je velmi jednoduchá, stačí udělat nekonečnou `while True` smyčku, jenž bude kontrolovat hodnotu ze snímače a pokud hodnota menší jak nějaká hraniční konstanta (v našem kódu `LUX_CAP`) tak nastavíme PWM na diodě ve zbývajícím rozmezí.

### Schema zapojení

- Konečné schema zapojení na které funguje náš testovací zdrojový kod

![Schema zapojení obvodu](úkol3.png =250x)
