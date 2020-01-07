# NAG-IoT
## Návrat

1. Zapojení RFID čtečky
    - RFID čtečka se skládá z 8 pinů SDA (Serial Data Signal), SCK (Serial Clock), MOSI (Master Out Slave In), MISO (Master In Slave Out), IRQ (Interrupt Request), GND (Ground Power), RST (Reset-Circuit) a 3.3v (3.3v Power In). Zapojení lze provést podle přiloženého diagramu.

2. Instalace MFRC52 knihovny
    - Jelikož se jedná již o složitější rozhraní, dovolili jsme si k naprogramování čtečky využít jednoduchou opensource knihovnu, jejíž instalace je dostupná přes PIP. Instalace je velice jednoduchá, pokud máme nainstalovaný `python3` balíček, měl by být nainstalovaný i `pip3` jako takový (alespoň v případě linuxu - raspberry). Pokud pip máme můžeme pomocí příkazu `pip3 install mfrc522` nainstalovat potřebnou knihovnu a dále je ještě potřeba balíček `spidev`, který mfrc používá. Ten nainstalujeme obdobně - `pip3 install spidev`.

3. Programování čtečky
    - Jako první musíme do našeho programu nalinkovat `GPIO` knihovnu a samotné `mfrc522`, to lze udělat pomocí příkazu import `RPi.GPIO as GPIO` a `from mfrc522 import SimpleMFRC522`. Tímto zajistíme, že náš program bude používat správné části knihovny.

    - Dále budeme potřebovat instanci naší čtečky, tu lze s touto knihovnou získat velmi jednoduše, stačí vytvořit `reader = SimpleMFRC522()` instanci třídy SimpleMFRC552() a nic více není třeba.

    - Vracet data ze čtečky také není složité, stačí nad instancí zavolat `.read()` metodu a tím vrátíme data ze čtečky.

4. Logika programu
    - Otvírat závoru lze stejným způsobem, jako na stisk tlačítka a tak vypadá i naše dočasné řešení. Časem však bude lepší závoru udělat jako ovladatelný podprogram, nad kterým pouze zavoláme funkcni na otevření z venčí (aby se dala kombinovat různé řešení jednoduše).

-- přidat diagram

zdroje:
- https://pimylifeup.com/raspberry-pi-rfid-rc522/
