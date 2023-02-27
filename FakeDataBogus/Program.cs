using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Bogus;

namespace FakeDataBogus
{
    public class Program
    {
        static void Main(string[] args)
        {
            var addressFaker = new Faker<Address>()
                .RuleFor(i => i.City, i => i.Address.City())
                .RuleFor(i => i.City, i => i.Address.StreetName())
                .RuleFor(i => i.City, i => i.Address.ZipCode());

            var userFaker = new Faker<User>()
                .RuleFor(i => i.Address, addressFaker)
                .RuleFor(i => i.Age, i => i.Random.Int(18, 65))
                .RuleFor(i => i.FirstName, i => i.Person.FirstName)
                .RuleFor(i => i.FirstName, i => i.Person.LastName)
                .RuleFor(i => i.Username, (i, j) => i.Internet.UserName(j.FirstName, j.LastName))
                .RuleFor(i => i.Id, i => i.Random.Guid())
                .RuleFor(i => i.Gender, i => i.PickRandom<Gender>())
                .RuleFor(i => i.EmailAddress, i => i.Person.Email);
            var generatedAddress = addressFaker.Generate(5);
            var generatedObject = userFaker.Generate(5);

            var opt = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };

            string valueAsJson = JsonSerializer.Serialize(generatedObject, opt);

          
         
            Console.WriteLine(valueAsJson);
        }
    }
    public enum Gender
    {
        Male,
        Female
    }
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public Gender Gender { get; set; }
        public Address Address { get; set; }

    }

    public class Address
    {
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string StreetName { get; set; }

    }
}
