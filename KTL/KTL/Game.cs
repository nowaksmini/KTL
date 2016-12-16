using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace KTL
{
    public class Game
    {
        public List<Color> Colors { get; set; }
        public List<Field> Fields { get; set; }
        public int K { get; set; }
        public int HumanLevel { get; set; }
        public int ComputerLevel { get; set; }
        public List<List<int>> Progressions { get; private set; }

        public void ComputerStep()
        {
            switch (ComputerLevel)
            {
                case 1:
                    ComputerStepLevel1();
                    break;
                case 2:
                    ComputerStepLevel2();
                    break;
                default:
                    MessageBox.Show("Nie zaimplementowano tego poziomu.");
                    break;
            }
            VerifyVictory();
        }

        private void ComputerStepLevel2()
        {
            var r = new Random();
            var changeProgression = Progressions[r.Next(Progressions.Count - 1)];
            var usedColors = changeProgression.Select(index => Fields[index].Color).ToList();
            var validColors = Colors.Where(c => !usedColors.Contains(c));
            double validFieldsCount = changeProgression.Sum(index => Fields[index].IsEmpty() ? 1 : 0);

            int notChangedValidFields = 0;
            var r2 = r.NextDouble();
            for (int i = 0; i < changeProgression.Count; i++)
            {
                if (Fields[changeProgression[i]].IsEmpty())
                {
                    notChangedValidFields++;
                    if (notChangedValidFields / validFieldsCount > r2)
                    {
                        Fields[changeProgression[i]].Select(Colors[r.Next(Colors.Count - 1)]);
                        break;
                    }
                }
            }
        }

        internal void NewGame()
        {

            Fields.ForEach(f => f.Enable());
            CreateProgressions();
        }

        private void CreateProgressions()
        {
            int n = Fields.Count();
            Progressions = new List<List<int>>();
            for (int i = 1; i <= n - K + 1; i++)
            {
                for (int r = 1; r <= n / (K - 1) && i + r * (K - 1) <= n; r++)
                {
                    var nowyciag = new List<int>();
                    for (int j = 0; j < K; j++)
                    {
                        nowyciag.Add(i + r * j-1);
                    }
                    Progressions.Add(nowyciag);
                }
            }
        }

        public bool? VerifyVictory()
        {
            UpdateValidProgressions();
            var vinProgression= Progressions.FirstOrDefault(p => !p.Any(index => Fields[index].IsEmpty()));
            if(vinProgression!=null)
            {
                MessageBox.Show("Komputer wygrał. "+string.Join(", ", vinProgression.Select(index=>index+1)));
                return false;
            }
            if (Progressions.Count == 0)
            {
                MessageBox.Show("Wygrał gracz!");
                return true;
            }
            return null;
        }

        private void UpdateValidProgressions()
        {
            Progressions.RemoveAll(p => !IsCorrect(p));
            SortProgressions();
        }

        private void SortProgressions()
        {
            Progressions.Sort((a, b) => a.Select(index => Fields[index]).Sum(f => f.IsEmpty() ? 1 : 0) - b.Select(index => Fields[index]).Sum(f => f.IsEmpty() ? 1 : 0));
        }

        private bool IsCorrect(List<int> p)
        {
            var colorIndexes = p.Where(index => !Fields[index].IsEmpty());
            return colorIndexes.Count() == colorIndexes.Select(index => Fields[index].Color).Distinct().Count();
        }

        private void ComputerStepLevel1()
        {
            var r = new Random();
            double validFieldsCount = Fields.Sum(f => f.Enabled ? 1 : 0);
            int notChangedValidFields = 0;
            var r2 = r.NextDouble();
            for (int i = 0; i < Fields.Count; i++)
            {
                if (Fields[i].IsEmpty())
                {
                    notChangedValidFields++;
                    if (notChangedValidFields / validFieldsCount > r2)
                    {
                        Fields[i].Select(Colors[r.Next(Colors.Count - 1)]);
                        break;
                    }
                }
            }
        }

        internal void ShowHint()
        {
            switch (HumanLevel)
            {
                case 1:
                    HintLevel1();
                    break;
                default:
                    MessageBox.Show("Nie zaimplementowano tego poziomu.");
                    break;
            }
        }

        private void HintLevel1()
        {
            var r = new Random();
            var changeProgression = Progressions[r.Next(Progressions.Count - 1)];
            var validColors = changeProgression.Where(index=>!Fields[index].IsEmpty()).Select(index => Fields[index].Color);
            if (!validColors.Any()) validColors = Colors;
            var color = Colors.IndexOf(validColors.ToList()[r.Next(validColors.Count() - 1)]);

            var validFieldsCount = changeProgression.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
            int notChangedValidFields = 0;
            var r2 = r.NextDouble();
            for (int i = 0; i < changeProgression.Count; i++)
            {
                if (Fields[changeProgression[i]].IsEmpty())
                {
                    notChangedValidFields++;
                    if (notChangedValidFields / validFieldsCount > r2)
                    {
                        MessageBox.Show(string.Format("pozycja: {0}, kolor: {1}", changeProgression[i], color));
                        break;
                    }
                }
            }
        }
    }
}
