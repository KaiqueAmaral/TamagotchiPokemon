using PokeTamagochi.PokeAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeTamagochi
{

    internal class Menu
    {

        public static string UserName { get; set; }
        public static int UserAge { get; set; }

        private static void DisplayMenuTitle(string title)
        {
            int lettersCount = title.Length;

            string titleSymbols = string.Empty.PadLeft(lettersCount, '*');
            Console.WriteLine(titleSymbols);
            Console.WriteLine(title);
            Console.WriteLine(titleSymbols + "\n");
        }

        public static void DisplayAdoptionMenu(Dictionary<string, Pokemon> availablePokemons)
        {
            Console.Clear();

            DisplayMenuTitle("Adotando um pokémon!");

            Console.WriteLine("Escolha uma das opções abaixo:\n");

            foreach (string pokemonName in availablePokemons.Keys)
            {
                Console.WriteLine($"-{pokemonName}");
            }
            Console.WriteLine();

            //Adicionar verificação se o pokemon existe
            string chosenPokemon = Console.ReadLine()!;

            Console.WriteLine($"\n{UserName}, você deseja:\n");

            Console.WriteLine($"1 - Adotar {chosenPokemon}");
            Console.WriteLine($"2 - Ver mais detalhes sobre {chosenPokemon}");
            Console.WriteLine($"3 - Voltar\n");


            int userInput = int.Parse(Console.ReadLine()!);

            switch (userInput)
            {
                case 1:
                    DisplayAdoptionConfirmation(chosenPokemon);
                    break;

                case 2:
                    //DisplayPokemonInfo();
                    break;

                case 3:

                    break;

                default:
                    Console.WriteLine("Opção inválida. Retornando para o menu principal.");
                    break;
            }

        }


        private static void DisplayAdoptionConfirmation(string pokemonName)
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
    }
}
