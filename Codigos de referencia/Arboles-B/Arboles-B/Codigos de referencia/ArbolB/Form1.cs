namespace ArbolB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ArbolB arbolb = new ArbolB();
      
        Graphics g;
        int datb = 0;

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                //con esta variable se encuentra la ubicacion de documentos del sistema
                //y se le adiciona el nombre de una carpeta llamada Arbol B
                string carpeta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/Arbol B/";

                if (!Directory.Exists(carpeta))//se verifica si este no directorio existe
                {
                    Directory.CreateDirectory(carpeta);//si no existe se crea el directorio
                }
                else if (Directory.Exists(carpeta))//se verifica si el directorio existe osea si la carpeta existe 
                {
                    Directory.Delete(carpeta, true);//se elimina la carpeta, juntos con todos los archivos que contenga
                                                    //para evitar la acumulacion de archivos basura
                    Directory.CreateDirectory(carpeta);//Se crea de nuevo la carpeta pero esta vez vacia 
                }
                txtMostrar.Text = "";
                arbolb.Insert(Int32.Parse(txtInsertar.Text));
               // txtMostrar.Text = arbolb.Mostrar();
                txtInsertar.Text = "";
                
            }
            catch (Exception)
            {
                MessageBox.Show("El formato del dato es incorrecto \n ingrese numeros enteros", "ERROR DE INSERCION", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtInsertar.Text = "";
            }
            
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                txtMostrar.Text = "";
                if (arbolb.Buscar(Int32.Parse(txtBuscar.Text)) == true)
                    MessageBox.Show("La clave " + txtBuscar.Text + " esta presente en el arbol", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("La clave no se encuntra", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMostrar.Text = arbolb.Mostrar();
                txtBuscar.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("El formato del dato es incorrecto \n ingrese numeros enteros", "ERROR AL BUSCAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtBuscar.Text = "";
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                txtMostrar.Text = "";
                arbolb.Borrar(Int32.Parse(txtEliminar.Text));
                txtMostrar.Text = arbolb.Mostrar();
                txtEliminar.Text = "";
            }
            catch (Exception)
            {
                MessageBox.Show("El formato del dato es incorrecto \n ingrese numeros enteros", "ERROR AL ELIMINAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEliminar.Text = "";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.Clear(this.BackColor);
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g = e.Graphics;
            arbolb.DibujarArbol(g, this.Font,arbolb,Brushes.White, Brushes.Black, Pens.White, datb, Brushes.Black);
           datb = 0;
        }
    }
}