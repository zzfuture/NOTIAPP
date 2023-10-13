using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Models;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class AuditoriaController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuditoriaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<AuditoriaDto>>> Get()
    {
        var Auditorias = await _unitOfWork.Auditorias.GetAllAsync();
        return _mapper.Map<List<AuditoriaDto>>(Auditorias);
    }

    [HttpGet("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuditoriaDto>> Get(int Id)
    {
        var Auditoria = await _unitOfWork.Auditorias.GetByIdAsync(Id);
        if (Auditoria == null)
        {
            return NotFound();
        }
        return _mapper.Map<AuditoriaDto>(Auditoria);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuditoriaDto>> Post(AuditoriaDto AuditoriaDto)
    {
        var Auditoria = _mapper.Map<Auditoria>(AuditoriaDto);
        if (AuditoriaDto.FechaCreacion == DateOnly.MinValue)
        {
            AuditoriaDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            Auditoria.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
        }
        if (AuditoriaDto.FechaModificacion == DateOnly.MinValue)
        {
            AuditoriaDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            Auditoria.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        }
        _unitOfWork.Auditorias.Add(Auditoria);
        await _unitOfWork.SaveAsync();
        if (Auditoria == null)
        {
            return BadRequest();
        }
        AuditoriaDto.Id = Auditoria.Id;
        return CreatedAtAction(nameof(Post), new { id = AuditoriaDto.Id }, AuditoriaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AuditoriaDto>> Put(int id, [FromBody] AuditoriaDto AuditoriaDto)
    {
        if (AuditoriaDto.Id == 0)
        {
            AuditoriaDto.Id = id;
        }
        if (AuditoriaDto.Id != id)
        {
            return NotFound();
        }
        var Auditoria = _mapper.Map<Auditoria>(AuditoriaDto);
        AuditoriaDto.Id = Auditoria.Id;
        AuditoriaDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
        _unitOfWork.Auditorias.Update(Auditoria);
        await _unitOfWork.SaveAsync();
        return AuditoriaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var Auditoria = await _unitOfWork.Auditorias.GetByIdAsync(id);
        if (Auditoria == null)
        {
            return NotFound();
        }
        _unitOfWork.Auditorias.Remove(Auditoria);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}