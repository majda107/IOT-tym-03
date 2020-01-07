# NAG-IoT
## Návrat

1. Nastavení API klíče
	- Podle přiloženého manuálu jsme zvolili jméno našeho týmu na soutěžní NAG IOT stránce. Prošli jsme jsi RESTAPI endpointy ve Swaggeru a rozhodli se napsat si vlastní python knihovnu přes `requests`.

2. Vlastní API klient v Pythonu
	- Na vytvoření vlastního REST.API klienta v pythonu jsme využili knihoven `requests` a `json`, pomocí nich jsme si napsali jednoduché statické metody jako například `get_luminosity` a `set_luminosity(value)`, které využívají funkce jako `requests.get(endpoint, headers)` a odesilájí na náš nag endpoint.

3. Nahookování na logiku programu
	- Nahookování našeho vlastního klienta na logiku programu je velice snadné, stačí volat metodu na nastavení osvětlení v proměnné na soutěžním serveru pomocí naší metody `set_luminosity(value)`
