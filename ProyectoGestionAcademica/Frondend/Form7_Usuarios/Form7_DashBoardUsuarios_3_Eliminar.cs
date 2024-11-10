using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProyectoGestionAcademica.Frondend.Form7_Usuarios
{
    public partial class Form7_DashBoardUsuarios_3_Eliminar : Form, IConfiguracion
    {
        private string titulo = "USUARIO: ELIMINAR";
        public Form7_DashBoardUsuarios_3_Eliminar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
    }
}
