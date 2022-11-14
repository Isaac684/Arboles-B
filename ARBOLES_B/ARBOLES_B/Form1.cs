using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ARBOLES_B
{
    public partial class Form1 : Form
    {
        int datoingresado = 0;
        public Form1()
        {
            InitializeComponent();
            
        }
        ArbolBMulticamino arbolbnavidenio = new ArbolBMulticamino();

        private void btnInsertar_Click(object sender, EventArgs e)
        {
            //txtMostrar.Text = "";
            arbolbnavidenio.Insert(Int32.Parse(txtInsertar.Text));
            txtMostrar.Text = arbolbnavidenio.Mostrar();
            
            //treeView1.Nodes.Clear();//elimina los nodos del arbol

            datoingresado++;

                
            treeView1.Nodes.Add(arbolbnavidenio.Mostrar());//agrega Nodos al Treeview(Arbol B)



        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (arbolbnavidenio.Buscar(Int32.Parse(txtBuscar.Text)) == true)
                MessageBox.Show("La clave "+txtBuscar.Text+" esta presente en el arbol", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("La clave no se encuntra", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            arbolbnavidenio.Borrar(Int32.Parse(txtEliminar.Text));
        }
    }
}
