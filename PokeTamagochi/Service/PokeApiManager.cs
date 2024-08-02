using Newtonsoft.Json;
using PokeTamagochi.PokeAPI;
using RestSharp;
using System.Net;

namespace PokeTamagochi.Service;
internal class PokeApiManager
{

    public PokeApiManager()
    {
        Pokemons = new List<Pokemon>();
    }
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }

    [JsonProperty("results")]
    public List<Pokemon> Pokemons { get; set; }


    public static string baseUrl = "https://pokeapi.co/api/v2/pokemon";


    public static string ExecuteGetRequest(string url)
    {
        string dados = "";
        try
        {
            var client = new RestClient(url);
            var request = new RestRequest("", Method.Get);
            var response = client.Get(request);

            if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
            {
                dados = response.Content;

            }
            else
            {
                dados = "Erro de conexão com API. Detalhes: " + response.ErrorMessage;

            }


        }
        catch (Exception ex)
        {
            dados = $"Error: {ex.Message}";

        }

        return dados;
    }


    public static T ParseAPIResult<T>(string json)
    {
        return JsonConvert.DeserializeObject<T>(json);
    }
}
