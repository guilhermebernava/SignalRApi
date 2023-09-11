using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace Api.Hubs;

//é a classe que vai poder ter os métodos que serão consumidos pela o CLIENT
public class MyHub : Hub //precisamos herdar dessa classe HUB
{
    private static ConcurrentBag<string> names = new();
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            //nesse caso a cada 1 segundo esse método vai mandar o tempo atual para o client.
            yield return DateTime.UtcNow;
            await Task.Delay(1000, cancellationToken);
        }

    }

    public async Task<List<string>> ShowNames(CancellationToken cancellationToken)
    {

        return names.ToList();
    }

    public async Task AddName(string name)
    {
        names.Add(name);
        await Clients.All.SendAsync("ReceiveName", names);
    }
}
