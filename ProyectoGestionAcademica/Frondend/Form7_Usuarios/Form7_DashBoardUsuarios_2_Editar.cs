﻿using System;
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
    public partial class Form7_DashBoardUsuarios_2_Editar : Form, IConfiguracion
    {
        private string titulo = "USUARIO: EDITAR";
        public Form7_DashBoardUsuarios_2_Editar()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form7_DashBoardUsuarios_2_Editar_Load(object sender, EventArgs e)
        {

        }
    }
}
