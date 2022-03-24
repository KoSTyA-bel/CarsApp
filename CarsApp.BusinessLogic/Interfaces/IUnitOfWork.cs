namespace CarsApp.Businesslogic.Interfaces;

/// <summary>
/// Unit of work.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saving changes to database.
    /// </summary>
    /// <returns>Task, changes are saved in the database.</returns>
    Task<int> Save();
}

