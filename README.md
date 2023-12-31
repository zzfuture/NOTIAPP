# Creacion de proyecto

- [Creacion de proyecto](#Creacion-de-proyecto)
  
  1. [Creacion de Solución](#creaci%C3%B3n-de-soluci%C3%B3n)
  
  2. [Creacion proyecto ClassLib](#Creacion-proyecto-ClassLib)
  
  3. [Creacion proyecto WebApi](#Creacion-Proyecto-WebApi)
  
  4. [Agregar proyectos a la solucion](#Agregar-proyectos-a-la-solucion)
  
  5. [Agregar referencia entre Proyectos](#Agregar-referencia-entre-Proyectos)

- [Instalacion de Paquetes](#Instalacion-de-paquetes)
  
  - [WebApi](#Proyecto-WebApi)
  
  - [Infrastructure](#Proyecto-Infrastructure)

- [API](#API)
  
  - [Controllers](#controllers)
    
    - [BaseController.cs](#basecontroller)
    - [ExampleController.cs](#Controller-Layout)
  
  - Dtos
    
    - [ExampleDto.cs](#Dtos)
  
  - Extensions
    
    - [ApplicationServicesExtension.cs](#ApplicationServicesExtension)
  
  - [Helper](#Helper)
    
    - [Pager.cs](#Pager)
    
    - [Params.cs](#Params)
  
  - Profiles
    
    - [MappingProfiles.cs](#mappingprofiles)
  
  - [Program.cs](#program)

- [Core](#Core)
  
  - [Interfaces](#Interfaces)
    
    - [IGenericRepository.cs](#IGenericRepository)
    
    - [IUnitOfWork.cs](#IUnitOfWork)

- [Infrastructure](#Infrastructure)
  
  - [Data](#Data)
    
    - Configuration
      
      - [ExampleConfiguration.cs](#Configuration)
    
    - [DbContext.cs](#DbContext)
  
  - [Repositories](#Repositories)
    
    - [GenericRepository.cs](#GenericRepository)
    
    - [SomeRepository.cs](#GenericRepository)
  
  - UnitOfWork
    
    - [UnitOfWork.cs](#UnitOfWork)

## 

# Creación de Solución

```bash
dotnet new sln
```

## Creacion proyecto ClassLib

```bash
dotnet new classlib -o Core
dotnet new classlib -o Infrastructure
```

## Creacion Proyecto WebApi

```bash
dotnet new webapi -o FolderDestino
```

El folder destino corresponde a la carpeta donde se creara el proyecto. Se recomienda que el nombre tenga la palabra Api Por ej. ApiAnimals.

# Agregar proyectos a la solucion

```csharp
dotnet sln add ApiAnimals
dotnet sln add Core
dotnet sln add Infrastructure
```

# Agregar referencia entre Proyectos

Nota. Recuerde que las referencias se establecen desde el proyecto que contiene la referencia

```bash
dotnet add reference ..\Infrastructure
dotnet add reference ..\Core
```

# Instalacion de paquetes

## Proyecto WebApi

Nota. Recuerde que los paquetes se instalan desde la carpeta de en este ejemplo (API)

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 7.0.11
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.11
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.11
dotnet add package Microsoft.Extensions.DependencyInjection --version 7.0.0
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.32.3
dotnet add package Serilog.AspNetCore --version 7.0.0
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
dotnet add package Microsoft.AspNetCore.RateLimiting
```

## Proyecto Infrastructure

Nota. Recuerde que los paquetes se instalan desde la carpeta de en este ejemplo (Infrastructure)

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 7.0.0
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.11
dotnet add package CsvHelper --version 30.0.1
```

#### Directorios

https://i.imgur.com/fE83ztj.png

## API

## Controllers

### BaseController

```csharp
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("Route[controller]")]
public class BaseController : Controller
{
    
}
```

### ExampleController

```csharp
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
```

### Dtos

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class BaseDto
    {
        public int Id { get; set; }
        public DateOnly FechaCreacion { get; set; }
        public DateOnly FechaModificacion { get; set; }
    }
}
```

## Extensions

### ApplicationServicesExtension

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRateLimit;
using Core.Interfaces;
using Infrastructure.UnitOfWork;

namespace API.Extensions;

public static class ApplicationServicesExtension
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin() // WithOrigins("https://domain.com")
            .AllowAnyMethod() // WithMethods("GET", "POST")
            .AllowAnyHeader(); // WithHeaders("accept", "content-type")
        });
    }); // Remember to put 'static' on the class and to add builder.Services.ConfigureCors(); and app.UseCors("CorsPolicy"); to Program.cs

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    } // Remember to add builder.Services.AddApplicationServices(); to Program.cs

    public static void ConfigureRateLimiting(this IServiceCollection services)
    {
        services.AddMemoryCache();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddInMemoryRateLimiting();
        services.Configure<IpRateLimitOptions>(options =>
        {
            options.EnableEndpointRateLimiting = true;
            options.StackBlockedRequests = false;
            options.HttpStatusCode = 429;
            options.RealIpHeader = "X-Real-IP";
            options.GeneralRules = new List<RateLimitRule>
            {
                    new RateLimitRule
                    {
                        Endpoint = "*", // Si quiere usar todos ponga *
                        Period = "10s", // Periodo de tiempo para hacer peticiones
                        Limit = 2       // Numero de peticiones durante el periodo de tiempo
                    }
            };
        });
    } // Remember adding builder.Services.ConfigureRateLimiting(); and builder.Services.AddAutoMapper(Assembly.GetEntryAssembly()); and app.UseRateLimiting(); to Program.cs
}
```

### Helper

#### Pager

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Helpers;

public class Pager<T> where T : class
{
    public string Search { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public List<T> Registers { get; private set; }

    public Pager()
    {
    }

    public Pager(List<T> registers, int total, int pageIndex, int pageSize, string search)
    {
        Registers = registers;
        Total = total;
        PageIndex = pageIndex;
        PageSize = pageSize;
        Search = search;
    }

    public int TotalPages
    {
        get { return (int)Math.Ceiling(Total / (double)PageSize); }
        set { this.TotalPages = value; }
    }

    public bool HasPreviousPage
    {
        get { return (PageIndex > 1); }
        set { this.HasPreviousPage = value; }
    }

    public bool HasNextPage
    {
        get { return (PageIndex < TotalPages); }
        set { this.HasNextPage = value; }
    }
}
```

### Params

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiAnimals.Helpers;

public class Params
{
    private int _pageSize = 5;
    private const int MaxPageSize = 50;
    private int _pageIndex = 1;
    private string _search;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }

    public int PageIndex
    {
        get => _pageIndex;
        set => _pageIndex = (value <= 0) ? 1 : value;
    }

    public string Search
    {
        get => _search;
        set => _search = (!String.IsNullOrEmpty(value)) ? value.ToLower() : "";
    }
}
```

### Profiles

#### MappingProfiles

```csharp
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
```

### Program

```csharp
using System.Reflection;
using API.Extensions;
using AspNetCoreRateLimit;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<NotiApiContext>(optionsBuilder =>
{
    string connectionString = builder.Configuration.GetConnectionString("MySqlConex");
    optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.ConfigureCors();

builder.Services.AddApplicationServices();

builder.Services.ConfigureRateLimiting();

builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseIpRateLimiting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

## Core

### Interfaces

#### IGenericRepository

```csharp
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int Id);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
} 
```

#### IUnitOfWork

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IAuditoria Auditorias { get; }
        public IBlockChain BlockChains { get; }
        public IEstadoNotificacion EstadosNotificaciones { get; }
        public IFormato Formatos { get; }
        public IGenericoVsSubModulo GenericosVsSubModulos { get; }
        public IHiloRespuestaNotificacion HiloRespuestaNoficaciones { get; }
        public IMaestroVsSubModulo MaestrosVsSubModulos { get; }
        public IModuloMaestro ModulosMaestros { get; }
        public IModuloNotificacion ModulosNotificaciones { get; }
        public IPermisoGenerico PermisosGenericos { get; }
        public IRadicado Radicados { get; }
        public IRol Roles { get; }
        public IRolVsMaestro RolesVsMaestros { get; }
        public ISubModulo SubModulos { get; }
        public ITipoNotificacion TipoNotificaciones { get; }
        public ITipoRequerimiento TipoRequerimientos { get; }
        Task<int> SaveAsync();
    }
}
```

## Infrastructure

### Data

#### DbContext

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class NotiApiContext : DbContext
    
    {
        public NotiApiContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Auditoria> Auditorias { get; set;}
        public DbSet<Blockchain> Blockchains { get; set;}
        public DbSet<EstadoNotificacion> EstadoNotificaciones { get; set;}
        public DbSet<Formato> Formatos { get; set;}
        public DbSet<GenericoVsSubModulo> GenericoVsSubModulos { get; set;}
        public DbSet<HiloRespuestaNotificacion> HiloRespuestaNotificaciones { get; set;}
        public DbSet<MaestroVsSubModulo> MaestroVsSubModulos { get; set;}
        public DbSet<ModuloMaestro> ModuloMaestros { get; set;}
        public DbSet<ModuloNotificacion> ModuloNotificaciones { get; set;}
        public DbSet<PermisoGenerico> PermisoGenericos { get; set;}
        public DbSet<Radicado> Radicados { get; set;}
        public DbSet<Rol> Roles { get; set;}
        public DbSet<RolVsMaestro> RolVsMaestros { get; set;}
        public DbSet<SubModulo> SubModulos { get; set;}
        public DbSet<TipoNotificacion> TipoNotificaciones { get; set;}
        public DbSet<TipoRequerimiento> TipoRequerimientos { get; set;}

    }
}
```

### Configuration

```csharp
using System.IO.Compression;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration;

public class BlockchainConfiguration : IEntityTypeConfiguration<Blockchain>
{
    public void Configure(EntityTypeBuilder<Blockchain> builder)
    {
        builder.ToTable("blockchain");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id);
        builder.HasOne(x => x.TipoNotificaciones).WithMany(x => x.Blockchains).HasForeignKey(x => x.IdNotificacion);
        builder.HasOne(x => x.HiloRespuestaNotificaciones).WithMany(x => x.Blockchains).HasForeignKey(x => x.IdHiloRespuesta);
        builder.HasOne(x => x.Auditorias).WithMany(x => x.Blockchains).HasForeignKey(x => x.IdAuditoria);
        builder.Property(x => x.HashGenerado).HasMaxLength(100);
        builder.Property(x => x.FechaCreacion).HasColumnType("date");
        builder.Property(x => x.FechaModificacion).HasColumnType("date");
    }
}
```

### Repositories

#### GenericRepository

```csharp
using System.Linq.Expressions;
using Core.Models;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly NotiApiContext _context;

    public GenericRepository(NotiApiContext context)
    {
        _context = context;
    }

    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
        // return (IEnumerable<T>) await _context.Entities.FromSqlRaw("SELECT * FROM entity").ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public virtual Task<T> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string _search
    )
    {
        var totalRegistros = await _context.Set<T>().CountAsync();
        var registros = await _context
            .Set<T>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}
```

#### ExampleRepository

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BlockChainRepository : GenericRepository<Blockchain>, IBlockChain
    {
        private readonly NotiApiContext _context;

        public BlockChainRepository(NotiApiContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Blockchain>> GetAllAsync()
        {
            return await _context.Blockchains.ToListAsync();
        }
        public override async Task<(int totalRegistros, IEnumerable<Blockchain> registros)> GetAllAsync(
            int pageIndex,
            int pageSize,
            string search
        )
        {
            var query = _context.Blockchains as IQueryable<Blockchain>;

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.HashGenerado.Contains(search));
            }
            query = query.OrderBy(p => p.Id);

            var totalRegistros = await query.CountAsync();
            var registros = await query
                            .Skip((pageIndex - 1) * pageSize)
                            .Take(pageSize)
                            .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}
```

### UnitOfWork

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private NotiApiContext _context;
        private IAuditoria _auditorias;
        private IBlockChain _blockchains;
        private IEstadoNotificacion _estadosnotificaciones;
        private IFormato _formatos;
        private IGenericoVsSubModulo _genericosvssubmodulos;
        private IHiloRespuestaNotificacion _hilorespuestanoficaciones;
        private IMaestroVsSubModulo _maestrosvssubmodulos;
        private IModuloMaestro _modulosmaestros;
        private IModuloNotificacion _modulosnotificaciones;
        private IPermisoGenerico _permisosgenericos;
        private IRadicado _radicados;
        private IRol _roles;
        private IRolVsMaestro _rolesvsmaestros;
        private ISubModulo _submodulos;
        private ITipoNotificacion _tiponotificaciones;
        private ITipoRequerimiento _tiporequerimientos;

        public IAuditoria Auditorias
        {
            get
            {
                if (_auditorias == null)
                {
                    _auditorias = new AuditoriaRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _auditorias;
            }
        }
        public IBlockChain BlockChains
        {
            get
            {
                if (_blockchains == null)
                {
                    _blockchains = new BlockChainRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _blockchains;
            }
        }
        public IEstadoNotificacion EstadosNotificaciones
        {
            get
            {
                if (_estadosnotificaciones == null)
                {
                    _estadosnotificaciones = new EstadoNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _estadosnotificaciones;
            }
        }
        public IFormato Formatos
        {
            get
            {
                if (_formatos == null)
                {
                    _formatos = new FormatoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _formatos;
            }
        }
        public IGenericoVsSubModulo GenericosVsSubModulos
        {
            get
            {
                if (_genericosvssubmodulos == null)
                {
                    _genericosvssubmodulos = new GenericoVsIGenericoVsSubModuloRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _genericosvssubmodulos;
            }
        }
        public IHiloRespuestaNotificacion HiloRespuestaNoficaciones
        {
            get
            {
                if (_hilorespuestanoficaciones == null)
                {
                    _hilorespuestanoficaciones = new HiloRespuestaNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _hilorespuestanoficaciones;
            }
        }
        public IMaestroVsSubModulo MaestrosVsSubModulos
        {
            get
            {
                if (_maestrosvssubmodulos == null)
                {
                    _maestrosvssubmodulos = new MaestroVsSubModuloRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _maestrosvssubmodulos;
            }
        }
        public IModuloMaestro ModulosMaestros
        {
            get
            {
                if (_modulosmaestros == null)
                {
                    _modulosmaestros = new ModuloMaestroRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _modulosmaestros;
            }
        }
        public IModuloNotificacion ModulosNotificaciones
        {
            get
            {
                if (_modulosnotificaciones == null)
                {
                    _modulosnotificaciones = new ModuloNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _modulosnotificaciones;
            }
        }
        public IPermisoGenerico PermisosGenericos
        {
            get
            {
                if (_permisosgenericos == null)
                {
                    _permisosgenericos = new PermisoGenericoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _permisosgenericos;
            }
        }
        public IRadicado Radicados
        {
            get
            {
                if (_radicados == null)
                {
                    _radicados = new RadicadoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _radicados;
            }
        }
        public IRol Roles
        {
            get
            {
                if (_roles == null)
                {
                    _roles = new RolRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _roles;
            }
        }
        public IRolVsMaestro RolesVsMaestros
        {
            get
            {
                if (_rolesvsmaestros == null)
                {
                    _rolesvsmaestros = new RolVsMaestroRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _rolesvsmaestros;
            }
        }
        public ISubModulo SubModulos
        {
            get
            {
                if (_submodulos == null)
                {
                    _submodulos = new SubModuloRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _submodulos;
            }
        }
        public ITipoNotificacion TipoNotificaciones
        {
            get
            {
                if (_tiponotificaciones == null)
                {
                    _tiponotificaciones = new TipoNotificacionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _tiponotificaciones;
            }
        }
        public ITipoRequerimiento TipoRequerimientos
        {
            get
            {
                if (_tiporequerimientos == null)
                {
                    _tiporequerimientos = new TipoRequerimientoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _tiporequerimientos;
            }
        }

        public UnitOfWork(NotiApiContext context)
        {
        _context = context;
            _context = context;
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
```
