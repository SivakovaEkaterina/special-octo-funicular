using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Shapes;

namespace Cool_daily_planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DateTime dt = DateTime.Now;
        public string date;
        public int prov = -1;
        public int nowon = 0;
        
        public MainWindow()
        {
            InitializeComponent();
            string s = "C:\\Users\\edlit\\OneDrive\\Рабочий стол\\Заметки\\zametki.json";
            if (!File.Exists(s))
            {
                string put = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                Directory.CreateDirectory(put + "\\Заметки");
                zametkidata zam = new zametkidata();
                zam.ID = 0;
                zam.date = "00.00.0000";
                zam.Name = "Проба";
                zam.Infor = "Проба пробы";
                List<zametkidata> listik = new List<zametkidata>();
                listik.Add(zam);

                Ser_n_Des.Cerialaz(listik);
            }
            date = dt.ToString("dd/MM/yyyy");
            ZametochkiNaSegodna();
            DayNow.Text = "Заметки на " + date;
        }
        
        private void Vibor_Dati(object sender, SelectionChangedEventArgs e)
        {
            /*DateTime dt = Convert.ToDateTime(Kalendar.Text);*/
        }

        private void Next_day(object sender, RoutedEventArgs e)
        {
            dt = dt.AddDays(1);
            date = dt.ToString("dd/MM/yyyy");
            DayNow.Text = "Заметки на " + date;
            ZametochkiNaSegodna();
        }

        private void Day_befor(object sender, RoutedEventArgs e)
        {
            dt = dt.AddDays(-1);
            date = dt.ToString("dd/MM/yyyy");
            DayNow.Text = "Заметки на " + date;
            ZametochkiNaSegodna();
        }
        public void ZametochkiNaSegodna()
        {
            int id = 0;
            TextBlock[] text = new TextBlock[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11, text12 };
            foreach (var i in text)
            {
                i.Text = "";
            }
            int schet = 1;
            List<zametkidata> infor = Ser_n_Des.DeCer<List<zametkidata>>("");
            foreach (var i in infor)
            {
                if (date == i.date)
                {
                    text[id].Text = i.Name;
                    id++;
                }
            }
        }

        private void new_zametka(object sender, RoutedEventArgs e)
        {
            int mnogo = -1;
            int id = 0;
            TextBlock[] text = new TextBlock[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11, text12 };
            if (Pole_vvoda.IsEnabled == true)
            {
                List<zametkidata> infor = Ser_n_Des.DeCer<List<zametkidata>>("");
                foreach (var i in text)
                {
                    if (i.Text == "") { break; }
                    id++;
                }
                foreach (var i in infor)
                {
                    if (i.ID > mnogo)
                    {
                        mnogo = i.ID;
                    }
                }
                text[id].Text = Pole_vvoda.Text;
                zametkidata zam = new zametkidata();
                zam.ID = mnogo+1;
                zam.date = date;
                zam.Name = Pole_vvoda.Text;
                zam.Infor = "";
                infor.Add(zam);

                Ser_n_Des.Cerialaz(infor);
                Pole_vvoda.IsEnabled = false;
                Pole_vvoda.Text = "";
            }
            else { Pole_vvoda.IsEnabled = true; }
            

        }

        private void VivediText(object sender, RoutedEventArgs e)
        {
            int id = 0;
            TextBlock[] text = new TextBlock[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11, text12 };
            Button[] button = new Button[] { TextBottom1, TextBottom2, TextBottom3, TextBottom4, TextBottom5, TextBottom6, TextBottom7, TextBottom8, TextBottom9, TextBottom10, TextBottom11, TextBottom12 };
            int schet = 1;
            List<zametkidata> infor = Ser_n_Des.DeCer<List<zametkidata>>("");
            int y = 0;
            string[] buttonst = new string[] { "TextBottom1", "TextBottom2", "TextBottom3", "TextBottom4", "TextBottom5", "TextBottom6", "TextBottom7", "TextBottom8", "TextBottom9", "TextBottom10", "TextBottom11", "TextBottom12" };

            string nazvanie ="";
            foreach (var i in buttonst)
            { 
                if((sender as Button).Name == buttonst[y])
                {
                    nazvanie = text[y].Text;
                }
                y++;
            }
            if (nazvanie != "")
            {
                foreach (var i in infor)
                {
                    if (nazvanie == i.Name && date == i.date)
                    {
                        spisokchitay.Text = i.Infor;
                        nowon = i.ID;
                    }
                }
                NewInformation.IsEnabled = true;
            }else
            {
                spisokchitay.Text = "";
                NewInformation.IsEnabled = false;
            }
        }

        private void New_iforming(object sender, RoutedEventArgs e)
        {
            int ww = 0;
            int id = 0;
            TextBlock[] text = new TextBlock[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11, text12 };
            if (Pole_vvoda.IsEnabled == true)
            {
                
                string texts ="", names = "", dates = "";
                int ids = -1;
                List<zametkidata> infor = Ser_n_Des.DeCer<List<zametkidata>>("");
                foreach (var i in infor)
                {
                    if (nowon == i.ID) 
                    {
                        ids = i.ID;
                        names = i.Name;
                        dates = i.date;
                        texts = Pole_vvoda.Text;
                        ww = 1;
                        break; 
                    }
                    id++;
                }
                if (ww == 1)
                {
                    infor.RemoveAt(id);
                    zametkidata zam = new zametkidata();
                    zam.ID = ids;
                    zam.date = dates;
                    zam.Name = names;
                    zam.Infor = texts;
                    infor.Add(zam);

                    Ser_n_Des.Cerialaz(infor);

                    Button[] button = new Button[] { TextBottom1, TextBottom2, TextBottom3, TextBottom4, TextBottom5, TextBottom6, TextBottom7, TextBottom8, TextBottom9, TextBottom10, TextBottom11, TextBottom12 };
                    foreach (var i in button)
                    {
                        i.IsEnabled = true;
                    }

                    Pole_vvoda.IsEnabled = false;
                    NextDay.IsEnabled = true;
                    BeforDay.IsEnabled = true;
                    NewZametka.IsEnabled = true;
                    Delet.IsEnabled = true;

                }
                Pole_vvoda.Text = "";
            }
            else 
            {
                Button[] button = new Button[] { TextBottom1, TextBottom2, TextBottom3, TextBottom4, TextBottom5, TextBottom6, TextBottom7, TextBottom8, TextBottom9, TextBottom10, TextBottom11, TextBottom12 };
                foreach (var i in button)
                {
                    i.IsEnabled = false;
                }
                Pole_vvoda.IsEnabled = true;
                NextDay.IsEnabled = false;
                BeforDay.IsEnabled = false;
                NewZametka.IsEnabled = false;
                Delet.IsEnabled = false;
            }
        }

        private void DeletZap(object sender, RoutedEventArgs e)
        {
            List<zametkidata> infor = Ser_n_Des.DeCer<List<zametkidata>>("");
            int no = 0, on = 0;
            foreach (var i in infor)
            {
                if (i.ID == nowon)
                {
                    no = on;
                }
                on++;
            }
            infor.RemoveAt(no);

            Ser_n_Des.Cerialaz(infor);
        }
    }
}
