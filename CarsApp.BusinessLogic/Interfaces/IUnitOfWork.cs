namespace CarsApp.Businesslogic.Interfaces;

public interface IUnitOfWork
{
    event Action<object, int>? OnComplete;

    Task<int> Save();
}

