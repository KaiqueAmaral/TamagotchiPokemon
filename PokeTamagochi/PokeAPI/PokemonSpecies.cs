using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace PokeTamagochi.PokeAPI;

internal class PokemonSpecies
{
    public PokemonSpecies()
    {
        Pokemons = new List<Pokemon>();
    }
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }

    [JsonProperty("results")]
    public List<Pokemon> Pokemons { get; set; }






}
