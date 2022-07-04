
using System.Diagnostics;
namespace WinFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> veriList1 = new List<string>();
        List<string> veriList2 = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text|*.txt";

            if (file.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    String veri = sr.ReadLine();
                    while (veri != null)
                    {
                        veriList1.Add(veri);
                        veri = sr.ReadLine();
                    }
                    veriList1[1] = veriList1[1].ToUpper();
                    textBox1.Text = veriList1[1] + "";

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Text|*.txt";

            if (file.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamReader sr = new StreamReader(file.FileName);
                    String veri = sr.ReadLine();

                    while (veri != null)
                    {
                        veriList2.Add(veri);
                        veri = sr.ReadLine();
                    }
                    veriList2[1] = veriList2[1].ToUpper();
                    textBox2.Text = veriList2[1] + "";

                }
                catch (Exception)
                {
                    throw;
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int Match = 1;
            int MisMatch = -1;
            int Gap = -2;
            if (textBox3.Text != String.Empty)
            {
                Match = Convert.ToInt32(textBox3.Text);
                MisMatch = Convert.ToInt32(textBox4.Text);
                Gap = Convert.ToInt32(textBox5.Text);
            }
            int skor = 0;
            char[] karakterler = veriList1[1].ToCharArray();
            char[] karakterler2 = veriList2[1].ToCharArray();


            dataGridView1.RowCount = karakterler2.Length + 1;
            dataGridView1.ColumnCount = karakterler.Length + 1;
            char tut = ' ';

            dataGridView1.Columns[0].Name = tut + "";

            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Columns[i].Name = karakterler[i - 1] + "";
            }
            dataGridView1.Rows[0].HeaderCell.Value = tut + "";
            for (int i = 1; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = karakterler2[i - 1] + "";
            }
            int sutunlar = karakterler.Length + 1;
            int satýrlar = karakterler2.Length + 1;
            int[,] matris = new int[satýrlar, sutunlar];
            dataGridView1.Rows[0].Cells[0].Value = 0;
            int tut1 = 0;

            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = tut1;
                matris[0, i] = tut1;


            }

            tut1 = 0;
            for (int i = 1; i < satýrlar; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = tut1;
                matris[i, 0] = tut1;

                for (int j = 1; j < sutunlar; j++)
                {
                    int scordiag = 0;
                    if (karakterler2[i - 1].Equals(karakterler[j - 1]))
                    {
                        scordiag = matris[i - 1, j - 1] + Match;

                    }
                    else
                    {
                        scordiag = matris[i - 1, j - 1] + MisMatch;

                    }
                    int scoreleft = matris[i, j - 1] + Gap;
                    int scoreup = matris[i - 1, j] + Gap;

                    if (scordiag < 0)
                    {
                        scordiag = 0;
                    }
                    if (scoreleft < 0)
                    {
                        scoreleft = 0;
                    }
                    if (scoreup < 0)
                    {
                        scoreup = 0;
                    }
                    int maxscor = Math.Max(Math.Max(scordiag, scoreleft), scoreup);
                    matris[i, j] = maxscor;

                    dataGridView1.Rows[i].Cells[j].Value = maxscor;
                }
            }
            int enbuyuk = 0;
            enbuyuk=matris[0, 0];

            for (int i = 0; i <satýrlar; i++)
            {
                for (int j = 0; j <sutunlar; j++)
                {
                    if (enbuyuk < matris[i, j])
                    {
                        enbuyuk = matris[i,j];
                    }
                }
            }

            
            List<char> listemhizalama=new List<char>();
            int topla = 0;
            int sayac = 0;
            int sayac2 = 0;
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = satýrlar-1; i >0; i--)
            {
                for (int j = sutunlar-1; j >0; j--)
                {
                    if (enbuyuk == matris[i, j])
                    {

                        
                        topla = enbuyuk;
                        list2.Add(matris[i, j]);
                        sayac = i;
                        sayac2 = j;
                        if(matris[i, j] == enbuyuk)
                        {
                            dataGridView1.Rows[i].Cells[j].Style.BackColor = Color.Blue;
                        }
                        while (matris[sayac - 1, sayac2 - 1]!=0)
                        {
                            dataGridView1.Rows[sayac - 1].Cells[sayac2 - 1].Style.BackColor = Color.Blue;
                            topla += matris[sayac - 1, sayac2 - 1];
                            list2.Add(matris[sayac - 1, sayac2 - 1]);
                            sayac--;
                            sayac2--;
                        }
                        if(matris[sayac - 1, sayac2 - 1] == 0)
                        {
                            dataGridView1.Rows[sayac - 1].Cells[sayac2 - 1].Style.BackColor = Color.Blue;
                        }
                        
                        list.Add(topla);     
                    }
                }
            }
            //for (int i = 0; i < list.Count; i++)
            //{
            //    listBox1.Items.Add(list[i]);
            //}

            stopwatch.Stop();
            list.Sort();
            textBox6.Text = Convert.ToString(list[list.Count-1]);
            textBox7.Text=Convert.ToString(stopwatch.ElapsedMilliseconds);


        }
    }
}