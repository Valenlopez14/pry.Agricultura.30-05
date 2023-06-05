using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace pry.Agricultura._30._05
{


    internal class clsProduccion
    {
        private OleDbConnection conector;
        private OleDbCommand comando;
        private OleDbDataAdapter adaptador;
        private DataTable tabla;

        public clsProduccion()
        {
            conector = new OleDbConnection(Properties.Settings.Default.CADENA);
            comando = new OleDbCommand();

            comando.Connection = conector;
            comando.CommandType = CommandType.TableDirect;
            comando.CommandText = "Produccion";

            adaptador = new OleDbDataAdapter(comando);
            tabla = new DataTable();
            adaptador.Fill(tabla);

            DataColumn[] dc = new DataColumn[2];
            dc[0] = tabla.Columns["localidad"];
            dc[1] = tabla.Columns["cultivo"];
            tabla.PrimaryKey = dc;
        }

        public bool Actualizar(int localidad, int cultivo, int tn)
        {
            bool valor = true;
            Object[] clave = new Object[2];
            clave[0] = localidad;
            clave[1] = cultivo;

            DataRow filaBuscada = tabla.Rows.Find(clave);

            if (filaBuscada == null)
            {

                DataRow fila = tabla.NewRow();
                fila["localidad"] = localidad;
                fila["cultivo"] = cultivo;
                fila["toneladas"] = tn;
                tabla.Rows.Add(fila);
                OleDbCommandBuilder ocb = new OleDbCommandBuilder(adaptador);
                adaptador.Update(tabla);

            }
            else
            {
                valor = false;
                
            }

            return valor;
        }

        public void Graficar(int Localidad, Chart chart)
        {
            clsCultivos objCultivo = new clsCultivos();
            DataTable tc = objCultivo.GetCultivos();

            chart.Series.Clear();

            Series serie = chart.Series.Add("Produccion");

            foreach (DataRow fc in tc.Rows)
            { 
                Object [] clave = new object[2];
                clave [0] = Localidad;
                clave [1] = fc["cultivo"].ToString();

                int toneladas = 0;
                DataRow fp = tabla.Rows.Find(clave);

                if (fp != null)
                {
                    toneladas =(int) fp["toneladas"];
                }

                serie.Points.AddXY(fc["nombre"], toneladas);
            }
        }
    }
}
