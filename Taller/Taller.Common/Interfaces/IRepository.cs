namespace Taller.Common.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);
        bool Add(T model);
        bool DeleteById(int id);
    }
}
