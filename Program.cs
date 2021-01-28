using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConsoleTables;

namespace ApiClient
{
    class Program
    {

        class Brewery
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("name")]
            public string Name { get; set; }
            [JsonPropertyName("brewery_type")]
            public string BreweryType { get; set; }
            [JsonPropertyName("street")]
            public string Street { get; set; }
            [JsonPropertyName("address_2")]
            public string Address_2 { get; set; }
            [JsonPropertyName("address_3")] public string Address_3 { get; set; }
            [JsonPropertyName("city")] public string City { get; set; }
            [JsonPropertyName("state")] public string State { get; set; }
            [JsonPropertyName("county_province")] public string CountyProvince { get; set; }
            [JsonPropertyName("postal_code")] public string PostalCode { get; set; }
            [JsonPropertyName("country")] public string Country { get; set; }
            [JsonPropertyName("longitude")] public string Longitude { get; set; }
            [JsonPropertyName("latitude")] public string Latitude { get; set; }
            [JsonPropertyName("phone")] public string Phone { get; set; }
            [JsonPropertyName("website_url")] public string WebsiteUrl { get; set; }
            [JsonPropertyName("updated_at")] public DateTime UpdatedAt { get; set; }
            [JsonPropertyName("created_at")] public DateTime CreatedAt { get; set; }
        }

        static async Task GetBreweryByName(string nameToLookup)
        {
            try
            {
                var client = new HttpClient();

                var responseFromApi = await client.GetStreamAsync($"https://api.openbrewerydb.org/breweries?by_name={nameToLookup}");

                var breweries = await JsonSerializer.DeserializeAsync<List<Brewery>>(responseFromApi);

                var table = new ConsoleTable("Name", "City", "State", "Brewery Type");

                foreach (var brewery in breweries)
                {
                    // Add one row to our table
                    table.AddRow(brewery.Name, brewery.City, brewery.State, brewery.BreweryType);
                }
                table.Write();


            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Could not find that Brewery");
            }

        }
        static async Task GetBreweryByCity(string cityToLookup)
        {
            try
            {
                var client = new HttpClient();

                var responseFromApi = await client.GetStreamAsync($"https://api.openbrewerydb.org/breweries?by_city={cityToLookup}");

                var breweries = await JsonSerializer.DeserializeAsync<List<Brewery>>(responseFromApi);

                var table = new ConsoleTable("Name", "City", "State", "Brewery Type");

                foreach (var brewery in breweries)
                {
                    // Add one row to our table
                    table.AddRow(brewery.Name, brewery.City, brewery.State, brewery.BreweryType);
                }
                table.Write();


            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Could not find a Brewery in that city");
            }

        }
        static async Task GetBreweryByPostal(string zipToLookup)
        {
            try
            {
                var client = new HttpClient();

                var responseFromApi = await client.GetStreamAsync($"https://api.openbrewerydb.org/breweries?by_postal={zipToLookup}");

                var breweries = await JsonSerializer.DeserializeAsync<List<Brewery>>(responseFromApi);

                var table = new ConsoleTable("Name", "City", "State", "Brewery Type");

                foreach (var brewery in breweries)
                {
                    // Add one row to our table
                    table.AddRow(brewery.Name, brewery.City, brewery.State, brewery.BreweryType);
                }
                table.Write();

            }
            catch (HttpRequestException)
            {
                Console.WriteLine("Could not find a Brewery in that zip");
            }

        }
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var responseFromApi = await client.GetStreamAsync("https://api.openbrewerydb.org/breweries");

            var breweries = await JsonSerializer.DeserializeAsync<List<Brewery>>(responseFromApi);

            Console.WriteLine("NAME - Search by Name");
            Console.WriteLine("CITY - Search by City");
            Console.WriteLine("ZIP - Search by Zip Code");
            Console.WriteLine("ALL - View ALL Breweries");
            Console.WriteLine("QUIT - Leave the program");
            Console.WriteLine();

            Console.Write("Make a selection from our menu options: ");
            var selection = Console.ReadLine().ToUpper().Trim();
            Console.WriteLine();

            var userHasChosenToQuit = false;
            while (userHasChosenToQuit == false)
            {
                switch (selection)
                {
                    case "NAME":
                        Console.Write("What name would you like to search by? ");
                        var name = Console.ReadLine();
                        await GetBreweryByName(name);

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;
                    case "ALL":
                        var table = new ConsoleTable("Name", "City", "State", "BreweryType");

                        foreach (var brewery in breweries)
                        {
                            // Add one row to our table
                            table.AddRow(brewery.Name, brewery.City, brewery.State, brewery.BreweryType);
                        }
                        table.Write();
                        break;
                    case "CITY":
                        Console.Write("What city would you like to search by? ");
                        var city = Console.ReadLine();
                        await GetBreweryByCity(city);

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;
                    case "ZIP":
                        Console.Write("What city would you like to search by? ");
                        var zip = Console.ReadLine();
                        await GetBreweryByCity(zip);

                        Console.WriteLine("Press ENTER to continue");
                        Console.ReadLine();

                        break;
                    case "QUIT":
                        userHasChosenToQuit = true;
                        Console.WriteLine("Thanks for stopping by!");
                        break;
                }
            }
        }
    }
}
