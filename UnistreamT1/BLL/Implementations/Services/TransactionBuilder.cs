using UnistreamT1.DAL.Entities;

namespace UnistreamT1.BLL.Implementations.Services;

public class TransactionBuilder
{
    private int? _id;
    private DateTime? _transactionDate;
    private decimal? _amount;

    public TransactionBuilder WithId(int id)
    {
        _id = id;

        return this;
    }   
    
    public TransactionBuilder WithTransactionDate(DateTime transactionDate)
    {
        _transactionDate = transactionDate;

        return this;
    }   
    
    public TransactionBuilder WithAmount(decimal amount)
    {
        _amount = amount;

        return this;
    }
    
    public Transaction Build()
    {
        return new Transaction
        {
            Id = _id ?? default,
            TransactionDate = _transactionDate ?? default,
            Amount = _amount ?? default
        };
    }
}