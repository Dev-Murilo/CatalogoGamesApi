using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CatalogoGames.Models.InputModel;
using CatalogoGames.Models.ViewModel;

namespace CatalogoGames.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Get(int pagina, int quantidade);
        Task<JogoViewModel> Get(Guid id);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Atualizar(Guid id, JogoInputModel jogo);
        Task Remover(Guid id);

    }
}