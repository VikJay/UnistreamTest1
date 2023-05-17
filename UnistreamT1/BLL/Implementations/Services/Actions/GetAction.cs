using System.Text.Json;
using UnistreamT1.BLL.Implementations.Models;
using UnistreamT1.BLL.Interfaces.Services;
using UnistreamT1.DAL.Providers.Interfaces;

namespace UnistreamT1.BLL.Implementations.Services.Actions;

public class GetAction : ActionBase, IAction
{
    private readonly IReadonlyStorageProvider _storageProvider;
    
    public GetAction(IReadonlyStorageProvider storageProvider)
        : base("get")
    {
        _storageProvider = storageProvider;
    }

    public override void Next()
    {
        RunNextAction();
    }

    private void TryGet(string? value)
    {
        if (!int.TryParse(value, out var id)
            || id < 1)
        {
            throw new ArgumentException("Invalid Id");
        }
        
        var result = _storageProvider.Get(id);
        Console.WriteLine(
            result is not null
                ? JsonSerializer.Serialize(result)
                : $"Transaction with id={id} not found.");
        
        Console.WriteLine("[OK]");
    }

    protected override void InitSteps()
    {
        _queue.Enqueue(new QueueActions(Name: "Please, write transaction id:", Method: TryGet));
    }
}