using System;
using System.Collections.Generic;

namespace Hinto.Domain.VO
{
    public class ListaInteresseVO
    {
        public long Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public UsuarioVO Usuario { get; set; }
        public List<MidiaVO> Midias { get; set; }
    }
}
