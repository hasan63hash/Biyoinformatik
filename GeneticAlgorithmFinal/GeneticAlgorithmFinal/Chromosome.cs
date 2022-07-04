using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmFinal
{
    public class Chromosome
    {
        public List<Sequence> sequences;
        public int fitness;
        int slength;
        public String rep;

        public Chromosome(List<Sequence> sequences, int slength)
        {
            this.slength = slength;
            this.sequences = sequences;

            calculateFitness();
            createRep();
        }

        public void mutate()
        {
            Random rnd = new Random();
            int si = rnd.Next(sequences.Count);
            sequences[si] = sequences[si].mutate();
        }

        public void createRep()
        {
            string rep = "";

            for (int i = 0; i < sequences.Count; i++)
            {
                Sequence seq = sequences[i];

                int gapIndex = -1;
                while (gapIndex < seq.length() && (gapIndex = seq.indexof('-', gapIndex + 1)) != -1)
                {
                    rep += gapIndex + " ";
                }

                rep += slength.ToString() + " ";
            }

            this.rep = rep;
        }

        public void calculateFitness()
        {
            int distance = 0;

            for (int i = 0; i < sequences.Count; i++)
            {
                Sequence seq = this.sequences[i];

                for (int j = i+1; j < sequences.Count; j++)
                {
                    Sequence seq2 = this.sequences[j];
                    distance += seq.ld(seq2);

                }
            }

            this.fitness = -1 * distance;
        }

        public void add(Sequence s)
        {
            sequences.Add(s);
        }
    }
}
