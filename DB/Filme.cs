using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DB
{
    public class Filme : Base
    {
        private Entidade.Filme PreencheEntidadeFilme(SqlDataReader lReader)
        {
            Entidade.Filme Filme = new Entidade.Filme();
            Filme.Codigo = PreencheAtributo(lReader, 0, Filme.Codigo);
            Filme.Nome = PreencheAtributo(lReader, 1, Filme.Nome);
            Filme.Capa = PreencheAtributo(lReader, 2, Filme.Capa);

            return Filme;
        }

        public void IncluirFilme(Entidade.Filme lFilme)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("INSERT INTO Filme (");
                sqlQuery.Append("   Codigo");
                sqlQuery.Append("  ,Nome");
                sqlQuery.Append("  ,Capa");
                sqlQuery.Append(" ) VALUES ( ");
                sqlQuery.Append("   @Codigo");
                sqlQuery.Append("  ,@Nome");
                sqlQuery.Append("  ,@Capa");
                sqlQuery.Append(") ");

                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@Codigo", lFilme.Codigo));
                    command.Parameters.Add(new SqlParameter("@Nome", lFilme.Nome));
                    command.Parameters.Add(new SqlParameter("@Capa", lFilme.Capa));
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public Entidade.Filme ObterFilme(int lCodigo)
        {
            Entidade.Filme filme = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT * FROM Filme");
                sqlQuery.Append(" WHERE Codigo = @Codigo");
   
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@Codigo", lCodigo));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filme = PreencheEntidadeFilme(reader);
                        }
                    }
                }
                con.Close();
            }
            return filme;
        }


        public List<Entidade.Filme> ListarFilmesPorEmocao(int lCodigoEmocao)
        {
            List<Entidade.Filme> colecao = new List<Entidade.Filme>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT top(3) f.* FROM Filme f");
                sqlQuery.Append(" JOIN Voto v");
                sqlQuery.Append(" ON v.CodigoFilme = f.Codigo");
                sqlQuery.Append(" WHERE v.CodigoEmocao = @CodigoEmocao");
                sqlQuery.Append(" GROUP BY f.Codigo, f.Nome, f.Capa");
                sqlQuery.Append(" ORDER BY sum(v.Valor) DESC");
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@CodigoEmocao", lCodigoEmocao));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            colecao.Add(PreencheEntidadeFilme(reader));
                        }
                    }
                }
                con.Close();
            }
            return colecao;
        }
    }
}
