using Microsoft.EntityFrameworkCore;
using SixMinAPI.Data;
using SixMinAPI.Models;

namespace SixMinAPI.Repositories;

public class CommandRepo
{
    private readonly AppDbContext _context;
    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }
    public async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<Command> GetAsync(int id)
    {
        return await _context.Commands.FirstAsync(x=>x.Id == id);
    }
    public async Task<IList<Command>> GetAsync()
    {
        return await _context.Commands.ToListAsync();
    }

    public async Task AddAsync(Command entity)
    {
        await _context.AddAsync(entity);
    }
    public void Update(Command entity)
    {
        _context.Update(entity);
    }
    public void Remove(Command entity)
    {
        _context.Remove(entity);
    }
}