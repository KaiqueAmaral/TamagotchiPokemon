using Newtonsoft.Json;

namespace PokeTamagochi.PokeAPI;

internal class Results
{

    public Results(string name, string url)
    {
        Name = name;
        Url = url;
    }

    public string Name { get; }
    public string Url { get; }



    public static PokemonEspecificacoes ConverteDadosRecebidosDaAPI(string json)
    {
        PokemonEspecificacoes dados;


        dados = JsonConvert.DeserializeObject<PokemonEspecificacoes>(json);

      
        return dados;
    }

}
