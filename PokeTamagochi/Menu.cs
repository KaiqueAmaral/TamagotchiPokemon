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

            string chosenPokemon = Console.ReadLine()!;

            Console.WriteLine($"Pokémon escolhido foi {chosenPokemon}");


            


        }
    }
}
