using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Semestrální_projekt
{
    internal class Program
    {
        //Stanovení statických proměných pro jednodušší práci s XML souborem
        static string xmlcesta = "databaseher.xml";
        static XDocument DatabaseHer = new XDocument(new XElement("Hry"));
        static void Main(string[] args)
        {
            //Ověření existence XML souboru
            if (!File.Exists(xmlcesta))
            {
                DatabaseHer.Save(xmlcesta);
            }
            else
            {
                DatabaseHer = XDocument.Load(xmlcesta);
            }

            while (true)
            {
                //Vyvolání metody menu
                Menu();
                //zvolení funkce uživatelem a převedení charakteru reprezentujícího funkci na malé písmeno
                char funkce = char.ToLower((Console.ReadKey().KeyChar));
                //Spuštění funkce na základě sisknutého znaku díky switch konstrukci
                switch (funkce)
                {
                    case 's':
                        Console.Clear();
                        fces();
                        break;
                    case 'p':
                        Console.Clear();
                        fcep();
                        Console.ReadKey();
                        break;
                    case 'o':
                        Console.Clear();
                        Console.WriteLine("Zadej id hry, kterou chceš odstranit ze seznamu vlastněných her");
                        fceo();
                        Console.ReadKey();
                        break;
                    case 'r':
                        Console.Clear();
                        Console.WriteLine("Zadej rok vydání");
                        fcer();
                        Console.ReadKey();
                        break;
                    case 'd':
                        Console.Clear();
                        Console.WriteLine("Zadej název vývojářského studia");
                        fced();
                        Console.ReadKey();
                        break;
                    case 'z':
                        Console.Clear();
                        Console.WriteLine("Zadej název požadovaného žánru");
                        fcez();
                        Console.ReadKey();
                        break;
                    case 'e':
                        Console.Clear();
                        Console.WriteLine("Zadej nejmenší požadované hodnocení na Metacritic");
                        fcee();
                        Console.ReadKey();
                        break;
                    case 'k':
                        Console.Clear();
                        Console.WriteLine("Program se ukončil");
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Stisknul jsi neplatnou klávesu!");
                        Console.WriteLine("Pro návrat do menu stiskněte libovolnou klávesu...");
                        Console.ReadKey();
                        break;
                }
            }
            //vytvoření metody menu
            void Menu()
            {
                Console.Clear();
                Console.WriteLine("Zvolte jednu z možností");
                Console.WriteLine("[s] - Pro vypsání všech zaznamenaných her");
                Console.WriteLine("[p] - Pro přidání hry do seznamu vlastněných her");
                Console.WriteLine("[o] - Pro odstranění hry ze seznamu vlastněných her");
                Console.WriteLine("[r] - Pro vyhledání hry podle roku vydání");
                Console.WriteLine("[d] - Pro vyhledání hry podle vývojářského studia");
                Console.WriteLine("[z] - Pro vyhledání hry podle žánru");
                Console.WriteLine("[e] - Pro editaci dat u vyžadované hry");
                Console.WriteLine("[k] - Pro ukončení programu");
            }
            //vytvoření metody fces pro vypsání seznamu her
            void fces()
            {
                Console.WriteLine("Szenam her:");
                if (DatabaseHer != null)
                {
                    foreach (var hra in DatabaseHer.Root.Elements("Hra"))
                    {
                        Console.WriteLine($"{hra.Attribute("id")?.Value}, {hra.Element("NázevHry")?.Value}, " +
                            $"{hra.Element("VyvojarskeStudio")?.Value}, {hra.Element("RokVydani")?.Value}, {hra.Element("Zanr")?.Value}," +
                            $" {hra.Element("PocetAchievementu")?.Value}");
                    }
                }
                Console.ReadLine();

            }
            //vytvoření metody fcep pro přidání hry do seznamu
            void fcep()
            {
                Console.Clear();
                //Vytvoření proměné noveid, která určuje id nové hry podle maximálního id v XML souboru, (využití lambda výrazu) pokud hra neexistuje, id bude 1
                int noveid = DatabaseHer.Root.Elements("Hra").Any() ?
                DatabaseHer.Root.Elements("Hra").Max(x => (int?)x.Attribute("id") ?? 0) + 1 : 1;

                Console.WriteLine("Zadej název nové hry:");
                //Získání dat od uživatele, pokud uživatel zadá null, bude použita prázdná hodnota
                string novynazev = Console.ReadLine() ?? "";

                Console.WriteLine("Zadej název nového studia:");
                string novestudio = Console.ReadLine() ?? "";

                Console.WriteLine("Zadej rok vydání hry v rozsahu 1990-2025:");
                int novyrokint;
                //Ověření validního roku pomocí while cyklu
                while (true)
                {
                    string novyrokstr = Console.ReadLine() ?? "";
                    if (Int32.TryParse(novyrokstr, out novyrokint))
                    {
                        if (novyrokint >= 1990 && novyrokint <= 2025)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Zadaný rok není validní, zadej rok mezi 1990-2025");
                        }
                    }
                }

                Console.WriteLine("Zadej žánr nové hry:");
                string novyzanr = Console.ReadLine() ?? "";

                Console.WriteLine("Zadej celkový počet achievementů v dané hře");

                string pocetachistr1 = Console.ReadLine() ?? "";
                if (Int32.TryParse(pocetachistr1, out int pocetachisint1) && pocetachisint1 > 0)
                {
                    Console.WriteLine("Zadal jste platný vstup");
                }
                else
                {
                    Console.WriteLine("Zadal jste neplatný vstup");
                }
                Console.WriteLine("Zadej počet splněných achievementů");
                string pocetachistr2 = Console.ReadLine() ?? "";
                if (Int32.TryParse(pocetachistr2, out int pocetachisint2) && pocetachisint1 >= pocetachisint2)
                {
                    Console.WriteLine("Zadal jsi platný vstup");
                    Console.WriteLine("Aktuální stav tvých achevementů je {0}/{1}", pocetachisint2, pocetachisint1);
                }
                else
                {
                    Console.WriteLine("Zadal jsi neplatný vstup");
                }

                //Vložení dat a uložení nového strukturovaného elementu "Hra" do XML souboru
                XElement novahra = new XElement("Hra",
                            new XAttribute("id", noveid),
                            new XElement("NázevHry", novynazev),
                            new XElement("VyvojarskeStudio", novestudio),
                            new XElement("RokVydani", novyrokint),
                            new XElement("Zanr", novyzanr),
                            new XElement("PocetAchievementu", $"{pocetachisint2}/{pocetachisint1}")
                            );

                DatabaseHer.Root.Add(novahra);
                DatabaseHer.Save(xmlcesta);
                Console.WriteLine($"Nová hra {novynazev} byla přídána");
            }
            //Metoda fceo pro odstranění hry ze seznamu podle id
            void fceo()
            {
                Console.Clear();
                Console.WriteLine("Zadej id hry, kterou chceš odstranit ze seznamu vlastněných her");
                string idstr = Console.ReadLine() ?? "";
                if (Int32.TryParse(idstr, out int idint))
                {
                    if (DatabaseHer?.Root != null)
                    {
                        //Vytvoření proměné hraNaOdstraneni, která vyhledá hru podle id (využití lambda výrazu)
                        var hraNaOdstraneni = DatabaseHer.Root.Elements("Hra").FirstOrDefault(h => (int?)h.Attribute("id") == idint);

                        {
                            if (hraNaOdstraneni != null)
                            {
                                hraNaOdstraneni.Remove();
                                DatabaseHer.Save(xmlcesta);
                            }
                            else
                            {
                                Console.WriteLine("Neexistuje hra se shodným id");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Zadal jsi neplatné id!");
                }
                Console.WriteLine("Hra byla úspěšně odstraněna");
                Console.WriteLine("Pro návrat do menu stiskněte libovolnou klávesu...");
            }
            //Metoda fcer pro vyhledání hry podle roku vydání
            void fcer()
            {
                if (DatabaseHer != null)
                {
                    while (true)
                    {
                        string rokstr = Console.ReadLine() ?? "";
                        if (Int32.TryParse(rokstr, out int rokint))
                        {
                            if (!string.IsNullOrWhiteSpace(rokstr) && rokint >= 1990 && rokint <= 2025)
                            {
                                Console.WriteLine("Zadal jsi validní rok");
                                foreach (var hra in DatabaseHer.Root.Elements("Hra"))
                                {
                                    if ((hra.Element("RokVydani")?.Value) == rokint.ToString())
                                    {
                                        Console.WriteLine($"{hra.Element("NázevHry")?.Value}, {hra.Element("VyvojarskeStudio")?.Value}, " +
                                            $"{hra.Element("RokVydani")?.Value}, {hra.Element("Zanr")?.Value}, {hra.Element("PocetAchievementu")?.Value}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Neexistuje hra se shodným rokem vydání");
                                    }
                                    break;
                                }
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Zadaný rok není validní");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Zadán neplatný vstup!");
                        }
                    }
                }
            }
            //Metoda fced pro vyhledání hry podle vývojářského studia
            void fced()
            {
                if (DatabaseHer != null)
                {
                    while (true)
                    {
                        string vyvojarstr = Console.ReadLine() ?? "";

                        if (!string.IsNullOrWhiteSpace(vyvojarstr))
                        {
                            Console.WriteLine("Zadal jsi validní název");
                            foreach (var hra in DatabaseHer.Root.Elements("Hra"))
                            {
                                if ((hra.Element("VyvojarskeStudio")?.Value) == vyvojarstr.ToString())
                                {
                                    //Využití symbolu $ pro efektivní získání hodnoty z proměnných
                                    Console.WriteLine($"{hra.Element("NázevHry")?.Value}, {hra.Element("VyvojarskeStudio")?.Value}, " +
                                        $"{hra.Element("RokVydani")?.Value}, {hra.Element("Zanr")?.Value}, {hra.Element("PocetAchievementu")?.Value}");
                                }
                                else
                                {
                                    Console.WriteLine("Neexistuje hra se shodným jménem vývojáře");
                                }
                                break;
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Zadán neplatný vstup!");
                        }
                    }
                }
            }
            //Metoda fcez pro vyhledání hry podle žánru
            void fcez()
            {
                if (DatabaseHer != null)
                {
                    while (true)
                    {
                        string zanrstr = Console.ReadLine() ?? "";

                        if (!string.IsNullOrWhiteSpace(zanrstr))
                        {
                            Console.WriteLine("Zadal jsi validní název");
                            foreach (var hra in DatabaseHer.Root.Elements("Hra"))
                            {
                                if ((hra.Element("Zanr")?.Value) == zanrstr.ToString())
                                {
                                    Console.WriteLine($"{hra.Element("NázevHry")?.Value}, {hra.Element("VyvojarskeStudio")?.Value}, " +
                                        $"{hra.Element("RokVydani")?.Value}, {hra.Element("Zanr")?.Value}, {hra.Element("PocetAchievementu")?.Value}");
                                }
                                else
                                {
                                    Console.WriteLine("Neexistuje hra se shodným žánrem");
                                }
                                break;
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Zadán neplatný vstup!");
                        }
                    }
                }
            }
            //Metoda fcee pro editaci dat u vyžadované hry
            void fcee()
            {
                Console.Clear();
                Console.WriteLine("Zadej id hry kterou chceš upravit");
                while (true)
                {
                    string idhrystr = Console.ReadLine() ?? "";
                    Console.Clear();

                    //Ověření validního id pomocí Int32
                    if (Int32.TryParse(idhrystr, out int idhryint))
                    {
                        if (DatabaseHer != null)
                        {
                            //Vytvoření proměné hraNaEditaci, která vyhledá hru podle id
                            var hraNaEditaci = DatabaseHer.Root.Elements("Hra").FirstOrDefault(h => (int?)h.Attribute("id") == idhryint);
                            if (hraNaEditaci != null)
                            {
                                Console.WriteLine($"{hraNaEditaci.Attribute("id")?.Value}, {hraNaEditaci.Element("NázevHry")?.Value}, " +
                            $"{hraNaEditaci.Element("VyvojarskeStudio")?.Value}, {hraNaEditaci.Element("RokVydani")?.Value}, {hraNaEditaci.Element("Zanr")?.Value}," +
                            $" {hraNaEditaci.Element("PocetAchievementu")?.Value}");

                                Console.WriteLine("Pro zachování původní hodnoty zanech prázdné pole a stiskni [ENTER]");
                                Console.WriteLine("Zadej nový název hry:");
                                string novynazev = Console.ReadLine() ?? "";
                                if (!string.IsNullOrWhiteSpace(novynazev))
                                {
                                    hraNaEditaci.SetElementValue("NázevHry", novynazev);
                                }
                                else
                                {
                                    Console.WriteLine("Zanechal jsi prázdné pole, původní hodnota byla zachována");
                                }

                                Console.WriteLine("Zadej nové vývojářské studio:");
                                string novestudio = Console.ReadLine() ?? "";
                                if (!string.IsNullOrWhiteSpace(novestudio))
                                {
                                    hraNaEditaci.SetElementValue("VyvojarskeStudio", novestudio);
                                }
                                else
                                {
                                    Console.WriteLine("Zanechal jsi prázdné pole, původní hodnota byla zachována");
                                }

                                Console.WriteLine("Zadej nový rok vydání hry v rozsahu 1990-2025:");
                                int novyrokintedit;
                                string novyrokstr = Console.ReadLine() ?? "";
                                if (string.IsNullOrWhiteSpace(novyrokstr))
                                {
                                    Console.WriteLine("Zanechal jsi prázdné pole, původní hodnota byla zachována");
                                }
                                else
                                {
                                    while (true)
                                    {

                                        if (Int32.TryParse(novyrokstr, out novyrokintedit))
                                        {
                                            if (novyrokintedit >= 1990 && novyrokintedit <= 2025)
                                            {
                                                hraNaEditaci.SetElementValue("RokVydani", novyrokintedit);
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Zadaný rok není validní, zadej rok mezi 1990-2025");
                                            }
                                        }
                                    }
                                }

                                Console.WriteLine("Zadej nový žánr nové hry:");
                                string novyzanr = Console.ReadLine() ?? "";
                                if (!string.IsNullOrWhiteSpace(novyzanr))
                                {
                                    hraNaEditaci.SetElementValue("Zanr", novyzanr);
                                }
                                else
                                {
                                    Console.WriteLine("Zanechal jsi prázdné pole, původní hodnota byla zachována");
                                }

                                Console.WriteLine("Zadej nový celkový počet achievementů v dané hře");
                                string pocetachistr1 = Console.ReadLine() ?? "";
                                int pocetachisint1 = 0;
                                if (!string.IsNullOrWhiteSpace(pocetachistr1))
                                {
                                    if (Int32.TryParse(pocetachistr1, out pocetachisint1) && pocetachisint1 > 0)
                                    {
                                        Console.WriteLine("Zadal jste platný vstup");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Zadal jste neplatný vstup");
                                    }                                   
                                }
                                else
                                {
                                    Console.WriteLine("Původní hodnota byla zachována");
                                }

                                Console.WriteLine("Zadej nový počet splněných achievementů");
                                string pocetachistr2 = Console.ReadLine() ?? "";
                                if (!string.IsNullOrWhiteSpace(pocetachistr2))
                                {
                                    if (Int32.TryParse(pocetachistr2, out int pocetachisint2) && pocetachisint1 >= pocetachisint2)
                                    {
                                        hraNaEditaci.SetElementValue("PocetAchievementu", $"{pocetachisint2}/{pocetachisint1}");
                                        Console.WriteLine("Zadal jste platný vstup");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Zadal jste neplatný vstup");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Původní hodnota byla zachována");
                                }
                                //Uložení změn do XML souboru
                                DatabaseHer.Save(xmlcesta);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Neexistuje hra se shodným id");
                            }
                        }
                    }
                }
            }
        }
    }
}
