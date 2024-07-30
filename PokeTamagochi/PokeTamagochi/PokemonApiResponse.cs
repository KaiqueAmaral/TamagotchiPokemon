namespace PokeTamagochi;

internal class PokemonApiResponse
{
    public PokemonApiResponse()
    {
        Results = new List<Pokemon>();
    }
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }
    public List<Pokemon> Results { get; set; }
}
