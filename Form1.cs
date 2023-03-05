using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; 



namespace AnalisisLexico
{
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        public string Descomponer(string palabra)
        {
            // Ruta del archivo de texto
            //El directorio de esta ruta se escuentra en la carpeta bin/Debug dentro del proyecto --Arauz
            string filePath2 = @"PalabrasReservadas.txt";
            string hola;
            // Crear un objeto StreamReader para leer el archivo
            //Cambiar a filePath2 por si no jalara el filePath normarl -- Arauz
            StreamReader reader = new StreamReader(filePath2);

            // Crear un diccionario para almacenar las claves y valores
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // Leer el archivo línea por línea
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                // Separar la línea en clave y valor utilizando un separador (por ejemplo, ":")
                string[] parts = line.Split(':');

                // Agregar la clave y valor al diccionario segun la separación anterior
                if (parts.Length == 2)
                {
                    dict.Add(parts[0], parts[1]);
                }
            }

            // Cerrar el objeto StreamReader
            reader.Close();
            //variable que me guardaara el token o valor de la clave que se va a ir a buscar al diccionario
            string caracterRerpresentado = "";
            int val = 0;
            float decimall;
            int validacion = 0;

            foreach (KeyValuePair<string, string> item in dict)
            {


                if (item.Key == palabra)
                {
                    caracterRerpresentado = item.Value;
                }
                else if (int.TryParse(palabra, out val))
                {
                    caracterRerpresentado = "Es un número";
                    validacion = 1;
                }
                else if (float.TryParse(palabra, out decimall))
                    {
                        caracterRerpresentado = "Es un número decimal";
                        validacion = 1;
                    }
                else if (palabra.StartsWith("'") && palabra.EndsWith("'"))
                    {
                        caracterRerpresentado = "Es un valor de variable string";
                        validacion = 1;
                    }
                //else if (validacion == 0)
                //{
                //    caracterRerpresentado = "nombre de variable";
                //}

            }

           
            return caracterRerpresentado;
        }
        public void Analisis()
        {
            string codigo = Pizarra.Text; // variable que identifica al cuadro blanco

            char[] jump = { '\n' }; // salto de linea

            char[] delimitador = { ' ' };

            string[] lineas = codigo.Split(jump); // se pasa a una arrays

            for (int i = 0; i < lineas.Length; i++)
            {
                string[] words = lineas[i].Split(delimitador);

                for (int z = 0; z < words.Length; z++)
                {
                    words[z] = words[z].Replace("\n", "");

                    if (words[z] != "")
                    {
                        DataGridViewRow fila = (DataGridViewRow)dgvtabladatos.Rows[0].Clone();
                        fila.Cells[0].Value = Descomponer(words[z]);
                        fila.Cells[1].Value = words[z];
                        fila.Cells[2].Value = z + 1;
                        fila.Cells[3].Value = i + 1;
                        dgvtabladatos.Rows.Add(fila); 
                    }
                }
            }

        }

       public void borrar()
        {
            this.dgvtabladatos.Rows.Clear(); 
        }
        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            SignosNoValidos();
            //ESTO ES PARA LA NUMERACION DE EDITOR DE PIZARRA

            // Obtiene la primera línea visible del RichTextBox llamado pizarra.
            int Primeralinea = Pizarra.GetLineFromCharIndex(Pizarra.GetCharIndexFromPosition(new Point(0, 0)));

            // Calcula el número total de líneas del RichTextBox llamado pizarra.
            int numerodelineas = Pizarra.Lines.Length;

            // Construye un string que contiene los números de línea.
            string cadenanumerolinea = "";
            for (int i = Primeralinea + 1; i <= numerodelineas; i++)
            {
                cadenanumerolinea += i.ToString() + "\n";

            }

            //Cambiar de color a las palabras reservadas
          
            personalizado();
         
            // Establece el texto del control Label llamado lblnumero para mostrar los números de línea.
            lblnumero.Text = cadenanumerolinea;
        }

        private void Form1_Load(System.Object sender, EventArgs e)
        {

        }

        private void dgvInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            borrar();
            Analisis();
            personalizado();
            
           // salvartexto();

        }

        // Establece el color de la palabra "ejemplo" en el control RichTextBox1
        public void personalizado()

        {
       
            // Busca la palabra "comenzar" en el control RichTextBox1

             string [] palabras = {"comenzar","fin"};
             foreach(string palabra in palabras)
             {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.HotPink;
                            startIndex += wordstartIndex + palabra.Length;
                            Pizarra.SelectionStart = Pizarra.Text.Length;
                            Pizarra.SelectionColor = Color.White;

                        }
                        else
                        {
                            break;
                        }
                    }
             }

             string [] palabras1 = {"entero","decimal","cadena","booleano","verdadero","falso"};
             foreach(string palabra in palabras1)
             {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.SkyBlue;
                            startIndex += wordstartIndex + palabra.Length;
                            Pizarra.SelectionStart = Pizarra.Text.Length;
                            Pizarra.SelectionColor = Color.White;

                        }
                        else
                        {
                            
                            break;
                        }
                    }
             }

             string [] palabras3 = {"si","entonces","sino","mientras","hacer","para"};
             foreach(string palabra in palabras3)
             {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.Orange;
                            startIndex += wordstartIndex + palabra.Length;
                           Pizarra.SelectionStart = Pizarra.Text.Length;
                           Pizarra.SelectionColor = Color.White;

                        }
                        else
                        {
                            break;
                        }
                    }
             }

             
             string [] palabras4 = {"//","/*","*/"};
             foreach(string palabra in palabras4)
             {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.GreenYellow;
                            startIndex += wordstartIndex + palabra.Length;
                            Pizarra.SelectionStart = Pizarra.Text.Length;
                            
                        }
                        else
                        {
                            break;
                        }
                    }
             }
        }


        private void lblnumero_Click(object sender, EventArgs e)
        {

   
        }



        //Guardar texto ingresado en la pizzara, pense que si se iba a usar pero creo que no -- Arauz
        public void salvartexto()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Codigo|*.txt";
            save.Title = "codigo";
            save.FileName = "Codigo 1";
            var resultado = save.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                StreamWriter escribir = new StreamWriter(save.FileName);
                foreach (object line in Pizarra.Lines)
                {
                    escribir.WriteLine(line);
                }
                escribir.Close();
            }

        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            borrar();
            Pizarra.Clear();
        }

        private void btnTema_Click(object sender, EventArgs e)
        {
            //cambia de tema
            if (this.BackColor == Color.FromArgb(57, 48, 83))
            {
                this.BackColor = Color.FromArgb(83, 97, 98);
                Pizarra.BackColor = Color.FromArgb(66, 70, 66);
                Pizarra.ForeColor = Color.FromArgb(238, 238, 238);
                lblnumero.ForeColor = Color.FromArgb(24, 18, 43);
                dgvtabladatos.BackgroundColor = Color.FromArgb(66, 70, 66);
                dgvtabladatos.ForeColor = Color.FromArgb(24, 18, 43);
                btnProcesar.BackColor = Color.FromArgb(66, 70, 66);
                btnProcesar.ForeColor = Color.FromArgb(243, 244, 237);
                btnBorrar.BackColor = Color.FromArgb(66, 70, 66);
                btnBorrar.ForeColor = Color.FromArgb(243, 244, 237);
                btnTema.BackColor = Color.FromArgb(66, 70, 66);
                btnTema.ForeColor = Color.FromArgb(243, 244, 237);
                btnTema.Text = "Tema Oscuro";
            }
            else
            {
                this.BackColor = Color.FromArgb(57, 48, 83);
                Pizarra.BackColor = Color.FromArgb(24, 18, 43);
                Pizarra.ForeColor = Color.FromArgb(238, 238, 238);
                lblnumero.ForeColor = Color.FromArgb(238, 238, 238);
                dgvtabladatos.BackgroundColor = Color.FromArgb(24, 18, 43);
                dgvtabladatos.ForeColor = Color.FromArgb(83, 97, 98);
                btnProcesar.BackColor = Color.FromArgb(24, 18, 43);
                btnProcesar.ForeColor = Color.FromArgb(243, 244, 237);
                btnBorrar.BackColor = Color.FromArgb(24, 18, 43);
                btnBorrar.ForeColor = Color.FromArgb(243, 244, 237);
                btnTema.BackColor = Color.FromArgb(24, 18, 43);
                btnTema.ForeColor = Color.FromArgb(243, 244, 237);
                btnTema.Text = "Tema Claro";
            }
        }
        private void SignosNoValidos()
        {

            // Verificar si el RichTextBox contiene caracteres no válidos
            if (Pizarra.Text.Contains("#"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '#'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("?"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '?'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("$"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '$'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("¡"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '¡'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("¿"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '¿'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("."))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '.'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("^"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '^'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("@"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '@'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("%"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '%'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("!"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '!'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("_"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '_'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("¬"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '¬'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("ª"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo 'ª'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("º"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo 'º'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("Ç"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo 'Ç'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
            else if (Pizarra.Text.Contains("~"))
            {
                // Mostrar mensaje de error con MessageBox
                MessageBox.Show("La Sintaxis no puede contener el símbolo '~'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Establecer el foco en el RichTextBox
                Pizarra.Focus();
            }
        }

    }
    }
    
    

