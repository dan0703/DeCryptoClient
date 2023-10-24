using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DeCryptoWPF.Logic
{
    public class Complements
    {
        public static string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = new StringBuilder();
                for (int bit = 0; bit < (bytes.Length); bit++)
                {
                    hash.Append(bytes[bit].ToString("x2"));
                }
                return hash.ToString();
            }
        }

        public static string UploadImage()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Selecciona la imagen de perfil que desees cargar",
                Filter = "Archivos de imágenes ( *.jpg)| *.jpg"
            };
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }
        public static bool SaveImage(string username, Image profilePicture)
        {

            var profilePicturePath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "../../Images/" + username + ".jpg";

            if (!Directory.Exists(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "../../Images/"))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "../../Images/");
            }
            //File.Delete(profilePicturePath);
            try
            {
                using (var fileStream = new FileStream(profilePicturePath, FileMode.Create))
                {
                    var jpegBitmapEncoder = new JpegBitmapEncoder();
                    jpegBitmapEncoder.Frames.Add(BitmapFrame.Create((BitmapSource)profilePicture.Source));
                    jpegBitmapEncoder.Save(fileStream);
                    return true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                FileAttributes attr = (new FileInfo(profilePicturePath)).Attributes;
                Console.Write("UnAuthorizedAccessException: Unable to access file. ");
                if ((attr & FileAttributes.ReadOnly) > 0)
                {
                    Console.Write("The file is read-only.");

                }
                return false;
            }
        }
    }
}
