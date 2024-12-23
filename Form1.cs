﻿using System;
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
using System.ComponentModel.Design;

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

            int filas = dgvtabladatos.RowCount - 1;
            for (int a = 0; a <= filas-1; a++)
            {

                if (dgvtabladatos.Rows[a].Cells[0].Value == "")
                {
                    string k = dgvtabladatos.Rows[a+1].Cells[4].Value.ToString();
                    ManejoErrores("E023", k);
                }
            }

            VarCreada();
            varrepetidas(); //manda a llamar metodo para verificar variables repetidos
            Reglas();

        }

        private void varrepetidas() //metodo para ver si hay varibales que se usan varias veces 
        {
            int numfilas = dgvtabladatos.RowCount - 1; //cuenta el numeor de filas que en el datagrivview

            for (int i = 1; i < numfilas; i++)
            {
                string datocell = dgvtabladatos.Rows[i].Cells[1].Value.ToString(); //guarda el contenido de la celda 2, fila i en esta variable
                string dato = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                for (int j = i; j < numfilas; j++)
                    {
                        string datocelll = dgvtabladatos.Rows[j].Cells[1].Value.ToString(); //guarda el contenido de la celda 2, fila j en esta variable
                    if (dato.EndsWith("ca") || dato.EndsWith("en") || dato.EndsWith("de"))
                    {
                        if (datocell == datocelll) //compara variables para cambiar id
                        {
                            dgvtabladatos.Rows[j].Cells[2].Value = dgvtabladatos.Rows[i].Cells[2].Value.ToString(); //sustituye el valor del valor id en la fila j por el dato de la fila i
                            dgvtabladatos.Rows[j].Cells[0].Value = dgvtabladatos.Rows[i].Cells[0].Value.ToString();

                            if (datocelll.StartsWith("_") && datocell.StartsWith("_"))
                            {

                                if (j != i && i < j)
                                {
                                    string datocellx = dgvtabladatos.Rows[j].Cells[0].Value.ToString(); //guarda el contenido de la celda 2, fila i en esta variable

                                    if (datocellx.StartsWith("Es un Nombre de Variable"))
                                    {
                                        string datocelly = dgvtabladatos.Rows[j - 1].Cells[0].Value.ToString();
                                        switch (datocelly)
                                        {
                                            case "Variable de tipo string":

                                                string f = dgvtabladatos.Rows[j - 1].Cells[4].Value.ToString();
                                                ManejoErrores("E001", f);
                                                break;
                                            case "Variable de tipo int":
                                                string ff = dgvtabladatos.Rows[j - 1].Cells[4].Value.ToString();
                                                ManejoErrores("E001", ff);
                                                break;
                                            case "Variable de tipo decimal":
                                                string fg = dgvtabladatos.Rows[j - 1].Cells[4].Value.ToString();
                                                ManejoErrores("E001", fg);
                                                break;
                                        }
                                    }

                                }

                            }
                        }
                    }

                }
                
            }
            int numf = dgvErrores.Rows.Count - 1;
            for (int l = 0; l < numf - 1; l++)
            {
                string a = dgvErrores.Rows[l].Cells[1].Value.ToString() + dgvErrores.Rows[l].Cells[2].Value.ToString();
                for (int r = 0; r < numf - 1; r++)
                {
                    string aa = dgvErrores.Rows[r].Cells[1].Value.ToString() + dgvErrores.Rows[r].Cells[2].Value.ToString();
                    if (r != l && l < r)
                    {
                        if (a == aa)
                        {
                            dgvErrores.Rows.RemoveAt(l);
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
                ManejoErrores("E002", "1");
            }
            if (fin != "fin") //comparamos y verificamos que en la ultima fila de la pizarra se encuentre el valor fin, que da fin al programa
            {
                ManejoErrores("E003", "");
            }


            for (int i = 1; i < filas - 1; i++)
            {
                string celda = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                cadena = "";
                entero = "";
                decima = "";
                leer = "";
                imprimir = "";
                if (celda == "Variable de tipo string")
                {
                    cadena += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable ca")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";" || celda =="&")
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
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E004", f);
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
                                if (celda == "Es un valor de variable string" || celda== "Es un Nombre de Variable ca")
                                {
                                    i++;
                                    cadena += "<" + celda + ">";
                                    celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                                    if (celda == ";" || celda == "&")
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
                                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            ManejoErrores("E004", f);
                                        }

                                        else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                        {
                                            erro = "0";
                                            cadena = "";
                                        }

                                    }
                                    else
                                    {
                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        ManejoErrores("E005", f);
                                        erro = "0";
                                        cadena = "";
                                    }
                                }
                                else
                                {
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E006", f);
                                    i++;
                                    i++;
                                    erro = "0";
                                    cadena = "";
                                }
                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                    }
                    else
                    {
                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        ManejoErrores("E007", f);
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
                    if (celda == "Es un Nombre de Variable en")
                    {
                        i++;
                        entero += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";" || celda == "," || celda == ")" || celda== "&")
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
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E004", f);
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
                                if (celda == "Es un número" || celda == "Es un Nombre de Variable en")
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
                                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            ManejoErrores("E004", f);
                                        }

                                        else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                        {
                                            erro = "0";
                                            entero = "";
                                        }
                                    }
                                    else if (celda == "+" || celda == "-" || celda == "/" || celda == "*")
                                    {
                                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                                        entero += "<" + celda + ">";
                                        i++;
                                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                                        if (celda == "Es un número" || celda == "Es un Nombre de Variable en")
                                        {
                                            i++;
                                            entero += "<" + celda + ">";
                                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                                            if (celda == ";" || celda == "," || celda == "&")
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
                                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                    ManejoErrores("E004", f);
                                                }

                                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                                {
                                                    erro = "0";
                                                    entero = "";
                                                }
                                            }
                                            else
                                            {
                                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                ManejoErrores("E005", f);
                                                erro = "0";
                                                entero = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        ManejoErrores("E005", f);
                                        erro = "0";
                                        entero = "";
                                    }
                                }
                                else
                                {
                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        ManejoErrores("E006", f);
                                        i++;
                                        i++;
                                        erro = "0";
                                        entero = "";
                                }
                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }

                        }
                    }
                    else
                    {
                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        ManejoErrores("E007", f);
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
                    if (celda == "Es un Nombre de Variable de")
                    {
                        i++;
                        decima += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        if (celda == ";" || celda == "&")
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
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E004", f);
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
                                    if (celda == ";" || celda == "&")
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
                                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            ManejoErrores("E004", f);
                                        }

                                        else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                        {
                                            erro = "0";
                                            decima = "";
                                        }




                                    }
                                    else
                                    {
                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        ManejoErrores("E005", f);
                                        erro = "0";
                                        decima = "";
                                    }
                                }
                                else
                                {
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E006", f);
                                    i++;
                                    i++;
                                    erro = "0";
                                    decima = "";

                                }
                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                    }
                    else
                    {
                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        ManejoErrores("E007", f);
                        i++;
                        i++;
                        i++;
                    }

                }
                else if (celda == "Condicion If")
                {
                    Validarif();
                }
                else if (celda == "Es un Nombre de Variable en")
                {
                    cadena += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "signo de asignacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número" || celda == "Es un Nombre de Variable en")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == "," || celda == ")" || celda == "&")
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
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E004", f);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else if (celda == "+" || celda == "-" || celda == "/" || celda == "*")
                            {
                                celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                                cadena += "<" + celda + ">";
                                i++;
                                celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                                if (celda == "Es un número" || celda == "Es un Nombre de Variable en")
                                {
                                    i++;
                                    cadena += "<" + celda + ">";
                                    celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                                    if (celda == ";" || celda == "," || celda == "&")
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
                                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            ManejoErrores("E004", f);
                                            cadena = "";
                                        }

                                        else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                        {
                                            erro = "0";
                                            cadena = "";
                                        }
                                    }
                                    else
                                    {
                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        ManejoErrores("E005", f);
                                        erro = "0";
                                        cadena = "";
                                    }
                                }
                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E006", f);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "signo mayor que" || celda == "signo menor que" || celda == "mayor igual" || celda == "menor igual")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número" || celda == "Es un Nombre de Variable en")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == "," || celda == ")")
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
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E004", f);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E008", f);
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
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E004", f);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                cadena = "";
                            }

                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E005", f);
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "signo de comparacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número" || celda == "Es un Nombre de Variable en")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ")" || celda == ")")
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
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E004", f);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E009", f);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else
                    {
                        cadena = "";
                    }
                }
                else if (celda == "cout")
                {
                    leer += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable de" || celda == "Es un Nombre de Variable ca" || celda == "Es un Nombre de Variable en" || celda == "Es un número" || celda == "Es un número decimal" || celda == "Es un valor de variable string")
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
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E004", f);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                leer = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E005", f);
                            erro = "0";

                        }
                    }
                    else
                    {
                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        ManejoErrores("E010", f);
                    }
                }
                else if (celda == "cin")
                {
                    imprimir += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "Es un Nombre de Variable de" || celda == "Es un Nombre de Variable ca" || celda == "Es un Nombre de Variable en")
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
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E004", f);
                            }

                            else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                            {
                                erro = "0";
                                imprimir = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E005", f);
                            erro = "0";
                            imprimir = "";

                        }
                    }
                    else
                    {
                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        ManejoErrores("E011", f);
                        imprimir = "";
                    }
                }
                else if (celda == "Ciclo For")
                {
                    Validarfor();
                }
                else if (celda == "Palabra reservada de inicio")
                {
                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                    ManejoErrores("E013", f);

                }
                else if (celda == "Palabra reservada de Final")
                {
                    DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone(); //crea una lista y me muestra un error sintactico del mismo
                    fila1.Cells[0].Value = "Error: Porque hay 2 Fin ???? xd";
                    fila1.Cells[1].Value = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                    dgvErrores.Rows.Add(fila1);
                }
                else if (celda == "Es un Nombre de Variable ca")
                {
                    cadena += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "signo de asignacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un valor de variable string" || celda == "Es un Nombre de Variable ca")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == "," || celda == ")" || celda == "&")
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
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E004", f);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E006", f);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "signo de comparacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un valor de variable string" || celda == "Es un Nombre de Variable ca")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ")" || celda == ")" || celda == "&")
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
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E004", f);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E009", f);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }

                }
                else if (celda == "Es un Nombre de Variable de")
                {
                    cadena += "<" + celda + ">";
                    celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                    if (celda == "signo de asignacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número decimal" || celda == "Es un Nombre de Variable de")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == "," || celda == ")" || celda == "&")
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
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E004", f);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E006", f);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                    else if (celda == "signo de comparacion")
                    {
                        i++;
                        cadena += "<" + celda + ">";
                        celda = dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString();
                        if (celda == "Es un número decimal" || celda == "Es un Nombre de Variable de")
                        {
                            i++;
                            cadena += "<" + celda + ">";
                            celda = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                            if (celda == ";" || celda == ")" || celda == ")" || celda == "&")
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
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E004", f);
                                }

                                else //si, si trae 1 en la respuesta me vuelve a inicializar las variables 
                                {
                                    erro = "0";
                                    cadena = "";
                                }

                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E005", f);
                                erro = "0";
                                cadena = "";
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E009", f);
                            i++;
                            i++;
                            erro = "0";
                            cadena = "";
                        }
                    }
                }
                else if (celda == "Es un Nombre de Variable")
                {
                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                    ManejoErrores("E016", f);
                    
                }
            
            }

            
        }

        public void VarCreada()
        {
            int filas = dgvtabladatos.RowCount - 1;
            string var1 = "";
            string var2 = "";

            for (int i = 1; i < filas -1; i++)
            {
                var1 = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                if (var1.StartsWith("_"))
                {
                    var2 = dgvtabladatos.Rows[i - 1].Cells[1].Value.ToString();
                    switch (var2)
                    {
                        case "cadena":

                            dgvtabladatos.Rows[i].Cells[0].Value = "Es un Nombre de Variable ca";
                            break;
                        case "entero":
                            dgvtabladatos.Rows[i].Cells[0].Value = "Es un Nombre de Variable en";
                            break;
                        case "decimal":
                            dgvtabladatos.Rows[i].Cells[0].Value = "Es un Nombre de Variable de";
                            break;
                        case "&":
                            string hola = dgvtabladatos.Rows[i - 2].Cells[0].Value.ToString();
                            if (hola.EndsWith("ca") || hola.EndsWith("en") || hola.EndsWith("de"))
                            {
                                dgvtabladatos.Rows[i].Cells[0].Value = hola;
                            }
                            break;
                    }
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
                string[] parts = linreglas.Split(':');
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
                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        ManejoErrores("E015", f);
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
                    if (datocell == "inicio for")
                    {
                        i++;
                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                        if (datocell == "Variable de tipo int")
                        {
                            i++;
                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                            datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                            if (datocell == "Es un Nombre de Variable en")
                            {
                                i++;
                                varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                if (datocell == "signo de asignacion")
                                {
                                    i++;
                                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                    if (datocell == "Es un número" || datocell == "Es un Nombre de Variable en")
                                    {
                                        i++;
                                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                        if (datocell == "separacion")
                                        {
                                            i++;
                                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                            datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                            if (datocell == "Es un Nombre de Variable en")
                                            { 
                                                i++;
                                                varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                if (datocell == "signo mayor que" || datocell == "signo menor que" || datocell == "mayor igual" || datocell == "menor igual" || datocell == "signo de comparacion")
                                                {
                                                    i++;
                                                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                    if (datocell == "Es un número" || datocell == "Es un Nombre de Variable en")
                                                    {
                                                        i++;
                                                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                        if (datocell == "separacion")
                                                        {
                                                            i++;
                                                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                            datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                            if (datocell == "Es un Nombre de Variable en")
                                                            {
                                                                i++;
                                                                varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                                if (datocell == "incremento")
                                                                {
                                                                    i++;
                                                                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                    datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                                    if (datocell == "fin for")
                                                                    {
                                                                        i++;
                                                                        varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                        datocell = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                                                                        if (datocell == "LLave inicio bloque")
                                                                        {
                                                                            i++;
                                                                            varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                            for (int o = i; o <= filas-1; o++)
                                                                            {
                                                                                datocell = dgvtabladatos.Rows[o].Cells[0].Value.ToString();

                                                                                if (datocell == "Llave cierre bloque")
                                                                                {
                                                                                    i++;
                                                                                    varlorc += "<" + datocell + ">"; //me lo guarda entre < > y me sigue concatenando el componente lexico de la palabra ingresada                    
                                                                                    
                                                                                    foreach (KeyValuePair<string, string> rulle in reglas) //recore el diccionario de los simbolos con los datos obtenidos del archivo txt
                                                                                    {
                                                                                        if (rulle.Key == varlorc)
                                                                                        {
                                                                                            error = rulle.Value;
                                                                                        }

                                                                                    }
                                                                                    if (error == "0") //si el valor devuelto sigue siendo el valor con el que inicializamos la variable
                                                                                    {
                                                                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                                                        ManejoErrores("E012", f);
                                                                                        
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        error = "0";
                                                                                        varlorc = "";
                                                                                    }
                                                                                }

                                                                            }
                                                                            
                                                                        }
                                                                        else
                                                                        {
                                                                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                                            ManejoErrores("E017", f);
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                                        ManejoErrores("E018", f);
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                                    ManejoErrores("E019", f);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                            ManejoErrores("E020", f);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                        ManejoErrores("E008", f);
                                                    }
                                                }
                                                else
                                                {
                                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                                    ManejoErrores("E009", f);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                            ManejoErrores("E020", f);
                                        }
                                    }
                                    else
                                    {
                                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                        ManejoErrores("E006", f);
                                    }
                                }
                                else
                                {
                                    string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                    ManejoErrores("E021", f);
                                }
                            }
                            else
                            {
                                string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                                ManejoErrores("E007", f);
                            }
                        }
                        else
                        {
                            string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                            ManejoErrores("E022", f);
                        }
                    }
                    else
                    {
                        string f = dgvtabladatos.Rows[i].Cells[4].Value.ToString();
                        ManejoErrores("E017", f);
                    }

                }
            }
            

        }

        public string ManejoErrores(string rulles, string fila)
        {
            string filePath4 = @"Errores.txt";
            StreamReader readerE = new StreamReader(filePath4);
            Dictionary<string, string> Errores = new Dictionary<string, string>();
            string resultado = "";
            string linerrors;
            while ((linerrors = readerE.ReadLine()) != null)
            {
                
                string[] parts = linerrors.Split(':');

                
                if (parts.Length == 2)
                {
                    Errores.Add(parts[0], parts[1]);
                }
            }

            foreach (KeyValuePair<string, string> itemregla in Errores) 
            {
                if (itemregla.Key == rulles)
                {
                    resultado =  itemregla.Value;
                }
            }


            DataGridViewRow fila1 = (DataGridViewRow)dgvErrores.Rows[0].Clone();
            fila1.Cells[0].Value = rulles;
            fila1.Cells[1].Value = resultado;
            fila1.Cells[2].Value = "Fila " + fila;
            dgvErrores.Rows.Add(fila1);

            return resultado;

        }

        public void Traduccion()
        {
            string filePath4 = @"Traduccion.txt";
            StreamReader readerE = new StreamReader(filePath4);
            Dictionary<string, string> Errores = new Dictionary<string, string>();

            string linerrors;

            while ((linerrors = readerE.ReadLine()) != null)
            {

                string[] parts = linerrors.Split(':');


                if (parts.Length == 2)
                {
                    Errores.Add(parts[0], parts[1]);
                }
            }

            int filas = dgvtabladatos.RowCount - 1;
            string palabra = "";
            for (int  i = 0; i < filas;i++)
            {
                string token = dgvtabladatos.Rows[i].Cells[0].Value.ToString();

                if(token.StartsWith("Es un Nombre de Variable "))
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    variable = variable.Replace("_", "");
                    palabra += variable + " ";

                }
                else if(token =="Es un número")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    palabra += variable + " ";
                }
                else if (token =="Es un número decimal")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    palabra += variable + " ";
                }
                else if(token=="Es un valor de variable string")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    variable = variable.Replace("-", " ");

                    variable = variable.Replace("'", "\"");


                    palabra += variable + " ";
                }
                else if(token== "fin de linea")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    palabra += variable + "\r\n";
                }
                else if (token == "Palabra reservada de inicio")
                {
                    palabra += "#include <iostream>\r\n\r\nusing namespace std;\r\n\r\nint main()\r\n{ \r\n";
                }
                else if(token == "Palabra reservada de Final")
                {
                    palabra += "return 0;\r\n}";
                }
                else
                {
                    foreach (KeyValuePair<string, string> itemregla in Errores)
                    {
                        if (itemregla.Key == token)
                        {
                            palabra += itemregla.Value;
                        }
                    }
                }
            }
            txtTraduccion.Text = palabra;
            
        }
        int t = 1;
        public void Traduccion2()
        {
            t = 1;
            string filePath4 = @"Traduccion c#.txt";
            StreamReader readerE = new StreamReader(filePath4);
            Dictionary<string, string> Errores = new Dictionary<string, string>();
            string resultado = "";
            string linerrors;
            while ((linerrors = readerE.ReadLine()) != null)
            {

                string[] parts = linerrors.Split(':');


                if (parts.Length == 2)
                {
                    Errores.Add(parts[0], parts[1]);
                }
            }

            foreach (KeyValuePair<string, string> itemregla in Errores)
            {
                if (itemregla.Key == "")
                {
                    resultado = itemregla.Value;
                }
            }


            int filas = dgvtabladatos.RowCount - 1;
            string palabra = "";
            for (int i = 0; i < filas; i++)
            {
                string token = dgvtabladatos.Rows[i].Cells[0].Value.ToString();
                if (token.StartsWith("Es un Nombre de Variable "))
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    variable = variable.Replace("_", "");
                    palabra += variable + " ";

                }
                else if (token == "Es un número")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    palabra += variable + " ";
                }
                else if (token == "Es un número decimal")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    palabra += variable + " ";
                }
                else if (token == "cout")
                {
                    if(dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString().EndsWith("en"))
                    {
                        string variable = dgvtabladatos.Rows[i+1].Cells[1].Value.ToString();
                        variable = variable.Replace("_", " ");
                        palabra += "Console.WriteLine(" + variable + ".ToStrign())";
                        i++;

                    }
                    else if(dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString().EndsWith("ca"))
                    {
                        string variable = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        variable = variable.Replace("_", " ");
                        palabra += "Console.WriteLine(" + variable + ")";
                        i++;
                    }
                    else if(dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString()== "Es un valor de variable string")
                    {
                        i++;
                        string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                        variable = variable.Replace("-", " ");
                        variable = variable.Replace("'", "\"");
                        palabra += "Console.WriteLine(" + variable + ")";

                    }
                    
                } 
                else if (token == "fin de linea")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    palabra += variable + "\r\n";
                }
                else if (token == "Es un valor de variable string")
                {
                    string variable = dgvtabladatos.Rows[i].Cells[1].Value.ToString();
                    variable = variable.Replace("-", " ");
                    variable = variable.Replace("'", "\"");
                    palabra += variable + " ";
                }
                else if(token== "cin")
                {
                    if(dgvtabladatos.Rows[i+1].Cells[0].Value.ToString().EndsWith("en"))
                    {
                        
                        string ayuda = "string apoyo" + t + " = Console.ReadLine() ;";
                        string variabel = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        i++;
                        variabel = variabel.Replace("_", "");
                        palabra += ayuda + "\r\n" + variabel + " = int.Parse(" + "apoyo"+ t + ")";
                        t++;
                    }
                    else if(dgvtabladatos.Rows[i + 1].Cells[0].Value.ToString().EndsWith("ca"))
                    {
                        string variabel = dgvtabladatos.Rows[i + 1].Cells[1].Value.ToString();
                        i++;
                        variabel = variabel.Replace("_", "");
                        palabra += variabel + " = Console.ReadLine()";
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, string> itemregla in Errores)
                    {
                        if (itemregla.Key == token)
                        {
                            palabra += itemregla.Value;
                        }
                    }
                }
            }
            txtTraduccion.Text = palabra;

        }

        public void borrar()
        {
            this.dgvtabladatos.Rows.Clear(); //se limpia la tabla xd
            this.dgvErrores.Rows.Clear();
            this.txtTraduccion.Clear();
            idv = 1;
        }

        public void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            SignosNoValidos();//funcion de signos no validos
            validacionvariable(); //funcion de validacion de variable 
            enumeracion();//funcion de la enumeracion 
        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            personalizado();
            idv = 1;
            borrar(); //funcion que limpia la tabla
            Analisis(); //llamamos la funcion que analiza el codigo de la pizarra
            
            if (dgvErrores.Rows[0].Cells[0].Value == null)
            {
                MessageBox.Show("Se Analizo con Exito c:");
                Traducir.Enabled = true;
                btntraduccionc.Enabled = true;
            }
            else
            {
                Traducir.Enabled = false;
                btntraduccionc.Enabled = false;
            }
        }

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
        
        private void btnBorrar_Click(object sender, EventArgs e) //limpia todo xd
        {
            borrar(); 
            Pizarra.Clear();
            txtTraduccion.Clear();
            Traducir.Enabled = false;
            btntraduccionc.Enabled = false;
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

        private void Traducir_Click(object sender, EventArgs e)
        {
            txtTraduccion.Enabled= true;
            Traduccion();
        }

        private void btntraduccionc_Click(object sender, EventArgs e)
        {
            txtTraduccion.Enabled = true;
            Traduccion2();
        }
    }
}