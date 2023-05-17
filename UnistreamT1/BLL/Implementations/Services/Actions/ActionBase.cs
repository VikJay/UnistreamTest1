using UnistreamT1.BLL.Implementations.Models;

namespace UnistreamT1.BLL.Implementations.Services.Actions;

public abstract class ActionBase
{
    public string ActionName { get; private set; }
    protected readonly Queue<QueueActions> _queue = new();

    protected ActionBase(string actionName)
    {
        ActionName = actionName;
        
        InitSteps();
    }

    public string GetName() => ActionName;
    
    public virtual bool CanExecute(string? actionName)
    {
        return ActionName == actionName;
    }

    public virtual bool IsComplete()
    {
        return _queue.Count == 0;
    }

    public virtual void StepDone()
    {
        _queue.Dequeue();
    }
    
    protected void RunNextAction()
    {
        var action = _queue.Peek();

        Console.WriteLine(action.Name);
        var value = Console.ReadLine();

        action.Method(value);
    }

    public abstract void Next();
    protected abstract void InitSteps();

}
