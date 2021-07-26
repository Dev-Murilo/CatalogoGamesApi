using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogoGames.Entidades;
using CatalogoGames.Models.InputModel;
using CatalogoGames.Models.ViewModel;
using CatalogoGames.Repositories;

namespace CatalogoGames.Services
{
    public class jogoService : IJogoService
    {
        private readonly IJogoRepositorio _jogoRepositorio;

        public jogoService(IJogoRepositorio jogoRepositorio)
        {
            _jogoRepositorio = jogoRepositorio;
        }


        public async Task<List<JogoViewModel>> Get(int pagina, int quantidade)
        {
            var jogos = await _jogoRepositorio.Obter(pagina, quantidade);
            return jogos.Select(jogo => new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Nota = jogo.Nota,
                Preco = jogo.Preco
            }).ToList();
        }

        public async Task<JogoViewModel> Get(Guid id)
        {
            var jogo = await _jogoRepositorio.Obter(id);
            if (jogo == null)
                return null;

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Nota = jogo.Nota,
                Preco = jogo.Preco

            };
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(id);

            if (entidadeJogo == null)
                throw new DuplicateWaitObjectException("Esse jogo não existe");

            entidadeJogo.Preco = preco;

            await _jogoRepositorio.Atualizar(entidadeJogo);

        }

        public async Task Atualizar(Guid id, JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(id);
            if (entidadeJogo == null)
                throw new DuplicateWaitObjectException("Esse jogo não existe");

            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;

            await _jogoRepositorio.Atualizar(entidadeJogo);
        }


        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepositorio.Obter(jogo.Nome, jogo.Produtora);
            if (entidadeJogo.Count() > 0)
                throw new DuplicateWaitObjectException("Jogo Já Cadastrado");

            var jogoInsert = new Jogo
            {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Nota = jogo.Nota,
                Preco = jogo.Preco
            };

            await _jogoRepositorio.Inserir(jogoInsert);

            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Nota = jogo.Nota,
                Preco = jogo.Preco
            };
        }

        public async Task Remover(Guid id)
        {
            var jogo = _jogoRepositorio.Obter(id);
            if (jogo == null)
                throw new Exception("Esse jogo não existe");

            await _jogoRepositorio.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepositorio?.Dispose();
        }
    }
}