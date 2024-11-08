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
    public partial class Form8_DashBoardPerfilUsuaerio : Form, IConfiguracion
    {
        private string titulo = "PERFIL DE USUARIO";
        public Form8_DashBoardPerfilUsuaerio()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form8_DashBoardPerfilUsuaerio_Load(object sender, EventArgs e)
        {

        }
    }
}
