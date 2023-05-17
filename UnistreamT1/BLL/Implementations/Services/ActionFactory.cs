using Microsoft.Extensions.DependencyInjection;

using UnistreamT1.BLL.Interfaces.Services;

namespace UnistreamT1.BLL.Implementations.Services;

public class ActionFactory
{
    private readonly IAction[] _actions;
    
    public ActionFactory(IAction[] actions)
    {
        _actions = actions;
    }

    public IAction? Resolve(string? actionName)
    {
        return _actions.SingleOrDefault(x => x.CanExecute(actionName));
    }
}