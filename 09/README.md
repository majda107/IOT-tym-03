# NAG-IoT
## Návrat

1. Zapojení displeje
    - Náš malý OLED 128x64 displej má pouze 4 výstupy, není tedy nijak složité ho připojit k Raspberry. Je k tomu zapotřeba 3V VCC (napájení), GND (země), SCL a SDA pro data. Ke komunikaci s mikropočítačem mu stačí tak málo pinů, díky domu že využívá snad nejrozšířenější I2C sběrnici.

2. Zapnutí I2C na Raspberry. 
    - Raspberry nemá by-default zapnutou podporu I2C, není však nic těžkého ji zapnout, stačí vlézt do nastavení pomocí příkazu `raspi-config`, zde sjet kurzorem na I2C a to povolit.

3. Instalace potřebných knihoven
    - K instalaci budeme potřebovat pár dalších balíčků a jednu knihovnu, balíčky lze všechny doinstalovat pomocí příkazu `sudo apt-get install -y python-imaging python-smbus i2c-tools`.

    - Knihovna je opensource a dostupná na GitHubu přímo od Adafruitu, není tedy nic jednoduššího než ji celou naklonovat pomocí `git clone https://github.com/adafruit/Adafruit_Python_SSD1306.git` a následně spustit instalační skript ve složce `Adafruit_Python_SSD1306`. Skript pustíme příkazem `python setup.py install`. V examples najdeme programy, pro odzkoušení displeje, je tedy dobré nějaký z nich následovně pustit a ověřit si, že displej opravdu funguje.

4. Psaní programu pro displej
    - Napsat program pro displej už může být nepatrně složitější jak předchozí úkoly, stále se však nejedná o nic extra složitého. Nejdříve naimportujeme potřebné knihovny `import Adafruit_SSD1306`, `from PIL import Image`, `from PIL import ImageDraw`, `from PIL import ImageFont` a pomocnou knihovnu `import time`.

    - Instanci displeje vytvoříme opět velmi jednoduše, nějakým takovýmto příkazem `disp = Adafruit_SSD1306.SSD1306_128_64(rst=None)` a zahájíme komunikaci pomocí `disp.begin()`. Poté pomocí příkazu `disp.clear()` a `disp.display()` vyčístíme celou obrazovku.

    - Dále bude potřeba vytvořit si prazdný obrazec, do kterého budeme ukládat data co se mají vykreslit a poté je vykreslíme naráz (tzv. backbuffer), ten můžeme vytvořit pomocí `image = Image.new('1', (disp.width, disp.height))` a získat z něho grafiku pomocí `draw = ImageDraw.Draw(image)`. 

    - Nad instancí `draw` už lze volat klasické metody, které nám vykreslí tvary / text jako `.rectangle` nebo `.text`, grafiku poté vykreslíme z bufferu na displej pomocí `disp.image(image)` a `disp.display()`. Před tímto vším doporučuji celý displej vyčistit tím, že přes vše překreslíme černý obdelník `  draw.rectangle((0, 0,disp.width, disp.height), outline=0, fill=0)`. 


-- diagram

zdroje:
- https://navody.arduino-shop.cz/navody-k-produktum/raspberry-pi-i2c-oled-displej-ssd1306.html