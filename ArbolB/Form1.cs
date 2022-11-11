namespace ArbolB
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        ArbolB arbolb = new ArbolB();
        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                txtMostrar.Text = "";
                arbolb.Insert(Int32.Parse(txtInsertar.Text));
                txtMostrar.Text = arbolb.Mostrar();
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
    }
}