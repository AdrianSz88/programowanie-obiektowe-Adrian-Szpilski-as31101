using AmtAssist.Models.Sprawy;

namespace AmtAssist.Models.Osoby
{
    public abstract class Klient : Osoba
    {
        //właściwości specyficzne dla klienta
        public string PESEL { get; private set; }
        public string AdresPolska { get; set; }
        public string AdresNiemcy { get; set; }
        public DateTime DataRejestracji { get; private set; }

        //lista spraw klienta (kolekcja generyczna)
        private List<Sprawa> _sprawy;

        //konstruktor
        protected Klient(string imię, string nazwisko, string email, string telefon, string pesel, string adresPolska, string adresNiemcy)
            : base(imię, nazwisko, email, telefon)  // Wywołanie konstruktora klasy Osoba
        {
            PESEL = pesel;
            AdresPolska = adresPolska;
            AdresNiemcy = adresNiemcy;
            DataRejestracji = DateTime.Now;
            _sprawy = new List<Sprawa>();
        }

        //metody do zarządzania sprawami
        public void DodajSprawę(Sprawa sprawa)
        {
            _sprawy.Add(sprawa);
        }

        public List<Sprawa> PobierzSprawy()
        {
            return _sprawy;
        }
        
        public List<Sprawa> PobierzAktywneSprawy()
        {
            return _sprawy.Where(s => s.Status != Enums.StatusSprawy.Zakończona
                                   && s.Status != Enums.StatusSprawy.Odrzucona).ToList();
        }

        public int LiczbaSpraw()
        {
            return _sprawy.Count;
        }
    }
}