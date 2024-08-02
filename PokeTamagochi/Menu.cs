using PokeTamagochi.PokeAPI;

namespace PokeTamagochi;


internal static class Menu
{

    public static Dictionary<string, Pokemon> AdoptedPokemons = new Dictionary<string, Pokemon>();

    private static void DisplayMenuTitle(string title)
    {
        int lettersCount = title.Length;

        string titleSymbols = string.Empty.PadLeft(lettersCount, '*');
        Console.WriteLine(titleSymbols);
        Console.WriteLine(title);
        Console.WriteLine(titleSymbols + "\n");
    }

    public static void DisplayAdoptionList(Dictionary<string, Pokemon> availablePokemons, User userData)
    {
        Console.Clear();

        DisplayMenuTitle("Adotando um pokémon!");

        Console.WriteLine("Escolha uma das opções abaixo:\n");

        foreach (string pokemonName in availablePokemons.Keys)
        {
            Console.WriteLine($"-{pokemonName}");
        }
        Console.WriteLine();

        string chosenPokemon = Console.ReadLine()!;

        Console.WriteLine();

        if (availablePokemons.ContainsKey(chosenPokemon))
        {
            DisplayAdoptionConfirmationOptions(availablePokemons, chosenPokemon, userData);
        }
        else
        {
            Console.WriteLine("\nPokémon não encontrado, retornando para o menu principal.");
            Thread.Sleep(3000);
        }




    }


    private static void DisplayAdoptionConfirmationOptions(Dictionary<string, Pokemon> availablePokemons, string pokemonName, User userData)
    {
        Console.Clear();

        DisplayMenuTitle($"{pokemonName} escolhido!");

        Console.WriteLine($"{userData.Name}, você deseja:\n");

        Console.WriteLine($"1 - Adotar {pokemonName}");
        Console.WriteLine($"2 - Ver mais detalhes sobre {pokemonName}");
        Console.WriteLine($"3 - Voltar\n");

        int userInput = int.Parse(Console.ReadLine()!);

        switch (userInput)
        {
            case 1:
                DisplayAdoptionConfirmation(availablePokemons, pokemonName);
                break;

            case 2:
                DisplayPokemonInfo(availablePokemons, pokemonName, userData);
                break;

            case 3:
                Console.WriteLine("Retornando para o menu principal.");
                break;

            default:
                Console.WriteLine("Opção inválida. Retornando para o menu principal.");
                Thread.Sleep(3000);
                break;
        }
    }

    private static void DisplayPokemonInfo(Dictionary<string, Pokemon> availablePokemons, string pokemonName, User userData)
    {
        Console.Clear();

        DisplayMenuTitle($"Informações {pokemonName}!");

        string json = PokeApiManager.ExecuteGetRequest(availablePokemons[pokemonName].InfoUrl);

        PokemonInfo pokemonDetails = PokeApiManager.ParseAPIResult<PokemonInfo>(json);

        Console.WriteLine($"Nome: {pokemonDetails.Name}");
        Console.WriteLine($"Altura: {pokemonDetails.Height}");
        Console.WriteLine($"Peso: {pokemonDetails.Weight}");
        Console.WriteLine($"Número do pokedex: {pokemonDetails.Id}");
        Console.Write($"Habilidades: ");

        foreach (PokemonAbilities ability in pokemonDetails.Abilities)
        {
            Console.Write($"{ability.Ability.Name} | ");

        }
        Console.WriteLine();
        Console.WriteLine("\nPressione qualquer tecla para voltar para opções");
        Console.ReadKey();

        DisplayAdoptionConfirmationOptions(availablePokemons, pokemonName, userData);

    }

    public static void DisplayAdoptionConfirmation(Dictionary<string, Pokemon> availablePokemons, string pokemonName)
    {
        Console.Clear();

        AdoptedPokemons.Add(pokemonName, availablePokemons[pokemonName]);

        DisplayMenuTitle($"{pokemonName} foi adotado com sucesso! O ovo já está chocando!");

        Console.WriteLine();

        Console.WriteLine(@"´´´´´´´´´´´´´´´´´´´´´´¶¶¶¶¶¶¶¶¶
´´´´´´´´´´´´´´´´´´´´¶¶´´´´´´´´´´¶¶
´´´´´´¶¶¶¶¶´´´´´´´¶¶´´´´´´´´´´´´´´¶¶
´´´´´¶´´´´´¶´´´´¶¶´´´´´¶¶´´´´¶¶´´´´´¶¶
´´´´´¶´´´´´¶´´´¶¶´´´´´´¶¶´´´´¶¶´´´´´´´¶¶
´´´´´¶´´´´¶´´¶¶´´´´´´´´¶¶´´´´¶¶´´´´´´´´¶¶
´´´´´´¶´´´¶´´´¶´´´´´´´´´´´´´´´´´´´´´´´´´¶¶
´´´´¶¶¶¶¶¶¶¶¶¶¶¶´´´´´´´´´´´´´´´´´´´´´´´´¶¶
´´´¶´´´´´´´´´´´´¶´¶¶´´´´´´´´´´´´´¶¶´´´´´¶¶
´´¶¶´´´´´´´´´´´´¶´´¶¶´´´´´´´´´´´´¶¶´´´´´¶¶
´¶¶´´´¶¶¶¶¶¶¶¶¶¶¶´´´´¶¶´´´´´´´´¶¶´´´´´´´¶¶
´¶´´´´´´´´´´´´´´´¶´´´´´¶¶¶¶¶¶¶´´´´´´´´´¶¶
´¶¶´´´´´´´´´´´´´´¶´´´´´´´´´´´´´´´´´´´´¶¶
´´¶´´´¶¶¶¶¶¶¶¶¶¶¶¶´´´´´´´´´´´´´´´´´´´¶¶
´´¶¶´´´´´´´´´´´¶´´¶¶´´´´´´´´´´´´´´´´¶¶
´´´¶¶¶¶¶¶¶¶¶¶¶¶´´´´´¶¶´´´´´´´´´´´´¶¶
´´´´´´´´´´´´´´´´´´´´´´´¶¶¶¶¶¶¶¶¶¶¶");

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal");
        Console.ReadKey();
    }


    public static void DisplayAdoptedPokemons(User userData)
    {
        Console.Clear();

        DisplayMenuTitle("Pokémons adotados!");

        if (AdoptedPokemons.Count <= 0)
        {
            Console.WriteLine($"\n{userData.Name}, você não adotou nenhum pokémon ainda =(");
            Thread.Sleep(3000);
            return;
        }

        Console.WriteLine("Você já adotou os seguintes pokémons:\n");

        foreach (string pokemonName in AdoptedPokemons.Keys)
        {
            Console.WriteLine($"-{pokemonName}");
        }


        Console.WriteLine("\nPressione qualquer botão para voltar ao menu principal");
        Console.ReadKey();
    }
}
