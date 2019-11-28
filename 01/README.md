# NAG-IoT
## Návrat

1. Výběr operačního systému pro raspberry pi
	- Jelikož potřebujeme co nejméně náročný, ale zároveň stabilní a oficiálně podporovaný systém, rozhodli jsme se pro Raspbian LITE (nogui)

2. Instalace operačního systému (headless setup)
	- První jsme veřejně dostupný systém stáhli z oficiálních stránek Raspberry
	- Následovně jsme použili utilitu **DD** pro zkopírování obrazu systému na kartu SD
	- Raspberry s kartou jsme zapojili do sítě a počkali 2-3 minuty až se celý systém správně zavede
	- Protože se jedná **headless setup**, potřebujeme si zapnout vzdálený SSH přístup, toho lze docílit tak, že do boot oddílu přidáte soubor s názvem **ssh** bez přípony!!!

3. Aktualizace systému, instalace základních nástrojů | balíků jako **VIM**
	- Aktualizaci systému lze provést příkazem `sudo apt-get update` a následovně `sudo apt-get upgrade`
	- Nástroje jako VIM lze doinstalovat příkazem `sudo apt-get install <nazev_nastroje>`
