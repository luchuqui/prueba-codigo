// See https://aka.ms/new-console-template for more information
using Tarea_1.Utilitario;
namespace Tarea_1
{
    //Console.WriteLine("Hello, World!");
    //Inicializando el objeto
    public class Program
    {
        public static void Main(string[] args)
        {
            Encrypted encrypted = new Encrypted();
            Console.WriteLine("==================================================");
            Console.WriteLine("MAESTRIA DE CYBER SEGURIDAD");
            Console.WriteLine("==================================================");
            Console.WriteLine("¿Qué desea hacer?");
            Console.WriteLine("1. Encryptar");
            Console.WriteLine("2. Desencryptar");
            var c = Console.ReadKey();
            string opt = Console.ReadLine();
            if (string.IsNullOrEmpty(opt))
            {
                Console.WriteLine("Opción invalida");
            }
            else if (opt.Equals("1"))
            {
                Console.WriteLine("Ingrese el nombre del archivo origen");
                string fileClear = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del archivo destino");
                string fileEncrypt = Console.ReadLine();
                //Console.WriteLine("Ingrese llave de 16 caracteres");
                //string key = Console.ReadLine();
                string key = "0123456789ABCDEF";
                encrypted.EncryptContentFileText(fileClear
                , key
                , fileEncrypt);
            }
            else if (opt.Equals("2"))
            {
                Console.WriteLine("Ingrese el nombre del archivo origen");
                string fileEncrypt = Console.ReadLine();
                Console.WriteLine("Ingrese el nombre del archivo destino");
                string fileClear = Console.ReadLine();
                Console.WriteLine("Ingrese llave de 16 caracteres");
                string key = Console.ReadLine();
                encrypted.DesEncryptContentFileText(fileEncrypt
              , key
              , fileClear);
            }
        }
    }
}
/**/

