using UnistreamT1.DAL.Entities;

namespace UnistreamT1.DAL.Providers.Interfaces;

public interface IReadonlyStorageProvider
{
    Transaction? Get(int id);
}