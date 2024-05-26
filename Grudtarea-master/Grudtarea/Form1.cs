using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grudtarea.Data;

using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Wfdb
{
    public partial class Form1 : Form
    {

        // Lista de razas
        private string[] razasDragonBall = {
            "Android",
            "Bio-Android",
            "Humana",
            "Humano",
            "Majin",
            "Namekuseijin",
            "Saiyajin",
            "Saiyajin/Humano",
            "Saiyajin/Saiyajin"
        };
        private PersonajeDB personaje;

        public Form1()
        {
            InitializeComponent();
            personaje = new PersonajeDB("localhost", "root", "Soynoob9182");
        }




        private void buttonCargaData_Click(object sender, EventArgs e)
        {
            dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Llenar el ComboBox con las razas
            comboBoxraza.Items.AddRange(razasDragonBall);
        }
        private void insertar_Click(object sender, EventArgs e)
        {
            string nombre = textBoxNombre.Text;
            string raza = textBoxRaza.Text;
            int nivelPoder = (int)numericUpDownNivelPoder.Value;
            DateTime fechaCreacion = dateTimePickerfechacreacion.Value;
            string historia = textBoxhistoria.Text;

            int respuesta = personaje.CrearPersonaje(nombre, raza, nivelPoder, fechaCreacion, historia);
            if (respuesta > 0)
            {
                MessageBox.Show("Personaje creado correctamente");
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Error al crear el personaje");
            }
        }
        private void buttonInsertar_Click(object sender, EventArgs e)
        {
            string nombre = textBoxNombre.Text;
            string raza = textBoxRaza.Text;
            int nivelPoder = (int)numericUpDownNivelPoder.Value;
            DateTime fechaCreacion = dateTimePickerfechacreacion.Value;
            string historia = textBoxhistoria.Text;

            int respuesta = personaje.CrearPersonaje(nombre, raza, nivelPoder, fechaCreacion, historia);
            if (respuesta > 0)
            {
                MessageBox.Show("Personaje creado correctamente");
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Error al crear el personaje");
            }
        }


        private void buscarPorId()
        {
            int idPersonajeABuscar = int.Parse(textBoxid.Text);

            DataTable personajeEncontrado = personaje.BuscarPersonajePorId(idPersonajeABuscar);

            if (personajeEncontrado.Rows.Count > 0)
            {
                // El personaje fue encontrado
                string nombre = personajeEncontrado.Rows[0]["nombre"].ToString();
                string raza = personajeEncontrado.Rows[0]["raza"].ToString();
                int nivelPoder = int.Parse(personajeEncontrado.Rows[0]["nivel_poder"].ToString());
                DateTime fechaCreacion = DateTime.Parse(personajeEncontrado.Rows[0]["fecha_creacion"].ToString());
                string historia = personajeEncontrado.Rows[0]["historia"].ToString();

                textBoxNombre.Text = nombre;
                textBoxRaza.Text = raza;
                comboBoxraza.Text = raza;
                numericUpDownNivelPoder.Value = nivelPoder;
                dateTimePickerfechacreacion.Value = fechaCreacion;
                textBoxhistoria.Text = historia;
            }
            else
            {
                // El personaje no fue encontrado
                Console.WriteLine("No se encontró el personaje con ID: " + idPersonajeABuscar);
            }
        }


        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            buscarPorId();
        }

        private void textBoxID_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxid.Text))
            {
                MessageBox.Show("Por favor, ingresa un valor en el campo de texto.");
                textBoxid.Focus(); // Devolver el foco al TextBox
            }
            else
            {
                buscarPorId();
            }
        }


        private void Form1_Load_1(object sender, EventArgs e)
        {
            comboBoxraza.Items.AddRange(razasDragonBall);
        }

        private void buttonTestCon_Click_1(object sender, EventArgs e)
        {
            if (personaje.ProbarConexion())
            {
                MessageBox.Show("Conexi�n exitosa");
            }
            else
            {
                MessageBox.Show("Error en la conexi�n");
            }
        }

        private void dataGridViewPersonajes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonCargaData_Click_1(object sender, EventArgs e)
        {
            dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
        }

        private void buttonBuscar_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxid.Text))
            {
                MessageBox.Show("Por favor, ingresa un valor en el campo de texto.");
                textBoxid.Focus(); // Devolver el foco al TextBox
            }
            else
            {
                buscarPorId();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }




        private void buttonEliminar_Click(object sender, EventArgs e)
        {
            // Verificar si el campo textBoxid no está vacío
            if (string.IsNullOrEmpty(textBoxid.Text))
            {
                MessageBox.Show("Por favor, ingresa un ID válido.");
                return;
            }

            // Intentar convertir el texto del campo textBoxid a un entero
            if (int.TryParse(textBoxid.Text, out int id))
            {
                // Llamar al método para eliminar el personaje
                personaje.EliminarPersonaje(id);
                MessageBox.Show("Personaje eliminado con éxito.");
                // Actualizar la lista de personajes en el DataGridView
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un ID válido.");
            }
        }
        private void buttonActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxid.Text))
            {
                MessageBox.Show("Por favor, ingresa un ID válido.");
                return;
            }

            if (int.TryParse(textBoxid.Text, out int id))
            {
                string nombre = textBoxNombre.Text;
                string raza = textBoxRaza.Text;
                int nivelPoder = (int)numericUpDownNivelPoder.Value;
                DateTime fechaCreacion = dateTimePickerfechacreacion.Value;
                string historia = textBoxhistoria.Text;

                personaje.ActualizarPersonaje(id, nombre, raza, nivelPoder, fechaCreacion, historia);
                MessageBox.Show("Personaje actualizado con éxito.");
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un ID válido.");
            }
        }

        private void buttonCargar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxid.Text))
            {
                MessageBox.Show("Por favor, ingresa un ID válido.");
                return;
            }

            if (int.TryParse(textBoxid.Text, out int id))
            {
                string nombre = textBoxNombre.Text;
                string raza = textBoxRaza.Text;
                int nivelPoder = (int)numericUpDownNivelPoder.Value;
                DateTime fechaCreacion = dateTimePickerfechacreacion.Value;
                string historia = textBoxhistoria.Text;

                personaje.ActualizarPersonaje(id, nombre, raza, nivelPoder, fechaCreacion, historia);
                MessageBox.Show("Personaje actualizado con éxito.");
                dataGridViewPersonajes.DataSource = personaje.LeerPersonajes();
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un ID válido.");
            }

        }
    }
}
