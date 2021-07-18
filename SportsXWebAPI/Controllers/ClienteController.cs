using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsXDomain;
using SportsXRepository;
using SportsXWebAPI.Dto;
using System.Linq;

namespace SportsXWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        public ClienteController(ISportsXRepository _repository, IMapper _mapper)
        {
            this._repository = _repository;
            this._mapper = _mapper;

        }
        private ISportsXRepository _repository { get; }
        private IMapper _mapper { get; }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repository.GetAllClientesAsync(true);
                var resultsDto = _mapper.Map<IEnumerable<ClienteDto>>(results);

                return Ok(resultsDto);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Clientes não encontrados: {ex.Message}");
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
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Cliente não encontrado: {ex.Message}");
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
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Clientes não encontrados por nome: {ex.Message}");
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
                    return Created($"/api/cliente/{cliente.Id}", _mapper.Map<ClienteDto>(cliente));
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível inserir Cliente : {ex.Message}");
            }

            return BadRequest("Error Post");
        }

        [HttpPut("{ClienteId}")]
        public async Task<IActionResult> Put(int ClienteId, ClienteDto model)
        {
            try
            {
                // Recupero os telefones da requisição
                var lstIDsTel = model.Telefones.Select(s => s.Id).ToList();

                var cliente = await _repository.GetClientesById(ClienteId);
                if (cliente == null)
                    return NotFound("Cliente não encontrado para atualizar");

                // Armazeno e deleto os telefones que estão no banco e não vieram pela requisição
                var telefones = cliente.Telefones.Where(w => !lstIDsTel.Contains(w.Id)).ToList();
                if (telefones.Count > 0)
                    telefones.ForEach(f => _repository.Delete(f));

                _mapper.Map(model, cliente);
                _repository.Update(cliente);

                if (await _repository.SaveChangesAsync())
                    return Created($"/api/cliente/{model.Id}", _mapper.Map<ClienteDto>(model));
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Não foi possível atualizar Cliente : {ex.Message}");
            }

            return BadRequest("Error Put");
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
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Cliente não encontrado: {ex.Message}");
            }

            return BadRequest("Error Delete");
        }
    }
}