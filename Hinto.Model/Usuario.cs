using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class Usuario
    {
        public Usuario()
        {
            ListaInteresses = new HashSet<ListaInteresse>();
        }

        public long Id { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string NomeUsuario { get; set; }
        public int Perfil { get; set; }
        public string Senha { get; set; }
        public int? Sexo { get; set; }
        public DateTime UltimoAcesso { get; set; }

        public virtual ICollection<ListaInteresse> ListaInteresses { get; set; }
    }
}
