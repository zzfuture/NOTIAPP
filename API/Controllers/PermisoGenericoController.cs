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
    public class PermisoGenericoController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PermisoGenericoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PermisoGenericoDto>>> Get(){
            var permisosgenericos = await _unitOfWork.PermisosGenericos.GetAllAsync();
            return _mapper.Map<List<PermisoGenericoDto>>(permisosgenericos);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PermisoGenericoDto>> Get(int id){
            var permisosgenericos = await _unitOfWork.PermisosGenericos.GetByIdAsync(id);
            if (permisosgenericos is null){
                return NotFound();
            }
            return _mapper.Map<PermisoGenericoDto>(permisosgenericos);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PermisoGenericoDto>> Post(PermisoGenericoDto PermisoGenericoDto){
            var permisosgenericos = _mapper.Map<PermisoGenerico>(PermisoGenericoDto);
            if (PermisoGenericoDto.FechaCreacion == DateOnly.MinValue)
            {
                PermisoGenericoDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                permisosgenericos.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (PermisoGenericoDto.FechaModificacion == DateOnly.MinValue)
            {
                PermisoGenericoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                permisosgenericos.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.PermisosGenericos.Add(permisosgenericos);
            await _unitOfWork.SaveAsync();
            if (permisosgenericos == null)
            {
                return BadRequest();
            }
            PermisoGenericoDto.Id = permisosgenericos.Id;
            return CreatedAtAction(nameof(Post), new { id = PermisoGenericoDto.Id }, PermisoGenericoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PermisoGenericoDto>> Put(int id, [FromBody] PermisoGenericoDto PermisoGenericoDto)
        {
            if (PermisoGenericoDto.Id == 0)
            {
                PermisoGenericoDto.Id = id;
            }
            if (PermisoGenericoDto.Id != id)
            {
                return NotFound();
            }
            var permisosgenericos = _mapper.Map<PermisoGenerico>(PermisoGenericoDto);
            PermisoGenericoDto.Id = permisosgenericos.Id;
            PermisoGenericoDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.PermisosGenericos.Update(permisosgenericos);
            await _unitOfWork.SaveAsync();
            return PermisoGenericoDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var permisosgenericos = await _unitOfWork.PermisosGenericos.GetByIdAsync(id);
            if (permisosgenericos == null)
            {
                return NotFound();
            }
            _unitOfWork.PermisosGenericos.Remove(permisosgenericos);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}