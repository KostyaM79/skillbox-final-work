﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace WebApiService
{
    public class ImageManager
    {
        public string SaveImage(string directoryName, IFormFile file)
        {
            string imgFileName = CreateFileName(file.FileName);

            Directory.CreateDirectory("img\\projects-images");                                  // Создаём каталог для изображений, если он не существует.
            using Stream s = System.IO.File.Create($"img\\{directoryName}\\{imgFileName}");     // Открываем поток для сохранения файла.
            file.OpenReadStream().CopyTo(s);                                                    // Записываем в поток файл.
            s.Close();                                                                          // Закрываем поток.

            return imgFileName;
        }

        public string Update(string oldName, Stream stream, string fileName, string directoryName)
        {
            string newFileName = CreateFileName(fileName);
            File.Delete($"img\\{directoryName}\\{oldName}");
            using FileStream fs = File.Create($"img\\{directoryName}\\{newFileName}");
            stream.CopyTo(fs);
            fs.Close();
            stream.Close();
            return newFileName;
        }

        public void DeleteImage()
        {

        }

        public FileStream GetImage(string dir, string fileName)
        {
            return new FileStream($"img\\{dir}\\{fileName}", FileMode.Open);
        }

        private string CreateFileName(string srcFileName)
        {
            Regex regex = new Regex("(\\..+)$");
            string ext = regex.Match(srcFileName).Value;
            string str = $"{srcFileName}{DateTime.Now}";
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            byte[] hash = MD5.Create().ComputeHash(bytes);
            string[] ss = hash.Select(e => $"{e:X2}").ToArray();
            StringBuilder sb = new StringBuilder();
            foreach (string s in ss)
                sb.Append(s);
            sb.Append(ext);
            return sb.ToString();
        }
    }
}