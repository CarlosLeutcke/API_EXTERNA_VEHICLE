using System.Text.RegularExpressions;

namespace VehicleApi.Models
{
    public class Vehicle
    {
        public long Id { get; set; }
        public string Plate { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }
        public string Color { get; set; } = null!;
        public string Renavam { get; set; } = null!;
        public string Chassis { get; set; } = null!;
        public string Municipality { get; set; } = null!;
        public string State { get; set; } = null!;
        public EnumDataVehicleCondition EnumDataVehicleCondition { get; set; }
        public EnumTypeFuelVehicle EnumTypeFuelVehicle { get; set; }
        public EnumTheftVehicleCondition TheftVehicleCondition { get; set; }
        public bool Wrecked { get; private set; }
        public bool JudicialRestriction { get; private set; }
        public bool Auction { get; private set; }
        public string OwnerName { get; private set; }
        public string OwnerCpfCnpj { get; private set; }
        public string OwnerCnh { get; private set; }

        public Vehicle() { }

        public Vehicle(string plate, string brand, string model, int year, string color, string renavam, string chassis, string municipality, string state, EnumDataVehicleCondition enumDataVehicleCondition, EnumTypeFuelVehicle enumTypeFuelVehicle, EnumTheftVehicleCondition theftVehicleCondition, bool wrecked, bool judicialRestriction, bool auction, string ownerName, string ownerCpfCnpj, string ownerCnh)
        {
            Plate = NormalizePlate(plate);
            ValidatePlate(Plate);
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Renavam = renavam;
            Chassis = chassis;
            Municipality = municipality;
            State = state;
            EnumDataVehicleCondition = enumDataVehicleCondition;
            EnumTypeFuelVehicle = enumTypeFuelVehicle;
            TheftVehicleCondition = theftVehicleCondition;
            Wrecked = wrecked;
            JudicialRestriction = judicialRestriction;
            Auction = auction;
            OwnerName = ownerName;
            OwnerCpfCnpj = ownerCpfCnpj;
            OwnerCnh = ownerCnh;
        }

        private static string NormalizePlate(string plate) =>
            (plate ?? string.Empty).Trim().ToUpperInvariant();

        private static void ValidatePlate(string plate)
        {
            if (string.IsNullOrWhiteSpace(plate)) throw new ArgumentException("Placa inválida");

            var oldPattern = new Regex("^[A-Z]{3}[0-9]{4}$");        // ABC1234
            var mercosulPattern = new Regex("^[A-Z]{3}[0-9][A-Z][0-9]{2}$"); // ABC1D23

            if (!oldPattern.IsMatch(plate) && !mercosulPattern.IsMatch(plate))
                throw new ArgumentException("Placa com formato desconhecido");
        }
    }
}
