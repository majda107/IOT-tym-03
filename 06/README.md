# NAG-IoT
## Návrat

6. Infra čidlo
	- Infra senzor se skládá z dvou částí - vysílače a přijímače. Vysílací dioda může svítit celou dobu, jejích je v infra-červeném spektru a není viditelný lidským okem, ale přes kameru mobilního telefonu lze toto spektrum vidět jako převážně fialovou barvu. Dioda má naschvál paprsek jenom v menším úhlu, aby nerušila ostatní IR zařížení, je tedy třeba dát si pozor aby byla přímo namířená na přijímací diodu. 

	- Přijímací diodu stačí připojit na GPIO vstup a nastavit pullup rezistor, pokud je hodnota na vstupu `LOW`, znamená to, že je IR spojení přerušeno a můžeme vypsat například stavovou hlášku `print("Onii-chan interrupted ~")`.

7. Integrování do obvodu
	- Integrovat do obvodu IR bránu + tlačítko už není tak jednoduché, jak se musí zdát. Použití `while: True` cyklu kompletně odpadá kvůli tomu že musíme zjištovat stav z obou vstupů, mikropocesor má naštěstí jedno užitečnou funkci, která tento problém skvěle řeší - *Interrupt* . 
	
	- Interrupt je "funkce", která se je schopná zavolat když určitý vstup detekuje změnu. Je zde několik typů změn jako `RISING`, `FALLING` nebo `BOTH`. Nejkratší způsob jak kód vyřešit je naslouchat na interrupt jako both a zde podmínkou kontrolovat, zda je na vstup na pinu HIGH nebo LOW, pokud je nějaký snímač přerušen, uložíme si jeho 

	- Zbytek logiky řešíme tak, že uděláme nekonečnou `while: True` smyčku s malým sleepem, a velmi často kontrolujeme zda jsou oba senzory zavřené + je delta času (momentální čas - poslední čas) větší jak určitý časový rozdíl `if shouldCloseSensor == True and shouldCloseButton and (datetime.now() - lastTimeChecked).seconds > 2:` (v této situaci 2 sekundy), pokud ano, závoru zavřeme. 

-- přidat kód
-- přidat zapojení
