using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ModuloMaestroController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModuloMaestroController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ModuloMaestroDto>>> Get(){
            var modulosmaestros = await _unitOfWork.ModulosMaestros.GetAllAsync();
            return _mapper.Map<List<ModuloMaestroDto>>(modulosmaestros);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ModuloMaestroDto>> Get(int id){
            var modulosmaestros = await _unitOfWork.ModulosMaestros.GetByIdAsync(id);
            if (modulosmaestros is null){
                return NotFound();
            }
            return _mapper.Map<ModuloMaestroDto>(modulosmaestros);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ModuloMaestroDto>> Post(ModuloMaestroDto ModuloMaestroDto){
            var modulosmaestros = _mapper.Map<ModuloMaestro>(ModuloMaestroDto);
            if (ModuloMaestroDto.FechaCreacion == DateOnly.MinValue)
            {
                ModuloMaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                modulosmaestros.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (ModuloMaestroDto.FechaModificacion == DateOnly.MinValue)
            {
                ModuloMaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                modulosmaestros.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.ModulosMaestros.Add(modulosmaestros);
            await _unitOfWork.SaveAsync();
            if (modulosmaestros == null)
            {
                return BadRequest();
            }
            ModuloMaestroDto.Id = modulosmaestros.Id;
            return CreatedAtAction(nameof(Post), new { id = ModuloMaestroDto.Id }, ModuloMaestroDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuloMaestroDto>> Put(int id, [FromBody] ModuloMaestroDto ModuloMaestroDto)
        {
            if (ModuloMaestroDto.Id == 0)
            {
                ModuloMaestroDto.Id = id;
            }
            if (ModuloMaestroDto.Id != id)
            {
                return NotFound();
            }
            var modulosmaestros = _mapper.Map<ModuloMaestro>(ModuloMaestroDto);
            ModuloMaestroDto.Id = modulosmaestros.Id;
            ModuloMaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.ModulosMaestros.Update(modulosmaestros);
            await _unitOfWork.SaveAsync();
            return ModuloMaestroDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var modulosmaestros = await _unitOfWork.ModulosMaestros.GetByIdAsync(id);
            if (modulosmaestros == null)
            {
                return NotFound();
            }
            _unitOfWork.ModulosMaestros.Remove(modulosmaestros);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}