# NAG-IoT
## Návrat

1. Zapojení PIR senzoru
    - PIR senzor má pouze 3 základní konektory, +, - a signál. To znamená, že můžeme + připojit do 5V pinu na raspberry, - do GND a signál do libovolného GPIO pinu. Správnost zapojení si můžete ovětřit přiloženým diagramem.

2. Čtení dat z PIR senzoru
    - Data z PIR senzoru se dají přečíst stejným způsobem jako z tlačítka, stačí tedy nastavit GPIO pin na vstup pomocí `GPIO.setup(pin, GPIO.IN)` a poté přečíst data z pomocí `GPIO.input(pin)`.

3. Logika kódu 
    --

-- diagram

zdroje:
- https://maker.pro/raspberry-pi/tutorial/how-to-interface-a-pir-motion-sensor-with-raspberry-pi-gpio