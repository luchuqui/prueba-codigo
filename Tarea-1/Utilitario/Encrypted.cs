using System.Text;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Tarea_1.Utilitario
{
    public class Encrypted
    {
        /*
        Cifrar contenido del archivo solo sirve para una linea de texto
        1. solicitar el archivo (txt).
        2. Si existe debemos abrir y leer linea por linea.
        3. Encriptar bajo AES con la clave ingresada
        4. Guardar el archivo con otro nombre
        */
        public void EncryptContentFileText(string fileName, string keyAES, string fileEncrypt)
        {
            //string.IsNullOrEmpty la variable está con información
            int i = 0;//i es una variable de validación de campo
            if (string.IsNullOrEmpty(fileName))//extension txt
            {
                i++;//incremento
                Console.WriteLine("Ingrese un nombre de archivo");
            }
            else if(fileName.EndsWith(".txt"))
            {
                i++;//incremento
                Console.WriteLine("Debe ser un archivo de texto");
            }
            if (string.IsNullOrEmpty(keyAES))
            {
                i = i + 1;// incremento de la variable
                Console.WriteLine("Ingrese una llaves AES");
            }
            else if (!(keyAES.Length == 16))
            {
                i++;
                Console.WriteLine("Llave debe ser de 16 caracteres");
            }
            
            if (string.IsNullOrEmpty(fileEncrypt))
            {
                i++;
                 Console.WriteLine("Ingrese el nombre del archivo encriptado");
            }
            else if(fileEncrypt.EndsWith(".txt"))
            {
                i++;
                Console.WriteLine("el archivo destino debe ser texto (txt)");
            }
            if (i != 0)
            {
                //hubo un error de validacion
                return;//retorno para no se ejecuten las siguientes lineas
            }
            /**Leer el archivo**/
            try
            {
                string line = string.Empty;
                using (StreamReader sr = new StreamReader(fileName))
                {
                    line = sr.ReadLine();//leemos la primera linea
                    while (line != null)
                    {
                        //line información en texto claro
                       try
                        {
                            using (FileStream fileStream = new FileStream(fileEncrypt, FileMode.Append))
                            {
                                using (Aes aes = Aes.Create())
                                {
                                    byte[] key = System.Text.Encoding.ASCII.GetBytes(keyAES);
                                    aes.Key = key;

                                    byte[] iv = aes.IV;
                                    fileStream.Write(iv, 0, iv.Length);

                                    using (CryptoStream cryptoStream = new(
                                        fileStream,
                                        aes.CreateEncryptor(),
                                        CryptoStreamMode.Write))
                                    {
                                        using (StreamWriter encryptWriter = new(cryptoStream))
                                        {
                                            encryptWriter.WriteLine(line);
                                        }
                                    }
                                }
                            }

                            Console.WriteLine("The file was encrypted.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"The encryption failed. {ex}");
                        }
                        line = sr.ReadLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The encryption failed. {ex}");
            }
        }

        public void DesEncryptContentFileText(string fileEncrypt, string keyAES, string fileClear)
        {
             //string.IsNullOrEmpty la variable está con información
            int i = 0;//i es una variable de validación de campo
            if (string.IsNullOrEmpty(fileEncrypt))
            {
                i++;//incremento
                Console.WriteLine("Ingrese un nombre de archivo encryptado");
            }
            else if(fileEncrypt.EndsWith(".txt"))
            {
                i++;//incremento
                Console.WriteLine("Archivo debe ser de texto");
            }
            if (string.IsNullOrEmpty(keyAES))
            {
                i = i + 1;// incremento de la variable
                Console.WriteLine("Ingrese una llaves");
            }
            else if (!(keyAES.Length == 16 ))
            {
                i++;
                Console.WriteLine("Llave debe ser de 16caracteres");
            }
            
            if (string.IsNullOrEmpty(fileClear))
            {
                i++;
                 Console.WriteLine("Ingrese el nombre del archivo des encriptado");
            }else if(fileClear.EndsWith(".txt"))
            {
                i++;
                 Console.WriteLine("Archivo de salida debe ser de texto (txt)");
            }
            if (i != 0)
            {
                //hubo un error de validacion
                return;//retorno para no se ejecuten las siguientes lineas
            }

            //desencriptar s

            try
            {
                try
            {
                using (FileStream fileStream = new(fileEncrypt, FileMode.OpenOrCreate))
                {
                    using (Aes aes = Aes.Create())
                    {
                        byte[] key = System.Text.Encoding.ASCII.GetBytes(keyAES);
                        aes.Key = key;

                        byte[] iv = new byte[16];
                        fileStream.Read(iv, 0, iv.Length);

                        int sizeCipherData = (int)fileStream.Length - iv.Length;
                        byte[] cipherText = new byte[sizeCipherData];
                        fileStream.Read(cipherText, 0, sizeCipherData);

                        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                        {
                            using (CryptoStream cryptoStream = new(
                                msDecrypt,
                                aes.CreateDecryptor(key, iv),
                                CryptoStreamMode.Read))
                            {
                                using (StreamReader encryptReader = new(cryptoStream))
                                {
                                    var clearText = encryptReader.ReadToEnd();
                                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileClear, true))
                                    {
                                        file.WriteLine(clearText);
                                    }
                                }
                            }
                        }
                    }
                }

                Console.WriteLine("The file was decrypted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The decryption failed. {ex}");
            }

                Console.WriteLine("The file was decrypted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"The decryption failed. {ex}");
            }

        }
    }
}
