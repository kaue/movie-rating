using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NG
{
    public class Emocao
    {
        public static Entidade.Emocao ObterEmocaoAleatoria()
        {
            DB.Emocao dbEmocao = new DB.Emocao();
            List<Entidade.Emocao> listaEmocao = dbEmocao.ListarEmocao();
            Random rnd = new Random();
            return listaEmocao[rnd.Next(listaEmocao.Count)];
        }

        public static List<Entidade.Emocao> ListarEmocaoPorFilme(int lCodigoFilme)
        {
            DB.Emocao dbEmocao = new DB.Emocao();
            List<Entidade.Emocao> listaEmocao = dbEmocao.ListarEmocaoPorFilme(lCodigoFilme);
            foreach(Entidade.Emocao emocao in listaEmocao){
                emocao.TotalGeralPositivo = listaEmocao.Where(x=> x.Total > 0).Sum(x=> x.Total);
                emocao.TotalGeralNegativo = listaEmocao.Where(x => x.Total < 0).Sum(x => x.Total);
            }
            return listaEmocao; 
        }
    }
}
