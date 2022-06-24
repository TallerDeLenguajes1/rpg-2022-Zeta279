using System.Text.Json.Serialization;
using System.Text.Json;

namespace JuegoRPG
{
    public class Persona
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("maiden_name")]
        public string MaidenName { get; set; }

        [JsonPropertyName("birth_data")]
        public string BirthData { get; set; }

        [JsonPropertyName("phone_h")]
        public string PhoneH { get; set; }

        [JsonPropertyName("phone_w")]
        public string PhoneW { get; set; }

        [JsonPropertyName("email_u")]
        public string EmailU { get; set; }

        [JsonPropertyName("email_d")]
        public string EmailD { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("domain")]
        public string Domain { get; set; }

        [JsonPropertyName("useragent")]
        public string Useragent { get; set; }

        [JsonPropertyName("ipv4")]
        public string Ipv4 { get; set; }

        [JsonPropertyName("macaddress")]
        public string Macaddress { get; set; }

        [JsonPropertyName("plasticcard")]
        public string Plasticcard { get; set; }

        [JsonPropertyName("cardexpir")]
        public string Cardexpir { get; set; }

        [JsonPropertyName("bonus")]
        public int Bonus { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("uuid")]
        public string Uuid { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("weight")]
        public double Weight { get; set; }

        [JsonPropertyName("blood")]
        public string Blood { get; set; }

        [JsonPropertyName("eye")]
        public string Eye { get; set; }

        [JsonPropertyName("hair")]
        public string Hair { get; set; }

        [JsonPropertyName("pict")]
        public string Pict { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("sport")]
        public string Sport { get; set; }

        [JsonPropertyName("ipv4_url")]
        public string Ipv4Url { get; set; }

        [JsonPropertyName("email_url")]
        public string EmailUrl { get; set; }

        [JsonPropertyName("domain_url")]
        public string DomainUrl { get; set; }

        static public async Task<Persona> ObtenerPersona()
        {
            HttpClient cliente = new HttpClient();
            cliente.Timeout = new TimeSpan(0, 0, 5);
            try
            {
                string json = await cliente.GetStringAsync(@"https://api.namefake.com/spanish-spain");
                Persona persona = JsonSerializer.Deserialize<Persona>(json);
                return persona;
            }
            catch
            {
                return null;
            }

        }

    }
}