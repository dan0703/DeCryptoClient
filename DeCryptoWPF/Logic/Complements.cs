using log4net;
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
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
                Filter = "Archivos de imágenes ( *.png)| *.png"
            };
            openFileDialog.ShowDialog();
            return openFileDialog.FileName;
        }
        public static bool SaveImage(string nickname, string sourceProfilePicturePath)
        {
            bool success = false;

            var profilePicturePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../Images/", nickname + ".png");

            if (!Directory.Exists(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../Images/")))
            {
                Directory.CreateDirectory(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "../../Images/"));
            }

            try
            {
                using (var fileStream = new FileStream(profilePicturePath, FileMode.Create))
                {
                    var bitmapDecoder = BitmapDecoder.Create(new Uri(sourceProfilePicturePath), BitmapCreateOptions.None, BitmapCacheOption.Default);
                    var pngBitmapEncoder = new PngBitmapEncoder();
                    pngBitmapEncoder.Frames.Add(bitmapDecoder.Frames[0]);
                    pngBitmapEncoder.Save(fileStream);  
                }
                success = true;
            }
            catch (UnauthorizedAccessException ex)
            {
                FileAttributes attr = (new FileInfo(profilePicturePath)).Attributes;
                log.Error("UnAuthorizedAccessException: Unable to access file.", ex);

                if ((attr & FileAttributes.ReadOnly) > 0)
                {
                    log.Error("The file is read-only");
                }
                success = false;
            }
            return success;
        }
    }
}
