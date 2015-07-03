using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TopUcozParcer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int pages = 0;
       

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 1; i < 51; i++) { listBox1.Items.Add("http://top.ucoz.ru/forums/" + i + "/"); }
            listBox1.Items.RemoveAt(3);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            textBox2.Text = listBox1.SelectedItem.ToString();
            WebRequest req = WebRequest.Create(textBox2.Text);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

            doc.LoadHtml(sr.ReadToEnd());
            try
            {
                var tdList = doc.DocumentNode.SelectNodes("//span");
                String str = "";
                foreach (var tr in tdList)
                {
                    //Получаем список столбцов i-ой строки
                    var trList = tr.ChildNodes.Where(x => x.Name == "b");
                    foreach (var td in tdList)
                    {
                        if (td.InnerText.Contains("Форумов")) { str = td.InnerText + "\n"; break; }
                    }
                    if (str.Contains("Форумов")) break;
                }
                String chislo = "";
                foreach (char a in str) { if (char.IsDigit(a)) chislo += a.ToString(); }
                int ch = int.Parse(chislo);
                label2.Text = ch.ToString();
                if (checkBox1.Checked) ch = int.Parse(textBox1.Text);
                pages = ch / 30;
                if (ch % 30 != 0) pages += 1;

                var names = doc.DocumentNode.SelectNodes("//title");
                String str1 = "";
                foreach (var tr in names)
                {
                    //Получаем список столбцов i-ой строки

                    if (tr.InnerText.Contains("uCoz TOP100")) { str1 = tr.InnerText + "\n"; break; }


                }

                label3.Text = str1.Replace(" - Рейтинг форумов - uCoz TOP100", "");
            }
            catch (Exception l)
            {
                textBox2.Text = l.Message;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                for (int k = 0; k < listBox3.Items.Count; k++)
                {
                    listBox2.Items.Add("Сайты по запросу " + listBox3.Items[k].ToString());
                    WebRequest req = WebRequest.Create(listBox3.Items[k].ToString());
                    WebResponse resp = req.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                    doc.LoadHtml(sr.ReadToEnd());
                    try
                    {
                        var tdList = doc.DocumentNode.SelectNodes("//span");
                        String str = "";
                        foreach (var tr in tdList)
                        {
                            //Получаем список столбцов i-ой строки
                            var trList = tr.ChildNodes.Where(x => x.Name == "b");
                            foreach (var td in tdList)
                            {
                                if (td.InnerText.Contains("Форумов")) { str = td.InnerText + "\n"; break; }
                            }
                            if (str.Contains("Форумов")) break;
                        }
                        String chislo = "";
                        foreach (char a in str) { if (char.IsDigit(a)) chislo += a.ToString(); }
                        int ch = int.Parse(chislo);
                        if (checkBox1.Checked) ch = int.Parse(textBox1.Text);

                        pages = ch / 30;

                        if (ch % 30 != 0) pages += 1;



                        for (int i = 1; i < pages + 1; i++)
                        {
                            int count = 1;
                            String url1 = "";
                            if (i != 1) url1 = listBox3.Items[k].ToString() + "index" + i.ToString() + ".html";
                            else url1 = listBox3.Items[k].ToString() + "index.html";
                            WebRequest req1 = WebRequest.Create(url1);
                            WebResponse resp1 = req1.GetResponse();
                            Stream stream1 = resp1.GetResponseStream();
                            StreamReader sr1 = new StreamReader(stream1);
                            HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                            doc1.LoadHtml(sr1.ReadToEnd());

                            String st1r = "";
                            var trList1 = doc1.DocumentNode.SelectNodes("//tr");
                            //Теперь для каждой строки tr, получаем все столбцы td
                            foreach (var tr1 in trList1)
                            {
                                //Получаем список столбцов i-ой строки
                                var tdList1 = tr1.ChildNodes.Where(x => x.Name == "td");
                                foreach (var td1 in tdList1)
                                {

                                    if (td1.Attributes["class"].Value == "utdescr")
                                    {
                                        if (count > int.Parse(textBox1.Text)) { break; }
                                        st1r = td1.InnerHtml.Replace(td1.InnerHtml.Split('\u0022')[0], "");
                                        st1r = st1r.Replace("\"_blank\" href=\"", "");
                                        st1r = st1r.Split('\u0022')[0];
                                        if ((st1r != "\n") & (st1r != ""))
                                        {
                                            listBox2.Items.Add(st1r + "\n");
                                            count += 1;
                                        }

                                    }

                                }
                                if (count > int.Parse(textBox1.Text)) { break; }
                            }

                        }
                    }
                    catch (Exception l)
                    {
                        listBox2.Items.Add(l.Message);
                    }

                }
            }
    else 
                if (checkBox3.Checked)
                {
                    for (int k = 0; k < listBox1.Items.Count; k++)
                    {
                        
                        
                        WebRequest req = WebRequest.Create(listBox1.Items[k].ToString());
                        WebResponse resp = req.GetResponse();
                        Stream stream = resp.GetResponseStream();
                        StreamReader sr = new StreamReader(stream);
                        HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();

                        doc.LoadHtml(sr.ReadToEnd());
                        try { 
                        var tdList = doc.DocumentNode.SelectNodes("//span");
                        String str = "";
                        foreach (var tr in tdList)
                        {
                            //Получаем список столбцов i-ой строки
                            var trList = tr.ChildNodes.Where(x => x.Name == "b");
                            foreach (var td in tdList)
                            {
                                if (td.InnerText.Contains("Форумов")) { str = td.InnerText + "\n"; break; }
                            }
                            if (str.Contains("Форумов")) break;
                        }
                        String chislo = "";
                        foreach (char a in str) { if (char.IsDigit(a)) chislo += a.ToString(); }
                        int ch = int.Parse(chislo);
                        if (checkBox1.Checked) ch = int.Parse(textBox1.Text);

                        pages = ch / 30;

                        if (ch % 30 != 0) pages += 1;



                        for (int i = 1; i < pages + 1; i++)
                        {
                            int count = 1;
                            String url1 = "";
                            if (i != 1) url1 = listBox1.Items[k].ToString() + "index" + i.ToString() + ".html";
                            else url1 = listBox1.Items[k].ToString() + "index.html";
                            WebRequest req1 = WebRequest.Create(url1);
                            WebResponse resp1 = req1.GetResponse();
                            Stream stream1 = resp1.GetResponseStream();
                            StreamReader sr1 = new StreamReader(stream1);
                            HtmlAgilityPack.HtmlDocument doc1 = new HtmlAgilityPack.HtmlDocument();
                            doc1.LoadHtml(sr1.ReadToEnd());

                            String st1r = "";
                            var trList1 = doc1.DocumentNode.SelectNodes("//tr");
                            //Теперь для каждой строки tr, получаем все столбцы td
                            foreach (var tr1 in trList1)
                            {
                                //Получаем список столбцов i-ой строки
                                var tdList1 = tr1.ChildNodes.Where(x => x.Name == "td");
                                foreach (var td1 in tdList1)
                                {

                                    if (td1.Attributes["class"].Value == "utdescr")
                                    {
                                        if (count > int.Parse(textBox1.Text)) { break; }
                                        st1r = td1.InnerHtml.Replace(td1.InnerHtml.Split('\u0022')[0], "");
                                        st1r = st1r.Replace("\"_blank\" href=\"", "");
                                        st1r = st1r.Split('\u0022')[0];
                                        if ((st1r != "\n") & (st1r != ""))
                                        {
                                            listBox2.Items.Add(st1r + "\n");
                                            count += 1;
                                        }

                                    }

                                }
                                if (count > int.Parse(textBox1.Text)) { break; }
                            }

                        }
                        }
                        catch (Exception l)
                        {
                            listBox2.Items.Add(l.Message);
                        }

                    }
                }


            else{
            for (int i = 1; i < pages + 1; i++)
            {
                int count = 1;

                String url = "";
                if(i!=1)  url = textBox2.Text + "index" + i.ToString() + ".html";
                else  url = textBox2.Text + "index.html";
                WebRequest req = WebRequest.Create(url);
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(sr.ReadToEnd());
                try { 
                String str = "";
                var trList = doc.DocumentNode.SelectNodes("//tr");
                //Теперь для каждой строки tr, получаем все столбцы td
                foreach (var tr in trList)
                {
                    //Получаем список столбцов i-ой строки
                    var tdList = tr.ChildNodes.Where(x => x.Name == "td");
                    foreach (var td in tdList)
                    {
                        
                        if (td.Attributes["class"].Value == "utdescr")
                        {
                            if (count > int.Parse(textBox1.Text)) { break; }
                            str = td.InnerHtml.Replace(td.InnerHtml.Split('\u0022')[0], "");
                            str = str.Replace("\"_blank\" href=\"", "");
                            str = str.Split('\u0022')[0];
                            if ((str != "\n")&(str != ""))
                            {
                                listBox2.Items.Add(str + "\n");
                                count += 1;
                            }

                        }
                    }
                    if (count > int.Parse(textBox1.Text)) { break; }
                }
                }
                catch (Exception l)
                {
                    listBox2.Items.Add(l.Message);
                }

            }
}
         
            
            
        
        

        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
           // Clipboard.SetDataObject(new DataObject(listBox2.SelectedItem));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(label3.Text.Replace(' ', '_') + ".txt"))
            {
                // Первой строкой записываем в файл число строк в нашем списке
                sw.WriteLine("По запросу "+label3.Text+" нашлось "+listBox2.Items.Count.ToString()+ " сайтов");
                for (int j = 0; j < listBox2.Items.Count; j++)
                    sw.WriteLine(listBox2.Items[j]);
                MessageBox.Show("Saved!", "Mdgagj");
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == (char)Keys.Down)
            {
                for (int i = 0; i < listBox1.SelectedItems.Count; i++)
                    listBox3.Items.Add(listBox1.SelectedItems[i]);
            }
        }

        private void listBox3_DoubleClick(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            listBox1.SelectionMode = SelectionMode.MultiSimple;
            else
                listBox1.SelectionMode = SelectionMode.One;
        }
    }
}
