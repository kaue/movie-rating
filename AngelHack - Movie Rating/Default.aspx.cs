using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Movie.Web
{
    public partial class Default : System.Web.UI.Page
    {
        private Entidade.Filme Filme1;
        private Entidade.Filme Filme2;
        public Entidade.Emocao Emocao;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreencherInformacoes();
                PreencherListasFilme();
            }
            else
            {
                Filme1 = (Entidade.Filme)Session["sFilme1"];
                Filme2 = (Entidade.Filme)Session["sFilme2"];
                Emocao = (Entidade.Emocao)Session["sEmocao"];
            }

        }
        #region Preencher
        //Preenche Informacoes
        private void PreencherInformacoes()
        {
            Emocao = NG.Emocao.ObterEmocaoAleatoria();
            //Obter numero de pagainas
            int numPaginas = Comum.MovieAPI.ObterNumeroPaginas();
            //Obter filmes
            Filme1 = Comum.MovieAPI.ObterFilmeAleatorio(numPaginas);
            Filme2 = Comum.MovieAPI.ObterFilmeAleatorio(numPaginas);
            //Evitar filmes iguais
            while (Filme2.Nome == Filme1.Nome)
                Filme2 = Comum.MovieAPI.ObterFilmeAleatorio(numPaginas);
            //Preencher Informacoes
            imgMovie1Cover.ImageUrl = "http://image.tmdb.org/t/p/w185" + Filme1.Capa;
            lbMovie1Name.Text = Filme1.Nome;
            imgMovie2Cover.ImageUrl = "http://image.tmdb.org/t/p/w185" + Filme2.Capa;
            lbMovie2Name.Text = Filme2.Nome;
            //Preenche Session
            Session["sFilme1"] = Filme1;
            Session["sFilme2"] = Filme2;
            Session["sEmocao"] = Emocao;
            DataBind();
            PreencherListasFilme();
        }
        //Preenche Listas
        private void PreencherListasFilme()
        {
            rptBestMovies.DataSource = NG.Filme.ListarFilmePorEmocao(Emocao.Codigo);
            rptBestMovies.DataBind();
        }
        #endregion

        #region Botoes
        //Filme 1 Selecionado
        protected void imgMovie1Cover_Click(object sender, ImageClickEventArgs e)
        {
            if (Filme1 != null && Filme2 != null)
            {
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme1, Emocao, 1));
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme2, Emocao, -1));
            }
            PreencherInformacoes();
        }
        //Filme 2 Selecionado
        protected void imgMovie2Cover_Click(object sender, ImageClickEventArgs e)
        {
            if (Filme1 != null && Filme2 != null)
            {
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme1, Emocao, -1));
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme2, Emocao, 1));
            }
            PreencherInformacoes();
        }
        //Nenhum Filme Selecionado
        protected void txtNeither_Click(object sender, EventArgs e)
        {
            if (Filme1 != null && Filme2 != null)
            {
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme1, Emocao, -1));
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme2, Emocao, -1));
            }
            PreencherInformacoes();
        }
        //Botao Mudar Emocao
        protected void btnRefresh_Click(object sender, ImageClickEventArgs e)
        {
            Emocao = NG.Emocao.ObterEmocaoAleatoria();
            Session["sEmocao"] = Emocao;
            PreencherListasFilme();
            DataBind();
        }
        //Passou o Voto
        protected void btnSkip_Click(object sender, EventArgs e)
        {
            if (Filme1 != null && Filme2 != null)
            {
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme1, Emocao, 0));
                NG.Voto.IncluirVoto(new Entidade.Voto(Filme2, Emocao, 0));
            }
            PreencherInformacoes();
        }
        #endregion


        #region Eventos
        protected void rptBestMovies_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Entidade.Filme filme = (Entidade.Filme)e.Item.DataItem;
                Label lbMovieName = (Label)e.Item.FindControl("lbMovieName");
                Image imgBestMovie = (Image)e.Item.FindControl("imgBestMovie");
                Repeater rptEmotions = (Repeater)e.Item.FindControl("rptEmotions");

                lbMovieName.Text = filme.Nome;
                imgBestMovie.ImageUrl = "http://image.tmdb.org/t/p/w185" + filme.Capa;
                rptEmotions.DataSource = NG.Emocao.ListarEmocaoPorFilme(filme.Codigo);
                rptEmotions.DataBind();
            }

        }
        protected void rptEmotions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Entidade.Emocao emocao = (Entidade.Emocao)e.Item.DataItem;
                Label lbEmotionName = (Label)e.Item.FindControl("lbEmotionName");
                Panel pnProgressLeft = (Panel)e.Item.FindControl("pnProgressLeft");
                Panel pnProgressRight = (Panel)e.Item.FindControl("pnProgressRight");

                int porcentagem = 0;
                if (emocao.Total > 0)
                {
                    porcentagem = (int)(((double)emocao.Total / (double)emocao.TotalGeralPositivo) * 100);
                    pnProgressLeft.Style.Add("width", "0%");
                    pnProgressRight.Style.Add("width", porcentagem + "%");
                }
                else if (emocao.Total < 0)
                {
                    porcentagem = (int)(((double)emocao.Total / (double)emocao.TotalGeralNegativo) * 100);
                    pnProgressLeft.Style.Add("width", porcentagem + "%");
                    pnProgressRight.Style.Add("width", "0%");
                }
                else
                {
                    pnProgressLeft.Style.Add("width", "0%");
                    pnProgressRight.Style.Add("width", "0%");
                }
                lbEmotionName.Text = emocao.Descricao;

            }
        }
        #endregion

    }
}