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
    public class HiloRespuestaNotificacionController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HiloRespuestaNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<HiloRespuestaNotificacionDto>>> Get(){
            var hilorespuestanotificacion = await _unitOfWork.HiloRespuestaNoficaciones.GetAllAsync();
            return _mapper.Map<List<HiloRespuestaNotificacionDto>>(hilorespuestanotificacion);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HiloRespuestaNotificacionDto>> Get(int id){
            var hilorespuestanotificacion = await _unitOfWork.HiloRespuestaNoficaciones.GetByIdAsync(id);
            if (hilorespuestanotificacion is null){
                return NotFound();
            }
            return _mapper.Map<HiloRespuestaNotificacionDto>(hilorespuestanotificacion);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HiloRespuestaNotificacionDto>> Post(HiloRespuestaNotificacionDto HiloRespuestaNotificacionDto){
            var hilorespuestanotificacion = _mapper.Map<HiloRespuestaNotificacion>(HiloRespuestaNotificacionDto);
            if (HiloRespuestaNotificacionDto.FechaCreacion == DateOnly.MinValue)
            {
                HiloRespuestaNotificacionDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                hilorespuestanotificacion.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (HiloRespuestaNotificacionDto.FechaModificacion == DateOnly.MinValue)
            {
                HiloRespuestaNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                hilorespuestanotificacion.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.HiloRespuestaNoficaciones.Add(hilorespuestanotificacion);
            await _unitOfWork.SaveAsync();
            if (hilorespuestanotificacion == null)
            {
                return BadRequest();
            }
            HiloRespuestaNotificacionDto.Id = hilorespuestanotificacion.Id;
            return CreatedAtAction(nameof(Post), new { id = HiloRespuestaNotificacionDto.Id }, HiloRespuestaNotificacionDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<HiloRespuestaNotificacionDto>> Put(int id, [FromBody] HiloRespuestaNotificacionDto HiloRespuestaNotificacionDto)
        {
            if (HiloRespuestaNotificacionDto.Id == 0)
            {
                HiloRespuestaNotificacionDto.Id = id;
            }
            if (HiloRespuestaNotificacionDto.Id != id)
            {
                return NotFound();
            }
            var hilorespuestanotificacion = _mapper.Map<HiloRespuestaNotificacion>(HiloRespuestaNotificacionDto);
            HiloRespuestaNotificacionDto.Id = hilorespuestanotificacion.Id;
            HiloRespuestaNotificacionDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.HiloRespuestaNoficaciones.Update(hilorespuestanotificacion);
            await _unitOfWork.SaveAsync();
            return HiloRespuestaNotificacionDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var hilorespuestanotificacion = await _unitOfWork.HiloRespuestaNoficaciones.GetByIdAsync(id);
            if (hilorespuestanotificacion == null)
            {
                return NotFound();
            }
            _unitOfWork.HiloRespuestaNoficaciones.Remove(hilorespuestanotificacion);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}