using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DB
{
    public class Emocao : Base
    {
        private Entidade.Emocao PreencheEntidadeEmocao(SqlDataReader lReader)
        {
            Entidade.Emocao Emocao = new Entidade.Emocao();

            Emocao.Codigo = PreencheAtributo(lReader, 0, Emocao.Codigo);
            Emocao.Descricao = PreencheAtributo(lReader, 1, Emocao.Descricao);

            return Emocao;
        }

        private Entidade.Emocao PreencheEntidadeEmocaoComTotal(SqlDataReader lReader)
        {
            Entidade.Emocao emocao = new Entidade.Emocao();

            emocao.Codigo = PreencheAtributo(lReader, 0, emocao.Codigo);
            emocao.Descricao = PreencheAtributo(lReader, 1, emocao.Descricao);
            emocao.Total = PreencheAtributo(lReader, 2, emocao.Total);

            return emocao;
        }

        public void IncluirEmocao(Entidade.Emocao lEmocao)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("INSERT INTO Emocao (");
                sqlQuery.Append("   Codigo");
                sqlQuery.Append("  ,Descricao");
                sqlQuery.Append(" ) VALUES ( ");
                sqlQuery.Append("   @Codigo");
                sqlQuery.Append("  ,@Descricao");
                sqlQuery.Append(") ");

                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@Codigo", lEmocao.Codigo));
                    command.Parameters.Add(new SqlParameter("@Descricao", lEmocao.Descricao));
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public Entidade.Emocao ObterEmocao(int lCodigo)
        {
            Entidade.Emocao Emocao = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT * FROM Emocao");
                sqlQuery.Append(" WHERE Codigo = @Codigo");

                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@Codigo", lCodigo));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Emocao = PreencheEntidadeEmocao(reader);
                        }

                    }
                }
                con.Close();
            }
            return Emocao;
        }

        public List<Entidade.Emocao> ListarEmocao()
        {
            List<Entidade.Emocao> colecao = new List<Entidade.Emocao>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT * FROM Emocao");

                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            colecao.Add(PreencheEntidadeEmocao(reader));
                        }
                    }
                }
                con.Close();
            }
            return colecao;
        }

        public List<Entidade.Emocao> ListarEmocaoPorFilme(int lCodigoFilme)
        {

            List<Entidade.Emocao> colecao = new List<Entidade.Emocao>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT e.Codigo, e.Descricao, isnull(sum(v.Valor),0) FROM Emocao e");
                sqlQuery.Append(" LEFT JOIN Voto v");
                sqlQuery.Append(" ON v.CodigoEmocao = e.Codigo");
                sqlQuery.Append(" AND v.CodigoFilme = @CodigoFilme");
                sqlQuery.Append(" GROUP BY e.Codigo, e.Descricao");
                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@CodigoFilme", lCodigoFilme));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            colecao.Add(PreencheEntidadeEmocaoComTotal(reader));
                        }
                    }
                }
                con.Close();
            }
            return colecao;
        }
    }
}
