using ProyectoGestionAcademica.Frondend.Form4_Carreras;
using ProyectoGestionAcademica.Frondend.Form5_Materias;
using ProyectoGestionAcademica.Frondend.Form6_Examenes;
using ProyectoGestionAcademica.Frondend.Form7_Usuarios;
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
        #region Atributos y Propiedades
        private int id_perfil;
        private string titulo = "MENU PRINCIPAL";
        private string backUpTitulo = "MENU PRINCIPAL";
        private string nombreUsuario;

        public int Id_perfil { get => id_perfil; set => id_perfil = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string BackUpTitulo { get => backUpTitulo; set => backUpTitulo = value; }
        public string NombreUsuario { get => nombreUsuario; set => nombreUsuario = value; }
        #endregion
        public Form2_DashboardGeneral(int N_DePerfilElegido, string NomUsuarioAceptado)
        {
            InitializeComponent();
            Id_perfil = N_DePerfilElegido;
            Form2_DashboardGeneral_Labell_Titulo.Text = Titulo;
            NombreUsuario = NomUsuarioAceptado;
            EstablecerPerfil();
            Configuracion();
            timer1.Start();
            this.Refresh();
        }
        private void EstablecerPerfil()
        {
            Form2_DashboardGeneral_Button_Perfil.Text = NombreUsuario;
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
        #region Alumnos
        private void Form2_DashboardGeneral_Button_Alumnos_Click(object sender, EventArgs e)
        {
            if (Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible == true)
            {
                Form2_DashboardGeneral_Panel_Isquierdo_SubMenuAlumnos.Visible = false;
                Form2_DashboardGeneral_Button_Alumnos.BackColor = Color.FromArgb(36, 60, 100);
                DevolverTituloOriginal();
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
        private void Form2_DashboardGeneral_Button_Alumnos_Agregar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_1_Agregar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Editar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_2_Editar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Eliminar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_3_Eliminar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Asignar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_4_Asignar>();
        }
        private void Form2_DashboardGeneral_Button_Alumnos_Informacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form3_DashBoardAlumnos_5_Informacion>();
        }
        #endregion
        #region Carreras
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
        private void Form2_DashboardGeneral_Button_Carreras_Agregar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form4_DashBoardCarreras_1_Agregar>();
        }
        private void Form2_DashboardGeneral_Button_Carreras_Editar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form4_DashBoardCarreras_2_Editar>();
        }
        private void Form2_DashboardGeneral_Button_Carreras_Eliminar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form4_DashBoardCarreras_3_Eliminar>();
        }
        private void Form2_DashboardGeneral_Button_Carreras_DefinirCursada_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form4_DashBoardCarreras_4_DefinirCursada>();
        }
        private void Form2_DashboardGeneral_Button_Carreras_Informacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form4_DashBoardCarreras_5_Informacion>();
        }
        #endregion
        #region Materias
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
        private void Form2_DashboardGeneral_Button_Materias_Agregar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form5_DashBoardMaterias_1_Agregar>();
        }
        private void Form2_DashboardGeneral_Button_Materias_Editar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form5_DashBoardMaterias_2_Editar>();
        }
        private void Form2_DashboardGeneral_Button_Materias_Eliminar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form5_DashBoardMaterias_3_Eliminar>();
        }
        private void Form2_DashboardGeneral_Button_Materias_Asignar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form5_DashBoardMaterias_4_Asignar>();
        }
        private void Form2_DashboardGeneral_Button_Materias_Informacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form5_DashBoardMaterias_5_Informacion>();
        }
        #endregion
        #region Examenes
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
        private void Form2_DashboardGeneral_Button_Examenes_Agregar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form6_DashBoardExamenes_1_Agregar>();
        }
        private void Form2_DashboardGeneral_Button_Examenes_Editar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form6_DashBoardExamenes_2_Editar>();
        }
        private void Form2_DashboardGeneral_Button_Examenes_Eliminar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form6_DashBoardExamenes_3_Eliminar>();
        }
        private void Form2_DashboardGeneral_Button_Examenes_Asignar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form6_DashBoardExamenes_4_Asignar>();
        }
        private void Form2_DashboardGeneral_Button_Examenes_Informacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form6_DashBoardExamenes_5_Informacion>();
        }
        #endregion
        #region Usuarios
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
        private void Form2_DashboardGeneral_Button_Usuarios_Agregar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form7_DashBoardUsuarios_1_Agregar>();
        }
        private void Form2_DashboardGeneral_Button_Usuarios_Editar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form7_DashBoardUsuarios_2_Editar>();
        }
        private void Form2_DashboardGeneral_Button_Usuarios_Eliminar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form7_DashBoardUsuarios_3_Eliminar>();
        }
        private void Form2_DashboardGeneral_Button_Usuarios_Asignar_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form7_DashBoardUsuarios_4_Asignar>();
        }
        private void Form2_DashboardGeneral_Button_Usuarios_Informacion_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form7_DashBoardUsuarios_5_Informacion>();
        }
        #endregion
        #region Perfil
        private void Form2_DashboardGeneral_Button_Perfil_Click(object sender, EventArgs e)
        {
            AbrirFormulario<Form8_DashBoardPerfilUsuaerio>();
        }
        #endregion
        //Metodo generico para regular que formulario se muestra y si debe crearse o no
        //<MiForm>{Nombre del tipo generico} () where {Condicion} MiForm : Form {El tipo generico debe heredar de la class Form}, new () {Su contructor debe ser vacio}
        private void AbrirFormulario<MiForm>() where MiForm : Form, new()
        {
            //Variable de tipo Form
            Form Formularios;
            Formularios = Form2_DashboardGeneral_Panel_Derecho_Principal.Controls.OfType<MiForm>().FirstOrDefault();
            if (Formularios == null)
            {
                Formularios = new MiForm();
                Formularios.TopLevel = false;//Determina que el nuevo formulario no se vuelva el formulario principal
                Formularios.Dock = DockStyle.Fill;
                Formularios.FormBorderStyle = FormBorderStyle.None;
                Form2_DashboardGeneral_Panel_Derecho_Principal.Controls.Add(Formularios); //Agrega a la coleccion del panel el formulario
                Form2_DashboardGeneral_Panel_Derecho_Principal.Tag = Formularios;
                Form2_DashboardGeneral_Labell_Titulo.Text = ((IConfiguracion)Formularios).Titulo; //Toma el valor de titulo del formulario que utiliza la interface y lo ingresa en la propiedad del formulario general
                Formularios.FormClosed += (s, e) => CierreDeFormulario(); //Establece el likn entre el evento CLose y el metodo Cierre
                Form2_DashboardGeneral_Labell_Titulo.Location = new Point((1056 - Form2_DashboardGeneral_Labell_Titulo.Size.Width) / 2, Form2_DashboardGeneral_Labell_Titulo.Location.Y);
                Formularios.BackColor = Color.FromArgb(177, 173, 189);
                Formularios.Show();
                Formularios.BringToFront();

            }
            else
            {
                Formularios.BringToFront();//Si el formulatrio existe lo trae al frente
                Form2_DashboardGeneral_Labell_Titulo.Text = ((IConfiguracion)Formularios).Titulo; //Toma el valor de titulo del formulario que utiliza la interface y lo ingresa en la propiedad del formulario general
                Form2_DashboardGeneral_Labell_Titulo.Location = new Point((1056 - Form2_DashboardGeneral_Labell_Titulo.Size.Width) / 2, Form2_DashboardGeneral_Labell_Titulo.Location.Y);//Centra el componente basado en el nuevo tamaño de texto.
            }
        }
        private void CierreDeFormulario()
        {
            DevolverTituloOriginal();
        }

        private void DevolverTituloOriginal()
        {
            Form2_DashboardGeneral_Labell_Titulo.Text = BackUpTitulo; //Toma el valor de titulo del formulario que utiliza la interface y lo ingresa en la propiedad del formulario general
            Form2_DashboardGeneral_Labell_Titulo.Location = new Point((1056 - Form2_DashboardGeneral_Labell_Titulo.Size.Width) / 2, Form2_DashboardGeneral_Labell_Titulo.Location.Y);
        }
        private void Configuracion()
        {
            #region Configuracion por perfil
            switch (Id_perfil)
            {
                case 1:
                    Form2_DashboardGeneral_Button_Alumnos.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Carreras.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_DefinirCursada.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Materias.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Usuarios.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Informacion.Visible = true;
                    break;
                case 2:
                    Form2_DashboardGeneral_Button_Alumnos.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Carreras.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_DefinirCursada.Visible = true;
                    Form2_DashboardGeneral_Button_Carreras_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Materias.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Materias_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Eliminar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Asignar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    //-----------------------------------------------------------------------------
                    Form2_DashboardGeneral_Button_Usuarios.Visible = true;
                    Form2_DashboardGeneral_Button_Usuarios_Informacion.Visible = true;
                    break;
                case 3:
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Agregar.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Editar.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos.Visible = true;
                    Form2_DashboardGeneral_Button_Alumnos_Informacion.Visible = true;
                    break;
                case 4:
                    Form2_DashboardGeneral_Button_Examenes.Visible = true;
                    Form2_DashboardGeneral_Button_Examenes_Informacion.Visible = true;
                    break;
            }
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime horaActual = DateTime.Now;
            Form2_DashboardGeneral_TextBox_Hora.Text = horaActual.ToString("HH:mm:ss");
        }
    }
}
