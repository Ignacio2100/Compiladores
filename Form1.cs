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
using static System.Net.WebRequestMethods;

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


        int idv = 1; //variable para el id de las variables

        public void Analisis()
        {
            string codigo = Pizarra.Text.ToLower(); // variable que identifica al cuadro blanco

            char[] jump = { '\n' }; // salto de linea

            char[] delimitador = { ' ' }; //espacio

            string[] lineas = codigo.Split(jump); // se pasa a una array por linea con el delimitador del enter


            for (int i = 0; i < lineas.Length; i++) //lee por lineas el codigo
            {
                string[] words = lineas[i].Split(delimitador); //guarda en otra array las palabras con el delimitador del espacio 

                for (int z = 0; z < words.Length; z++) //se recorre cada plabra 
                {
                    words[z] = words[z].Replace("\n", ""); //remplaza un salto de linea de la palabra por un salto vacio 

                    if (words[z] != "") //si la palabra es diferente a vacio entra al if
                    {
                        string lexema = Descomponer(words[z]); // se guarda en una string el valor del siguiente metodo

                        if (com == 1) // si la variable com = 1 el texto no es un comentario
                        {
                            DataGridViewRow fila = (DataGridViewRow)dgvtabladatos.Rows[0].Clone(); //crea una lista 
                            fila.Cells[0].Value = lexema; //guarda el texto del metodo descomponer en la columna componente lexico
                            fila.Cells[1].Value = words[z]; //guarda la palabra que estamos analizando en la columna palabra ingresada
                            if (id == 1) // si la variable id = 1 el texto que trae es el nombre de una variable
                            {
                                fila.Cells[2].Value = idv ;
                                idv++;
                            }
                            else
                            {
                                fila.Cells[2].Value = " ";
                            }
                            fila.Cells[3].Value = z + 1; //guarda el numero de columna en la que esta la palabra que estamos analiznando
                            fila.Cells[4].Value = i + 1; //guarda el numero de fila
                            dgvtabladatos.Rows.Add(fila); //añade la lista a la data griv view                                
                        }
                    }
                }
            }

            varrepetidas(); //manda a llamar metodo para verificar variables repetidos
            Reglas();
        }

        private void varrepetidas() //metodo para ver si hay varibales que se usan varias veces 
        {
            int numfilas = dgvtabladatos.RowCount - 1; //cuenta el numeor de filas que en el datagrivview

            for (int i = 0; i < numfilas; i++)
            {
                string datocell = dgvtabladatos.Rows[i].Cells[1].Value.ToString(); //guarda el contenido de la celda 2, fila i en esta variable

                    for (int j = 0; j < numfilas; j++)
                    {
                        string datocelll = dgvtabladatos.Rows[j].Cells[1].Value.ToString(); //guarda el contenido de la celda 2, fila j en esta variable

                        if (datocell == datocelll) //compara variables para cambiar id
                        {
                            dgvtabladatos.Rows[j].Cells[2].Value = dgvtabladatos.Rows[i].Cells[2].Value.ToString(); //sustituye el valor del valor id en la fila j por el dato de la fila i
                            idv--;
                        
                            if (datocelll.StartsWith("_"))
                            {
                                 
                                if (j != i && i<j)
                                {
                                string datocellx = dgvtabladatos.Rows[j].Cells[0].Value.ToString(); //guarda el contenido de la celda 2, fila i en esta variable

                                    if (datocellx == "Es un Nombre de Variable")
                                    {
                                        string datocelly = dgvtabladatos.Rows[j - 1].Cells[0].Value.ToString();
                                        switch (datocelly)
                                        {
                                            case "Variable de tipo string":
                                                DataGridViewRow fila = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista para imprimir el error en la tabla 
                                                fila.Cells[0].Value = "Error: No se puede repetir nombre de variables";
                                                fila.Cells[1].Value = dgvtabladatos.Rows[j - 1].Cells[4].Value.ToString();
                                                dgvErrores.Rows.Add(fila);
                                                break;
                                            case "Variable de tipo int":
                                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista  para imprimir el error en la tabla 
                                            fila1.Cells[0].Value = "Error: No se puede repetir nombre de variables";
                                                fila1.Cells[1].Value = dgvtabladatos.Rows[j - 1].Cells[4].Value.ToString();
                                                dgvErrores.Rows.Add(fila1);
                                                break;
                                            case "Variable de tipo decimal":
                                                DataGridViewRow fila2 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista  para imprimir el error en la tabla 
                                            fila2.Cells[0].Value = "Error: No se puede repetir nombre de variables";
                                                fila2.Cells[1].Value = dgvtabladatos.Rows[j - 1].Cells[4].Value.ToString();
                                                dgvErrores.Rows.Add(fila2);
                                                break;
                                        }
                                    }

                                }
                            }
                        }

                    }
                
            }
            

        }
        private void Reglas ()
        {
            string filePath4 = @"Reglas.txt";
            StreamReader readerReglas = new StreamReader(filePath4);
            Dictionary<string, string> reglas = new Dictionary<string, string>(); //creamos un diccionario con las reglas que tenemos en el yxy reglas

            string linreglas;
            while ((linreglas = readerReglas.ReadLine()) != null)
            {
                // Separar la línea en clave y valor utilizando un separador (por ejemplo, ":")
                string[] parts = linreglas.Split(':');

                // Agregar la clave y valor al diccionario segun la separación anterior
                if (parts.Length == 2)
                {
                    reglas.Add(parts[0], parts[1]);
                }
            }

            int filas = dgvtabladatos.RowCount - 1; //contamos el numero de filas que tiene el data griv view de los datos
            string cadena = "";
            string entero = "";
            string decima = "";
            string leer = "";
            string imprimir = "";
            string erro = "0";
            string inicio = dgvtabladatos.Rows[0].Cells[1].Value.ToString(); //insertamos el valor de la fila 0 
            string fin = dgvtabladatos.Rows[filas-1].Cells[1].Value.ToString(); //insertamos el valor de la ultima fila que haya en el data griv view
          
            if (inicio != "comenzar") //comparamos y verificamos que en la fila 1 de la pizarra, este la inicializacion del sistema
            {
                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista e imprimimos el error de falta de la palabra comenzar
                fila1.Cells[0].Value = "Error: Se Esperaba inicializacion de sistema 'comenzar' ";
                fila1.Cells[1].Value = 1;
                dgvErrores.Rows.Add(fila1);
            }
            if (fin != "fin") //comparamos y verificamos que en la ultima fila de la pizarra se encuentre el valor fin, que da fin al programa
            {
                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista para imprimir el error que falta la palabra fin, al final del codigo
                fila1.Cells[0].Value = "Error: Se Esperaba finalizacion de sistema 'fin' ";
                fila1.Cells[1].Value = " ";
                dgvErrores.Rows.Add(fila1);
            }


            for (int i = 1; i < filas - 1; i++)
            {
                string celda = dgvtabladatos.Rows[i].Cells[0].Value.ToString();

                if (celda == "Variable de tipo string")
                {
                    cadena += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";")
                        {
                            i++;
                            foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                            {
                                if (rulle.Key == cadena)
                                {
                                    erro = rulle.Value;
                                }
                            }
                            if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: Sintactico";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                cadena = "";

                            }


                        }
                        else
                        {
                            celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                            if (celda == "signo de asignacion")
                            {
                                i++;
                                cadena += "<" + celda + ">";
                                celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                                if (celda == "Es un valor de variable string")
                                {
                                    i++;
                                    cadena += "<" + celda + ">";
                                    celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                                    if (celda == ";")
                                    {
                                        i++;
                                        foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                        {
                                            if (rulle.Key == cadena)
                                            {
                                                erro = rulle.Value;
                                            }
                                        }

                                        if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                        {
                                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                            fila1.Cells[0].Value = "Error: Sintactico";
                                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            dgvErrores.Rows.Add(fila1);
                                        }

                                        else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                        {
                                            erro = "0";
                                            cadena = "";
                                        }

                                    }
                                    else
                                    {
                                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                        fila1.Cells[0].Value = "Error: falto ; ";
                                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        dgvErrores.Rows.Add(fila1);
                                        erro = "0";
                                        cadena = "";
                                    }
                                }
                                else
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: valor no valido";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                    i++;
                                    i++;
                                    erro = "0";
                                    cadena = "";
                                }
                            }
                        }
                    }
                    else
                    {
                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                        fila1.Cells[0].Value = "Error: falto nombrar la variable";
                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        dgvErrores.Rows.Add(fila1);
                        i++;
                        i++;
                        i++;
                        erro = "0";
                        cadena = "";
                    }

                }
                else if (celda == "Variable de tipo int")
                {
                    entero += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable")
                    {
                        i++;
                        entero += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";" || celda == ",")
                        {
                            i++;
                            foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                            {
                                if (rulle.Key == entero)
                                {
                                    erro = rulle.Value;
                                }
                            }
                            if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: Sintactico";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                entero = "";

                            }


                        }
                        else
                        {
                            celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                            if (celda == "signo de asignacion")
                            {
                                i++;
                                entero += "<" + celda + ">";
                                celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                                if (celda == "Es un número")
                                {
                                    i++;
                                    entero += "<" + celda + ">";
                                    celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                                    if (celda == ";" || celda == ",")
                                    {
                                        i++;
                                        foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                        {
                                            if (rulle.Key == entero)
                                            {
                                                erro = rulle.Value;
                                            }
                                        }

                                        if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                        {
                                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                            fila1.Cells[0].Value = "Error: Sintactico";
                                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            dgvErrores.Rows.Add(fila1);
                                        }

                                        else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                        {
                                            erro = "0";
                                            entero = "";
                                        }




                                    }
                                    else
                                    {
                                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                        fila1.Cells[0].Value = "Error: falto ; ";
                                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        dgvErrores.Rows.Add(fila1);
                                        erro = "0";
                                        entero = "";
                                    }
                                }
                            }
                            else
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: valor no valido";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                                i++;
                                i++;
                                erro = "0";
                                entero = "";
                            }
                        }
                    }
                    else
                    {
                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                        fila1.Cells[0].Value = "Error: falto nombrar la variable";
                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        dgvErrores.Rows.Add(fila1);
                        i++;
                        i++;
                        i++;
                        erro = "0";
                        entero = "";
                    }
                }
                else if (celda == "Variable de tipo decimal")
                {
                    decima += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable")
                    {
                        i++;
                        decima += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";")
                        {
                            i++;
                            foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                            {
                                if (rulle.Key == decima)
                                {
                                    erro = rulle.Value;
                                }
                            }
                            if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: Sintactico";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                decima = "";

                            }


                        }
                        else
                        {
                            celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                            if (celda == "signo de asignacion")
                            {
                                i++;
                                decima += "<" + celda + ">";
                                celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                                if (celda == "Es un número decimal")
                                {
                                    i++;
                                    decima += "<" + celda + ">";
                                    celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                                    if (celda == ";")
                                    {
                                        i++;
                                        foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                        {
                                            if (rulle.Key == decima)
                                            {
                                                erro = rulle.Value;
                                            }
                                        }

                                        if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                        {
                                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                            fila1.Cells[0].Value = "Error: Sintactico";
                                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            dgvErrores.Rows.Add(fila1);
                                        }

                                        else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                        {
                                            erro = "0";
                                            decima = "";
                                        }




                                    }
                                    else
                                    {
                                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                        fila1.Cells[0].Value = "Error: falto ; ";
                                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        dgvErrores.Rows.Add(fila1);
                                        erro = "0";
                                        decima = "";
                                    }
                                }
                                else
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: valor no valido";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                    i++;
                                    i++;
                                    erro = "0";
                                    decima = "";

                                }
                            }
                        }
                    }
                    else
                    {
                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                        fila1.Cells[0].Value = "Error: falto nombrar la variable";
                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        dgvErrores.Rows.Add(fila1);
                        i++;
                        i++;
                        i++;
                    }

                }
                else if (celda == "Condicion If")
                {
                    Validarif();
                }
                else if (celda == "Es un Nombre de Variable")
                {
                    cadena += "<" + celda + ">";

                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "signo de asignacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un valor de variable string" || celda == "Es un número" || celda == "Es un número decimal")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ",")
                            {
                                i++;
                                foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                {
                                    if (rulle.Key == cadena)
                                    {
                                        erro = rulle.Value;
                                    }
                                }

                                if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: Sintactico";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: falto ; ";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: valor no valido";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "signo mayor que")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ",")
                            {
                                i++;
                                foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                {
                                    if (rulle.Key == cadena)
                                    {
                                        erro = rulle.Value;
                                    }
                                }

                                if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: Sintactico";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: falto ; ";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: valor no valido";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "signo menor que")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ",")
                            {
                                i++;
                                foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                {
                                    if (rulle.Key == cadena)
                                    {
                                        erro = rulle.Value;
                                    }
                                }

                                if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: Sintactico";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: falto ; ";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: valor no valido";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "mayor igual")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ",")
                            {
                                i++;
                                foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                {
                                    if (rulle.Key == cadena)
                                    {
                                        erro = rulle.Value;
                                    }
                                }

                                if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: Sintactico";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: falto ; ";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: valor no valido";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "menor igual")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ",")
                            {
                                i++;
                                foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                {
                                    if (rulle.Key == cadena)
                                    {
                                        erro = rulle.Value;
                                    }
                                }

                                if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: Sintactico";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: falto ; ";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: valor no valido";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "incremento")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";" || celda == ">>")
                        {
                            i++;
                            foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                            {
                                if (rulle.Key == cadena)
                                {
                                    erro = rulle.Value;
                                }
                            }

                            if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: Sintactico";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                cadena = "";
                            }

                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: falto ; ";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "signo de comparacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un valor de variable string" || celda == "Es un número")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ")")
                            {
                                i++;
                                foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                {
                                    if (rulle.Key == cadena)
                                    {
                                        erro = rulle.Value;
                                    }
                                }

                                if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                {
                                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                    fila1.Cells[0].Value = "Error: Sintactico";
                                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    dgvErrores.Rows.Add(fila1);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: falto ; ";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: valor no valido";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                }
                else if (celda == "cout")
                {
                    leer += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable" || celda == "Es un número" || celda == "Es un número decimal" || celda == "Es un valor de variable string")
                    {
                        i++;
                        leer += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";")
                        {
                            i++;
                            foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                            {
                                if (rulle.Key == leer)
                                {
                                    erro = rulle.Value;
                                }
                            }

                            if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: Sintactico";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                leer = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error:  falta ; ";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            erro = "0";

                        }
                    }
                    else
                    {
                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                        fila1.Cells[0].Value = "Error: No se puede leer eso";
                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        dgvErrores.Rows.Add(fila1);
                    }
                }
                else if (celda == "cin")
                {
                    imprimir += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable")
                    {
                        i++;
                        imprimir += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";")
                        {
                            i++;
                            foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                            {
                                if (rulle.Key == imprimir)
                                {
                                    erro = rulle.Value;
                                }
                            }

                            if (erro == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                            {
                                DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                                fila1.Cells[0].Value = "Error: Sintactico";
                                fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                dgvErrores.Rows.Add(fila1);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                leer = "";
                            }
                        }
                        else
                        {
                            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                            fila1.Cells[0].Value = "Error: falto ; ";
                            fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            dgvErrores.Rows.Add(fila1);
                            erro = "0";

                        }
                    }
                    else
                    {
                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                        fila1.Cells[0].Value = "Error: No se puede imprimir eso";
                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        dgvErrores.Rows.Add(fila1);
                    }
                }
                else if (celda == "Ciclo For")
                {
                    Validarfor();
                }
            
            }


        }

        public void Validarif()
        {
            string filePath4 = @"Reglas.txt";
            StreamReader readerReglas = new StreamReader(filePath4);
            Dictionary<string, string> reglas = new Dictionary<string, string>(); //creamos un diccionario con las reglas que tenemos en el yxy reglas

            string linreglas;
            while ((linreglas = readerReglas.ReadLine()) != null)
            {
                // Separar la línea en clave y valor utilizando un separador (por ejemplo, ":")
                string[] parts = linreglas.Split(':');

                // Agregar la clave y valor al diccionario segun la separación anterior
                if (parts.Length == 2)
                {
                    reglas.Add(parts[0], parts[1]);
                }
            }

            int filas = dgvtabladatos.RowCount - 1;
            string varlorc = "";
            string error = "0";

            for (int i = 0; i < filas -1; i++)
            {
                string datocell = dgvtabladatos.Rows[i].Cells[1].Value.ToString(); //guarda el contenido de la celda 2, fila i en esta variable
                string datocelñl = dgvtabladatos.Rows[i+1].Cells[1].Value.ToString(); 

                if (datocell == "si" || datocell == "entonces" || datocell == "(" || datocell == ")" || datocell == "[" || datocell == "]")
                {
                    varlorc += "<" + dgvtabladatos.Rows[i].Cells[0].Value.ToString() + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                }
                else if (datocell == "finsi")
                {
                    foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                    {
                        if (rulle.Key == varlorc)
                        {
                            error = rulle.Value;
                        }
                        
                    }
                    if (error == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                    {
                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                        fila1.Cells[0].Value = "Error: 'si' mal estructurado";
                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        dgvErrores.Rows.Add(fila1);
                    }
                    else
                    {
                        error = "0";
                        varlorc = "";
                    }

                }
            }


        }

        public void Validarfor()
        {
            string filePath4 = @"Reglas.txt";
            StreamReader readerReglas = new StreamReader(filePath4);
            Dictionary<string, string> reglas = new Dictionary<string, string>(); //creamos un diccionario con las reglas que tenemos en el yxy reglas

            string linreglas;
            while ((linreglas = readerReglas.ReadLine()) != null)
            {
                // Separar la línea en clave y valor utilizando un separador (por ejemplo, ":")
                string[] parts = linreglas.Split(':');

                // Agregar la clave y valor al diccionario segun la separación anterior
                if (parts.Length == 2)
                {
                    reglas.Add(parts[0], parts[1]);
                }
            }

            int filas = dgvtabladatos.RowCount - 1;
            string varlorc = "";
            string error = "0";
            string datocell = "";

            for (int i = 0; i < filas - 1; i++)
            {
                 datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString(); //guarda el contenido de la celda 2, fila i en esta variable

                if (datocell == "Ciclo For")
                {
                    i++;
                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                    if(datocell == "inicio for")
                    {
                        i++;
                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                        if (datocell == "Variable de tipo int")
                        {
                            i++;
                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                            datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                            if (datocell == "Es un Nombre de Variable")
                            {
                                i++;
                                varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                if (datocell == "signo de asignacion")
                                {
                                    i++;
                                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                    if (datocell == "Es un número")
                                    {
                                        i++;
                                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                        if (datocell == "separacion")
                                        {
                                            i++;
                                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                            datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                            if (datocell == "Es un Nombre de Variable")
                                            {
                                                i++;
                                                varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                if(datocell == "signo mayor que"||datocell== "signo menor que"||datocell== "mayor igual"||datocell== "menor igual"||datocell== "signo de comparacion")
                                                {
                                                    i++;
                                                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                    if (datocell == "Es un número")
                                                    {
                                                        i++;
                                                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                        if (datocell == "separacion")
                                                        {
                                                            i++;
                                                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                            datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                            if (datocell == "Es un Nombre de Variable")
                                                            {
                                                                i++;
                                                                varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                                if (datocell == "incremento")
                                                                {
                                                                    i++;
                                                                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                                    if(datocell == "fin for")
                                                                    {
                                                                        i++;
                                                                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                                        if( datocell == "LLave inicio bloque")
                                                                        {
                                                                            i++;
                                                                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                            datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                else if(datocell== "Llave cierre bloque")
                {
                    i++;
                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                }
                else if (datocell == "fin del for")
                {
                    foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                    {
                        if (rulle.Key == varlorc)
                        {
                            error = rulle.Value;
                        }

                    }
                    if (error == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                    {
                        DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                        fila1.Cells[0].Value = "Error: 'para' mal estructurado";
                        fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        dgvErrores.Rows.Add(fila1);
                    }
                    else
                    {
                        error = "0";
                        datocell = "";
                    }

                }
            }

        }

        public void borrar()
        {
            this.dgvtabladatos.Rows.Clear(); //se limpia la tabla xd
            this.dgvErrores.Rows.Clear();
            idv = 1;
        }

        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            SignosNoValidos();//funcion de signos no validos
            validacionvariable(); //funcion de validacion de variable 
            //personalizado();// funcion del color de la variable 
            enumeracion();//funcion de la enumeracion 
        }

        private void Form1_Load(System.Object sender, EventArgs e)
        {

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            idv = 1;
            borrar(); //funcion que limpia la tabla
            Analisis(); //llamamos la funcion que analiza el codigo de la pizarra
            //personalizado(); //funcion que agrega los colores 
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