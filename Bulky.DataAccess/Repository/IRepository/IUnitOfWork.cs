using BulkyBook.DataAccess.Repository.IRepository;

namespace Bulky.BookDataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        void Save();
    }
}
