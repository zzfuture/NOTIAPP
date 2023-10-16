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
    public class TipoNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TipoNotificacionDto>>> Get(){
            var tiponotificaciones = await _unitOfWork.TipoNotificaciones.GetAllAsync();
            return _mapper.Map<List<TipoNotificacionDto>>(tiponotificaciones);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoNotificacionDto>> Get(int id){
            var tiponotificaciones = await _unitOfWork.TipoNotificaciones.GetByIdAsync(id);
            if (tiponotificaciones is null){
                return NotFound();
            }
            return _mapper.Map<TipoNotificacionDto>(tiponotificaciones);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoNotificacionDto>> Post(TipoNotificacionDto TipoNotificacionDto){
            var tiponotificaciones = _mapper.Map<TipoNotificacion>(TipoNotificacionDto);
            if (TipoNotificacionDto.FechaCreacion == DateOnly.MinValue)
            {
                TipoNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                tiponotificaciones.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (TipoNotificacionDto.FechaModificacion == DateOnly.MinValue)
            {
                TipoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                tiponotificaciones.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.TipoNotificaciones.Add(tiponotificaciones);
            await _unitOfWork.SaveAsync();
            if (tiponotificaciones == null)
            {
                return BadRequest();
            }
            TipoNotificacionDto.Id = tiponotificaciones.Id;
            return CreatedAtAction(nameof(Post), new { id = TipoNotificacionDto.Id }, TipoNotificacionDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoNotificacionDto>> Put(int id, [FromBody] TipoNotificacionDto TipoNotificacionDto)
        {
            if (TipoNotificacionDto.Id == 0)
            {
                TipoNotificacionDto.Id = id;
            }
            if (TipoNotificacionDto.Id != id)
            {
                return NotFound();
            }
            var tiponotificaciones = _mapper.Map<TipoNotificacion>(TipoNotificacionDto);
            TipoNotificacionDto.Id = tiponotificaciones.Id;
            TipoNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.TipoNotificaciones.Update(tiponotificaciones);
            await _unitOfWork.SaveAsync();
            return TipoNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tiponotificaciones = await _unitOfWork.TipoNotificaciones.GetByIdAsync(id);
            if (tiponotificaciones == null)
            {
                return NotFound();
            }
            _unitOfWork.TipoNotificaciones.Remove(tiponotificaciones);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}