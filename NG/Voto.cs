using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NG
{
    public class Voto
    {
        public static void IncluirVoto(Entidade.Voto lVoto)
        {
            //Incluir filme caso nao exista no banco
            if (!NG.Filme.ExisteFilme(lVoto.Filme))
                NG.Filme.IncluirFilme(lVoto.Filme);

            DB.Voto dbVoto = new DB.Voto();
            dbVoto.IncluirVoto(lVoto);
        }
    }
}
