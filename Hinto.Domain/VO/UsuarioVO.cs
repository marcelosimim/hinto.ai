using Hinto.Common.Enum;
using System;

namespace Hinto.Domain.VO
{
    public class UsuarioVO
    {
        public long Id { get; set; }
        public Perfil Perfil { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public DateTime UltimoAcesso { get; set; }
        public DateTime DataCriacao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
    }
}
