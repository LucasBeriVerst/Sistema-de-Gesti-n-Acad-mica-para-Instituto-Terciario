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
    public partial class Form6_DashBoardExamenes : Form, IConfiguracion
    {
        private string titulo = "SUB-MENU: EXAMENES";
        public Form6_DashBoardExamenes()
        {
            InitializeComponent();
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form6_DashBoardExamenes_Load(object sender, EventArgs e)
        {

        }
    }
}
