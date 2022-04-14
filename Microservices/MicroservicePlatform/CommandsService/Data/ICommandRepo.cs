using CommandsService.Models;

namespace CommandsService.Data;
public interface ICommandRepo
{
    bool SaveChanges();

    //Platform
    IEnumerable<Platform> GetPlatforms();
    void Create(Platform item);
    bool IsExistPlatform(int platformId);
    bool IsExistExternalPlatform(int externalPlatformId);

    //Commands
    IEnumerable<Command> GetCommands(int platformId);
    void Create(Command item);
    Command GetCommand(int platformId, int commandId);
}