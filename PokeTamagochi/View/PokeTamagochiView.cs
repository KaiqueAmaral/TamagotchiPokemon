using PokeTamagochi.Model;
using PokeTamagochi.PokeAPI;
using PokeTamagochi.Service;

namespace PokeTamagochi.View;


internal static class PokeTamagochiView
{

    private static void DisplayMenuTitle(string title)
    {
        int lettersCount = title.Length;

        string titleSymbols = string.Empty.PadLeft(lettersCount, '*');
        Console.WriteLine(titleSymbols);
        Console.WriteLine(title);
        Console.WriteLine(titleSymbols + "\n");
    }




    public static void DisplayHomeScreen(User user)
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

        Console.WriteLine($"Boas vindas ao PokeTamagochi {user.Name}!\n");

        Console.WriteLine("O que deseja fazer?");

        Console.WriteLine("1 - Adotar um pokémon");
        Console.WriteLine("2 - Ver pokémons adotados");
        Console.WriteLine("3 - Sair\n");
    }



    public static User GetUserInfoMenu()
    {
        User user;
        string name;
        int age;
        DisplayMenuTitle("Boas vindas!");

        Console.WriteLine("Antes de começarmos por favor preencha alguns dados.");
        Console.Write("Qual o seu nome? ");
        name = Console.ReadLine()!;
        Console.WriteLine();
        Console.Write("Qual a sua idade? ");
        age = int.Parse(Console.ReadLine()!);

        user = new User(name, age);

        return user;
    }



    public static string DisplayAdoptionList(Dictionary<string, Pokemon> availablePokemons, User userData)
    {
        string chosenPokemon;
        string adoptedPokemonName = "";

        Console.Clear();

        DisplayMenuTitle("Adotando um pokémon!");

        Console.WriteLine("Escolha uma das opções abaixo:\n");

        foreach (string pokemonName in availablePokemons.Keys)
        {
            Console.WriteLine($"-{pokemonName}");
        }
        Console.WriteLine();

        chosenPokemon = Console.ReadLine()!;

        Console.WriteLine();

        if (availablePokemons.ContainsKey(chosenPokemon))
        {
            adoptedPokemonName = DisplayAdoptionConfirmationOptions(availablePokemons, chosenPokemon, userData);
        }
        else
        {
            Console.WriteLine("\nPokémon não encontrado, retornando para o menu principal.");
            Thread.Sleep(4000);
        }


        return adoptedPokemonName;

    }


    private static string DisplayAdoptionConfirmationOptions(Dictionary<string, Pokemon> availablePokemons, string pokemonName, User userData)
    {
        bool stillAdopting = true;
        string adoptedPokemonName = "";
        


        while (stillAdopting)
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
                    adoptedPokemonName = pokemonName;
                    DisplayAdoptionConfirmation(availablePokemons, pokemonName);
                    stillAdopting = false;
                    break;

                case 2:
                    DisplayPokemonInfo(availablePokemons, pokemonName, userData);
                    break;

                case 3:
                    Console.WriteLine("Retornando para o menu principal.");
                    stillAdopting = false;
                    break;

                default:
                    Console.WriteLine("Opção inválida. Retornando para o menu principal.");
                    stillAdopting = false;
                    Thread.Sleep(3000);
                    break;
            }
        }
       


        return adoptedPokemonName;
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

    }

    public static void DisplayAdoptionConfirmation(Dictionary<string, Pokemon> availablePokemons, string pokemonName)
    {
        Console.Clear();


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


    public static void DisplayAdoptedPokemons(Dictionary<string, Pokemon> adoptedPokemons, User userData)
    {
        Console.Clear();

        DisplayMenuTitle("Pokémons adotados!");

        if (adoptedPokemons.Count <= 0)
        {
            Console.WriteLine($"\n{userData.Name}, você não adotou nenhum pokémon ainda =(");
            Thread.Sleep(4000);
            return;
        }

        Console.WriteLine("Você já adotou os seguintes pokémons:\n");

        foreach (string pokemonName in adoptedPokemons.Keys)
        {
            Console.WriteLine($"-{pokemonName}");
        }


        Console.WriteLine("\nPressione qualquer botão para voltar ao menu principal");
        Console.ReadKey();
    }
}
