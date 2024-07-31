using Newtonsoft.Json;

namespace PokeTamagochi.PokeAPI;

internal class PokemonAbilities
{

    public PokemonAbilities()
    {
        //Abilities = new List<AbilityReference>();
    }

    public AbilityReference Ability { get; set; }


    [JsonProperty("is_hidden")]
    public bool IsHidden { get; set; }
    public int Slot { get; set; }

}
