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
    public partial class Form2_1DashBoardGeneralVolver : Form, IConfiguracion
    {
        private string titulo = "MENU PRINCIPAL";
        public string Titulo { get => titulo; set => titulo = value; }
        public Form2_1DashBoardGeneralVolver()
        {

        }
        public Form2_1DashBoardGeneralVolver(string titulo)
        {
            Titulo = titulo;
        }
    }
}
