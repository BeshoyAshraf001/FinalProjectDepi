namespace FinalProjectDepi.DAL.Repositories.Classes;

using Contexts;
using Interfaces;

public class GenericRepo<T> : IGenericRepo<T>
    where T : class
{
    protected readonly AppDbContext _context;

    public GenericRepo(AppDbContext context)
    {
        _context = context;
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public T? GetById(int id)
    {
        return _context.Set<T>().Find(id);
    }

    public void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        Save();
    }

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
        Save();
    }

    public void Delete(int id)
    {
        var entity = GetById(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            Save();
        }
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}