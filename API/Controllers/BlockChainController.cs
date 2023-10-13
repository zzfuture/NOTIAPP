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
    public class BlockChainController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlockChainController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<BlockchainDto>>> Get(){
            var Blockchains = await _unitOfWork.BlockChains.GetAllAsync();
            return _mapper.Map<List<BlockchainDto>>(Blockchains);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BlockchainDto>> Get(int id){
            var BlockChain = await _unitOfWork.BlockChains.GetByIdAsync(id);
            if (BlockChain is null){
                return NotFound();
            }
            return _mapper.Map<BlockchainDto>(BlockChain);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlockchainDto>> Post(BlockchainDto BlockchainDto){
            var blockchain = _mapper.Map<Blockchain>(BlockchainDto);
            if (BlockchainDto.FechaCreacion == DateOnly.MinValue){
                BlockchainDto.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
                blockchain.FechaCreacion = DateOnly.FromDateTime(DateTime.Now);
            }
            if (BlockchainDto.FechaModificacion == DateOnly.MinValue){
                BlockchainDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
                blockchain.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            }
            _unitOfWork.BlockChains.Add(blockchain);
            await _unitOfWork.SaveAsync();
            if (blockchain is null){
                return BadRequest();
            }
            BlockchainDto.Id = blockchain.Id;
            return CreatedAtAction(nameof(Post), new {id = BlockchainDto.Id}, BlockchainDto);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<BlockchainDto>> Put(int id, [FromBody] BlockchainDto BlockchainDto){
            if (BlockchainDto.Id == 0){
                BlockchainDto.Id = id;
            }
            if(BlockchainDto.Id != 0){
                return NotFound();
            }
            var blockchain = _mapper.Map<Blockchain>(BlockchainDto);
            BlockchainDto.Id = blockchain.Id;
            BlockchainDto.FechaModificacion = DateOnly.FromDateTime(DateTime.Now);
            _unitOfWork.BlockChains.Update(blockchain);
            await _unitOfWork.SaveAsync();
            return BlockchainDto;
        }
    }
}