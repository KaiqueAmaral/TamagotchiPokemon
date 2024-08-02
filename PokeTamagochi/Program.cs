using PokeTamagochi;
using PokeTamagochi.PokeAPI;
using System.Runtime.CompilerServices;

Dictionary<string, Pokemon> availablePokemons = new();


bool exit = false;
User userData;

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
    string name = Console.ReadLine()!;
    Console.WriteLine();
    Console.Write("Qual a sua idade? ");
    int age = int.Parse(Console.ReadLine()!);

    userData = new User(name, age);
}

void DisplayHomeScreen()
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

    Console.WriteLine($"Boas vindas ao PokeTamagochi {userData.Name}!\n");

    Console.WriteLine("O que deseja fazer?");

    Console.WriteLine("1 - Adotar um pokémon");
    Console.WriteLine("2 - Ver pokémons adotados");
    Console.WriteLine("3 - Sair\n");

    int userInput = int.Parse(Console.ReadLine()!);

    switch (userInput)
    {
        case 1:
            Menu.DisplayAdoptionList(availablePokemons, userData);
            break;

        case 2:
            Menu.DisplayAdoptedPokemons(userData);
            break;

        case 3:
            Console.WriteLine($"Obrigado por jogar! Até uma proxima {userData.Name} <3");
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    if (userInput != 3)
    {
        DisplayHomeScreen();
    }
}

Start();
