# NAG-IoT
## Návrat

1. Zapojení PIR senzoru
    - PIR senzor má pouze 3 základní konektory, +, - a signál. To znamená, že můžeme + připojit do 5V pinu na raspberry, - do GND a signál do libovolného GPIO pinu. Správnost zapojení si můžete ovětřit přiloženým diagramem.

2. Čtení dat z PIR senzoru
    - Data z PIR senzoru se dají přečíst stejným způsobem jako z tlačítka, stačí tedy nastavit GPIO pin na vstup pomocí `GPIO.setup(pin, GPIO.IN)` a poté přečíst data z pomocí `GPIO.input(pin)`.

3. Logika kódu 
    - Jelikož se PIRko chová (z hlediska programu) jako klasické tlačítko a na bzučák lze nastavit akorát logickou hodnotu HIGH, není logika programu příliž složitá. Stačí pouze v nekonečné smyčče kontrolovat hodnotu na senzoru, když se sensor zaktivuje, zapneme bzučák, diodu a počkáme asi 0.5s, aby se eliminovali divné zvuky z bzučáku.

-- diagram

zdroje:
- https://maker.pro/raspberry-pi/tutorial/how-to-interface-a-pir-motion-sensor-with-raspberry-pi-gpio
- https://www.hackster.io/hardikrathod/pir-motion-sensor-with-raspberry-pi-415c04