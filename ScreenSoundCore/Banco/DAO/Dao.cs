namespace ScreenSoundCore.Banco.Dao;

public interface IDao<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    T GetSingle(Func<T, bool> condition);
    IEnumerable<T> GetAllWith(Func<T, bool> condition);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}
