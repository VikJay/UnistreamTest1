using UnistreamT1.BLL.Implementations.Models;
using UnistreamT1.BLL.Interfaces.Services;

namespace UnistreamT1.BLL.Implementations.Services.Actions;

public class ExitAction : ActionBase, IAction
{
    public ExitAction()
        : base("exit")
    {
    }

    public override void Next()
    {
        RunNextAction();
    }

    protected override void InitSteps()
    {
        _queue.Enqueue(new QueueActions(Name: "Exit...", Method: _ => { }));
    }
}