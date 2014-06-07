using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Data;
using System.Data.Common;
namespace DB
{
    public class Base
    {
        protected string connectionString = @"data source=GGNOTE\SQLEXPRESS;initial catalog=movieDB;user id=gg;password=desenv";


        public string PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, string Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return null;
            }
            else
            {
                return lDataReader.GetString(NumeroColuna);
            }
        }

        public DateTime PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, System.DateTime Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return DateTime.MinValue;
            }
            else
            {
                return lDataReader.GetDateTime(NumeroColuna);
            }
        }

        public short PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, short Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return Int16.MinValue;
            }
            else
            {
                return lDataReader.GetInt16(NumeroColuna);
            }
        }

        public int PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, int Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return Int32.MinValue;
            }
            else
            {
                return lDataReader.GetInt32(NumeroColuna);
            }
        }

        public long PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, long Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return Int64.MinValue;
            }
            else
            {
                return lDataReader.GetInt64(NumeroColuna);
            }
        }

        public float PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, float Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return float.MinValue;
            }
            else
            {
                return lDataReader.GetFloat(NumeroColuna);
            }
        }

        public double PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, double Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return double.MinValue;
            }
            else
            {
                return lDataReader.GetDouble(NumeroColuna);
            }
        }

        public decimal PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, decimal Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return decimal.MinValue;
            }
            else
            {
                return lDataReader.GetDecimal(NumeroColuna);
            }
        }

        public byte PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, byte Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return byte.MinValue;
            }
            else
            {
                return lDataReader.GetByte(NumeroColuna);
            }
        }

        public byte[] PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, byte[] Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                Byte[] TimesTamp = new Byte[8];
                return TimesTamp;
            }
            else
            {
                Byte[] TimesTamp = new Byte[8];
                lDataReader.GetBytes(NumeroColuna, 0, TimesTamp, 0, 8);
                return TimesTamp;
            }
        }

        public char PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, char Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return char.MinValue;
            }
            else
            {
                return Convert.ToChar(lDataReader.GetString(NumeroColuna));
            }
        }

        public bool PreencheAtributo(System.Data.IDataReader lDataReader, int NumeroColuna, bool Atributo)
        {
            if ((lDataReader.IsDBNull(NumeroColuna) == true))
            {
                return false;
            }
            else
            {
                return lDataReader.GetBoolean(NumeroColuna);
            }
        }
    }
}
