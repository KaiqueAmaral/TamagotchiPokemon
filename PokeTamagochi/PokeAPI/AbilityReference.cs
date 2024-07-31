using Newtonsoft.Json;

namespace PokeTamagochi.PokeAPI;

internal class AbilityReference
{

    public AbilityReference()
    {
        
    }

    public string Name { get; set; }
    [JsonProperty("url")]
    public string UrlInfo { get; set; }
}
