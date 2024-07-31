using Newtonsoft.Json;

namespace PokeTamagochi.PokeAPI;

internal class PokemonAbilities
{

    public PokemonAbilities()
    {
        
    }

    public string Name { get; set; }

    [JsonProperty("url")]
    public string InfoUrl { get; set; }

}
