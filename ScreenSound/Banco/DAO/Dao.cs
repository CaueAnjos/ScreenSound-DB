namespace ScreenSound.Banco.Dao;

internal interface IDao<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Remove(T entity);
    void Update(T entity);
}
