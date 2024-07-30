using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace PokeTamagochi.PokeAPI;

internal class ApiResponse
{
    public ApiResponse()
    {
        Results = new List<Results>();
    }
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }
    public List<Results> Results { get; set; }



    public static string ConectarComAPI(string url)
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

    public static Dictionary<string, Results> ConverteDadosRecebidosDaAPI(string json)
    {
        Dictionary<string, Results> dadosTrabalhosApi = new();

        ApiResponse dadosApi;

        dadosApi = JsonConvert.DeserializeObject<ApiResponse>(json);

        foreach (Results pokemon in dadosApi.Results)
        {
            dadosTrabalhosApi.Add($"{pokemon.Name}", pokemon);
        }

        return dadosTrabalhosApi;

    }
}
