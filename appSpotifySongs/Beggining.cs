using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace appSpotifySongs
{
    public partial class Beggining : Form
    {
        // Propiedad estática para acceder a la instancia del formulario de inicio de sesión
        public static Beggining Instancia { get; private set; }

        public Beggining()
        {
            InitializeComponent();
            Instancia = this; // Asigna la instancia actual a la propiedad estática
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Consulta SQL para verificar las credenciales del usuario
            string query = @"
                SELECT CASE 
                    WHEN EXISTS (
                                 SELECT 1
                                 FROM Users
                                 WHERE NAME = @NombreUsuario
                                 AND CONVERT(VARCHAR(50), DECRYPTBYPASSPHRASE('040329', PASSWORD_ENCRIPTADO)) = @ContraseñaIngresada
                             ) THEN 1
                    ELSE 0
                END AS CredencialesCorrectas";

            // Ejecutar la consulta SQL y obtener el resultado como un objeto
            object result = Connection.ExecuteScalar(query,
                new SqlParameter("@NombreUsuario", txtName.Text),
                new SqlParameter("@ContraseñaIngresada", txtPassword.Text)
            );

            // Verificar si el resultado no es nulo y es convertible a un entero
            if (result != null && int.TryParse(result.ToString(), out int credencialesCorrectas))
            {
                // Verificar si las credenciales son correctas
                if (credencialesCorrectas == 1)
                {
                    // Las credenciales son correctas, puedes continuar con el resto del proceso
                    MessageBox.Show("Datos de inicio de sesión correctos. Acceso permitido.");
                    Form1 newUserInstance = new Form1();
                    newUserInstance.Visible = true;
                    this.Visible = false;
                    txtName.Clear();
                    txtPassword.Clear();
                }
                else
                {
                    // Las credenciales son incorrectas
                    MessageBox.Show("Nombre de usuario o contraseña incorrectos. Por favor, inténtalo de nuevo.");
                }
            }
            else
            {
                // No se pudo obtener un resultado válido de la consulta
                MessageBox.Show("Error al verificar las credenciales. Por favor, inténtalo de nuevo.");
            }
        }
    } 
}
