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
        int com = 1; //creamos una variable para diferenciar si el texto es un comentario o no
        int id = 0; //creamos una variable para diferenciar si estamos agregando el nombre de una variable
        public string Descomponer(string palabra) //iniciamos el metodo descomponer xd 
        {
            com = 1; //reseteamos la variable para comentarios
            id = 0; //reseteamos la variable para id de variables 
            // Ruta del archivo de texto
            //El directorio de esta ruta se escuentra en la carpeta bin/Debug dentro del proyecto --Arauz
            string filePath2 = @"PalabrasReservadas.txt";
            string filePath3 = @"Simbolos.txt";
            // Crear un objeto StreamReader para leer el archivo
            StreamReader reader = new StreamReader(filePath2);
            StreamReader readerSimbolos = new StreamReader(filePath3);

            // Crear un diccionario para almacenar las claves y valores
            Dictionary<string, string> dict = new Dictionary<string, string>();
            Dictionary<string, string> dictSimbolos = new Dictionary<string, string>();

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

            string lineSimbolo;
            while ((lineSimbolo = readerSimbolos.ReadLine()) != null)
            {
                // Separar la línea en clave y valor utilizando un separador (por ejemplo, ":")
                string[] parts = lineSimbolo.Split(':');

                // Agregar la clave y valor al diccionario segun la separación anterior
                if (parts.Length == 2)
                {
                    dictSimbolos.Add(parts[0], parts[1]);
                }
            }
            
            // Cerrar el objeto StreamReader para evitar el cuello de botella xd 
            reader.Close();
            readerSimbolos.Close();
            
            string caracterRerpresentado = ""; //variable que me guardara el token o valor de la clave que se va a ir a buscar al diccionario
            int val = 0; //para comprobar si el texto ingresado es un numero
            float decimall; //para comprobar si el texto ingresado es un decimal
            int validacion = 0;

            foreach (KeyValuePair<string, string> item in dict) //recorre el diccionario con las palabras que se obtuvieron del archivo txt
            {
                if (item.Key == palabra) //si palabra está en el diccionario entra al if 
                {
                    caracterRerpresentado = item.Value; //guarda en esta string lo que se obtuvo del diccionario
                }
                else if (int.TryParse(palabra, out val)) //si no, si el valor se puede guardar en una variable entera es un numero
                {
                    caracterRerpresentado = "Es un número";
                    validacion = 1;
                }
                else if (float.TryParse(palabra, out decimall)) //si no, si el valor se puede guardar en una variable tipo float es un decimal
                {
                    caracterRerpresentado = "Es un número decimal";
                    validacion = 1;
                }
                else if (palabra.StartsWith("'") && palabra.EndsWith("'")) //todas las palabras que esten entre comillas simples, es el valor de una variable tipo cadena
                {
                    caracterRerpresentado = "Es un valor de variable string";
                    validacion = 1;
                }
                else if (palabra.StartsWith("_") && palabra.EndsWith("")) //si empieza con guion bajo, el texto representa el nombre de una variable
                {
                    caracterRerpresentado = "Es un Nombre de Variable";
                    id = 1; //cambia el valor de la variable id, para ingresar el id de la variable en la tabla
                    validacion = 1;
                }
                else if (palabra.StartsWith("//") && palabra.EndsWith("//")) //si el texto está entre diagonales es un comentario
                {
                    com = 0; //cambia el valor de la variable com, para no guardar los datos en la tabla

                }

            }

            foreach (KeyValuePair<string, string> itemSimbolo in dictSimbolos) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
            {
                if (itemSimbolo.Key == palabra)
                {
                    caracterRerpresentado = itemSimbolo.Value; //ya se sabe
                }
            }
            return caracterRerpresentado; //retorna el significado del texto que se analizó
        }
        public void Analisis()
        {
            string codigo = Pizarra.Text.ToLower(); // variable que identifica al cuadro blanco

            char[] jump = { '\n' }; // salto de linea

            char[] delimitador = { ' ' }; //espacio

            string[] lineas = codigo.Split(jump); // se pasa a una array por linea con el delimitador del enter
            int idv = 1; //variable para el id de las variables

            for (int i = 0; i < lineas.Length; i++) //lee por lineas el codigo
            {
                string[] words = lineas[i].Split(delimitador); //guarda en otra array las palabras con el delimitador del espacio 

                for (int z = 0; z < words.Length; z++) //se recorre cada plabra 
                {
                    words[z] = words[z].Replace("\n", ""); //remplaza un salto de linea de la palabra por un salto vacio 

                    if (words[z] != "") //si la palabra es diferente a vacio entra al fin
                    {
                        string lexema = Descomponer(words[z]); // se guarda en una string el valor del siguiente metodo
                        if (com == 1) // si la variable com = 1 el texto no es un comentario
                        {
                            if (id == 1) // si la variable id = 1 el texto que trae es el nombre de una variable
                            {
                                DataGridViewRow fila = (DataGridViewRow)dgvtabladatos.Rows[0].Clone(); //crea una lista 
                                fila.Cells[0].Value = lexema; //guarda el texto del metodo descomponer en la columna componente lexico
                                fila.Cells[1].Value = words[z]; //guarda la palabra que estamos analizando en la columna palabra ingresada
                                fila.Cells[2].Value = idv; //guarda el id del nombre de las variables
                                fila.Cells[3].Value = z + 1; //guarda el numero de columna en la que esta la palabra que estamos analiznando
                                fila.Cells[4].Value = i + 1; //guarda el numero de fila
                                dgvtabladatos.Rows.Add(fila); //añade la lista a la data griv view
                                idv++; //suma 1 a la variable de id para variables xd
                            }
                          
                            else //si no es el nombre de una variable, ejecuta todo menos el id variable
                            {
                                DataGridViewRow fila = (DataGridViewRow)dgvtabladatos.Rows[0].Clone();
                                fila.Cells[0].Value = lexema;
                                fila.Cells[1].Value = words[z];
                                fila.Cells[3].Value = z + 1;
                                fila.Cells[4].Value = i + 1;
                                dgvtabladatos.Rows.Add(fila);
                            }
                        }
                    }
                }
            }
        }

        public void borrar()
        {
            this.dgvtabladatos.Rows.Clear(); //se limpia la tabla xd
        }

        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            SignosNoValidos();//funcion de signos no validos
            validacionvariable(); //funcion de validacion de variable 
            personalizado();// funcion del color de la variable 
            enumeracion();//funcion de la enumeracion 
        }

        private void Form1_Load(System.Object sender, EventArgs e)
        {

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            borrar(); //funcion que limpia la tabla
            Analisis(); //llamamos la funcion que analiza el codigo de la pizarra
            personalizado(); //funcion que agrega los colores 
         //   salvartexto();
        }

        // Establece el color de la palabra "ejemplo" en el control RichTextBox1
        public void personalizado()
        {
            if (sbtnTema.Checked == false) // si la interfaz de usuario es modo claro se agregan esos colores a las palabras reservadas
            {
                // Busca la palabra "comenzar" en el control RichTextBox1
                string[] palabras = { "comenzar", "fin" }; // se guardan las palabras que se van a colorear en el array
                foreach (string palabra in palabras) // se recorre el array palabras y se guarda en el string palabra por cada palabra
                {
                    int startIndex = 0; // se crea la variable tipo int que asigna la posicion inicial donde va a empezar a recorrer
                    while (startIndex < Pizarra.TextLength) // recorrer desde el inicio hasta a la ultima palabra que esta escrita en el richTextBox
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None); // si encuentra la palabra se va a guardar en el wordStartIndex y entrar a la condicion if
                        if (wordstartIndex != -1) // valida si la palabra la encontro va a entrar en el if
                        {
                            Pizarra.SelectionStart = wordstartIndex; // seleccionar desde el primer caracter que tiene la palabra
                            Pizarra.SelectionLength = palabra.Length; // seleccionar hasta donde termina la palabra
                            Pizarra.SelectionColor = Color.DeepPink; // hace el cambio de color de la palabra
                            startIndex += wordstartIndex + palabra.Length; // va a seguir en la posicion en la que se quedo para seguir recorriendo
                            Pizarra.SelectionStart = Pizarra.Text.Length; // deselecciona la palabra
                            Pizarra.SelectionColor = Color.Black; // cambio el color de la palabra al color normal
                        }
                        else
                        {
                            break; // si no encontro la palabra reservada se termina la condicion
                        }
                    }
                }
                // los siguientes hace lo mismo solo que con diferentes palabras
                string[] palabras1 = { "entero", "decimal", "cadena" };
                foreach (string palabra in palabras1)
                {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.DarkBlue;
                            startIndex += wordstartIndex + palabra.Length;
                            Pizarra.SelectionStart = Pizarra.Text.Length;
                            Pizarra.SelectionColor = Color.Black;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                string[] palabras3 = { "si", "entonces", "para" };
                foreach (string palabra in palabras3)
                {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.DarkOliveGreen;
                            startIndex += wordstartIndex + palabra.Length;
                            Pizarra.SelectionStart = Pizarra.Text.Length;
                            Pizarra.SelectionColor = Color.Black;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                string[] palabras5 = { "imprimir", "imprimirln", "leer", "leerln" };
                foreach (string palabra in palabras5)
                {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.IndianRed;
                            startIndex += wordstartIndex + palabra.Length;
                            Pizarra.SelectionStart = Pizarra.Text.Length;
                            Pizarra.SelectionColor = Color.Black;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                string[] palabras4 = { "[", "]" };
                foreach (string palabra in palabras4)
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
            else // si la interfaz es modo oscuro se agregan esos colores a las palabras reservadas
            {
                string[] palabras = { "comenzar", "fin" };
                foreach (string palabra in palabras)
                {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.LightPink;
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

                string[] palabras1 = { "entero", "decimal", "cadena" };
                foreach (string palabra in palabras1)
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

                string[] palabras3 = { "si", "entonces", "para" };
                foreach (string palabra in palabras3)
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
                            Pizarra.SelectionColor = Color.White;
                        }
                        else
                        {
                            break;
                        }
                    }
                }

                string[] palabras5 = { "imprimir", "imprimirln", "leer", "leerln" };
                foreach (string palabra in palabras5)
                {
                    int startIndex = 0;
                    while (startIndex < Pizarra.TextLength)
                    {
                        int wordstartIndex = Pizarra.Find(palabra, startIndex, RichTextBoxFinds.None);
                        if (wordstartIndex != -1)
                        {
                            Pizarra.SelectionStart = wordstartIndex;
                            Pizarra.SelectionLength = palabra.Length;
                            Pizarra.SelectionColor = Color.LightGray;
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

                string[] palabras4 = { "//", "//" };
                foreach (string palabra in palabras4)
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
            
        }

        //Guardar texto ingresado en la pizzara, pense que si se iba a usar pero creo que no -- Arauz
        public void salvartexto()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Codigo|*.cpp";
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

        private void btnBorrar_Click(object sender, EventArgs e) //limpia todo xd
        {
            borrar(); // Se llama la funcion para limpiar toda la interfaz
            Pizarra.Clear();
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
        
        private void validacionvariable()
        {
            string letras = "abcdefghijklmnopqrstuvwxyz"; //crea un string de nombre letras y guarda todo el abecedario
            for (int i = 0; i < letras.Length; i++) //recorre el string
            {
                if (Pizarra.Text.Contains("1" + letras[i])) // si encuentra un numero antes de una letra envia un mensage de error
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("2" + letras[i])) // se hace lo mismo con los demas
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("3" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("4" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("5" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("6" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("7" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("8" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("9" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("0" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains(")" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("(" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("{" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("}" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("[" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("]" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }

                else if (Pizarra.Text.Contains("+" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }

                else if (Pizarra.Text.Contains("*" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains(">" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("<" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
                else if (Pizarra.Text.Contains("=" + letras[i]))
                {
                    MessageBox.Show("Error de Inicializacion de Variable.");
                    break;
                }
            }
        }

        
        private void enumeracion()
        {
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

            // Establece el texto del control Label llamado lblnumero para mostrar los números de línea.
            lblnumero.Text = cadenanumerolinea;
        }

        private void sbtnTema_CheckedChanged(object sender, EventArgs e)
        {
            if (sbtnTema.Checked == false) // si el boton = falso cambia la interfaz a modo claro
            {
                Image imagen = Image.FromFile(@"luna.png");
                gboxCodigo.ForeColor = Color.Black;
                gboxTabla.ForeColor = Color.Black;
                this.BackColor = Color.White;
                Pizarra.BackColor = Color.WhiteSmoke;
                Pizarra.ForeColor = Color.Black;
                lblnumero.ForeColor = Color.Black;
                dgvtabladatos.BackgroundColor = Color.WhiteSmoke;
                dgvtabladatos.ForeColor = Color.Black;
                btnProcesar.BackColor = Color.WhiteSmoke;
                btnProcesar.ForeColor = Color.Black;
                btnBorrar.BackColor = Color.WhiteSmoke;
                btnBorrar.ForeColor = Color.Black;
                PBIcono.Image = imagen;

            }
            else // si el boton = true cambia la interfaz a modo oscuro
            {
                Image imagen = Image.FromFile(@"dom.png");
                gboxCodigo.ForeColor = Color.White;
                gboxTabla.ForeColor = Color.White;
                this.BackColor = Color.FromArgb(50, 50, 50);
                Pizarra.BackColor = Color.FromArgb(80, 80, 80);
                Pizarra.ForeColor = Color.White;
                lblnumero.ForeColor = Color.White;
                dgvtabladatos.BackgroundColor = Color.FromArgb(80, 80, 80);
                dgvtabladatos.ForeColor = Color.White;
                btnProcesar.BackColor = Color.FromArgb(80, 80, 80);
                btnProcesar.ForeColor = Color.White;
                btnBorrar.BackColor = Color.FromArgb(80, 80, 80);
                btnBorrar.ForeColor = Color.White;
                PBIcono.Image = imagen;
            }
        }

    }
}