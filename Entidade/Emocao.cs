using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class Emocao
    {
        public int Codigo { get; set; }
        public string Descricao { get; set; }
        public int Total { get; set; }
        public int TotalGeralPositivo { get; set; }
        public int TotalGeralNegativo { get; set; }
    }
}