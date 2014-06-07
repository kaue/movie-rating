using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class Voto
    {
        public int CodigoFilme { get; set; }
        public int CodigoEmocao { get; set; }
        public int Valor { get; set; }
        public DateTime Data { get; set; }
        public Entidade.Filme Filme { get; set; }
        public Entidade.Emocao Emocao { get; set; }
    }
}
