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
    public class GenericoVsSubModuloController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GenericoVsSubModuloController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<GenericoVsSubModuloDto>>> Get(){
            var genericosvssubmodulos = await _unitOfWork.GenericosVsSubModulos.GetAllAsync();
            return _mapper.Map<List<GenericoVsSubModuloDto>>(genericosvssubmodulos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenericoVsSubModuloDto>> Get(int id){
            var genericosvssubmodulos = await _unitOfWork.GenericosVsSubModulos.GetByIdAsync(id);
            if (genericosvssubmodulos is null){
                return NotFound();
            }
            return _mapper.Map<GenericoVsSubModuloDto>(genericosvssubmodulos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GenericoVsSubModuloDto>> Post(GenericoVsSubModuloDto GenericoVsSubModuloDto){
            var genericosvssubmodulos = _mapper.Map<GenericoVsSubModulo>(GenericoVsSubModuloDto);
            if (GenericoVsSubModuloDto.FechaCreacion == DateOnly.MinValue)
            {
                GenericoVsSubModuloDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                genericosvssubmodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (GenericoVsSubModuloDto.FechaModificacion == DateOnly.MinValue)
            {
                GenericoVsSubModuloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                genericosvssubmodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.GenericosVsSubModulos.Add(genericosvssubmodulos);
            await _unitOfWork.SaveAsync();
            if (genericosvssubmodulos == null)
            {
                return BadRequest();
            }
            GenericoVsSubModuloDto.Id = genericosvssubmodulos.Id;
            return CreatedAtAction(nameof(Post), new { id = GenericoVsSubModuloDto.Id }, GenericoVsSubModuloDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GenericoVsSubModuloDto>> Put(int id, [FromBody] GenericoVsSubModuloDto GenericoVsSubModuloDto)
        {
            if (GenericoVsSubModuloDto.Id == 0)
            {
                GenericoVsSubModuloDto.Id = id;
            }
            if (GenericoVsSubModuloDto.Id != id)
            {
                return NotFound();
            }
            var genericosvssubmodulos = _mapper.Map<GenericoVsSubModulo>(GenericoVsSubModuloDto);
            GenericoVsSubModuloDto.Id = genericosvssubmodulos.Id;
            GenericoVsSubModuloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.GenericosVsSubModulos.Update(genericosvssubmodulos);
            await _unitOfWork.SaveAsync();
            return GenericoVsSubModuloDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var genericosvssubmodulos = await _unitOfWork.GenericosVsSubModulos.GetByIdAsync(id);
            if (genericosvssubmodulos == null)
            {
                return NotFound();
            }
            _unitOfWork.GenericosVsSubModulos.Remove(genericosvssubmodulos);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}