using System.Collections.Generic;
using AutoMapper;
using Commander.Controllers;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVCPractice.Test.Commander
{
    public class CommandsControllerTest : CommandsController
    {
        private ICommanderRepo _repository;
        private IMapper _mapper;
        public CommandsControllerTest(ICommanderRepo repository, IMapper mapper) : base(repository, mapper)
        {
            repository = _repository;
            mapper = _mapper;
        }

        new public ActionResult <IEnumerable<CommandReadDto>> GetAllCommands()
        {
            IEnumerable<Command> commandItems = _repository.GetAllCommands();
            
            List<CommandReadDto> result = new List<CommandReadDto>();

            foreach (var command in commandItems)
            {
                result.Add(new CommandReadDto{
                    Id = command.Id,
                    HowTo = command.HowTo,
                    Line = command.Line
                });
            };

            return Ok(result);
        
        }
    }
}