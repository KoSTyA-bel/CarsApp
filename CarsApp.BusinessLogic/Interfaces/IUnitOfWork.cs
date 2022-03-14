namespace CarsApp.Businesslogic.Interfaces;

public interface IUnitOfWork
{
    Task<int> Save();
}

