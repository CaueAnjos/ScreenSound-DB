namespace ScreenSoundCore.Banco.Dao;

[Obsolete("We are trying to use DbContext(EntityContext) instead")]
public interface IDao<T>
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    T? GetSingle(Func<T, bool> condition);
    IEnumerable<T> GetAllWith(Func<T, bool> condition);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}
