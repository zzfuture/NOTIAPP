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
    public class RolVsMaestroController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RolVsMaestroController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RolVsMaestroDto>>> Get(){
            var rolesvsmaestros = await _unitOfWork.RolesVsMaestros.GetAllAsync();
            return _mapper.Map<List<RolVsMaestroDto>>(rolesvsmaestros);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RolVsMaestroDto>> Get(int id){
            var rolesvsmaestros = await _unitOfWork.RolesVsMaestros.GetByIdAsync(id);
            if (rolesvsmaestros is null){
                return NotFound();
            }
            return _mapper.Map<RolVsMaestroDto>(rolesvsmaestros);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<RolVsMaestroDto>> Post(RolVsMaestroDto RolVsMaestroDto){
            var rolesvsmaestros = _mapper.Map<RolVsMaestro>(RolVsMaestroDto);
            if (RolVsMaestroDto.FechaCreacion == DateOnly.MinValue)
            {
                RolVsMaestroDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                rolesvsmaestros.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (RolVsMaestroDto.FechaModificacion == DateOnly.MinValue)
            {
                RolVsMaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                rolesvsmaestros.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.RolesVsMaestros.Add(rolesvsmaestros);
            await _unitOfWork.SaveAsync();
            if (rolesvsmaestros == null)
            {
                return BadRequest();
            }
            RolVsMaestroDto.Id = rolesvsmaestros.Id;
            return CreatedAtAction(nameof(Post), new { id = RolVsMaestroDto.Id }, RolVsMaestroDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RolVsMaestroDto>> Put(int id, [FromBody] RolVsMaestroDto RolVsMaestroDto)
        {
            if (RolVsMaestroDto.Id == 0)
            {
                RolVsMaestroDto.Id = id;
            }
            if (RolVsMaestroDto.Id != id)
            {
                return NotFound();
            }
            var rolesvsmaestros = _mapper.Map<RolVsMaestro>(RolVsMaestroDto);
            RolVsMaestroDto.Id = rolesvsmaestros.Id;
            RolVsMaestroDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.RolesVsMaestros.Update(rolesvsmaestros);
            await _unitOfWork.SaveAsync();
            return RolVsMaestroDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var rolesvsmaestros = await _unitOfWork.RolesVsMaestros.GetByIdAsync(id);
            if (rolesvsmaestros == null)
            {
                return NotFound();
            }
            _unitOfWork.RolesVsMaestros.Remove(rolesvsmaestros);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}