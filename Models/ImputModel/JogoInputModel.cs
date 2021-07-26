using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogoGames.Models.InputModel
{
    public class JogoInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Nome invalido: o nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Nome invalido: o nome da produtora deve ter entre 1 e 100 caracteres")]
        public string Produtora { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "a nota deve ser entre 1 e 10")]
        public double Nota { get; set; }


        [Required]
        [Range(1, 1000, ErrorMessage = "O preço não pode ultrapassar R$ 1000 e deve ser no mínimo R$ 1")]
        public double Preco { get; set; }



    }
}