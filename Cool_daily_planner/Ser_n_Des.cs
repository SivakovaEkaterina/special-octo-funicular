using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cool_daily_planner
{
    internal class Ser_n_Des
    {
        public static T DeCer<T>(string files)
        {
            string put = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string file = put + "\\Заметки" + "\\zametki.json";
            string json = File.ReadAllText(file);
            T uchastnik = JsonConvert.DeserializeObject<T>(json);
            return uchastnik;
        }
        public static void Cerialaz<T>(T pleer)
        {
            string put = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string file = put + "\\Заметки" + "\\zametki.json";
            var json = JsonConvert.SerializeObject(pleer);
            File.WriteAllText(file, json);
        }
    }
}
