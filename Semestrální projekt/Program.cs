using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Semestrální_projekt
{
    internal class Program
    {
        static string xmlcesta = "databaseher.xml";
        static XDocument DatabaseHer = new XDocument(new XElement("Hry"));
        static void Main(string[] args)
        {
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
                //vyvolání metody menu
                Menu();
                //zvolení funkce uživatelem a převedení charakteru reprezentujícího funkci na malé písmeno
                char funkce = char.ToLower((Console.ReadKey().KeyChar));
                //spuštění funkce na základě sisknutého znaku
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
                    case 'h':
                        Console.Clear();
                        Console.WriteLine("Zadej nejmenší požadované hodnocení na Metacritic");
                        fceh();
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
                Console.WriteLine("[r] - Pro vyhledání hry podle roku vydání");
                Console.WriteLine("[d] - Pro vyhledání hry podle vývojářského studia");
                Console.WriteLine("[z] - Pro vyhledání hry podle žánru");
                //Console.WriteLine("[h] - ");
                Console.WriteLine("[k] - Pro ukončení programu");
            }

            void fces()
            {
                Console.WriteLine("Szenam her:");
                if (DatabaseHer != null)
                {
                    foreach (var hra in DatabaseHer.Root.Elements("Hra"))
                    {
                        Console.WriteLine($"{hra.Element("NázevHry")?.Value}, " +
                            $"{hra.Element("VyvojarskeStudio")?.Value}, {hra.Element("RokVydani")?.Value}, {hra.Element("Zanr")?.Value}," +
                            $" {hra.Element("PocetAchievementu")?.Value}");
                    }
                }
                Console.ReadLine();

            }

            void fcep()
            {
                Console.Clear();
                int noveid = DatabaseHer.Root.Elements("Hra").Any() ?
                DatabaseHer.Root.Elements("Hra").Max(x => (int?)x.Attribute("id") ?? 0) + 1 : 1;

                Console.WriteLine("Zadej název nové hry:");
                string novynazev = Console.ReadLine() ?? "";

                Console.WriteLine("Zadej název nového studia:");
                string novestudio = Console.ReadLine() ?? "";

                Console.WriteLine("Zadej rok vydání hry v rozsahu 1990-2025:");
                int novyrokint;
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

            void fceh()
            {
                Console.Clear();
                Console.WriteLine("Zadej celkový počet achievementů v dané hře");
                string pocetachistr1 = Console.ReadLine() ?? "";
                if (Int32.TryParse(pocetachistr1, out int pocetachisint1))
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
            }
        }
    }
}
