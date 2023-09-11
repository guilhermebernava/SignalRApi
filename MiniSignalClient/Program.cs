using Microsoft.AspNetCore.SignalR.Client;

//URL para conectar com o HUB, isso é definido pelo servidor
const string URL = "https://localhost:7267/chat";

//criar conexão com o servidor
await using var connection = new HubConnectionBuilder().WithUrl(URL).Build();
//conectando com o servidor


//lendo os dados do servidor
//var t1 = Task.Run(async () =>
//{
//    //precisa informar qual o nome do método que vai ficar escutando
//    await foreach (var date in connection.StreamAsync<DateTime>("Streaming"))
//    {
//        Console.WriteLine(date);
//    }
//});


//await Task.Run(async () =>
//{
//    Console.Write("Escreva um nome: ");
//    var input = Console.ReadLine();
//    await connection.InvokeAsync("AddName", input!);

//    await foreach (var date in connection.StreamAsync<List<string>>("ShowNames"))
//    {
//        date.ForEach(_ =>
//        {
//            Console.WriteLine(_);
//        });
//    }
//});

connection.On<List<string>>("ReceiveName", names =>
{
    foreach (var name in names)
    {
        Console.WriteLine(name);
    }

});

try
{
    await connection.StartAsync();

    while (true)
    {
        Console.Write("Enter a name (or 'exit' to quit): ");
        string? input = Console.ReadLine();

        if (input == null) continue;

        if (input.ToLower() == "exit")
        {
            break;
        }

        await connection.InvokeAsync("AddName", input);
        Console.WriteLine();


    }
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    Console.ReadKey();
}
finally
{
    await connection.StopAsync();
}







