using PokeTamagochi;
using RestSharp;
using System.Net;

Dictionary<string,Pokemon> pokemonsDisponiveis = new();


//void InitialScreen()
//{
//    Console.WriteLine("******************************");
//    Console.WriteLine("Bem vindos ao PokeTamagochi!");
//    Console.WriteLine("******************************\n");

//    Console.WriteLine("Aqui estão todas as opções de pokémons disponíveis para serem selecionados:\n");



//}



void StartConnectionWithAPI()
{
    try
    {
        var client = new RestClient("https://pokeapi.co/api/v2/pokmon");
        var request = new RestRequest("", Method.Get);
        var response = client.Get(request);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Console.WriteLine("Conexão estabelicida. Resultado:\n");
            Console.WriteLine(response.Content);
        }
        else
        {
            Console.WriteLine("Erro de conexão com API. Detalhes: " + response.ErrorMessage);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");
    }
    

}


StartConnectionWithAPI();
//InitialScreen(); 