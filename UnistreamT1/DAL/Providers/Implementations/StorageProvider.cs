using UnistreamT1.DAL.Entities;
using UnistreamT1.DAL.Providers.Interfaces;

namespace UnistreamT1.DAL.Providers.Implementations;

public class StorageProvider : IReadonlyStorageProvider, IWritableStorageProvider
{
    private readonly Dictionary<int, Transaction> _storage;

    public StorageProvider()
    {
        _storage = new();
    }
    
    public Transaction? Get(int id)
    {
        _storage.TryGetValue(id, out var result);
        return result.DeepClone();
    }

    public Transaction? Add(Transaction transaction)
    {
        var dto = transaction.DeepClone();
        return _storage.TryAdd(dto.Id, dto)
            ? transaction
            : null;
    }
}