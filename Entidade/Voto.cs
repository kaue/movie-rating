using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidade
{
    public class Voto
    {
        public Voto(Entidade.Filme lFilme = null, Entidade.Emocao lEmocao = null, int? lValor = null)
        {
            if (lFilme != null && lEmocao != null && lValor != null)
            {
                CodigoFilme = lFilme.Codigo;
                Filme = lFilme;
                CodigoEmocao = lEmocao.Codigo;
                Emocao = lEmocao;
                Valor = (int)lValor;
                Data = DateTime.Now;
            }
        }
        public int CodigoFilme { get; set; }
        public int CodigoEmocao { get; set; }
        public int Valor { get; set; }
        public DateTime Data { get; set; }
        public Entidade.Filme Filme { get; set; }
        public Entidade.Emocao Emocao { get; set; }
    }
}
