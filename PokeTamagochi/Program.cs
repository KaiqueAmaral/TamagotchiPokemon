using RestSharp;
using System.Net;
using Newtonsoft.Json;
using System;
using PokeTamagochi.PokeAPI;

ApiResponse pokemonApiResponse;
Dictionary<string, Results> pokemonsDisponiveis = new();

const string URL_PRINCIPAL_API = "https://pokeapi.co/api/v2/pokemon";


void Iniciar()
{
    string dadosApiEmJson = "";
    try
    {
        dadosApiEmJson = ApiResponse.ConectarComAPI(URL_PRINCIPAL_API);

        if(!dadosApiEmJson.StartsWith("Erro"))
        {
            pokemonsDisponiveis = ApiResponse.ConverteDadosRecebidosDaAPI(dadosApiEmJson);

            ExibirTelaInicial();
        }
        else
        {
            Console.WriteLine(dadosApiEmJson);
        }

        

    }
    catch (Exception ex)
    {
    }
}


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


void ExibeTodosPokemonsDisponiveis()
{
    foreach (string nome in pokemonsDisponiveis.Keys)
    {
        Console.WriteLine("-" + nome);
    }

    Console.WriteLine();

}

void ExibeDetalhesDoPokemon(string nome)
{
    Console.Clear();

    PokemonEspecificacoes pokemonDetalhes;

    Results pokemon = pokemonsDisponiveis[nome];

    string repostaApiEmJson = ApiResponse.ConectarComAPI(pokemon.Url);

   
    if (!repostaApiEmJson.StartsWith("Erro"))
    {
        pokemonDetalhes = Results.ConverteDadosRecebidosDaAPI(repostaApiEmJson);
    }
    else
    {
        Console.WriteLine(repostaApiEmJson);
        return;
    }

    Console.WriteLine($"-Nome: {pokemonDetalhes.Name}\n-Experiência base: {pokemonDetalhes.BaseExperience}\n-Altura: {pokemonDetalhes.Height}\n-Número pokedex: {pokemonDetalhes.Id}\n" +
        $"-É inicial: {pokemonDetalhes.IsDefault}\n-Ulr das áreas de encontro: {pokemonDetalhes.LocantionAreaEncountersUrl}\n-Ordem: {pokemonDetalhes.Order}\n" +
        $"-Peso: {pokemonDetalhes.Weight}");

}

Iniciar();
