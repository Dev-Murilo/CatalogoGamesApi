using System;

namespace CatalogoGames.Entidades
{
    public class Jogo
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Produtora { get; set; }
        public double Nota { get; set; }
        public double Preco { get; set; }
    }
}