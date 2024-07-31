using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System;
using PokeTamagochi.PokeAPI;

Dictionary<string, Pokemon> availablePokemons = new();


void Start()
{
    string apiDataJson = "";

    PokemonSpecies pokemonApiResponse;
    try
    {
        apiDataJson = PokeApiManager.ExecuteGetRequest(PokeApiManager.baseUrl);

        if(!apiDataJson.StartsWith("Erro"))
        {

            pokemonApiResponse = PokeApiManager.ParseAPIResult<PokemonSpecies>(apiDataJson);

            availablePokemons = Pokemon.CreatePokemonDictionary(pokemonApiResponse.Pokemons);

            DisplayHomeScreen();
        }
        else
        {
            Console.WriteLine(apiDataJson);
        }

        

    }
    catch (Exception ex)
    {
    }
}


void DisplayHomeScreen()
{
    Console.WriteLine("******************************");
    Console.WriteLine("Bem vindos ao PokeTamagochi!");
    Console.WriteLine("******************************\n");

    Console.WriteLine("Aqui estão todas as opções de pokémons disponíveis para serem selecionados:\n");

    ShowAllAvailablePokemons();

    Console.Write("Digite o nome de algum pokemon para exibir detalhes: ");

    string chosenPokemonName = Console.ReadLine()!;

    ShowPokemonInfo(chosenPokemonName);

}


void ShowAllAvailablePokemons()
{
    foreach (string name in availablePokemons.Keys)
    {
        Console.WriteLine("-" + name);
    }

    Console.WriteLine();

}

void ShowPokemonInfo(string name)
{
    Console.Clear();

    PokemonInfo pokemonInfo;

    Pokemon pokemon = availablePokemons[name];

    string apiDataJson = PokeApiManager.ExecuteGetRequest(pokemon.InfoUrl);

   
    if (!apiDataJson.StartsWith("Erro"))
    {
        pokemonInfo = PokeApiManager.ParseAPIResult<PokemonInfo>(apiDataJson);

        //pokemonDetalhes = Pokemon.ConverteDadosRecebidosDaAPI(repostaApiEmJson);
    }
    else
    {
        Console.WriteLine(apiDataJson);
        return;
    }

    Console.WriteLine($"-Nome: {pokemonInfo.Name}\n-Experiência base: {pokemonInfo.BaseExperience}\n-Altura: {pokemonInfo.Height}\n-Número pokedex: {pokemonInfo.Id}\n" +
        $"-É inicial: {pokemonInfo.IsDefault}\n-Ulr das áreas de encontro: {pokemonInfo.LocantionAreaEncountersUrl}\n-Ordem: {pokemonInfo.Order}\n" +
        $"-Peso: {pokemonInfo.Weight}");

}

Start();
