using RickMorty;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
HttpClient httpClient = new HttpClient();
List<Character> characters = new List<Character>();
app.Map("/", async (context) =>
{
    var response = context.Response;
    var request = context.Request;
    string id = request.Query["id"];
    Character character = await httpClient.GetFromJsonAsync<Character>($"https://rickandmortyapi.com/api/character/{id}");
    if (id != null) characters.Add(character);
    if (characters.Count > 0)
    {
        foreach (var person in characters)
            await response.WriteAsync($"Name: {person.Name}, Status: {person.Status}, " +
                $"Species: {person.Species}, Gender: {person.Gender}.\n");
    }
    else
    {
        response.StatusCode = 401;
        await response.WriteAsync("There is no any charakter.");
    }
});

app.Run();