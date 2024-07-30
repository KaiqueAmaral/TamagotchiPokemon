using Newtonsoft.Json;

namespace PokeTamagochi.PokeAPI;

internal class PokemonEspecificacoes
{
    //Mostras alguns detalhes especificos por enquanto
    public PokemonEspecificacoes()
    {

    }

    [JsonProperty("base_experience")]
    public int BaseExperience { get; set; }
    public int Height { get; set; }
    public int Id { get; set; }

    [JsonProperty("is_default")]
    public bool IsDefault { get; set; }

    [JsonProperty("location_area_encounters")]
    public string LocantionAreaEncountersUrl { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public int Weight { get; set; }

}
