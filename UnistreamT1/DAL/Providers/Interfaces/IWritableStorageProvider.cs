using UnistreamT1.DAL.Entities;

namespace UnistreamT1.DAL.Providers.Interfaces;

public interface IWritableStorageProvider
{
    Transaction? Add(Transaction transaction);
}