using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Comum
{
    public class MovieAPI
    {
        private static int votosMinimos = 200;

        public static string GerarUrlApi(int lPage)
        {
            return string.Format("https://api.themoviedb.org/3/discover/movie?api_key=e6797a9cbed39809f952f4245d0cca9b&vote_count.gte={0}&page={1}&include_adult=false", votosMinimos, lPage);
        }

        public static int ObterNumeroPaginas()
        {
            string apiResult;
            JObject jObj;
            Random rnd = new Random(DateTime.Now.Millisecond);
            //Obter numero de paginas.
            apiResult = Core.HttpGet(GerarUrlApi(1));
            jObj = JObject.Parse(apiResult);
            int numeroPaginas = int.Parse(((JValue)jObj["total_pages"]).ToString());
            return numeroPaginas;
        }

        public static Entidade.Filme ObterFilmeAleatorio(int lPaginas)
        {
            Entidade.Filme filme = new Entidade.Filme();
            string apiResult;
            JObject jObj;
            Random rnd = new Random(DateTime.Now.Millisecond);
            //Obter Filmes
            apiResult = Core.HttpGet(GerarUrlApi(rnd.Next(1, lPaginas)));
            jObj = JObject.Parse(apiResult);
            int totalFilmesPagina = jObj["results"].Count();
            var filmeSelecionado = jObj["results"][rnd.Next(0, totalFilmesPagina)];
            filme.Codigo = int.Parse(filmeSelecionado["id"].ToString());
            filme.Nome = filmeSelecionado["title"].ToString();
            filme.Capa = filmeSelecionado["poster_path"].ToString();
            return filme;
        }

    }
}
