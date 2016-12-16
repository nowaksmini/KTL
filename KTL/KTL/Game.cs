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
            if (Progressions.Count == 0)
            {
                return;
            }
            switch (ComputerLevel)
            {
                case 1:
                    ComputerStepLevel1();
                    break;
                case 2:
                    ComputerStepLevel2();
                    break;
                case 3:
                    ComputerStepLevel3();
                    break;
                default:
                    MessageBox.Show("Nie zaimplementowano tego poziomu.");
                    break;
            }
            VerifyVictory();
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

        /// <summary>
        /// Losowy wybór dowolnego ciągu znajdującego się w kolejce priorytetowej
        /// PQ - oznaczonego jako ai.Prawdopodobieństwo wylosowania ciągu ai
        /// wynosi 1−(sumaxi/suma_wszystkich wolnych pól), gdzie sumaxi to ilość pól niepokolorowanych
        /// w ciągu ai. Wybranie dowolnego niepokolorowanego jeszcze pola w wylosowanym ciągu ai oraz 
        /// dowolnego koloru z dostępnej puli kolorów jeszcze nie użytych w ciągu ai.
        /// </summary>
        private void ComputerStepLevel3()
        {
            var r = new Random();

            List<int> probability = new List<int>();
            int sumValidFieldsCount = 0 ;
            foreach (var a in Progressions)
            {
                int validFieldsCount1 = a.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
                if (probability.Count == 0)
                {
                    probability.Add(validFieldsCount1);
                }
                sumValidFieldsCount += validFieldsCount1;
            }

            probability.ForEach(index => { index = sumValidFieldsCount - index; });
            // tutaj całe prawdopodobieństwo to rozmiar probability - 1

            for(int i = 1; i< probability.Count; i++)
            {
                probability[i] = probability[i - 1] + probability[i];
            }

            int selected = r.Next(probability[probability.Count - 1]);

            int aNumber = probability.IndexOf(probability.First(index => (index >= selected)));

            var changeProgression = Progressions[aNumber];
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
                        nowyciag.Add(i + r * j - 1);
                    }
                    Progressions.Add(nowyciag);
                }
            }
        }

        public bool? VerifyVictory()
        {
            UpdateValidProgressions();
            var vinProgression = Progressions.FirstOrDefault(p => !p.Any(index => Fields[index].IsEmpty()));
            if (vinProgression != null)
            {
                MessageBox.Show("Komputer wygrał. " + string.Join(", ", vinProgression.Select(index => index + 1)));
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

        internal void ShowHint()
        {
            if (Progressions.Count == 0)
            {
                return;
            }
            switch (HumanLevel)
            {
                case 1:
                    MessageBox.Show("Brak wskazówek dla gracza");
                    break;
                case 2:
                    HintLevel1();
                    break;
                case 3:
                    HintLevel2();
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
            var validColors = changeProgression.Where(index => !Fields[index].IsEmpty()).Select(index => Fields[index].Color);
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

        /// <summary>
        /// Losowy wybór dowolnego ciągu znajdującego się w kolejce priorytetowej
        /// PQ - oznaczonego jako ai.Prawdopodobieństwo wylosowania ciągu ai
        /// wynosi sumaxi/suma_wszystkich wolnych pól, gdzie sumaxi to ilość pól niepokolorowanych
        /// w ciągu ai. Wybranie dowolnego niepokolorowanego jeszcze pola w wylosowanym ciągu ai oraz 
        /// dowolnego koloru z dostępnej puli kolorów jeszcze nie użytych w ciągu ai.
        /// </summary>
        private void HintLevel2()
        {
            var r = new Random();

            List<int> probability = new List<int>();

            foreach (var a in Progressions)
            {
                int validFieldsCount1 = a.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
                if (probability.Count == 0)
                {
                    probability.Add(validFieldsCount1);
                }
                else
                {
                    probability.Add(validFieldsCount1 + probability[probability.Count - 1]);
                }
            }

            int selected = r.Next(probability[probability.Count - 1]);

            int aNumber = probability.IndexOf(probability.First(index => (index >= selected)));

            var changeProgression = Progressions[aNumber];
            var usedColors = changeProgression.Select(index => Fields[index].Color).ToList();
            var validColors = Colors.Where(c => !usedColors.Contains(c));
            double validFieldsCount = changeProgression.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
            var color = Colors.IndexOf(validColors.ToList()[r.Next(validColors.Count() - 1)]);
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
