using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class Filme
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Capa { get; set; }
        public List<Emocao> ListaEmocao { get; set; }
    }
}
