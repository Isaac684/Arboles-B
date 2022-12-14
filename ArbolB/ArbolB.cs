using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArbolB
{
    internal class ArbolB
    {
        private static readonly int M = 5;//orden 
        private static readonly int MAX = M - 1;//máximo de clave en cada página
        //Ceiling Devuelve el valor integral más pequeño que es mayor o igual que el número decimal especificado.
        private static readonly int MIN = (int)Math.Ceiling((double)M / 2) - 1;
        public String arbol, mostrar;
        int x = 394, xhijo, y;//variables para indicar la posicion de cada clave en las paginas

        private Nodo padre;//nodo padre

        public ArbolB()//contructor
        {
            padre = null;//nodo padre inicializa en null
            //y = 30;
        }

        public bool Buscar(int x)//metodo que busca  una clave entera en el árbol
        {
            if (Buscar(x, padre) == null)// de forma recursiva busca la clave en el árbol(aca se utiliza el metodo buscar que tiene dos parametros, es e qu esta bajo)
                return false;// si buscar retorna null significa que no se encontro el nodo
            return true;// si buscar retorna true significa que el nodo se encuentra en el árbol
        }

        private Nodo Buscar(int x, Nodo p)//este metodo es el que recorre el arbol y busca la clave 
        {
            if (p == null)       /*clave x no presente en el árbol*/
                return null;//retorna null

            int n = 0;// n es el primer nivel del arbol que corresponde al nodo padre
            if (BuscarNodo(x, p, ref n) == true) //busca la clave x en un nodo o pagina, se usa el metodo de abajo
                return p;// retorna la pagina o nodo donde se encontro la clave

            return Buscar(x, p.hijo[n]); /* Buscar en Nodo p.hijo[n] */
        }
        //este metodo busca la clave dentro de una página en particular
        private bool BuscarNodo(int x, Nodo p, ref int n)
        {
            if (x < p.clave[1])// verifica si la clave x es menor que la clave 
            {
                n = 0;// las claves en la pagina son cero 
                return false;//retorna falso
            }

            n = p.numclaves;//n=al numero de claves de la pagina
            while ((x < p.clave[n]) && n > 1)//si la clave x es menor  que cualquier clave dentro de la pagina y mayor a uno 
                n--;//se decrementa n(lo que permitira recorrer todas las claves que se encuentre en un nodo o pagina del árbol b


            if (x == p.clave[n])//si la clave coincide 
                return true;//retorna true(se llama en el metodo buscar)
            else
                return false;//sino false
        }
        /*recorre el arbol en orden de forma recursiva*/
        public void Inorder()
        {
            Inorder(padre);
        }
        // este metodo recibe un nodo para recorrer a partir de dicho nodo el arbol
        private void Inorder(Nodo p)
        {
            if (p == null)// si el nodo es null
                return;//regresar

            int i;
            for (i = 0; i < p.numclaves; i++)//for que recorre todas las claves de una página
            {
                Inorder(p.hijo[i]);//se recorren  las páginas hijas recursivamente
                Console.Write(p.clave[i + 1] + "  ");// se muestran en consola las páginas o nodos 
            }
            // Inorder(p.hijo[i]);
        }

        public String Mostrar(ref PictureBox pictureBox, Font font)//metodo  que llama a metodo mostrar esto es para encapsular mas.
        {
            //se mandaron los parametros a mostrar de picturebox y font Para indicarle al codigo
            //donde dibujara los cuadrados, las claves y la fuente que tendra las claves
            arbol = Mostrar(padre, 0, null, ref pictureBox, font,x);
            return arbol;
        }

        private String Mostrar(Nodo p, int espaciosblancos, string archivo, ref PictureBox pictureBox, Font font, int x)// recibe nodo y espacios entre nodos o paginas
        {

            if (p != null)// si el nodo es diferente de nulo
            {
                //Variable para conocer l direccion donde se encuentra la carpeta de documentos
                //Y adicional se le agrega el nombre de una carpeta llamada Arbol B
                //Y en esta carpeta se guardaran los archivos de cada pagina
                //se guarda la direccion de esta carpeta, en la variable string carpeta
                string carpeta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Arbol B/";//
                bool esHijo = false;//variable para identificar cuando un nodo es hijo de la raiz o subhijo

                mostrar = "";

                int i;

                for (i = 1; i <= espaciosblancos; i++)// dejar espacios en blanco
                {
                    mostrar += "|";//muestra espacios en blanco
                    esHijo = true;//si se muestran espacios es porque este nodo es un hijo de raiz
                    if (esHijo)
                        y += 2;//si es una pagina hija aumentara su valor en Y para quedar separada una pagina hija de otra
                }


                if (esHijo == false)//si no es un hijo entonces es la raiz
                {
                    carpeta += "Raiz ";//se adiciona a la direccion de la carpeta un archivo si extension de nombre raiz
                    y = 0;//muestra la raiz al inicio del picture box
                }

                for (i = 1; i <= p.numclaves; i++)// recorre las claves que hay en cada pagina
                {
                    mostrar += p.clave[i] + "| ";//nuestra las claves que hay en una pagina padre
                    carpeta += p.clave[i] + "_";//se adiciona al nombre raiz si es una raiz o no

                    //modificar el dibujar paginas
                    dibujarPaginas(p.clave[i], x, y, ref pictureBox, font);//se manda el valor de la clave y donde se dibujara

                    //este metodo se vuelve recursivo si tiene vlaves hijas y repite el mismo procedimiento
                    x += 15;//se coloca a la derecha de cada clave de una misma pagina por eso se desplaza 15px a la derecha
                    //y += 20;
                    if (archivo != null)//se verifica si ya existe un archivo padre para guardar en el las claves que contiene esta pagina
                    {
                        File.AppendAllText(archivo, p.clave[i].ToString() + "| ");//con esta linea se escribe adicionando al archivo
                                                                                  //las claves de sus hijos ya que se escribe en una hoja padre
                    }
                }
                mostrar += "\r\n";//da un espacio por página

                string direccion = carpeta + ".txt";//despues de haber colocado la clave como nombre del archivo
                                                    //se le coloca la extension que este tendra ".txt"
                StreamWriter file = new StreamWriter(direccion);//se crea el archivo txt
                file.Close();                                   //y a continuacion se cierra con file.close();

                for (i = 0; i <= p.numclaves; i++)//este for recorre las paginas hijas para mostrar las claves
                {
                    y += 10;
                    //if (p.hijo[i] != null && padre.clave[i] > p.hijo[i].clave[i])
                    //{
                    //    xhijo = 600 / 2;
                    //}
                    //if (p.hijo[i] != null && padre.clave[i] < p.hijo[i].clave[i])
                    //{
                    //    xhijo = 600;
                    //}
                    mostrar += Mostrar(p.hijo[i], espaciosblancos + 1, direccion, ref pictureBox, font, xhijo);//muestra las claves de las  páginas hijas
                                                                                                               //si es que el directorio tiene y se manda el nombre
                                                                                                               //del archivo padre donde se encuentran las claves
                                                                                                               //esto para poder escribirlas en ella por medio de la recursividad
                }
                return mostrar;
            }
            return "";
        }

        //Funcion para dibujar las paginas
        public void dibujarPaginas(int clave, int x, int y, ref PictureBox pictureBox, Font font) 
        {
            Graphics g;//definicion de un objeto tipo Graphics
            g = pictureBox.CreateGraphics();//A este objeto Graphics se le asigna el pictureBox
                                            //para decirle donde debe dibujar los objetos
            Pen lapiz = new Pen(Color.Black);//se crea un lapiz de color negro para trazar el borde de la pagina
            g.FillRectangle(Brushes.LightGoldenrodYellow, x, y, 15, 15);//se crea un cuadrado con relleno sin bordes
            g.DrawRectangle(lapiz, x, y, 15, 15);//se dibuja el cuadrado sin relleno solo los bordes negros
            Point p = new Point(x, y);//se crea un punto en coordenadas x,y para indicar donde dibujarlo
            g.DrawString(clave.ToString(), font, Brushes.Black, p);//mandamos los parametros para dibujar la clave en las paginas
        }

        public void Insert(int x)//ingresar una clave al arbol b
        {
            int iclave = 0;//clave inicial en cero
            Nodo iclaveDhijo = null;//clave hijos en null

            bool agregar = Insert(x, padre, ref iclave, ref iclaveDhijo);//metodo recursivo inserta la clave

            if (agregar)  /* Altura aumentada en uno, hay que crear nueva pagina en el arbol */
            {
                Nodo temp = new Nodo(M);// se crea una pagina temporal
                temp.hijo[0] = padre;// se coloca al padre actual como hijo (ya que las paginas rompen para arriba
                padre = temp;//el padre es el nuevo nodo
                //inicializa valores de nueva pagina
                padre.numclaves = 1;// nuevo padre tiene una clave inicialmente
                padre.clave[1] = iclave;//se asigana clave el valor de cero
                padre.hijo[1] = iclaveDhijo;// se asigna a hijo el valor de null
                MessageBox.Show("Clave insertada correctamente", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private bool Insert(int x, Nodo p, ref int iclave, ref Nodo iclaveDhijo)
        {
            if (p == null)  /*Primer caso base: clave no encontrada*/
            {
                iclave = x;
                iclaveDhijo = null;
                return true;
            }

            int n = 0;
            if (BuscarNodo(x, p, ref n) == true)// se busca la pagina indicada
            /*Segundo caso base: clave encontrada*/
            {
                MessageBox.Show("La clave ya esta presente en el árbol", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; /*No es necesario insertar la clave*/
            }

            bool bandera = Insert(x, p.hijo[n], ref iclave, ref iclaveDhijo);//se llama recursivamenre  cada nodo

            if (bandera == true)// si se inserto la pagina 
            {
                if (p.numclaves < MAX)// numero de claves debe ser menor a 4
                {
                    InsertarPorCambio(p, n, iclave, iclaveDhijo);//se inserta la clave en la pagina exitosamente

                    return false; /*Inserción en la página*/
                }
                else
                {   //en caso de ser mayor hay que romper la pagina
                    Separar(p, n, ref iclave, ref iclaveDhijo);
                    return true;  /*Inserción no terminada: clave mediana aún por insertar*/
                }
            }
            return false;
        }

        //permite agregar una clave agregandola a la página(aca se aplica cuando hay cupo en la página)
        //     5
        //134   6789
        //(nodo 1,3,2, nodo 3)
        private void InsertarPorCambio(Nodo p, int n, int iclave, Nodo iclaveDhijo)
        {
            //(i=3; 3>3;
            for (int i = p.numclaves; i > n; i--)//recorre la pagina de derecha a izquierda
            {
                //p.clave[4]=p.clave[3] 
                //
                p.clave[i + 1] = p.clave[i];//mueve las claves a la izquierda
                p.hijo[i + 1] = p.hijo[i];
            }
            //
            p.clave[n + 1] = iclave;//ingresa la nueva clave
            p.hijo[n + 1] = iclaveDhijo;
            p.numclaves++;
        }

        private void Separar(Nodo p, int n, ref int iclave, ref Nodo iclaveDhijo)
        {
            int i, j;
            int ultimaclave;
            Nodo ultimohijo;

            if (n == MAX)// si ya hay 4 claves en la página
            {
                ultimaclave = iclave;  //ultima clave= clave a insertar
                ultimohijo = iclaveDhijo;//ultimo hijo=
            }
            else
            {// si es menor a 4
                ultimaclave = p.clave[MAX];
                ultimohijo = p.hijo[MAX];

                for (i = p.numclaves - 1; i > n; i--)//se recorre la pagina
                {
                    p.clave[i + 1] = p.clave[i];//se reordenan las claves en la pagina
                    p.hijo[i + 1] = p.hijo[i];//se reordenan las paginas hijas
                }

                p.clave[i + 1] = iclave;
                p.hijo[i + 1] = iclaveDhijo;
            }
            //se busca la clave central
            int d = (M + 1) / 2;// 3
            int medianclave = p.clave[d];// se establece la clave mediana
            Nodo nuevoNodo = new Nodo(M);//se crea una nueva pagina o nodo
            nuevoNodo.numclaves = M - d;//se establece el numero de claves que cben en el nodo 5-3=2

            nuevoNodo.hijo[0] = p.hijo[d];//

            for (i = 1, j = d + 1; j <= MAX; i++, j++)
            {
                nuevoNodo.clave[i] = p.clave[j];//se asigana clave a nueva pagina
                nuevoNodo.hijo[i] = p.hijo[j];
            }
            nuevoNodo.clave[i] = ultimaclave;
            nuevoNodo.hijo[i] = ultimohijo;

            p.numclaves = d - 1;

            iclave = medianclave;
            iclaveDhijo = nuevoNodo;
        }

        public void Borrar(int x)
        {
            if (padre == null)
            {
                MessageBox.Show("El arbol esta vacio", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Borrar(x, padre);//borra nodo recursivamente

            if (padre != null && padre.numclaves == 0)
                /*La altura del árbol disminuyó en 1*/
                padre = padre.hijo[0];
        }

        private void Borrar(int x, Nodo p)
        {
            int n = 0;

            if (BuscarNodo(x, p, ref n) == true) /* clave x encontrada en Nodo p */
            {
                if (p.hijo[n] == null)     /* Nodo p es un Nodo hoja */
                {
                    BorrarByShift(p, n);
                    return;
                }
                else                    /* Nodo p es un Nodo sin hoja */
                {
                    Nodo s = p.hijo[n];
                    while (s.hijo[0] != null)
                        s = s.hijo[0];
                    p.clave[n] = s.clave[1];
                    Borrar(s.clave[1], p.hijo[n]);
                }
            }
            else /*clave x no encontrada en Nodo p */
            {
                if (p.hijo[n] == null) /* p es una hoja Nodo */
                {
                    MessageBox.Show("El valor " + x + " no esta en el arbol", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else  /* p es un Nodo que no es hoja */
                    Borrar(x, p.hijo[n]);
            }

            if (p.hijo[n].numclaves < MIN)
                Restaurar(p, n);
        }

        private void BorrarByShift(Nodo p, int n)
        {
            for (int i = n + 1; i <= p.numclaves; i++)
            {
                p.clave[i - 1] = p.clave[i];
                p.hijo[i - 1] = p.hijo[i];
            }
            p.numclaves--;
        }

        // Llamado cuando p.hijo[n] pasa a estar bajo flujo
        private void Restaurar(Nodo p, int n)
        {
            if (n != 0 && p.hijo[n - 1].numclaves > MIN)
                PrestadoIzquierdo(p, n);
            else if (n != p.numclaves && p.hijo[n + 1].numclaves > MIN)
                PrestadoDerecha(p, n);
            else
            {
                if (n != 0) //si hay una hermana izquierda
                    Combinar(p, n);   // *Combinar con hermana izquierda * /
                else
                    Combinar(p, n + 1);  /*Combinar con el hermano derecho*/
            }
        }

        private void PrestadoIzquierdo(Nodo p, int n)
        {
            Nodo bajoflujonodo = p.hijo[n];
            Nodo hermanoizquierdo = p.hijo[n - 1];

            bajoflujonodo.numclaves++;

            /*Desplazar todas las claves e hijosen en underflow Nodo una posición a la derecha*/
            for (int i = bajoflujonodo.numclaves; i > 0; i--)
            {
                bajoflujonodo.clave[i + 1] = bajoflujonodo.clave[i];
                bajoflujonodo.hijo[i + 1] = bajoflujonodo.hijo[i];
            }
            bajoflujonodo.hijo[1] = bajoflujonodo.hijo[0];

            /* Mueve la clave del separador del padre Nodo p a bajoflujonodo */
            bajoflujonodo.clave[1] = p.clave[n];

            /* Mueve la clave más a la derecha de Nodo hermanoizquierdo al padre Nodo p */
            p.clave[n] = hermanoizquierdo.clave[hermanoizquierdo.numclaves];

            /*El hijo más a la derecha de hermanoizquierdo se convierte en el hijo más a la izquierda de bajoflujonodo */
            bajoflujonodo.hijo[0] = hermanoizquierdo.hijo[hermanoizquierdo.numclaves];

            hermanoizquierdo.numclaves--;
        }

        private void PrestadoDerecha(Nodo p, int n)
        {
            Nodo bajoflujonodo = p.hijo[n];
            Nodo hermanoDerecho = p.hijo[n + 1];

            //Mueve la clave del separador del padre Nodo p a bajoflujonodo */
            bajoflujonodo.numclaves++;
            bajoflujonodo.clave[bajoflujonodo.numclaves] = p.clave[n + 1];

            /* El hijo más a la izquierda de hermanoDerecho se convierte en el hijo más a la derecha de underflowNode */
            bajoflujonodo.hijo[bajoflujonodo.numclaves] = hermanoDerecho.hijo[0];


            /*Mueve la clave más a la izquierda de hermanoDerecho al padre Nodo p */
            p.clave[n + 1] = hermanoDerecho.clave[1];
            hermanoDerecho.numclaves--;

            /* Cambia todas las claves e hijos de hermanoDerecho una posición a la izquierda */
            hermanoDerecho.hijo[0] = hermanoDerecho.hijo[1];
            for (int i = 1; i <= hermanoDerecho.numclaves; i++)
            {
                hermanoDerecho.clave[i] = hermanoDerecho.clave[i + 1];
                hermanoDerecho.hijo[i] = hermanoDerecho.hijo[i + 1];
            }
        }

        private void Combinar(Nodo p, int m)
        {
            Nodo NodoA = p.hijo[m - 1];
            Nodo NodoB = p.hijo[m];

            NodoA.numclaves++;

            /* Mueve la clave del separador del padre Nodo p a NodoA */
            NodoA.clave[NodoA.numclaves] = p.clave[m];


            int i;
            for (i = m; i < p.numclaves; i++)
            {
                p.clave[i] = p.clave[i + 1];
                p.hijo[i] = p.hijo[i + 1];
            }
            p.numclaves--;

            /* El hijo más a la izquierda de NodoB se convierte en el hijo más a la derecha de NodoA */
            NodoA.hijo[NodoA.numclaves] = NodoB.hijo[0];

            /* Inserta todas las claves e hijos de NodoB al final de NodoA */
            for (i = 1; i <= NodoB.numclaves; i++)
            {
                NodoA.numclaves++;
                NodoA.clave[NodoA.numclaves] = NodoB.clave[i];
                NodoA.hijo[NodoA.numclaves] = NodoB.hijo[i];
            }
        }

    }
}
