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
    public class RadicadoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RadicadoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RadicadoDto>>> Get(){
            var radicados = await _unitOfWork.Radicados.GetAllAsync();
            return _mapper.Map<List<RadicadoDto>>(radicados);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RadicadoDto>> Get(int id){
            var radicados = await _unitOfWork.Radicados.GetByIdAsync(id);
            if (radicados is null){
                return NotFound();
            }
            return _mapper.Map<RadicadoDto>(radicados);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RadicadoDto>> Post(RadicadoDto RadicadoDto){
            var radicados = _mapper.Map<Radicado>(RadicadoDto);
            if (RadicadoDto.FechaCreacion == DateOnly.MinValue)
            {
                RadicadoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                radicados.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (RadicadoDto.FechaModificacion == DateOnly.MinValue)
            {
                RadicadoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                radicados.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.Radicados.Add(radicados);
            await _unitOfWork.SaveAsync();
            if (radicados == null)
            {
                return BadRequest();
            }
            RadicadoDto.Id = radicados.Id;
            return CreatedAtAction(nameof(Post), new { id = RadicadoDto.Id }, RadicadoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RadicadoDto>> Put(int id, [FromBody] RadicadoDto RadicadoDto)
        {
            if (RadicadoDto.Id == 0)
            {
                RadicadoDto.Id = id;
            }
            if (RadicadoDto.Id != id)
            {
                return NotFound();
            }
            var radicados = _mapper.Map<Radicado>(RadicadoDto);
            RadicadoDto.Id = radicados.Id;
            RadicadoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.Radicados.Update(radicados);
            await _unitOfWork.SaveAsync();
            return RadicadoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var radicados = await _unitOfWork.Radicados.GetByIdAsync(id);
            if (radicados == null)
            {
                return NotFound();
            }
            _unitOfWork.Radicados.Remove(radicados);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}