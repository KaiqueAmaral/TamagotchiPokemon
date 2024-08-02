using Newtonsoft.Json;

namespace PokeTamagochi.Model;

internal class Pokemon
{

    public Pokemon(string name, string infoUrl)
    {
        Name = name;
        InfoUrl = infoUrl;
    }

    public string Name { get; }

    [JsonProperty("url")]
    public string InfoUrl { get; }


    public static Dictionary<string, Pokemon> CreatePokemonDictionary(List<Pokemon> pokemons)
    {
        Dictionary<string, Pokemon> pokemonsDictionary = new();

        foreach (Pokemon pokemon in pokemons)
        {
            pokemonsDictionary.Add($"{pokemon.Name}", pokemon);
        }

        return pokemonsDictionary;
    }
}
