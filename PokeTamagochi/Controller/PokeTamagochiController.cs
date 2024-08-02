using PokeTamagochi.Model;
using PokeTamagochi.PokeAPI;
using PokeTamagochi.Service;
using PokeTamagochi.View;
using System.ComponentModel.Design;

namespace PokeTamagochi.Controller;

internal class PokeTamagochiController
{
    public Dictionary<string, Pokemon> availablePokemons = new();

    public static Dictionary<string, Pokemon> adoptedPokemons = new();


    public User user;


    public void Setup()
    {
        try
        {
            ExecuteFirstApiRequest();

            user = PokeTamagochiView.GetUserInfoMenu();

            Start();
        }
        catch (Exception ex)
        {

        }


    }


    private void Start()
    {
        bool exit = false;

        do
        {
            PokeTamagochiView.DisplayHomeScreen(user);

            int userInput = int.Parse(Console.ReadLine()!);

            switch (userInput)
            {
                case 1:
                    string adoptedPokemon;
                    adoptedPokemon = PokeTamagochiView.DisplayAdoptionList(availablePokemons, user);
                    AddPokemonToAdoptedList(adoptedPokemon);
                    break;

                case 2:
                    PokeTamagochiView.DisplayAdoptedPokemons(adoptedPokemons, user);
                    break;

                case 3:
                    exit = true;
                    Console.WriteLine($"Obrigado por jogar! Até uma proxima {user.Name} <3");
                    break;

                default:
                    Console.WriteLine("Por favor digite uma opção válida\n");
                    Thread.Sleep(3000);
                    break;
            }

        } while (!exit);

    }

    private void ExecuteFirstApiRequest()
    {
        string apiDataJson = "";

        PokeApiManager pokemonApiResponse;
        try
        {
            apiDataJson = PokeApiManager.ExecuteGetRequest(PokeApiManager.baseUrl);

            if (!apiDataJson.StartsWith("Erro"))
            {

                pokemonApiResponse = PokeApiManager.ParseAPIResult<PokeApiManager>(apiDataJson);

                availablePokemons = Pokemon.CreatePokemonDictionary(pokemonApiResponse.Pokemons);
            }
            else
            {
                Console.WriteLine(apiDataJson);
            }



        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void AddPokemonToAdoptedList(string pokemonName)
    {
        if (pokemonName == "") return;


        adoptedPokemons.Add(pokemonName, availablePokemons[pokemonName]);
    }
}
