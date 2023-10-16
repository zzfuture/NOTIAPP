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
    public class FormatoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FormatoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<FormatoDto>>> Get(){
            var formatos = await _unitOfWork.Formatos.GetAllAsync();
            return _mapper.Map<List<FormatoDto>>(formatos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormatoDto>> Get(int id){
            var formatos = await _unitOfWork.Formatos.GetByIdAsync(id);
            if (formatos is null){
                return NotFound();
            }
            return _mapper.Map<FormatoDto>(formatos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<FormatoDto>> Post(FormatoDto FormatoDto){
            var formatos = _mapper.Map<Formato>(FormatoDto);
            if (FormatoDto.FechaCreacion == DateOnly.MinValue)
            {
                FormatoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                formatos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (FormatoDto.FechaModificacion == DateOnly.MinValue)
            {
                FormatoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                formatos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.Formatos.Add(formatos);
            await _unitOfWork.SaveAsync();
            if (formatos == null)
            {
                return BadRequest();
            }
            FormatoDto.Id = formatos.Id;
            return CreatedAtAction(nameof(Post), new { id = FormatoDto.Id }, FormatoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<FormatoDto>> Put(int id, [FromBody] FormatoDto FormatoDto)
        {
            if (FormatoDto.Id == 0)
            {
                FormatoDto.Id = id;
            }
            if (FormatoDto.Id != id)
            {
                return NotFound();
            }
            var formatos = _mapper.Map<Formato>(FormatoDto);
            FormatoDto.Id = formatos.Id;
            FormatoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.Formatos.Update(formatos);
            await _unitOfWork.SaveAsync();
            return FormatoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var formatos = await _unitOfWork.Formatos.GetByIdAsync(id);
            if (formatos == null)
            {
                return NotFound();
            }
            _unitOfWork.Formatos.Remove(formatos);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}