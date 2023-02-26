using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



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
            string caracterRerpresentado = "";
            switch (palabra) // aqui agregamos cualquier palabra
            {
                case "comenzar":
                    caracterRerpresentado = "Palabra reservada de inicio";
                    break;
                case "cadena":
                    caracterRerpresentado = "Variable de tipo string";
                    break;
                case "entero":
                    caracterRerpresentado = "Variable de tipo int";
                    break;
                case "decimal":
                    caracterRerpresentado = "Variable de tipo decimal";
                    break;
                case ";":
                    caracterRerpresentado = "Signo de cierre de sentencia";
                    break;
                case "=":
                    caracterRerpresentado = "Signo de asignación";
                    break;
                case "==":
                    caracterRerpresentado = "Signo de comparación";
                    break;
                case "+":
                    caracterRerpresentado = "Signo de suma";
                    break;
                case "-":
                    caracterRerpresentado = "Signo de resta";
                    break;
                case ">":
                    caracterRerpresentado = "Signo mayor";
                    break;
                case "<":
                    caracterRerpresentado = "Signo menor";
                    break;
                case "=<":
                    caracterRerpresentado = "Menor igual que";
                    break;
                case "=>":
                    caracterRerpresentado = "Mayor igual que";
                    break;
                case "(":
                    caracterRerpresentado = "Parentesis izquierdo";
                    break;
                case ")":
                    caracterRerpresentado = "Parentesis derecho";
                    break;
                case "{":
                    caracterRerpresentado = "Llave derecho";
                    break;
                case "}":
                    caracterRerpresentado = "Llave izquierdo";
                    break;
                case "[":
                    caracterRerpresentado = "Corchete izquierdo";
                    break;
                case "]":
                    caracterRerpresentado = "Corchete derecho";
                    break;
                case "&":
                    caracterRerpresentado = "Operador AND";
                    break;
                case "||":
                    caracterRerpresentado = "Operador OR";
                    break;
                case "fin":
                    caracterRerpresentado = "Palabra reservada de Final";
                    break;
                case "ifi":
                    caracterRerpresentado = "Condicion If";
                    break;
                case "fore":
                    caracterRerpresentado = "Ciclo For";
                    break;
                default:
                    int val = 0;
                    float decimall ;
                    int validacion = 0;
                        //validamos si es un numero o no
                    if (int.TryParse(palabra, out val))
                    {
                        caracterRerpresentado = "Es un número";
                        validacion = 1;
                    }else
                    //validamos si es un numero decimal o no
                    if (float.TryParse(palabra, out decimall))
                    {
                        caracterRerpresentado = "Es un número decimal";
                        validacion = 1;
                    }
                    //validamos si es una variable o no
                    if (palabra.StartsWith("'") && palabra.EndsWith("'"))
                    {
                        caracterRerpresentado = "Es un nombre de variable";
                        validacion = 1;
                    }
                    //validamos si es identificador o no
                    if (validacion == 0)
                    {
                        caracterRerpresentado = "Nombre de Variable";
                    }
                    break;
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
        private void richTextBox1_TextChanged(object sender, EventArgs e)
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

        }

        // Establece el color de la palabra "ejemplo" en el control RichTextBox1
        public void personalizado()
        {
            // Busca la palabra "comenzar" en el control RichTextBox1
            int uno = Pizarra.Find("comenzar");

            // Si se encuentra la palabra, establece su color
            if (uno != -1)
            {
                Pizarra.Select(uno, "comenzar".Length);
                Pizarra.SelectionColor = Color.Red;
            }

            // Busca la palabra "entero" en el control RichTextBox1
            int dos = Pizarra.Find("entero");

            // Si se encuentra la palabra, establece su color
            if (dos != -1)
            {
                Pizarra.Select(dos, "entero".Length);
                Pizarra.SelectionColor = Color.Red;
            }

            // Busca la palabra "fin" en el control RichTextBox1
            int tres = Pizarra.Find("fin");

            // Si se encuentra la palabra, establece su color
            if (tres != -1)
            {
                Pizarra.Select(tres, "fin".Length);
                Pizarra.SelectionColor = Color.Red;
            }

            // Busca la palabra "cadena" en el control RichTextBox1
            int cuatro = Pizarra.Find("cadena");

            // Si se encuentra la palabra, establece su color
            if (cuatro != -1)
            {
                Pizarra.Select(cuatro, "cadena".Length);
                Pizarra.SelectionColor = Color.Red;
            }

            // Busca la palabra "cadena" en el control RichTextBox1
            int cinco = Pizarra.Find("decimal");

            // Si se encuentra la palabra, establece su color
            if (cinco != -1)
            {
                Pizarra.Select(cinco, "decimal".Length);
                Pizarra.SelectionColor = Color.Red;
            }

            // Busca la palabra "ifi" en el control RichTextBox1
            int seis = Pizarra.Find("ifi");

            // Si se encuentra la palabra, establece su color
            if (seis != -1)
            {
                Pizarra.Select(seis, "ifi".Length);
                Pizarra.SelectionColor = Color.Red;
            }

            // Busca la palabra "fore" en el control RichTextBox1
            int siete = Pizarra.Find("fore");

            // Si se encuentra la palabra, establece su color
            if (siete != -1)
            {
                Pizarra.Select(siete, "fore".Length);
                Pizarra.SelectionColor = Color.Red;
            }

           

        }

        private void lblnumero_Click(object sender, EventArgs e)
        {

        }

       
    }
    }
    
    

