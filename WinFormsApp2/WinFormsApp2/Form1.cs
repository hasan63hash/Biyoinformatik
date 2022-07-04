using System.Diagnostics;
namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        List<string> veriList1 = new List<string>();
        List<string> veriList2 = new List<string>();

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
                        veriList1.Add(veri);
                        veri = sr.ReadLine();
                    }
                    veriList1[1]=veriList1[1].ToUpper();
                    textBox1.Text = veriList1[1] + "";

                }
                catch (Exception)
                {
                    throw;
                }

            }

        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
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
                dataGridView1.Rows[i].HeaderCell.Value = karakterler2[i-1] + "";
            }
            int sutunlar = karakterler.Length + 1;
            int satýrlar = karakterler2.Length + 1;
            int[,] matris = new int[satýrlar, sutunlar];
            dataGridView1.Rows[0].Cells[0].Value = 0;
            int tut1 = Gap;

            for (int i = 1; i < dataGridView1.ColumnCount; i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = tut1;
                matris[0, i] = tut1;
                tut1 += Gap;

            }

            tut1 = Gap;
            for (int i = 1; i < satýrlar; i++)
            {
                dataGridView1.Rows[i].Cells[0].Value = tut1;
                matris[i, 0] = tut1;
                tut1 += Gap;
                for (int j = 1; j < sutunlar; j++)
                {
                    int scordiag = 0;
                    if (karakterler2[i - 1].Equals(karakterler[j - 1]))
                    {
                        scordiag = matris[i-1,j-1]+Match;
                        
                    }
                    else
                    {
                        scordiag = matris[i-1,j-1]+MisMatch;
                        
                    }
                    int scoreleft=matris[i,j-1]+Gap;
                    int scoreup=matris[i-1,j]+Gap;


                    int maxscor = Math.Max(Math.Max(scordiag, scoreleft),scoreup);
                    matris[i,j] = maxscor;
                    
                    dataGridView1.Rows[i].Cells[j].Value = maxscor;
                }
            }

            string alA = string.Empty;
            string alB = string.Empty;
            int m = satýrlar-1;
            int n=sutunlar-1;
            dataGridView1.Rows[0].Cells[0].Style.BackColor = Color.Blue;
            while (m>0 && n>0)
            {
                int scordiag = 0;
                if (m == karakterler2.Length && n == karakterler.Length)
                {
                    dataGridView1.Rows[m].Cells[n].Style.BackColor = Color.Blue;
                }
                if (karakterler2[m-1].Equals(karakterler[n-1]))
                {
                    dataGridView1.Rows[m - 1].Cells[n - 1].Style.BackColor = Color.Blue;
                    scordiag = 1;
                }
                else
                {
                    int scdiag = matris[m - 1, n - 1];

                    int scorelft = matris[m, n - 1];
                    int scoreup = matris[m - 1, n];
                    if (scdiag>= scoreup && scdiag>=scorelft)
                    {
                        dataGridView1.Rows[m - 1].Cells[n - 1].Style.BackColor = Color.Blue;
                    }
                    else if(scoreup>=scorelft)
                    {
                        dataGridView1.Rows[m-1].Cells[n].Style.BackColor = Color.Blue;
                    }
                    else 
                    {
                        dataGridView1.Rows[m].Cells[n - 1].Style.BackColor = Color.Blue;
                    }
                    scordiag =-1;
                }
                if (m > 0 && n > 0 && matris[m, n] == matris[m - 1, n - 1] + scordiag)
                {
                    alA = karakterler[n - 1] + alA;
                    alB = karakterler2[m - 1] + alB;
                    m = m - 1;
                    n = n - 1;
                }
                else if (n > 0 && matris[m, n] == matris[m, n - 1] - 2)
                {

                    alA = karakterler[n - 1] + alA;
                    alB = "-" + alB;
                    n = n - 1;
                }
                else
                {

                    alA = "-" + alA;
                    alB = karakterler2[m - 1] + alB;
                    m = m - 1;
                }
            }
            //eðer m ve n nin durumuna göre datagridviewde boyalý olmayan hücreleri boyama
            for (int i = m; i >0; i--)
            {
                dataGridView1.Rows[i].Cells[0].Style.BackColor = Color.Blue;
            }
            for (int i = n; i > 0; i--)
            {
                dataGridView1.Rows[0].Cells[i].Style.BackColor = Color.Blue;
            }
            //eðer m ve n nin durumuna göre hizalama için - karakteri koyma
            if (n > 0)
            {
                for (int i = n; i > 0; i--)
                {
                    alB = "-" + alB;
                    alA = karakterler[i - 1] + alA;
                }
                listBox2.Items.Add(alA);
                listBox2.Items.Add(alB);
            }
            else
            {
                for (int i = m; i > 0; i--)
                {
                    alA = "-" + alA;
                    alB = karakterler2[i - 1] + alB;
                }
                listBox2.Items.Add(alA);
                listBox2.Items.Add(alB);
            }

            //toplam skor hesaplama
            int toplamskor = 0;
            char[] ala = alA.ToCharArray();
            char[] alb = alB.ToCharArray();
            for (int i = 0; i < ala.Length; i++)
            {
                if (ala[i].Equals(alb[i]))
                {
                    toplamskor = toplamskor + Match;
                }
                else if (ala[i]=='-'|| alb[i] == '-')
                {
                    toplamskor = toplamskor + Gap;
                }
                else if (!ala[i].Equals(alb[i]))
                {
                    toplamskor = toplamskor + MisMatch;
                }
            }
            textBox7.Text=Convert.ToString(toplamskor);
            stopwatch.Stop();
            textBox6.Text = Convert.ToString(stopwatch.Elapsed.Milliseconds);




        }
            
    }
}