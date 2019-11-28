# NAG-IoT
## Návrat 
### Zapojení a svícení LED diody

1. Fyzické zapojení diody
	- Diodu samotnou o sobě lze zapojit jednoduše, anodu (delší nožičku diody) připojíme na libovolný GPIO výstup, katodu (kratší nožičku diody) připojíme na GND (zem)

2. Rozsvícení diody
	- Diodu lze rozsvítit velmi jednoduše, stačí nastavit logickou 1 (HIGH) na příslušní GPIO výstup, to lze provést například (jako v naši situaci) přes GPIO knihovnu (která na pozadí mění hodnoty v souborech v linuxové architektuře)
	- Pro základní funkčnost diody, stačí nastavit typ GPIO rozložení `GPIO.setmode(GPIO.BOARD)`, nastavit pin do výstupního modu `GPIO.setup(40, GPIO.OUT, initial=GPIO.LOW)`, a poté nastavit logickou 1 na výstup `GPIO.output(40, GPIO.HIGH)` 


### Zapojení tlačítka

1. Fyzické zapojení tlačítka
	- Tlačítko lze (alespoň na venek) zapojit obdobně jako diodu, ale hlouběji funguje trochu jinak. K tlačítku je potřeba připojit ještě odpor, ale Raspberry nám poskytuje jednu vychytáku zvanou `PULLUP` ~ **interní odpor**
	- Tuto věc lze nastavit v GPIO knihovně ve funkci setup lze přidat parametr `pull_up_down=GPIO.PUD_DOWN`, celý příkaz co nastavuje tlačítko jako vstup pak tedy vypadá následovně `GPIO.setup(38, GPIO.IN, pull_up_down=GPIO.PUD_DOWN)`
	- Nyní už stačí přidat nějakou základní logiku, lze například využíváme funkce `sleep()` z knihovny `time`, v složitějčích případech by se mělo na detekci tlačítka využít funkce procesoru zvané `interrupt`, ale to na tento základní příklad nepotřebujeme

### Základní interní logika kódu

<code>
while True: // nekonecna smycka
	if GPIO.input(38) == GPIO.HIGH: // pokud je tlacitko sepnuto
		print("I was touched by onii-chan ~ OwO") // vypis zpravu
		GPIO.output(40, GPIO.HIGH) // rozsvit diodu
		sleep(10) // pockej 10s
		GPIO.output(40, GPIO.LOW) // zhasni diodu
	sleep(0.01)
</code>

- Můžete si povšimnout funkce `sleep(0.01)` na konci smyčky, to je zde kvůli tzv. *debounce* tlačítka, to je situace kdy se spoj fyzicky chvěje...
