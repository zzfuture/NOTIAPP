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
    public class EstadoNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EstadoNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<EstadoNotificacionDto>>> Get(){
            var estadosnotificacion = await _unitOfWork.EstadosNotificaciones.GetAllAsync();
            return _mapper.Map<List<EstadoNotificacionDto>>(estadosnotificacion);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EstadoNotificacionDto>> Get(int id){
            var estadosnotificacion = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
            if (estadosnotificacion is null){
                return NotFound();
            }
            return _mapper.Map<EstadoNotificacionDto>(estadosnotificacion);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<EstadoNotificacionDto>> Post(EstadoNotificacionDto EstadoNotificacionDto){
            var estadosnotificacion = _mapper.Map<EstadoNotificacion>(EstadoNotificacionDto);
            if (EstadoNotificacionDto.FechaCreacion == DateOnly.MinValue)
            {
                EstadoNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                estadosnotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (EstadoNotificacionDto.FechaModificacion == DateOnly.MinValue)
            {
                EstadoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                estadosnotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.EstadosNotificaciones.Add(estadosnotificacion);
            await _unitOfWork.SaveAsync();
            if (estadosnotificacion == null)
            {
                return BadRequest();
            }
            EstadoNotificacionDto.Id = estadosnotificacion.Id;
            return CreatedAtAction(nameof(Post), new { id = EstadoNotificacionDto.Id }, EstadoNotificacionDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EstadoNotificacionDto>> Put(int id, [FromBody] EstadoNotificacionDto EstadoNotificacionDto)
        {
            if (EstadoNotificacionDto.Id == 0)
            {
                EstadoNotificacionDto.Id = id;
            }
            if (EstadoNotificacionDto.Id != id)
            {
                return NotFound();
            }
            var estadosnotificacion = _mapper.Map<EstadoNotificacion>(EstadoNotificacionDto);
            EstadoNotificacionDto.Id = estadosnotificacion.Id;
            EstadoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.EstadosNotificaciones.Update(estadosnotificacion);
            await _unitOfWork.SaveAsync();
            return EstadoNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var estadosnotificacion = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
            if (estadosnotificacion == null)
            {
                return NotFound();
            }
            _unitOfWork.EstadosNotificaciones.Remove(estadosnotificacion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}