using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService2.Models;

namespace WcfService2
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        int sumar(int numero1, int numero2);

        [OperationContract]
        int restar(int numero1, int numero2);


        [OperationContract]
        Producto ObtenerProducto();


        [OperationContract]
        string ProbarProductos();


        [OperationContract]
        List<Productos> ListarProductoDB(int numero);

        [OperationContract]
        List<Categorias> ListarCategoriasDB();
    }


   
   
    
}
