using System;
using System.Text.RegularExpressions;

namespace VehicleApi.Models
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Plate { get; set; } = null!;
        public string Make { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int Year { get; set; }

        public Vehicle() { }

        public Vehicle(string plate, string make, string model, int year)
        {
            Plate = NormalizePlate(plate);
            ValidatePlate(Plate);
            Make = make;
            Model = model;
            Year = year;
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
