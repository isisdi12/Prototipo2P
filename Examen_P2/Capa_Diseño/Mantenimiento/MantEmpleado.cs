﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Capa_Logica;
using Capa_Diseño.Consulta;

namespace Capa_Diseño.Mantenimiento
{
    public partial class MantEmpleado : Form
    {
        Logica logic = new Logica();
        string scampo;

        public MantEmpleado()
        {
            InitializeComponent();
        }

        private void Btn_cerrar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Btn_minimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        void bloquearTXT()
        {
            TextBox[] txtBox = { TxtCodEmpleado, TxtNombre, TxtDepartamento, TxtPuesto, TxtSueldo }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            for (int i = 0; i < txtBox.Length; i++)
            {
                txtBox[i].Enabled = false;
            }
            //COMBO QUE USARAN 
            ComboBox[] comboBox = { comboBox1 }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            //ComboBox[] comboBox = {comboBox1, comboBox2, etc}   Aqui podemos declarar los combo 
            for (int i = 0; i < comboBox.Length; i++)
            {
                comboBox[i].Enabled = false;
            }
        }
        //Metodo para validar los txt que no esten vacios
        private int validarTXT(TextBox[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (string.IsNullOrEmpty(list[i].Text))
                {
                    MessageBox.Show("Debe completar la informacion en el campo " + list[i].Name);
                    return 0;
                }
            }
            return 1;
        }

        void limpiarTXT(TextBox[] txtBox, ComboBox[] comboBo)
        {
            //Aqui se limpian los txt
            for (int i = 0; i < txtBox.Length; i++)
            {
                txtBox[i].Text = "";
            }
            //Aqui colocamos el siguiente codigo de la tabla y su llave primaria 
            scampo = logic.siguiente("empleado", "COD");
            TxtCodEmpleado.Text = scampo;
            if (comboBox1.Text != "")
            {
                comboBox1.Text = "Activo";
            }
            else
            {
                comboBox1.Text = "Inactico";
            }
        }

        private void Btn_ingresar_Click(object sender, EventArgs e)
        {
            TextBox[] txtBox = { TxtCodEmpleado,TxtNombre,TxtDepartamento,TxtPuesto,TxtSueldo }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            for (int i = 0; i < txtBox.Length; i++)
            {
                txtBox[i].Enabled = true;
            }
            ComboBox[] comboBox = { comboBox1 }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            for (int i = 0; i < comboBox.Length; i++)
            {
                comboBox[i].Enabled = true;
            }
        }

        private void Btn_editar_Click(object sender, EventArgs e)
        {
            //COMBO QUE USARAN 
            ComboBox[] comboBox = { comboBox1 }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            //ComboBox[] comboBox = {comboBox1, comboBox2, etc}   Aqui podemos declarar los combo 
            //Primero debemos de validar si los txt vienen vacios
            TextBox[] txtBox = { TxtCodEmpleado, TxtNombre, TxtDepartamento, TxtPuesto, TxtSueldo }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            //ComboBox[] comboBox = {comboBox1, comboBox2, etc}   Aqui podemos declarar los combo 
            if (validarTXT(txtBox) == 0)
                return;
            else
            {
                if (comboBox1.Text == "Activo")
                {
                    comboBox1.Text = "1";
                }
                else
                {
                    comboBox1.Text = "0";
                }
                //Aqui se declara la tabla donde se ira a modificar y en el segundoa arreglo, se debe de colocar los nombre de los campos.
                string[] valores = { "empleado", TxtCodEmpleado.Text, TxtNombre.Text, TxtDepartamento.Text, TxtPuesto.Text, TxtSueldo.Text, comboBox1.Text };
                string[] campos = { "COD", "nombre", "departamento", "puesto","sueldo","estado" };
                if (logic.Modificar(valores, campos) == null)
                    MessageBox.Show("Ocurrio un error al modificar los datos.");
                else
                {
                    MessageBox.Show("Datos modificados exitosamente.");
                    limpiarTXT(txtBox, comboBox); //Si usamos combo, debemos de enviarselo aqui a la funcion y declarar a la funcion que recibe
                    bloquearTXT();
                }
            }

        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            //Primero debemos de validar si los txt vienen vacios
            TextBox[] txtBox = { TxtCodEmpleado, TxtNombre, TxtDepartamento, TxtPuesto, TxtSueldo }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            //COMBO QUE USARAN 
            ComboBox[] comboBox = { comboBox1 }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            //ComboBox[] comboBox = {comboBox1, comboBox2, etc}   Aqui podemos declarar los combo 
            if (validarTXT(txtBox) == 0)
                return;
            else
            {
                if (comboBox1.Text == "Activo")
                {
                    comboBox1.Text = "1";
                }
                else
                {
                    comboBox1.Text = "0";
                }
                //Aqui se declara la tabla donde se ira a insertar y los txt que se guardaran en el orden de la tabla
                string[] valores = { "empleado", TxtCodEmpleado.Text, TxtNombre.Text, TxtDepartamento.Text, TxtPuesto.Text, TxtSueldo.Text, comboBox1.Text };
                if (logic.Insertar(valores) == null)
                    MessageBox.Show("Ocurrio un error al guardar los datos.");
                else
                {
                    MessageBox.Show("Datos guardados exitosamente.");
                    limpiarTXT(txtBox, comboBox); //Si usamos combo, debemos de enviarselo aqui a la funcion y declarar a la funcion que recibe
                    bloquearTXT();
                }
            }
        }

        private void Btn_borrar_Click(object sender, EventArgs e)
        {
            //COMBO QUE USARAN 
            ComboBox[] comboBox = { comboBox1 }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            //ComboBox[] comboBox = {comboBox1, comboBox2, etc}   Aqui podemos declarar los combo 
            TextBox[] txtBox = { TxtCodEmpleado, TxtNombre, TxtDepartamento, TxtPuesto, TxtSueldo }; //COLOCAR TODOS LOS TEXTBOX QUE SE UTILZIARAN
            //Aqui se declara la tabla que se ira a eliminar, el codigo por medio del txt y el nombre de la llave primaria.
            string[] valores = { "empleado", TxtCodEmpleado.Text, "COD" };
            if (logic.Eliminar(valores) == null)
                MessageBox.Show("Ocurrio un error al borrar los datos.");
            else
            {
                MessageBox.Show("Datos eliminados exitosamente.");
                limpiarTXT(txtBox, comboBox); //Si usamos combo, debemos de enviarselo aqui a la funcion y declarar a la funcion que recibe
                bloquearTXT();
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Frm_consultaEmpleado concep = new Frm_consultaEmpleado();
            concep.ShowDialog();

            if (concep.DialogResult == DialogResult.OK)
            {
                TxtDepartamento.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                      Cells[0].Value.ToString();
            }

            TxtDepartamento.Enabled = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Frm_consultaEmpleado concep = new Frm_consultaEmpleado();
            concep.ShowDialog();

            if (concep.DialogResult == DialogResult.OK)
            {
                TxtDepartamento.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                      Cells[0].Value.ToString();
            }

            TxtDepartamento.Enabled = false;
        }

        private void Btn_consultar_Click(object sender, EventArgs e)
        {
            Frm_consultaEmpleado concep = new Frm_consultaEmpleado();
            concep.ShowDialog();

            if (concep.DialogResult == DialogResult.OK)
            {

                TxtCodEmpleado.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                      Cells[0].Value.ToString();
                TxtNombre.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                      Cells[1].Value.ToString();
                TxtPuesto.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                      Cells[2].Value.ToString();
                TxtDepartamento.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                      Cells[3].Value.ToString();
                TxtSueldo.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                     Cells[4].Value.ToString();
                comboBox1.Text = concep.Dgv_consulta.Rows[concep.Dgv_consulta.CurrentRow.Index].
                    Cells[5].Value.ToString();
              

            }
        }
    }
}
