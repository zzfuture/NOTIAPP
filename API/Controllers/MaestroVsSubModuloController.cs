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
    public class MaestroVsSubModuloController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaestroVsSubModuloController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MaestroVsSubModuloDto>>> Get(){
            var maestrosvssubmodulo = await _unitOfWork.MaestrosVsSubModulos.GetAllAsync();
            return _mapper.Map<List<MaestroVsSubModuloDto>>(maestrosvssubmodulo);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MaestroVsSubModuloDto>> Get(int id){
            var maestrosvssubmodulo = await _unitOfWork.MaestrosVsSubModulos.GetByIdAsync(id);
            if (maestrosvssubmodulo is null){
                return NotFound();
            }
            return _mapper.Map<MaestroVsSubModuloDto>(maestrosvssubmodulo);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MaestroVsSubModuloDto>> Post(MaestroVsSubModuloDto MaestroVsSubModuloDto){
            var maestrosvssubmodulo = _mapper.Map<MaestroVsSubModulo>(MaestroVsSubModuloDto);
            if (MaestroVsSubModuloDto.FechaCreacion == DateOnly.MinValue)
            {
                MaestroVsSubModuloDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                maestrosvssubmodulo.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (MaestroVsSubModuloDto.FechaModificacion == DateOnly.MinValue)
            {
                MaestroVsSubModuloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                maestrosvssubmodulo.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.MaestrosVsSubModulos.Add(maestrosvssubmodulo);
            await _unitOfWork.SaveAsync();
            if (maestrosvssubmodulo == null)
            {
                return BadRequest();
            }
            MaestroVsSubModuloDto.Id = maestrosvssubmodulo.Id;
            return CreatedAtAction(nameof(Post), new { id = MaestroVsSubModuloDto.Id }, MaestroVsSubModuloDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MaestroVsSubModuloDto>> Put(int id, [FromBody] MaestroVsSubModuloDto MaestroVsSubModuloDto)
        {
            if (MaestroVsSubModuloDto.Id == 0)
            {
                MaestroVsSubModuloDto.Id = id;
            }
            if (MaestroVsSubModuloDto.Id != id)
            {
                return NotFound();
            }
            var maestrosvssubmodulo = _mapper.Map<MaestroVsSubModulo>(MaestroVsSubModuloDto);
            MaestroVsSubModuloDto.Id = maestrosvssubmodulo.Id;
            MaestroVsSubModuloDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.MaestrosVsSubModulos.Update(maestrosvssubmodulo);
            await _unitOfWork.SaveAsync();
            return MaestroVsSubModuloDto;
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var maestrosvssubmodulo = await _unitOfWork.MaestrosVsSubModulos.GetByIdAsync(id);
            if (maestrosvssubmodulo == null)
            {
                return NotFound();
            }
            _unitOfWork.MaestrosVsSubModulos.Remove(maestrosvssubmodulo);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}