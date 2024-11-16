using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text;

namespace WebClient.Services
{
    public class PhrasesService : IPhrasesService
    {
        public string GetPhrase()
        {
            Random rnd = new Random(DateTime.Now.Millisecond);

            string[] arr = new string[] { "Первая случайная фраза.",
                "Вторая случайная фраза.",
                "Третья случайная фраза.",
                "Четвёртая случайная фраза.",
                "Пятая случайная фраза." };

            File.WriteAllText("files\\test.json", JsonSerializer.Serialize(arr));

            string[] phrases = JsonSerializer.Deserialize<string[]>(File.ReadAllText("files\\test.json", Encoding.ASCII));
            return phrases[rnd.Next(0, phrases.Length)];
        }
    }
}
