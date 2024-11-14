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
    public partial class Form7_DashBoardUsuarios : Form, IConfiguracion
    {
        private string titulo = "SUB-MENU: USUARIO";
        public Form7_DashBoardUsuarios()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form7_DashBoardUsuarios_Load(object sender, EventArgs e)
        {

        }
    }
}
