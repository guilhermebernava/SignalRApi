using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs;

//é a classe que vai poder ter os métodos que serão consumidos pela o CLIENT
public class MyHub : Hub //precisamos herdar dessa classe HUB
{
    public async IAsyncEnumerable<DateTime> Streaming(CancellationToken cancellationToken)
    {
        while (true)
        {
            //nesse caso a cada 1 segundo esse método vai mandar o tempo atual para o client.
            yield return DateTime.UtcNow;
            await Task.Delay(1000, cancellationToken);
        }

    }

    public async IAsyncEnumerable<string> ShowNames(CancellationToken cancellationToken)
    {
        while (true)
        {
            List<string> firstNames = new List<string>
                {
                    "John", "Jane", "Michael", "Emily", "David", "Sarah", "Daniel", "Olivia", "Robert", "Sophia"
                };

            List<string> lastNames = new List<string>
                {
                    "Smith", "Johnson", "Williams", "Brown", "Jones", "Davis", "Miller", "Wilson", "Moore", "Taylor"
                };

            Random random = new Random();

            string firstName = firstNames[random.Next(firstNames.Count)];
            string lastName = lastNames[random.Next(lastNames.Count)];

            yield return firstName + " " + lastName;
            await Task.Delay(3000, cancellationToken);
        }

    }
}
