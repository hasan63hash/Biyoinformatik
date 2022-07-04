

namespace GeneticAlgorithmFinal
{
    public partial class Form1 : Form
    {
        List<Chromosome> chromosomes = new List<Chromosome>();
        List<Chromosome> nextgen = new List<Chromosome>();
        List<Sequence> sequences = new List<Sequence>();
        List<string> veriList2 = new List<string>();
        int iter = 10;
        int popsize = 10;
        int slength;
        int genindex;
        Random rnd;

        public Form1()
        {
            rnd = new Random();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void readSequences()
        {
            sequences.Clear();
            for (int i = 0; i < veriList2.Count; i++)
            {
                sequences.Add(new Sequence(veriList2[i]));
            }
        }

        public void printGen(List<Chromosome> gen)
        {
            textBox1.AppendText("////////////////////////// " + genindex + ". Nesil" + " ////////////////////////// ");
            for (int i = 0; i < gen.Count; i++)
            {
                print(gen[i]);
            }

            genindex++;
        }

        public void initGA()
        {
            chromosomes.Clear();
            nextgen.Clear();
            genindex = 1;

            int maxlength = sequences[0].length();
            for (int i = 1; i < sequences.Count; i++)
            {
                if(sequences[i].length() > maxlength)
                {
                    maxlength = sequences[i].length();
                }
            }

            slength = (int) Math.Ceiling(1.2 * maxlength);

            for (int i = 0; i < popsize; i++)
            {
                Chromosome c = new(getRandomSequencesWithGaps(), slength);
                chromosomes.Add(c);
            }
        }

        public void iterateGA()
        {
            for (int i=0; i<iter; i++)
            {
                nextgen = new List<Chromosome>();
                eliteSelection();

                while (nextgen.Count < popsize)
                {
                    int r = rnd.Next(10);

                    if (r % 2 == 0)
                    {
                        mutation();
                    }
                    else
                    {
                        crossover();
                    }
                }

                chromosomes = nextgen;
                printGen(chromosomes);
            }
        }

        public void mutation()
        {
            // En iyi 2 kromozomu bul ve mutasyon uygula
            int maxIndex = 0, secondMaxIndex = 0, maxFitness = int.MinValue, secondMaxFitness = int.MinValue;

            for (int i = 0; i < chromosomes.Count; i++)
            {
                Chromosome c = chromosomes[i];
                if (c.fitness >= maxFitness)
                {
                    secondMaxFitness = maxFitness;
                    secondMaxIndex = maxIndex;
                    maxFitness = c.fitness;
                    maxIndex = i;
                }
                else if (c.fitness >= secondMaxFitness)
                {
                    secondMaxFitness = c.fitness;
                    secondMaxIndex = i;
                }
              
            }

            Chromosome c1 = chromosomes[maxIndex];
            Chromosome c2 = chromosomes[secondMaxIndex];

            c1.mutate();
            c2.mutate();
            nextgen.Add(c1);
            nextgen.Add(c2);
        }

        public void crossover()
        {
            // En iyi 2 kromozomu bul ve çaprazla
            int maxIndex = 0, secondMaxIndex = 0, maxFitness = int.MinValue, secondMaxFitness = int.MinValue;

            for (int i = 0; i < chromosomes.Count; i++)
            {
                Chromosome c = chromosomes[i];
                if (c.fitness >= maxFitness)
                {
                    secondMaxFitness = maxFitness;
                    secondMaxIndex = maxIndex;
                    maxFitness = c.fitness;
                    maxIndex = i;
                }
                else if (c.fitness >= secondMaxFitness)
                {
                    secondMaxFitness = c.fitness;
                    secondMaxIndex = i;
                }
            }

            Chromosome c1 = chromosomes[maxIndex];
            Chromosome c2 = chromosomes[secondMaxIndex];

            int crossPoint = rnd.Next(sequences.Count - 1);
            List<Sequence> c1Sequences = new List<Sequence>();
            List<Sequence> c2Sequences = new List<Sequence>();

            for (int i=0; i<sequences.Count; i++)
            {
                if (i <= crossPoint)
                {
                    c1Sequences.Add(c1.sequences[i]);
                    c2Sequences.Add(c2.sequences[i]);
                }
                else
                {
                    c1Sequences.Add(c2.sequences[i]);
                    c2Sequences.Add(c1.sequences[i]);
                }
            }

            Chromosome newC1 = new Chromosome(c1Sequences, slength);
            Chromosome newC2 = new Chromosome(c2Sequences, slength);
            nextgen.Add(newC1);
            nextgen.Add(newC2);
        }

        public void eliteSelection()
        {
            int maxIndex = 0, secondMaxIndex = 0, maxFitness = int.MinValue, secondMaxFitness = int.MinValue;

            for (int i=0; i<chromosomes.Count; i++)
            {
                Chromosome c = chromosomes[i];
                if (c.fitness >= maxFitness)
                {
                    secondMaxFitness = maxFitness;
                    secondMaxIndex = maxIndex;
                    maxFitness = c.fitness;
                    maxIndex = i;
                }
                else if (c.fitness >= secondMaxFitness)
                {
                    secondMaxFitness = c.fitness;
                    secondMaxIndex = i;
                }
            }

            // En iyi 2 kromozomu aktar
            Chromosome c1 = chromosomes[maxIndex];
            Chromosome c2 = chromosomes[secondMaxIndex];
            nextgen.Add(c1);
            nextgen.Add(c2);

            if (maxIndex > secondMaxIndex)
            {
                chromosomes.RemoveAt(maxIndex);
                chromosomes.RemoveAt(secondMaxIndex);
            }
            else
            {
                chromosomes.RemoveAt(secondMaxIndex);
                chromosomes.RemoveAt(maxIndex);
            }
        }

        public List<Sequence> getRandomSequencesWithGaps()
        {
            List<Sequence> newSequences = new List<Sequence>();
            foreach (Sequence seq in sequences)
            {
                String text = seq.text;
                int gapNum = slength - text.Length;
                for (int i = 0; i < gapNum; i++)
                {
                    int ri = rnd.Next(text.Length);
                    text = text.Substring(0, ri) + "-" + text.Substring(ri, text.Length - ri);
                }

                Sequence newSeq = new(text);
                newSequences.Add(newSeq);
            }
            return newSequences;
        }

        public void print(Chromosome c)
        {
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("==================");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("fitness = " + c.fitness);
            textBox1.AppendText(Environment.NewLine);
            for (int i = 0; i < c.sequences.Count; i++)
            {
                textBox1.AppendText(Environment.NewLine);
                textBox1.AppendText("seqtext = " + c.sequences[i].text);
                textBox1.AppendText(Environment.NewLine);
            }
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(c.rep);
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("==================");
            textBox1.AppendText(Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            iter = Convert.ToInt16(textBox2.Text);
            popsize = Convert.ToInt16(textBox3.Text);

            readSequences();
            initGA();
            printGen(chromosomes);
            iterateGA();
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
                }
                catch (Exception)
                {
                    throw;
                }

            }
            for (int i = 0; i < veriList2.Count; i++)
            {
                listBox1.Items.Add(veriList2[i]);

            }
        }
    }
}