namespace UnistreamT1.BLL.Interfaces.Services;

public interface IAction
{
    bool CanExecute(string? actionName);
    void Next();
    bool IsComplete();
    void StepDone();
    string GetName();
}