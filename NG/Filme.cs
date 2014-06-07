using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NG
{
    public class Filme
    {
        public static void IncluirFilme(Entidade.Filme lFilme)
        {
            DB.Filme dbFilme = new DB.Filme();
            dbFilme.IncluirFilme(lFilme);
        }

        public static bool ExisteFilme(Entidade.Filme lFilme)
        {
            DB.Filme dbFilme = new DB.Filme();
            if (dbFilme.ObterFilme(lFilme.Codigo) == null)
            {
                //Nao Existe Filme
                return false;
            }
            else
            {
                //Existe Filme
                return true;
            }
        }

        public static List<Entidade.Filme> ListarFilmePorEmocao(int lCodigoEmocao)
        {
            DB.Filme dbFilme = new DB.Filme();
            return dbFilme.ListarFilmesPorEmocao(lCodigoEmocao);
        }
    }
}
