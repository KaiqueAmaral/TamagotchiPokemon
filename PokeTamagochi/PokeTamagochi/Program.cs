using PokeTamagochi;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

PokemonApiResponse pokemonApiResponse;

void InitialScreen()
{
    Console.WriteLine("******************************");
    Console.WriteLine("Bem vindos ao PokeTamagochi!");
    Console.WriteLine("******************************\n");

    Console.WriteLine("Aqui estão todas as opções de pokémons disponíveis para serem selecionados:\n");

    DisplayAllAvailablePokemons();

}



void StartConnectionWithAPI()
{
    try
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokemon");
        var request = new RestRequest("", Method.Get);
        var response = client.Get(request);

        if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
        {
            ParseApiResponse(response.Content);
            InitialScreen();

        }
        else
        {
            Console.WriteLine("Erro de conexão com API. Detalhes: " + response.ErrorMessage);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    

}

void ParseApiResponse(string responseAPI)
{

    pokemonApiResponse = JsonConvert.DeserializeObject<PokemonApiResponse>(responseAPI);

   
   
}

void DisplayAllAvailablePokemons()
{
    foreach (Pokemon pokemon in pokemonApiResponse.Results)
    {
        Console.WriteLine("Nome: " + pokemon.Name);
    }
}


StartConnectionWithAPI();
