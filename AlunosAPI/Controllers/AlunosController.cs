using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AlunosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    //[Produces("application/json")]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos");
            }
        }

        [HttpGet("AlunoPorNome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunosByName([FromQuery]string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunosByNome(nome);
                if (alunos == null)
                    return NotFound($"Não existem alunos com o critério {nome}!");
                return Ok(alunos);
            }
            catch
            {
                return BadRequest("Request inválida!");
            }
        }

        [HttpGet("{id:int}", Name ="GetAluno")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAluno([FromQuery]int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null)
                    return NotFound($"Não existem o aluno com o id = {id}!");
                return Ok(aluno);
            }
            catch
            {
                return BadRequest("Request inválida!");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return CreatedAtRoute(nameof(GetAluno), new { id = aluno.Id }, aluno);
            }
            catch 
            {
                return BadRequest("Request inválida!");                
            }
        }

        [HttpPut("{id:int}")] 
        public async Task<ActionResult> Edit(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                await _alunoService.UpdateAluno(aluno);
                //return NoContent(); - retorna 204
                return Ok($"Aluno com id={id} alterado com sucesso!");                    
                }else{
                    return BadRequest("Informações inconsistentes!");
                }
            }
            catch 
            {
                return BadRequest("Request inválida!");                
            }
        }
        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return Ok("Aluno deletado com sucesso!");
                }else{
                    return NotFound("Aluno não encontrado!");
                }
            }
            catch 
            {
                return BadRequest("Request inválida!");                
            }
        }
    }
}