using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System;
using PokeTamagochi.PokeAPI;

ApiResponse pokemonApiResponse;

const string URL_PRINCIPAL_API = "https://pokeapi.co/api/v2/pokemon";
Dictionary<string, Results> pokemonsDisponiveis = new();

void ExibirTelaInicial()
{
    Console.WriteLine("******************************");
    Console.WriteLine("Bem vindos ao PokeTamagochi!");
    Console.WriteLine("******************************\n");

    Console.WriteLine("Aqui estão todas as opções de pokémons disponíveis para serem selecionados:\n");

    ExibeTodosPokemonsDisponiveis();

    Console.Write("Digite o nome de algum pokemon para exibir detalhes: ");

    string nomePokemonEscolhido = Console.ReadLine()!;

    ExibeDetalhesDoPokemon(nomePokemonEscolhido);

}



void ConexaoComAPI(string url)
{
    try
    {
        var client = new RestClient(url);
        var request = new RestRequest("", Method.Get);
        var response = client.Get(request);

        if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
        {
            ConverteDadosRecebidosDaAPI(response.Content);
            ExibirTelaInicial();

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

void ConverteDadosRecebidosDaAPI(string responseAPI)
{

    pokemonApiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseAPI);

    foreach(Results pokemon in pokemonApiResponse.Results)
    {
        pokemonsDisponiveis.Add($"{pokemon.Name}", pokemon);
    }
      
}

void ExibeTodosPokemonsDisponiveis()
{
    foreach (string nome in pokemonsDisponiveis.Keys)
    {
        Console.WriteLine("-" + nome);
    }
}

void ExibeDetalhesDoPokemon(string nome)
{
    Results pokemon = pokemonsDisponiveis[nome];
    PokemonEspecificacoes pokemonDetalhes = new PokemonEspecificacoes();

    var client = new RestClient(pokemon.Url);
    var request = new RestRequest("", Method.Get);
    var response = client.Get(request);

    if (response.StatusCode == HttpStatusCode.OK && response.Content != null)
    {

        pokemonDetalhes = JsonConvert.DeserializeObject<PokemonEspecificacoes>(response.Content);
    }

    Console.WriteLine(pokemonDetalhes.Name);
    Console.WriteLine(pokemonDetalhes.BaseExperience);
    Console.WriteLine(pokemonDetalhes.Height);
    Console.WriteLine(pokemonDetalhes.Id);
    Console.WriteLine(pokemonDetalhes.IsDefault);
    Console.WriteLine(pokemonDetalhes.LocantionAreaEncountersUrl);
    Console.WriteLine(pokemonDetalhes.Order);
    Console.WriteLine(pokemonDetalhes.Weight);


}

ConexaoComAPI(URL_PRINCIPAL_API);
