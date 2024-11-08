using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend
{
    public partial class Form4_DashBoardCarreras : Form, IConfiguracion
    {
        private string titulo = "SUB-MENU: CARRERAS";
        public Form4_DashBoardCarreras()
        {
            InitializeComponent();
            Titulo = titulo;
        }

        public string Titulo { get => titulo; set => titulo = value; }

        private void Form4_DashBoardCarreras_Load(object sender, EventArgs e)
        {

        }
    }
}
