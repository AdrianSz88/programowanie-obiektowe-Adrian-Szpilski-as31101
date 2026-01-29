using AmtAssist.Models.Enums;
using AmtAssist.Models.Osoby;

namespace AmtAssist.Models.Sprawy
{
    public class WniosekKindergeld : Sprawa
    {
        //właściwości specyficzne dla Kindergeld
        public int LiczbaDzieci { get; set; }
        public List<string> ImionaDzieci { get; set; }
        public bool CzyPierwszyWniosek { get; set; }

        public WniosekKindergeld(
            Klient klient,
            string numerSprawy,
            DateTime dataUtworzenia,
            StatusSprawy status,
            int liczbaDzieci,
            List<string> imionaDzieci,
            bool czyPierwszyWniosek,
            DateTime? termin)
            : base(klient, numerSprawy, dataUtworzenia, status, termin)
        {
            LiczbaDzieci = liczbaDzieci;
            ImionaDzieci = imionaDzieci;
            CzyPierwszyWniosek = czyPierwszyWniosek;
        }

        //konstruktor
        public WniosekKindergeld(Klient klient, int liczbaDzieci, List<string> imionaDzieci, bool czyPierwszyWniosek, DateTime? termin = null)
            : base(klient, termin)
        {
            LiczbaDzieci = liczbaDzieci;
            ImionaDzieci = imionaDzieci;
            CzyPierwszyWniosek = czyPierwszyWniosek;
        }

        //implementacja metod abstrakcyjnych z klasy Sprawa
        public override string PobierzNazwęSprawy()
        {
            return "Wniosek Kindergeld (Zasiłek na dziecko)";
        }

        public override decimal ObliczOpłatę()  //POLIMORFIZM - Każda sprawa ma swoją metodę obliczania opłaty.
        {
            //opłata bazowa
            decimal opłataBazowa = 200m; 

            //dodatkowe 50 zł za każde kolejne dziecko
            decimal dopłataZaDzieci = (LiczbaDzieci - 1) * 50m;

            //jeśli to pierwszy wniosek, dodatkowe 100 zł (więcej pracy)
            decimal dopłataPierwszy = CzyPierwszyWniosek ? 100m : 0m;

            return opłataBazowa + dopłataZaDzieci + dopłataPierwszy;
        }

        public override List<string> PobierzWymaganeDokumenty()
        {
            var dokumenty = new List<string>
            {
                "Paszport lub dowód osobisty",
                "Zaświadczenie o zameldowaniu (Meldebescheinigung)",
                "Umowa o pracę (Arbeitsvertrag)",
                "Akty urodzenia dzieci",
                "Numer identyfikacji podatkowej (Steuer-ID)"
            };

            if (CzyPierwszyWniosek)
            {
                dokumenty.Add("Zaświadczenie z polskiego urzędu skarbowego");
            }

            return dokumenty;
        }

        public override bool SprawdźKompletność()
        {
            //prosta walidacja - czy są wszystkie podstawowe dane
            if (LiczbaDzieci <= 0) return false;
            if (ImionaDzieci == null || ImionaDzieci.Count != LiczbaDzieci) return false;
            if (Klient == null) return false;

            return true;
        }

        //dodatkowa metoda specyficzna dla Kindergeld
        public decimal ObliczPrzyszłyZasiłek()
        {
            //kwota Kindergeld w 2024: 250 EUR na dziecko miesięcznie
            decimal kindergeldMiesięcznie = 259m * LiczbaDzieci;
            return kindergeldMiesięcznie * 12;
        }

        //override metody wyświetlania informacji
        public override void WyświetlInformacje()
        {
            base.WyświetlInformacje();

            Console.WriteLine($"Liczba dzieci: {LiczbaDzieci}");
            Console.WriteLine($"Imiona dzieci: {string.Join(", ", ImionaDzieci)}");
            Console.WriteLine($"Pierwszy wniosek: {(CzyPierwszyWniosek ? "TAK" : "NIE")}");
            Console.WriteLine($"Przewidywany zasiłek rocznie: {ObliczPrzyszłyZasiłek()} EUR");

            Console.WriteLine("\nWymagane dokumenty:");
            int nr = 1;
            foreach (var dok in PobierzWymaganeDokumenty())
            {
                Console.WriteLine($"  {nr}. {dok}");
                nr++;
            }
        }
    }
}
