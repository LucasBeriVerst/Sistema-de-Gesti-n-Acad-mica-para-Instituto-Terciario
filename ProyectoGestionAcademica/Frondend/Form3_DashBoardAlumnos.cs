﻿using System;
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
    public partial class Form3_DashBoardAlumnos : Form, IConfiguracion
    {
        private string titulo = "SUB-MENU: ALUMNOS";
        public Form3_DashBoardAlumnos()
        {
            InitializeComponent();
            Titulo = titulo;
        }
        public string Titulo { get => titulo; set => titulo = value; }
        private void Form3_Load(object sender, EventArgs e)
        {
        }
        private void Form3_DashBoardAlumnos_DataGrigViewAlumnos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
