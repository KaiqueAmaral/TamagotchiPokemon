using PokeTamagochi;
using PokeTamagochi.PokeAPI;
using System.Runtime.CompilerServices;

Dictionary<string, Pokemon> availablePokemons = new();


bool exit = false;


void Start()
{
    string apiDataJson = "";

    PokemonSpecies pokemonApiResponse;
    try
    {
        apiDataJson = PokeApiManager.ExecuteGetRequest(PokeApiManager.baseUrl);

        if (!apiDataJson.StartsWith("Erro"))
        {

            pokemonApiResponse = PokeApiManager.ParseAPIResult<PokemonSpecies>(apiDataJson);

            availablePokemons = Pokemon.CreatePokemonDictionary(pokemonApiResponse.Pokemons);

            GetUserInfo();
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


void GetUserInfo()
{
    Console.WriteLine("Antes de começarmos por favor preencha alguns dados.");
    Console.Write("Qual o seu nome? ");
    Menu.UserName = Console.ReadLine()!;
    Console.WriteLine();
    Console.Write("Qual a sua idade? ");
    Menu.UserAge = int.Parse(Console.ReadLine()!);
}

void DisplayHomeScreen()
{
    while (!exit)
    {
        Console.Clear();

        Console.WriteLine(@"
██████╗░░█████╗░██╗░░██╗███████╗████████╗░█████╗░███╗░░░███╗░█████╗░░██████╗░░█████╗░░█████╗░██╗░░██╗██╗
██╔══██╗██╔══██╗██║░██╔╝██╔════╝╚══██╔══╝██╔══██╗████╗░████║██╔══██╗██╔════╝░██╔══██╗██╔══██╗██║░░██║██║
██████╔╝██║░░██║█████═╝░█████╗░░░░░██║░░░███████║██╔████╔██║███████║██║░░██╗░██║░░██║██║░░╚═╝███████║██║
██╔═══╝░██║░░██║██╔═██╗░██╔══╝░░░░░██║░░░██╔══██║██║╚██╔╝██║██╔══██║██║░░╚██╗██║░░██║██║░░██╗██╔══██║██║
██║░░░░░╚█████╔╝██║░╚██╗███████╗░░░██║░░░██║░░██║██║░╚═╝░██║██║░░██║╚██████╔╝╚█████╔╝╚█████╔╝██║░░██║██║
╚═╝░░░░░░╚════╝░╚═╝░░╚═╝╚══════╝░░░╚═╝░░░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚═╝░░╚═╝░╚═════╝░░╚════╝░░╚════╝░╚═╝░░╚═╝╚═╝
");

        Console.WriteLine($"Boas vindas ao PokeTamagochi {Menu.UserName}!\n");

        Console.WriteLine("O que deseja fazer?");

        Console.WriteLine("1 - Adotar um pokémon");
        Console.WriteLine("2 - Ver pokémons adotados");
        Console.WriteLine("3 - Sair\n");

        int userInput = int.Parse(Console.ReadLine()!);

        switch (userInput)
        {
            case 1:
                Menu.DisplayAdoptionMenu(availablePokemons);
                break;

            case 2:
                break;

            case 3:
                break;

            default:
                Console.WriteLine("Opção inválida");
                exit = true;
                break;
        }
    }
  

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

    Console.WriteLine($"-Nome: {pokemonInfo.Name}\n-Altura: {pokemonInfo.Height}\n-Peso: {pokemonInfo.Weight}\n-Experiência base: {pokemonInfo.BaseExperience}\n" +
        $"-Número pokedex: {pokemonInfo.Id}\n-Habilidades: ");

    foreach (PokemonAbilities abilities in pokemonInfo.Abilities)
    {
        Console.WriteLine($"\t-{abilities.Ability.Name}, {abilities.Ability.UrlInfo}");
    }

}

Start();
