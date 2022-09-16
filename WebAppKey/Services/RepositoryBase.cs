using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebAppKey.Data;
using WebAppKey.Services.Interfaces;

namespace WebAppKey.Services;

public class RepositoryBase<T> : IRepositoryBase<T> where T : class
{
    protected DataContext _context;

    public RepositoryBase(DataContext context)
    {
        _context = context;
    }

    public async Task<T> GetById(int Id)
    {
        var entity = await _context.Set<T>().FindAsync(Id);
        if (entity == null)
        {
            throw new Exception($"Identificador {Id} não encontrado!");
        }

        return entity;
    }

    public async Task<IEnumerable<T>> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
        var entity = await _context.Set<T>().FirstOrDefaultAsync(predicate);
        if (entity == null) throw new Exception("Nenhum registro localizado!");
        return entity;
    }

    /*public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate, Expression<Func<T, T>> include)
    {
        var entity = await _context.Set<T>().Include(include).FirstOrDefaultAsync(predicate);
        if (entity == null) throw new Exception("Nenhum registro localizado!");
        return entity;
    }*/

    public async Task Add(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteById(int Id)
    {
        try
        {
            var entity = await GetById(Id);
            await Delete(entity);
        }
        catch (Exception e)
        {
            throw new Exception($"Erro ao deletar Id{Id}! \n" + e.Message);
        }
    }
}
