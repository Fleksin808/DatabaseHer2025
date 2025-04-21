Projekt pro „PROGRAMOVÁNÍ (programy INFO ETE15E, SYI ETE56E, TF ETE28E)“
Štěpán Flekač, xfles003@studenti.czu.cz
Název: Správa databáze her v XML formátu
Popis:
Program umožňuje spravovat databázi her, která je uložena ve formátu XML. Uživatel může přidávat nové hry, upravovat nebo mazat existující záznamy, filtrovat podle zvolených kritérií (například žánr nebo vývojář) a zobrazovat všechny záznamy. Program dále umožňuje ukládat upravená data zpět do souboru. Komunikace s uživatelem probíhá prostřednictvím textového menu v konzoli.
Funkce programu:
•	Načtení her z existujícího XML souboru.
•	Přidání nové hry do databáze.
•	Úprava existující hry na základě názvu.
•	Odstranění hry ze seznamu.
•	Filtrování her dle zvoleného kritéria (např. žánr, rok vydání).
•	Zobrazení všech her.
•	Uložení změn do XML souboru.
Návrh hlavních proměnných a datových struktur:
•	XDocument DatabaseHer – XML dokument sloužící k načítání a ukládání dat
•	string xmlcesta – cesta k XML souboru
•	XElement Zanr – výčtový typ pro žánry her (např. Akční, RPG, Adventura...)
•	XElement Hra – třída reprezentující jednu hru (název, žánr, platforma, rok vydání...)
Koncepční popis programu:
1.	Program po spuštění načte XML soubor s databází her (pokud existuje)
2.	Zobrazí uživateli přehledné textové menu s možnostmi
3.	Na základě výběru uživatele provede akci (např. přidání nové hry, výpis všech her)
4.	Umožní uživateli filtrovat hry podle různých parametrů
5.	Změny může uživatel kdykoli uložit zpět do XML souboru
6.	Program může běžet opakovaně, dokud uživatel nezvolí ukončení
Vstupní omezení a možné problémy:
•	XML soubor musí mít validní strukturu dle očekávaného schématu
•	Při úpravách nebo mazání se hledá hra podle id
•	Při nesprávném zadání (např. neexistující žánr) je uživatel vyzván k opakování
•	Program předpokládá, že vstup bude zadán korektně z klávesnice (např. číselné hodnoty u roku vydání)
•	Maximální počet her je omezen pouze pamětí počítače

