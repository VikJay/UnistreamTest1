using UnistreamT1.BLL.Implementations.Models;
using UnistreamT1.BLL.Interfaces.Services;
using UnistreamT1.DAL.Providers.Interfaces;

namespace UnistreamT1.BLL.Implementations.Services.Actions;

public class AddAction : ActionBase, IAction
{
    private readonly TransactionBuilder _transactionBuilder;

    private readonly IWritableStorageProvider _writableStorageProvider;

    public AddAction(IWritableStorageProvider writableStorageProvider)
        : base("add")
    {
        _writableStorageProvider = writableStorageProvider;

        _transactionBuilder = new TransactionBuilder();
    }

    public override void Next()
    {
        RunNextAction();
    }

    protected sealed override void InitSteps()
    {
        _queue.Enqueue(new QueueActions("Enter Id:", StepAddId));
        _queue.Enqueue(new QueueActions("Enter TransactionDate:", StepAddTransactionDate));
        _queue.Enqueue(new QueueActions("Enter Amount:", StepAddAmount));
    }

    public override void StepDone()
    {
        base.StepDone();

        if (IsComplete())
        {
            TryAdd();
        }
    }

    private void TryAdd()
    {
        var transaction = _transactionBuilder.Build();
        var result = _writableStorageProvider.Add(transaction);

        if (result is null)
        {
            Console.WriteLine($"Cannot add transaction with id={transaction.Id}");
            return;
        }

        Console.WriteLine("[OK]");
    }

    private void StepAddId(string id)
    {
        if (int.TryParse(id, out var inputId)
            && inputId > 0)
        {
            _transactionBuilder.WithId(inputId);
            return;
        }

        throw new ArgumentException("Invalid Id");
    }

    private void StepAddTransactionDate(string date)
    {
        if (DateTime.TryParse(date, out var inputDate)
            && inputDate.ToUniversalTime() <= DateTime.UtcNow)
        {
            _transactionBuilder.WithTransactionDate(inputDate);
            return;
        }

        throw new ArgumentException("Invalid Transaction Date");
    }

    private void StepAddAmount(string amount)
    {
        if (decimal.TryParse(amount, out var inputAmount)
            && inputAmount > 0)
        {
            _transactionBuilder.WithAmount(inputAmount);
            return;
        }

        throw new ArgumentException("Invalid amount");
    }
}