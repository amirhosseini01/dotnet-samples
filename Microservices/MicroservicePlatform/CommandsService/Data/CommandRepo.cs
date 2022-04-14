using CommandsService.Models;

namespace CommandsService.Data;
public class CommandRepo : ICommandRepo
{
    private readonly AppDbContext _context;
    public CommandRepo(AppDbContext context)
    {
        _context = context;
    }
    public void Create(Platform item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        _context.Platforms.Add(item);
    }

    public void Create(Command item)
    {
        if (item is null)
            throw new ArgumentNullException(nameof(item));

        _context.Commands.Add(item);
    }

    public Command GetCommand(int platformId, int commandId)
    {
        return _context.Commands
        .Where(x => x.PlatformId == platformId && x.Id == commandId).FirstOrDefault();
    }

    public IEnumerable<Command> GetCommands(int platformId)
    {
        return _context.Commands
        .Where(x => x.PlatformId == platformId)
        .OrderBy(x => x.Platform.Name);
    }

    public IEnumerable<Platform> GetPlatforms()
    {
        return _context.Platforms.ToList();
    }

    public bool IsExistExternalPlatform(int externalPlatformId)
    {
        return _context.Platforms.Any(x => x.ExternalId == externalPlatformId);
    }

    public bool IsExistPlatform(int platformId)
    {
        return _context.Platforms.Any(x => x.Id == platformId);
    }

    public bool SaveChanges()
    {
        return _context.SaveChanges() >= 0;
    }
}