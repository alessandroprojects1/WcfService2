using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data.Entity;
using WcfService2.Models;

namespace WcfService2
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Service1" en el código, en svc y en el archivo de configuración.
    //NOTE: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Service1.svc o Service1.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public int sumar(int numero1, int numero2)
        {

            return numero1 + numero2;
        }

        public int restar(int numero1, int numero2)
        {
            return numero1 - numero2;
        }

        public Producto ObtenerProducto()
        {
            return new Producto
            {
                Id = 1,
                Nombre = "lAPTOP X5",
                Precio = 2500.00m
            };
        }

        public string ProbarProductos()
        {
            using (var db = new TiendaDBEntities())
            {


                //select * from Productos
                var productos = db.Productos.ToList();

                string resultado = "";

                foreach (var item in productos)
                {
                    resultado += item.Nombre + ";";
                }

                return resultado;
            }
        }

        public List<Productos> ListarProductoDB()
        {
            using (var db = new TiendaDBEntities())
            {

                //select * from Productos where id = 1

                var productos = db.Productos.ToList();

                //var productos = db.Productos.OrderByDescending(p => p.Nombre).ToList();

                return productos;

            }
        }

        public List<Categorias> ListarCategoriasDB()
        {
            using (var db = new TiendaDBEntities())
            {
                var categorias = db.Categorias.ToList();

                return categorias;
            }

        }

        public Productos buscarProductoId(int id)
        {
            using( var db = new TiendaDBEntities())
            {
                var producto = db.Productos.FirstOrDefault(p => p.Id == id);

                return producto;

            }
        }

        public void insertarProducto(Productos producto)
        {
            using (TiendaDBEntities bd = new TiendaDBEntities())
            {
                bd.Productos.Add(producto);
                bd.SaveChanges();
            }
        }

        public void ActualizarProducto(Productos producto)
        {
            using (TiendaDBEntities bd = new TiendaDBEntities())
            {

                Productos ProductoDB = bd.Productos.FirstOrDefault(p => p.Id == producto.Id);

                if(ProductoDB != null)
                {
                    ProductoDB.Nombre = producto.Nombre;
                    ProductoDB.Precio = producto.Precio;
                    
                    bd.SaveChanges();
                }

            }

        }

        public string EliminarProducto(int id)
        {

            using (TiendaDBEntities bd = new TiendaDBEntities()) {

                Productos ProductoDB = bd.Productos.FirstOrDefault(p => p.Id == id);

                if (ProductoDB != null)
                {
                    bd.Productos.Remove(ProductoDB);
                    bd.SaveChanges() ;

                    return "Se eliminó el producto solicitado";
                }
                else
                {
                    return "No se encontró el producto con el ID: " + id;
                }

            }

        }
        
        public  List<Productos> ListarProductoSP()
        {
            using (var db = new TiendaDBEntities())
            {
                var productosSP = db.sp_ListarProductos().ToList();

              var productos =   productosSP.Select(p => new Productos
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Precio = p.Precio
                }).ToList();

                return productos;
            }
        }


        public void insertarProductoSP(String nombre, decimal precio)
        {
            using (var db = new TiendaDBEntities())
            {
                db.sp_InsertarProducto(nombre, precio);
            }
            
        }

        public bool EliminarProductosSP(int id)
        {
            using (var db = new TiendaDBEntities())
            {
                int resultado = db.sp_EliminarProducto(id);

                return resultado>0;
               
            }
        }
    }
}
