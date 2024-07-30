namespace PokeTamagochi.PokeAPI;

internal class ApiResponse
{
    public ApiResponse()
    {
        Results = new List<Results>();
    }
    public int Count { get; set; }
    public string Next { get; set; }
    public string Previous { get; set; }
    public List<Results> Results { get; set; }
}
