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
    public partial class Form5_DashBoardMaterias : Form, IConfiguracion
    {
        private string titulo = "SUB-MENU: MATERIAS";
        public Form5_DashBoardMaterias()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form5_DashBoardMaterias_Load(object sender, EventArgs e)
        {

        }
    }
}
