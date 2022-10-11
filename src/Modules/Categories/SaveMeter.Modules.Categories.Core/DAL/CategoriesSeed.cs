using System.Collections.Generic;
using SaveMeter.Modules.Categories.Core.Entities;

namespace SaveMeter.Modules.Categories.Core.DAL
{
    class CategoriesSeed
    {
        public const string Bus = "Autobus/Taxi";
        public const string Hairdresser = "Fryzjer";
        public const string Book = "Książka";
        public const string Fuel = "Paliwo";
        public const string Restaurant = "Restauracja";
        public const string Beauty = "Beauty";
        public const string Shop = "Sklep";
        public const string Bunnies = "Tiszki";
        public const string Clothes = "Ubranie";
        public const string Holiday = "Wakacje";
        public const string House = "Dom";
        public const string Gift = "Prezent";
        public const string Health = "Zdrowie";
        public const string Computer = "Komputer";
        public const string Entertainment = "Rozrywka";
        public const string Car = "Samochód";
        public const string Apartment = "Mieszkanie";
        public const string Phone = "Telefon";
        public const string Evolution = "Rozwój";
        public const string Renovation = "Remont";
        public const string Income = "Przychód";
        public const string Business = "Firma";
        public const string InternalTransfer = "Przelew wewnętrzny";
        public const string Skip = "Brak analizy";
        private static List<Category> _categories =>
            new List<Category>()
            {
                Category.Create("07BF10A2-A1A6-43A2-8B1D-E5674AA70887", Bus),
                Category.Create("FEBA9E91-2AC8-4B0A-B20D-47449D394446", Hairdresser),
                Category.Create("C9D7317C-92FD-4FFD-A39E-30EE8E21228B", Book),
                Category.Create("29FC56E2-5884-43D4-AD42-8F7467A1A7ED", Fuel),
                Category.Create("A93D7E22-EED0-436B-A0B8-587423FF8953", Restaurant),
                Category.Create("0ED355A4-6C4D-4DE9-A43D-639B0C128F29", Beauty),
                Category.Create("CCC1DAB9-0F02-4AF2-9814-D390DFDF9DC1", Shop),
                Category.Create("D67CA6FF-5099-4601-81D1-A385C4A0B3F3", Bunnies),
                Category.Create("5BF1CAA2-507E-4BAB-A727-1E118C57DFA1", Clothes),
                Category.Create("792BADF0-B216-42F1-A312-C105D1B87051", Holiday),
                Category.Create("9D160CFD-BB1B-4F31-8C9D-6FADA63A140A", House),
                Category.Create("B1E1E82E-39E6-4A3A-9E32-ACD25E4F83A7", Gift),
                Category.Create("74C2B443-1D1A-4922-8EEA-5A7D3155A37F", Health),
                Category.Create("2FDE0E73-47A6-4446-BEBF-77DCF00BBD46", Computer),
                Category.Create("7C0D6075-C480-40CE-954B-025053225C26", Entertainment),
                Category.Create("3BB52C4C-FF23-4B90-8255-045F87087E76", Car),
                Category.Create("C28415C7-D83F-46B7-9819-F85EF802A2A9", Apartment),
                Category.Create("B7F32D36-37E4-4E56-9637-08E6A2CC9A0E", Phone),
                Category.Create("48971A94-2F04-4C17-AD42-E3C80541DE48", Evolution),
                Category.Create("E124C393-F0E7-4663-BFBE-C53B906177C2", Renovation),
                Category.Create("5820CC56-2EE2-44F2-A23C-3E8E6AA6E187", Income),
                Category.Create("A1DDC557-9EB0-4F09-8BCB-58F99944602E", Business),
                Category.Create("4F664D11-310E-46A9-AFAB-19C5DEDF529E", InternalTransfer),
                Category.Create("D676EA72-09D3-4892-B0EF-1DFDC7E19177", Skip),
            };

        public static IReadOnlyList<Category> Categories => _categories;
    }
}
