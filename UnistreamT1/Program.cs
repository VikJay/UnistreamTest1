using Microsoft.Extensions.DependencyInjection;
using UnistreamT1.BLL.Implementations.Services;
using UnistreamT1.BLL.Implementations.Services.Actions;
using UnistreamT1.BLL.Interfaces.Services;
using UnistreamT1.DAL.Providers.Implementations;
using UnistreamT1.DAL.Providers.Interfaces;

var storage = new StorageProvider();

using var serviceProvider = new ServiceCollection()
    .AddSingleton<IWritableStorageProvider>(storage)
    .AddSingleton<IReadonlyStorageProvider>(storage)
    .AddScoped<IAction, AddAction>()
    .AddScoped<IAction, GetAction>()
    .AddScoped<IAction, ExitAction>()
    .AddScoped<ActionFactory>(x => new ActionFactory(x.GetServices<IAction>().ToArray()))
    .BuildServiceProvider();

IAction? action = null!;

while (action?.GetName() != "exit")
{
    using var scope = serviceProvider.CreateScope();
    
    Console.WriteLine("Please, input command name:");

    var command = Console.ReadLine();

    var actionFactory = scope.ServiceProvider.GetService<ActionFactory>()!;

    action = actionFactory.Resolve(command);

    if (action is null)
    {
        Console.WriteLine("Command is invalid...");
        continue;
    }

    while (!action.IsComplete())
    {
        try
        {
            action.Next();
            action.StepDone();
        }
        catch (ArgumentException e)
        {
            Console.WriteLine($"Validate exception: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

return 0;