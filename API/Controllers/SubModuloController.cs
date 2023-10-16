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
    public class SubModuloController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SubModuloController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<SubModuloDto>>> Get(){
            var submodulos = await _unitOfWork.SubModulos.GetAllAsync();
            return _mapper.Map<List<SubModuloDto>>(submodulos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubModuloDto>> Get(int id){
            var submodulos = await _unitOfWork.SubModulos.GetByIdAsync(id);
            if (submodulos is null){
                return NotFound();
            }
            return _mapper.Map<SubModuloDto>(submodulos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SubModuloDto>> Post(SubModuloDto SubModuloDto){
            var submodulos = _mapper.Map<SubModulo>(SubModuloDto);
            if (SubModuloDto.FechaCreacion == DateOnly.MinValue)
            {
                SubModuloDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                submodulos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (SubModuloDto.FechaModificacion == DateOnly.MinValue)
            {
                SubModuloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                submodulos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.SubModulos.Add(submodulos);
            await _unitOfWork.SaveAsync();
            if (submodulos == null)
            {
                return BadRequest();
            }
            SubModuloDto.Id = submodulos.Id;
            return CreatedAtAction(nameof(Post), new { id = SubModuloDto.Id }, SubModuloDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SubModuloDto>> Put(int id, [FromBody] SubModuloDto SubModuloDto)
        {
            if (SubModuloDto.Id == 0)
            {
                SubModuloDto.Id = id;
            }
            if (SubModuloDto.Id != id)
            {
                return NotFound();
            }
            var submodulos = _mapper.Map<SubModulo>(SubModuloDto);
            SubModuloDto.Id = submodulos.Id;
            SubModuloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.SubModulos.Update(submodulos);
            await _unitOfWork.SaveAsync();
            return SubModuloDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var submodulos = await _unitOfWork.SubModulos.GetByIdAsync(id);
            if (submodulos == null)
            {
                return NotFound();
            }
            _unitOfWork.SubModulos.Remove(submodulos);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}