using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
using NLayerProject.Core.Entities;
using NLayerProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IService<Person> _personService;
        private readonly IMapper _mapper;
        public PersonsController(IService<Person> personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Save(PersonDTO personDTO)
        {
            var newPerson = await _personService.AddAsync(_mapper.Map<Person>(personDTO));
            return Created(string.Empty, _mapper.Map<PersonDTO>(newPerson));
        }

        [HttpPut]
        public IActionResult Update(PersonDTO personDTO)
        {
            var updatePerson = _personService.Update(_mapper.Map<Person>(personDTO));
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var person = _personService.GetByIdAsync(id).Result;
            _personService.Remove(person);
            return NoContent();
        }
    }
}
