using System;
using System.Collections.Generic;

#nullable disable

namespace Hinto.Model
{
    public partial class ListaInteresse
    {
        public long Id { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public DateTime DataCriacao { get; set; }
        public long? UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; }
    }
}
