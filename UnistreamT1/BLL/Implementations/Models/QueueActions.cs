namespace UnistreamT1.BLL.Implementations.Models;

public record QueueActions(string Name, Action<string?> Method);