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
    public class ModuloNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModuloNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ModuloNotificacionDto>>> Get(){
            var modulosnotificaciones = await _unitOfWork.ModulosNotificaciones.GetAllAsync();
            return _mapper.Map<List<ModuloNotificacionDto>>(modulosnotificaciones);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ModuloNotificacionDto>> Get(int id){
            var modulosnotificaciones = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
            if (modulosnotificaciones is null){
                return NotFound();
            }
            return _mapper.Map<ModuloNotificacionDto>(modulosnotificaciones);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ModuloNotificacionDto>> Post(ModuloNotificacionDto ModuloNotificacionDto){
            var modulosnotificaciones = _mapper.Map<ModuloNotificacion>(ModuloNotificacionDto);
            if (ModuloNotificacionDto.FechaCreacion == DateOnly.MinValue)
            {
                ModuloNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                modulosnotificaciones.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (ModuloNotificacionDto.FechaModificacion == DateOnly.MinValue)
            {
                ModuloNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                modulosnotificaciones.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.ModulosNotificaciones.Add(modulosnotificaciones);
            await _unitOfWork.SaveAsync();
            if (modulosnotificaciones == null)
            {
                return BadRequest();
            }
            ModuloNotificacionDto.Id = modulosnotificaciones.Id;
            return CreatedAtAction(nameof(Post), new { id = ModuloNotificacionDto.Id }, ModuloNotificacionDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ModuloNotificacionDto>> Put(int id, [FromBody] ModuloNotificacionDto ModuloNotificacionDto)
        {
            if (ModuloNotificacionDto.Id == 0)
            {
                ModuloNotificacionDto.Id = id;
            }
            if (ModuloNotificacionDto.Id != id)
            {
                return NotFound();
            }
            var modulosnotificaciones = _mapper.Map<ModuloNotificacion>(ModuloNotificacionDto);
            ModuloNotificacionDto.Id = modulosnotificaciones.Id;
            ModuloNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.ModulosNotificaciones.Update(modulosnotificaciones);
            await _unitOfWork.SaveAsync();
            return ModuloNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var modulosnotificaciones = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
            if (modulosnotificaciones == null)
            {
                return NotFound();
            }
            _unitOfWork.ModulosNotificaciones.Remove(modulosnotificaciones);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}