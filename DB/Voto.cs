using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DB
{
    public class Voto : Base
    {
        private Entidade.Voto PreencheEntidadeVoto(SqlDataReader lReader)
        {
            Entidade.Voto Voto = new Entidade.Voto();

            Voto.CodigoFilme = PreencheAtributo(lReader, 0, Voto.CodigoFilme);
            Voto.CodigoEmocao = PreencheAtributo(lReader, 1, Voto.CodigoEmocao);
            Voto.Valor = PreencheAtributo(lReader, 2, Voto.Valor);
            Voto.Data = PreencheAtributo(lReader, 3, Voto.Data);

            return Voto;
        }



        public void IncluirVoto(Entidade.Voto lVoto)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("INSERT INTO Voto (");
                sqlQuery.Append("   CodigoFilme");
                sqlQuery.Append("  ,CodigoEmocao");
                sqlQuery.Append("  ,Valor");
                sqlQuery.Append("  ,Data");
                sqlQuery.Append(" ) VALUES ( ");
                sqlQuery.Append("   @CodigoFilme");
                sqlQuery.Append("  ,@CodigoEmocao");
                sqlQuery.Append("  ,@Valor");
                sqlQuery.Append("  ,@Data");
                sqlQuery.Append(") ");

                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@CodigoFilme", lVoto.CodigoFilme));
                    command.Parameters.Add(new SqlParameter("@CodigoEmocao", lVoto.CodigoEmocao));
                    command.Parameters.Add(new SqlParameter("@Valor", lVoto.Valor));
                    command.Parameters.Add(new SqlParameter("@Data", lVoto.Data));
                    command.ExecuteNonQuery();
                }
                con.Close();
            }
        }

        public int ObterTotalVotos(int lCodigoFilme, int lCodigoEmocao)
        {
            int totalVotos = 0;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                StringBuilder sqlQuery = new StringBuilder();
                sqlQuery.Append("SELECT sum(Valor) FROM Voto");
                sqlQuery.Append(" WHERE CodigoFilme = @CodigoFilme");
                sqlQuery.Append(" AND CodigoEmocao = @CodigoEmocao");

                con.Open();
                using (SqlCommand command = new SqlCommand(sqlQuery.ToString(), con))
                {
                    command.Parameters.Add(new SqlParameter("@CodigoFilme", lCodigoFilme));
                    command.Parameters.Add(new SqlParameter("@CodigoEmocao", lCodigoFilme));
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            totalVotos = reader.GetInt32(0);
                        }
                    }
                }
                con.Close();
            }
            return totalVotos;
        }


    }
}
