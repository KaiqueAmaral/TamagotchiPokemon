namespace PokeTamagochi;

internal class Pokemon
{

    public Pokemon(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public string Name { get; }
    public string Url { get; }

}
