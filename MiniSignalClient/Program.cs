using Microsoft.AspNetCore.SignalR.Client;

//URL para conectar com o HUB, isso é definido pelo servidor
const string URL = "https://localhost:7267/chat";

//criar conexão com o servidor
await using var connection = new HubConnectionBuilder().WithUrl(URL).Build();
//conectando com o servidor
await connection.StartAsync();

//lendo os dados do servidor
var t1 = Task.Run(async () =>
{
    //precisa informar qual o nome do método que vai ficar escutando
    await foreach (var date in connection.StreamAsync<DateTime>("Streaming"))
    {
        Console.WriteLine(date);
    }
});


var t2 = Task.Run(async () =>
{
    await foreach (var date in connection.StreamAsync<string>("ShowNames"))
    {
        Console.WriteLine(date);
    }
});

Task.WaitAll(t1,t2);

