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
    public class RolController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RolDto>>> Get(){
            var roles = await _unitOfWork.Roles.GetAllAsync();
            return _mapper.Map<List<RolDto>>(roles);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RolDto>> Get(int id){
            var roles = await _unitOfWork.Roles.GetByIdAsync(id);
            if (roles is null){
                return NotFound();
            }
            return _mapper.Map<RolDto>(roles);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RolDto>> Post(RolDto RolDto){
            var roles = _mapper.Map<Rol>(RolDto);
            if (RolDto.FechaCreacion == DateOnly.MinValue)
            {
                RolDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                roles.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (RolDto.FechaModificacion == DateOnly.MinValue)
            {
                RolDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                roles.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.Roles.Add(roles);
            await _unitOfWork.SaveAsync();
            if (roles == null)
            {
                return BadRequest();
            }
            RolDto.Id = roles.Id;
            return CreatedAtAction(nameof(Post), new { id = RolDto.Id }, RolDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolDto>> Put(int id, [FromBody] RolDto RolDto)
        {
            if (RolDto.Id == 0)
            {
                RolDto.Id = id;
            }
            if (RolDto.Id != id)
            {
                return NotFound();
            }
            var roles = _mapper.Map<Rol>(RolDto);
            RolDto.Id = roles.Id;
            RolDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.Roles.Update(roles);
            await _unitOfWork.SaveAsync();
            return RolDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var roles = await _unitOfWork.Roles.GetByIdAsync(id);
            if (roles == null)
            {
                return NotFound();
            }
            _unitOfWork.Roles.Remove(roles);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}