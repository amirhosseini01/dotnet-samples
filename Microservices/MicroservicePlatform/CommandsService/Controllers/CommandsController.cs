using AutoMapper;
using CommandsService.Data;
using CommandsService.Dto;
using CommandsService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandsService.Controllers
{
    [Route("api/c/Platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;
        public CommandsController(IMapper mapper, ICommandRepo commandRepo)
        {
            _mapper = mapper;
            _commandRepo = commandRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsForPlatform(int platformId)
        {
            System.Console.WriteLine($"--> Hit GetCommandsForPlatform: {platformId}");

            if (!_commandRepo.IsExistPlatform(platformId))
            {
                return NotFound();
            }
            var commands = _commandRepo.GetCommands(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public ActionResult<CommandReadDto> GetCommandForPlatform(int platformId, int commandId)
        {
            System.Console.WriteLine($"--> Hit GetCommandForPlatform: {platformId} / {commandId}");

            if (!_commandRepo.IsExistPlatform(platformId))
            {
                return NotFound();
            }
            var commands = _commandRepo.GetCommand(platformId, commandId);

            return Ok(_mapper.Map<CommandReadDto>(commands));
        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto dto)
        {
            System.Console.WriteLine($"--> Hit CreateCommandForPlatform: {platformId}");

            if (!_commandRepo.IsExistPlatform(platformId))
            {
                return NotFound();
            }

            var command = _mapper.Map<Command>(dto);

            _commandRepo.Create(command);
            _commandRepo.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command);
            int commandId = commandReadDto.Id;

            return CreatedAtRoute(nameof(GetCommandForPlatform),
                 new { platformId, commandId, commandReadDto });
        }
    }
}