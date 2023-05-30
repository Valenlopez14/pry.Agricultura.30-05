using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry.Agricultura._30._05
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cbLocalidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            clsLocalidades objLocalidades = new clsLocalidades();
            objLocalidades.Ver(cbLocalidad);
            objLocalidades.Ver(lbGraficar);

            clsCultivos objCultivos = new clsCultivos();
            objCultivos.Ver(cbCultivo);

            
            


        }

        private void btnGraficar_Click(object sender, EventArgs e)
        {
            clsProduccion objProduccion = new clsProduccion();
            objProduccion.Graficar((int)cbLocalidad.SelectedValue, chart1);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            clsProduccion objProduccion = new clsProduccion();
            bool valor  = objProduccion.Actualizar(Convert.ToInt32(cbLocalidad.SelectedValue), Convert.ToInt32(cbCultivo.SelectedValue), Convert.ToInt32(txtTonelada.Text));

            if (valor == true)
            {
                txtTonelada.Text = "";
            }
            else
            {
                MessageBox.Show("Estos datos ya estan cargados", "ERROR");
            }
        }
    }
}
