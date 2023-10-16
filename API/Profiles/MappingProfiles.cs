using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Models;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles() // Remember adding : Profile in the class
    {
        CreateMap<Auditoria, AuditoriaDto>().ReverseMap();

        CreateMap<Blockchain, BlockchainDto>().ReverseMap();

        CreateMap<EstadoNotificacion, EstadoNotificacionDto>().ReverseMap();

        CreateMap<Formato, FormatoDto>().ReverseMap();

        CreateMap<GenericoVsSubModulo, GenericoVsSubModuloDto>().ReverseMap();

        CreateMap<HiloRespuestaNotificacion, HiloRespuestaNotificacionDto>().ReverseMap();

        CreateMap<MaestroVsSubModulo, MaestroVsSubModuloDto>().ReverseMap();

        CreateMap<ModuloMaestro, ModuloMaestroDto>().ReverseMap();

        CreateMap<ModuloNotificacion, ModuloNotificacionDto>().ReverseMap();

        CreateMap<PermisoGenerico, PermisoGenericoDto>().ReverseMap();

        CreateMap<Radicado, RadicadoDto>().ReverseMap();

        CreateMap<Rol, RolDto>().ReverseMap();

        CreateMap<RolVsMaestro, RolVsMaestroDto>().ReverseMap();

        CreateMap<SubModulo, SubModuloDto>().ReverseMap();

        CreateMap<TipoNotificacion, TipoNotificacionDto>().ReverseMap();

        CreateMap<TipoRequerimiento, TipoRequerimientoDto>().ReverseMap();
    }
}