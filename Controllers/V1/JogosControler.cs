using CatalogoGames.Models.InputModel;
using CatalogoGames.Models.ViewModel;
using CatalogoGames.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoGames.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class JogosControler : ControllerBase
    {
        private readonly IJogoService _jogoSevices;

        public JogosControler(IJogoService jogoService)
        {
            _jogoSevices = jogoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JogoViewModel>>> Get([FromQuery, Range(1, int.MaxValue)] int pagina = 1, [FromQuery, Range(1, 50)] int quantidade = 5)
        {
            var jogos = await _jogoSevices.Get(pagina, quantidade);

            if (jogos.Count() == 0)
                return NoContent();

            return Ok(jogos);
        }

        [HttpGet("{idJogo:guid}")]
        public async Task<ActionResult<JogoViewModel>> Get([FromRoute] Guid idJogo)
        {
            var jogo = await _jogoSevices.Get(idJogo);
            if (jogo == null)
                return NoContent();

            return Ok(jogo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoViewModel>> InserirJogo([FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                var jogo = await _jogoSevices.Inserir(jogoInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return UnprocessableEntity("já Existe um jogo com essas informações");
            }

        }

        [HttpPut("{idJogo:guid}")]
        public async Task<ActionResult> AtualizarJogo([FromRoute] Guid idJogo, [FromBody] JogoInputModel jogoInputModel)
        {
            try
            {
                await _jogoSevices.Atualizar(idJogo, jogoInputModel);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("Esse jogo não existe");
            }

        }

        [HttpPatch]
        public async Task<ActionResult> AtualizarJogo(Guid idJogo, double preco)
        {
            try
            {
                await _jogoSevices.Atualizar(idJogo, preco);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("Esse jogo não existe");
            }


        }

        [HttpDelete("{idJogo:guid}")]
        public async Task<ActionResult> DeletarJogo([FromRoute] Guid idJogo)
        {
            try
            {
                await _jogoSevices.Remover(idJogo);
                return Ok();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("Esse jogo não existe");
            }

        }
    }
}