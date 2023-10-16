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
    public class TipoRequerimientoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TipoRequerimientoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TipoRequerimientoDto>>> Get(){
            var tiporequerimiento = await _unitOfWork.TipoRequerimientos.GetAllAsync();
            return _mapper.Map<List<TipoRequerimientoDto>>(tiporequerimiento);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoRequerimientoDto>> Get(int id){
            var tiporequerimiento = await _unitOfWork.TipoRequerimientos.GetByIdAsync(id);
            if (tiporequerimiento is null){
                return NotFound();
            }
            return _mapper.Map<TipoRequerimientoDto>(tiporequerimiento);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TipoRequerimientoDto>> Post(TipoRequerimientoDto TipoRequerimientoDto){
            var tiporequerimiento = _mapper.Map<TipoRequerimiento>(TipoRequerimientoDto);
            if (TipoRequerimientoDto.FechaCreacion == DateOnly.MinValue)
            {
                TipoRequerimientoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                tiporequerimiento.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (TipoRequerimientoDto.FechaModificacion == DateOnly.MinValue)
            {
                TipoRequerimientoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                tiporequerimiento.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.TipoRequerimientos.Add(tiporequerimiento);
            await _unitOfWork.SaveAsync();
            if (tiporequerimiento == null)
            {
                return BadRequest();
            }
            TipoRequerimientoDto.Id = tiporequerimiento.Id;
            return CreatedAtAction(nameof(Post), new { id = TipoRequerimientoDto.Id }, TipoRequerimientoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TipoRequerimientoDto>> Put(int id, [FromBody] TipoRequerimientoDto TipoRequerimientoDto)
        {
            if (TipoRequerimientoDto.Id == 0)
            {
                TipoRequerimientoDto.Id = id;
            }
            if (TipoRequerimientoDto.Id != id)
            {
                return NotFound();
            }
            var tiporequerimiento = _mapper.Map<TipoRequerimiento>(TipoRequerimientoDto);
            TipoRequerimientoDto.Id = tiporequerimiento.Id;
            TipoRequerimientoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.TipoRequerimientos.Update(tiporequerimiento);
            await _unitOfWork.SaveAsync();
            return TipoRequerimientoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var tiporequerimiento = await _unitOfWork.TipoRequerimientos.GetByIdAsync(id);
            if (tiporequerimiento == null)
            {
                return NotFound();
            }
            _unitOfWork.TipoRequerimientos.Remove(tiporequerimiento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}