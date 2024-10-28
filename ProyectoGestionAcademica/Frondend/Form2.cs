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
    public partial class Form2_DashboardGeneral : Form
    {
        public Form2_DashboardGeneral()
        {
            InitializeComponent();
        }

        private void Form2_DashboardGeneral_LinkLabell_CerrarApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult = MessageBox.Show("¿ Seguro que queres salir ?", "Salir...", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.OK)
            {
                Application.Exit();
            }
        }
        private void Form2_DashboardGeneral_LinkLabell_MinimizarApp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
            }

        }
        private void Form2_DashboardGeneral_Button_Carreras_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
            }
        }

        private void Form2_DashboardGeneral_Button_Materias_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
            }
        }

        private void Form2_DashboardGeneral_Button_Examenes_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = true;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
            }
        }
        private void Form2_DashboardGeneral_Button_Usuarios_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = false;
                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(36, 60, 100);
            }
            else
            {

                Form2_DashboardGeneral_Button_Usuarios.BackColor = Color.FromArgb(3, 1, 254);
                Form2_DashboardGeneral_Button_Carreras.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Materias.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Button_Examenes.BackColor = Color.FromArgb(36, 60, 100);
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuMaterias.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuCarreras.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuExamenes.Visible = false;
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuUsuarios.Visible = true;
            }
        }
    }
}
