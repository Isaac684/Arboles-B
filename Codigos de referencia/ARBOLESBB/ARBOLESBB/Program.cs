using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARBOLESBB
{
    internal class Program
    {


		static void Main(string[] args)
		
        {
			//objeto de la clase arbolmuticamino
			ArbolBMulticamino arbolbnavidenio = new ArbolBMulticamino();
			int clave, opcion;

			while (true)
			{
				Console.WriteLine("1.Buscar");
				Console.WriteLine("2.Insertar");
				Console.WriteLine("3.Borrar");
				Console.WriteLine("4.Mostrar");
				Console.WriteLine("5.Inorder traversal");
				Console.WriteLine("6.Salir");

				Console.Write("Ingrese la opción : ");
				opcion = Convert.ToInt32(Console.ReadLine());

				if (opcion == 6)
					break;

				switch (opcion)
				{
					case 1:
						Console.WriteLine("Ingrese la clave a buscar: ");
						clave = Convert.ToInt32(Console.ReadLine());

						if (arbolbnavidenio.Buscar(clave) == true)
							Console.WriteLine("Clave está presente en el árbol");
						else
							Console.WriteLine("Clave no se encuentra");
						break;
					case 2:
						Console.Write("Ingrese la clave a insertar: ");
						clave = Convert.ToInt32(Console.ReadLine());
						arbolbnavidenio.Insert(clave);
						break;
					case 3:
						Console.WriteLine("Ingrese la clave a borrar : ");
						clave = Convert.ToInt32(Console.ReadLine());
						arbolbnavidenio.Borrar(clave);
						break;
					case 4:
						Console.WriteLine("El ÁRBOL B ES :\n\n");
						arbolbnavidenio.Mostrar();
						Console.WriteLine("\n\n");
						break;
					case 5:
						arbolbnavidenio.Inorder();
						Console.WriteLine("\n\n");
						break;
					default:
						Console.WriteLine("Opción invalida\n");
						break;
				}
			}


		}
    }
}
