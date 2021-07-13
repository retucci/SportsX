using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsXDomain;
using SportsXRepository;
using SportsXWebAPI.Dto;

namespace SportsXWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private ISportsXRepository _repository { get; }
        private IMapper _mapper { get; }
        public ClienteController(ISportsXRepository repository, IMapper mapper)
        {
            this._mapper = mapper;
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllClientesAsync(true);
                var resultsDto = _mapper.Map<IEnumerable<ClienteDto>>(results);

                return Ok(resultsDto);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Clientes não encontrados");
            }
        }

        [HttpGet("{ClienteId}")]
        public async Task<IActionResult> Get(int ClienteId)
        {
            try
            {
                var results = await _repository.GetClientesById(ClienteId);
                var resultsDto = _mapper.Map<ClienteDto>(results);
                return Ok(resultsDto);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Cliente não encontrado");
            }
        }

        [HttpGet("getByName/{nome}")]
        public async Task<IActionResult> Get(string nome)
        {
            try
            {
                var results = await _repository.GetAllClientesAsyncByName(nome);
                var resultsDto = _mapper.Map<IEnumerable<ClienteDto>>(results);
                return Ok(resultsDto);
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Cliente não encontrado");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClienteDto model)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(model);
                _repository.Add(cliente);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/cliente/{model.Id}", _mapper.Map<ClienteDto>(model));
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível inserir Cliente : {ex.Message}");
            }

            return BadRequest("Deu erro");
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> Put(int ClienteId, ClienteDto model)
        {
            try
            {
                var cliente = await _repository.GetClientesById(ClienteId);
                if (cliente == null)
                    return NotFound("Cliente não encontrado para atualizar");

                _mapper.Map(model,cliente);
                _repository.Update(cliente);

                if (await _repository.SaveChangesAsync())
                    return Created($"/api/cliente/{model.Id}", _mapper.Map<ClienteDto>(model));
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível atualizar Cliente : {ex.Message}");
            }

            return BadRequest("Deu erro");
        }

        [HttpDelete("{ClienteId}")]
        public async Task<IActionResult> Delete(int ClienteId)
        {
            try
            {
                var cliente = await _repository.GetClientesById(ClienteId);
                if (cliente == null)
                    return NotFound("Cliente não encontrado para atualizar");

                _repository.Delete(cliente);
                if (await _repository.SaveChangesAsync())
                    return Ok();
            }
            catch (System.Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Cliente não encontrado");
            }

            return BadRequest("Deu erro");
        }
    }
}