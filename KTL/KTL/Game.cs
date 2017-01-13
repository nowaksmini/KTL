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

        public Tuple<int, int> ComputerStep()
        {
            if (Progressions.Count == 0)
            {
                return null;
            }
            switch (ComputerLevel)
            {
                case 1:
                    return ComputerStepLevel1();
                case 2:
                    return ComputerStepLevel2();
                case 3:
                    return ComputerStepLevel3();
                case 4:
                    return ComputerStepLevel4();
                case 5:
                    return ComputerStepLevel5();
                default:
                    return null;
            }
            VerifyVictory();
        }

        public void CreateColors(int colorCount)
        {
            Colors = new List<Color>();
            var r = new Random();
            for (int i = 0; i < colorCount; i++)
            {
                Colors.Add(Color.FromArgb(r.Next() | (255 << 24)));
            }
        }

        /// <summary>
        /// Losowy wybór koloru oraz pola.
        /// kolor - losowo wybrany kolor z dostępnej puli
        /// pole - losowo wybrana liczba i z przedziału [1,n], dla której pole pi jestjeszcze niepokolorowane
        /// Podczas losowania danych wartości nie jest wykorzystywana kolejka priorytetowa.
        /// </summary>
        private Tuple<int, int> ComputerStepLevel1()
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
                        int colorIndex = r.Next(Colors.Count - 1);
                        Fields[i].Select(Colors[colorIndex]);
                        return Tuple.Create(i, colorIndex);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Losowy wybór dowolnego ciągu znajdującego się w kolejce priorytetowej oznaczonego jako ai. 
        /// Wybranie dowolnego niepokolorowanego jeszcze pola w ciągu ai oraz dowolnego koloru z dostępnej puli kolorów 
        /// jeszcze nieużytych w wylosowanym ciągu ai.
        /// </summary>
        private Tuple<int, int> ComputerStepLevel2()
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
                        int colorIndex = Colors.FindIndex(x => x == validColors.ElementAt(r.Next(validColors.Count() - 1)));
                        Fields[changeProgression[i]].Select(Colors[colorIndex]);
                        return Tuple.Create(changeProgression[i], colorIndex);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Losowy wybór dowolnego ciągu znajdującego się w kolejce priorytetowej
        /// PQ - oznaczonego jako ai.Prawdopodobieństwo wylosowania ciągu ai
        /// wynosi 1−(sumaxi/suma_wszystkich wolnych pól), gdzie sumaxi to ilość pól niepokolorowanych
        /// w ciągu ai. Wybranie dowolnego niepokolorowanego jeszcze pola w wylosowanym ciągu ai oraz 
        /// dowolnego koloru z dostępnej puli kolorów jeszcze nie użytych w ciągu ai.
        /// </summary>
        private Tuple<int, int> ComputerStepLevel3()
        {
            var r = new Random();

            List<int> probability = new List<int>();
            int sumValidFieldsCount = 0;
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

            for (int i = 1; i < probability.Count; i++)
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
                        int colorIndex = Colors.FindIndex(x => x == validColors.ElementAt(r.Next(validColors.Count() - 1)));
                        Fields[changeProgression[i]].Select(Colors[colorIndex]);
                        return Tuple.Create(changeProgression[i], colorIndex);
                    }
                }
            }
            return null;
        }

        public void SelectField(int index, Color color)
        {
            Fields[index].Disable(color);
        }

        /// <summary>
        /// Z kolejki priorytetowej wybierany jest element z maksymalnym priorytetem. 
        /// Z wybranego ciągu ai losowane jest jeszcze niepokolorowane pole oraz kolor nie występujący do tej 
        /// pory w ciągu ai.
        /// </summary>
        private Tuple<int, int> ComputerStepLevel4()
        {
            var r = new Random();
            var changeProgression = Progressions[0];
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
                        int colorIndex = Colors.FindIndex(x => x == validColors.ElementAt(r.Next(validColors.Count() - 1)));
                        Fields[changeProgression[i]].Select(Colors[colorIndex]);
                        return Tuple.Create(changeProgression[i], colorIndex);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Z kolejki priorytetowej wybierany jest zbiór elementów oznaczony A o największym priorytecie. 
        /// Tworzona jest lista par (kolor, waga koloru) oznaczona  jako ColorList,  zawierająca  wszystkie  kolory  k,  
        /// które  możnaby  było  użyć  w  dowolnym  ciągu ai∈A,  nie  psując  przy  tym  warunku bycia  tęczą  ciągu ai.  
        /// Dla  każdego  koloru  waga  koloru  wyliczana  jest  na podstawie występowalności we wszystkich ciągach ai∈kolejki. 
        /// Jeżeli ciągai wystąpił 5 razy w całej kolejce priorytetowej to waga Koloru i= 5. 
        /// Elementy w liście ColorList posortowane są od najmniejszej wartości wagi koloru do największej. 
        /// Kolory występujące w liście ColorList są unikalne. Dodatkowo  tworzona  jest  lista  par  
        /// (numer  pola,  waga  pola)  oznaczonajako FieldList, zawierająca wszystkie numery pól niepokolorowanych 
        /// występujących  w  ciągach ai∈A.  Dla  każdego  pola  waga  pola  wyliczana jest  na  podstawie  występowalności  
        /// we  wszystkich  ciągach ai∈kolejki priorytetowej.  Jeżeli pole o numerze p występuje 10 razy 
        /// w kolejce priorytetowej towaga P = 10. Elementy w liście FiledList posortowane są od największej  
        /// wartości  wagi  pola  do  najmniejszej.  Następuje  iteracja  po  każdym ciągu ai∈A. 
        /// Dla każdego ciągu ai ustawiana jest tzw. tymczasowa waga tmpweight_i, wyliczana na podstawie 
        /// tmpweight_i = - waga pierwszego koloru z listyColorListnie wykorzystanego w ciągu_ai
        //  plus waga pierwszego pola z listy FieldList znajdującego się w ciągu ai a nie będącego jeszcze pokolorowanym.
        /// Wybierany jest taki ciąg ai, dla którego waga tmpweight_i jest największa.
        /// Wówczas w swoim ruchu komputer wybiera pole o numerze l oraz j-tykolor.
        /// </summary>
        private Tuple<int, int> ComputerStepLevel5()
        {
            var maxPriorityProgressions = new List<Tuple<List<int>, int>>(); // para(ciąg,waga_ciągu)
            int maxValidFieldsCount = Progressions[0].Sum(index => Fields[index].IsEmpty() ? 1 : 0);
            var possibleColorsInBestOnesList = new List<Tuple<Color, int>>();
            var possibleFiledsNumbersInBestOnesList = new List<Tuple<int, int>>();

            foreach (var a in Progressions)
            {
                int aValidFieldsCount = a.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
                if (aValidFieldsCount == maxValidFieldsCount)
                {
                    maxPriorityProgressions.Add(Tuple.Create(a, 0));
                    foreach (var index in a)
                    {
                        if (!Fields[index].Enabled)
                        {
                            Color c = Fields[index].Color;
                            // dodajemy kolory, które jeszcze nie wystąpiły w pewnym najlepszym ciągu
                            foreach (var cc in Colors)
                            {
                                if (!cc.Equals(c) && !possibleColorsInBestOnesList.Exists(x => x.Item1.Equals(cc)))
                                {
                                    possibleColorsInBestOnesList.Add(Tuple.Create(cc, 0));
                                }
                            }
                        }
                        else
                        {
                            // dodajemy pola, które można pokolorować
                            if (!possibleFiledsNumbersInBestOnesList.Exists(x => x.Item1.Equals(index)))
                            {
                                possibleFiledsNumbersInBestOnesList.Add(Tuple.Create(index, 0));
                            }
                        }
                    }
                }
            }

            foreach (var a in Progressions)
            {
                foreach (var index in a)
                {
                    if (!Fields[index].Enabled)
                    {
                        Color c = Fields[index].Color;
                        for (int i = 0; i < possibleColorsInBestOnesList.Count; i++)
                        {
                            if (possibleColorsInBestOnesList[i].Item1.Equals(c))
                            {
                                possibleColorsInBestOnesList[i] = Tuple.Create(c, possibleColorsInBestOnesList[i].Item2 + 1);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < possibleFiledsNumbersInBestOnesList.Count; i++)
                        {
                            if (possibleFiledsNumbersInBestOnesList[i].Item1.Equals(index))
                            {
                                possibleFiledsNumbersInBestOnesList[i] = Tuple.Create(index, possibleFiledsNumbersInBestOnesList[i].Item2 + 1);
                            }
                        }
                    }
                }
            }

            possibleColorsInBestOnesList.Sort((a, b) => a.Item2 - b.Item2);
            possibleFiledsNumbersInBestOnesList.Sort((a, b) => b.Item2 - a.Item2);

            int selectedField = -1;
            Color selectedColor = Color.White;
            int actualMaxWeight = Int32.MinValue;
            foreach (var a in maxPriorityProgressions)
            {
                int maxFiledWeight = Int32.MinValue;
                int minColorWeight = Int32.MaxValue;
                int tmpSelectedFieldNumer = -1;
                Color tmpSelectedColor = Color.White;
                // iteracja po polach w ciągu
                var possibleColors = new List<Color>(Colors);
                foreach (var index in a.Item1)
                {
                    //pole w ciągu, które nie jest pokolorowane
                    var possibleFiled = possibleFiledsNumbersInBestOnesList.Find(x => x.Item1 == index && Fields[index].Enabled);
                    if (possibleFiled != null)
                    {
                        if (maxFiledWeight < possibleFiled.Item2)
                        {
                            maxFiledWeight = possibleFiled.Item2;
                            tmpSelectedFieldNumer = index;
                        }
                    }
                    // usuń potencjalny kolor jeżeli w ciągu już taki istnieje
                    possibleColors.Remove(Fields[index].Color);
                }
                foreach (var c in possibleColors)
                {
                    // waga potencjalnego koloru
                    int weight = possibleColorsInBestOnesList.Find(x => x.Item1 == c).Item2;
                    if (weight < minColorWeight)
                    {
                        minColorWeight = weight;
                        tmpSelectedColor = c;
                    }
                }
                int aWeight = maxFiledWeight - minColorWeight;
                if (actualMaxWeight < aWeight)
                {
                    actualMaxWeight = aWeight;
                    selectedColor = tmpSelectedColor;
                    selectedField = tmpSelectedFieldNumer;
                }
            }
            Fields[selectedField].Select(selectedColor);
            return Tuple.Create(selectedField, Colors.FindIndex(x => x == selectedColor));
        }

        internal void NewGame()
        {

            Fields.ForEach(f => f.Enable());
            CreateProgressions();
        }
        public void CreateFields(int n)
        {
            Fields = new List<Field>();
            for (int i = 0; i < n; i++)
            {
                Fields.Add(new Field());
            }
        }
        public void CreateProgressions()
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
        public void ShowHint()
        {
            var hint = GetHint();
            if (hint != null) MessageBox.Show(string.Format("pozycja: {0}, kolor: {1}", hint.Item1, hint.Item2));
        }
        public Tuple<int, int> GetHint(bool showMessage = true)
        {
            if (Progressions.Count == 0)
            {
                return null;
            }
            Tuple<int, int> hint;
            switch (HumanLevel)
            {
                case 1:
                    if (showMessage) MessageBox.Show("Brak wskazówek dla gracza");
                    return null;
                case 2:
                    hint = HintLevel2();
                    break;
                case 3:
                    hint = HintLevel3();
                    break;
                case 4:
                    hint = HintLevel4();
                    break;
                case 5:
                    hint = HintLevel5();
                    break;
                default:
                    if (showMessage) MessageBox.Show("Nieznany poziom.");
                    return null;
            }
            return hint;
        }

        /// <summary>
        ///  Losowy wybór dowolnego ciągu znajdującego się w kolejce priorytetowej oznaczonego jako ai.
        ///  Wybranie dowolnego niepokolorowanego jeszcze pola w ciągu ai oraz dowolnego koloru z dostępnej 
        ///  puli kolorów już użytych w wylosowanym ciągu ai. Jeżeli takiego koloru nie ma, 
        ///  losowany jest dowolny kolor ze wszystkich kolorów dostępnych podczas gry.
        /// </summary>
        private Tuple<int, int> HintLevel2()
        {
            var r = new Random();
            var changeProgression = Progressions[r.Next(Progressions.Count - 1)];
            return HintUsedColor(changeProgression);
        }

        /// <summary>
        ///  Losowy wybór dowolnego ciągu znajdującego się w kolejce priorytetowej oznaczonego jako ai.
        ///  Prawdopodobieństwo  wylosowania  ciągu ai wynosi sumaxi/∑size(PQ)p=1sumaxi), 
        ///  gdzie sumaxi to liczba pól niepokolorowanych  w  ciągu ai.  
        ///  Wybranie  dowolnego  niepokolorowanego  jeszcze pola w wylosowanym ciągu ai oraz 
        ///  dowolnego koloru z dostępnej puli kolorów już użytych w ciągu ai. 
        ///  Jeżeli takiego koloru nie ma, losowany jest dowolny kolor ze wszystkich kolorów dostępnych podczas gry.
        /// </summary>
        private Tuple<int, int> HintLevel3()
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

            // probability[probability.Count - 1] to ilość wszystkich niepokolorowanych pól we wszystkich ciągach w kolejce
            int selected = r.Next(probability[probability.Count - 1]);

            int aNumber = probability.IndexOf(probability.First(index => (index >= selected)));

            var changeProgression = Progressions[aNumber];
            var usedColors = changeProgression.Select(index => Fields[index].Color).ToList();
            var validColors = Colors.Where(c => usedColors.Contains(c));
            double validFieldsCount = changeProgression.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
            var color = validColors.Count() == 0 ? r.Next(Colors.Count) :
            Colors.IndexOf(validColors.ToList()[r.Next(validColors.Count() - 1)]);
            int notChangedValidFields = 0;
            var r2 = r.NextDouble();
            for (int i = 0; i < changeProgression.Count; i++)
            {
                if (Fields[changeProgression[i]].IsEmpty())
                {
                    notChangedValidFields++;
                    if (notChangedValidFields / validFieldsCount > r2)
                    {
                        return new Tuple<int, int>(changeProgression[i] + 1, color);
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///  Z kolejki priorytetowej wybierany jest element z maksymalnym priorytetem. 
        ///  Z wybranego ciągu ai losowane jest jeszcze niepokolorowane pole oraz kolor występujący już w ciągu ai.
        ///  Jeżeli takiego koloru nie ma, losowany jest dowolny kolor ze wszystkich kolorów dostępnych podczas gry.
        /// </summary>
        private Tuple<int, int> HintLevel4()
        {
            return HintUsedColor(Progressions[0]);
        }

        private Tuple<int, int> HintUsedColor(List<int> progression)
        {
            var r = new Random();
            var validColors = progression.Where(index => !Fields[index].IsEmpty()).Select(index => Fields[index].Color);
            if (!validColors.Any()) validColors = Colors;
            var color = Colors.IndexOf(validColors.ToList()[r.Next(validColors.Count() - 1)]);

            var validFieldsCount = progression.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
            int notChangedValidFields = 0;
            var r2 = r.NextDouble();
            for (int i = 0; i < progression.Count; i++)
            {
                if (Fields[progression[i]].IsEmpty())
                {
                    notChangedValidFields++;
                    if (notChangedValidFields / validFieldsCount > r2)
                    {
                        return new Tuple<int, int>(progression[i] + 1, color);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Z kolejki priorytetowej wybierany jest zbiór elementów oznaczony A o największym priorytecie. 
        /// Tworzona jest lista par (kolor, waga koloru) oznaczona jako ColorList, zawierająca wszystkie kolory k,
        /// które można by było użyć w dowolnym ciągu ai ∈ A, psując przy tym warunek bycia tęczą ciągu ai. 
        /// Dla każdego koloru waga koloru wyliczana jest na podstawie występowalności we wszystkich ciągach ai ∈ kolejka. 
        /// Jeżeli kolor i wystąpił 5 razy w całej kolejce priorytetowej to wagakoloru_i = 5. 
        /// Elementy w liście ColorList posortowane są od największej wartości wagi koloru do najmniejszej. 
        /// Kolory występujące w liście ColorList są unikalne. Dodatkowo tworzona jest lista par (numer pola, waga pola) oznaczona jako FieldList, 
        /// zawierająca wszystkie numery pól niepokolorowanych występujących w ciągach ai ∈ A. 
        /// Dla każdego pola waga pola wyliczana jest na podstawie występowalności we wszystkich ciągach ai ∈ kolejka. 
        /// Jeżeli pole o numerze p występuje 10 razy w kolejce priorytetowej to waga Pola p = 10. 
        /// Elementy w liście FiledList posortowane są od największej wartości wagi pola do najmniejszej.
        /// Następuje iteracja po każdym ciągu ai ∈ A. Dla każdego ciągu ai ustawiana jest tzw.tymczasowa waga tmp weight_i, 
        /// wyliczana na podstawie tmp weight_i = waga pierwszego koloru z listy ColorList nie wykorzystanego w ciągu ai 
        /// + waga pierwszego pola z listy FieldList znajdującego się w ciągu ai a nie będącego jeszcze pokolorowanym.
        /// Wybierany jest taki ciąg ai, dla którego waga tmp weight_i jest największa. 
        /// </summary>
        private Tuple<int, int> HintLevel5()
        {
            var maxPriorityProgressions = new List<Tuple<List<int>, int>>(); // para(ciąg,waga_ciągu)
            int maxValidFieldsCount = Progressions[0].Sum(index => Fields[index].IsEmpty() ? 1 : 0);
            var existingColorsInBestOnesList = new List<Tuple<Color, int>>();
            var possibleFiledsNumbersInBestOnesList = new List<Tuple<int, int>>();
            var r = new Random();

            foreach (var a in Progressions)
            {
                int aValidFieldsCount = a.Sum(index => Fields[index].IsEmpty() ? 1 : 0);
                if (aValidFieldsCount == maxValidFieldsCount)
                {
                    maxPriorityProgressions.Add(Tuple.Create(a, 0));
                    foreach (var index in a)
                    {
                        if (!Fields[index].Enabled)
                        {
                            Color c = Fields[index].Color;
                            // dodajemy kolor, który już wystąpił w pewnym najlepszym ciągu
                            if (!existingColorsInBestOnesList.Exists(x => x.Item1.Equals(c)))
                            {
                                existingColorsInBestOnesList.Add(Tuple.Create(c, 0));
                            }
                        }
                        else
                        {
                            // dodajemy pola, które można pokolorować
                            if (!possibleFiledsNumbersInBestOnesList.Exists(x => x.Item1.Equals(index)))
                            {
                                possibleFiledsNumbersInBestOnesList.Add(Tuple.Create(index, 0));
                            }
                        }
                    }
                }
            }

            foreach (var a in Progressions)
            {
                foreach (var index in a)
                {
                    if (!Fields[index].Enabled)
                    {
                        // zliczanie wagi koloru
                        Color c = Fields[index].Color;
                        for (int i = 0; i < existingColorsInBestOnesList.Count; i++)
                        {
                            if (existingColorsInBestOnesList[i].Item1.Equals(c))
                            {
                                existingColorsInBestOnesList[i] = Tuple.Create(c, existingColorsInBestOnesList[i].Item2 + 1);
                            }
                        }
                    }
                    else
                    {
                        // zliczanie wagi pola
                        for (int i = 0; i < possibleFiledsNumbersInBestOnesList.Count; i++)
                        {
                            if (possibleFiledsNumbersInBestOnesList[i].Item1.Equals(index))
                            {
                                possibleFiledsNumbersInBestOnesList[i] = Tuple.Create(index, possibleFiledsNumbersInBestOnesList[i].Item2 + 1);
                            }
                        }
                    }
                }
            }

            existingColorsInBestOnesList.Sort((a, b) => b.Item2 - a.Item2);
            possibleFiledsNumbersInBestOnesList.Sort((a, b) => b.Item2 - a.Item2);

            int selectedField = -1;
            Color selectedColor = Color.White;
            int actualMaxWeight = Int32.MinValue;
            foreach (var a in maxPriorityProgressions)
            {
                int maxFiledWeight = Int32.MinValue;
                int maxColorWeight = Int32.MinValue;
                int tmpSelectedFieldNumer = -1;
                Color tmpSelectedColor = Color.White;
                // iteracja po polach w ciągu
                var possibleColors = new List<Color>();
                foreach (var index in a.Item1)
                {
                    //pole w ciągu, które nie jest pokolorowane
                    var possibleFiled = possibleFiledsNumbersInBestOnesList.Find(x => x.Item1 == index && Fields[index].Enabled);
                    if (possibleFiled != null)
                    {
                        if (maxFiledWeight < possibleFiled.Item2)
                        {
                            maxFiledWeight = possibleFiled.Item2;
                            tmpSelectedFieldNumer = index;
                        }
                    }
                    // dodaj kolor
                    possibleColors.Add(Fields[index].Color);
                }
                foreach (var c in possibleColors)
                {
                    // waga potencjalnego koloru
                    var color = existingColorsInBestOnesList.Find(x => x.Item1 == c);
                    int weight = color != null ? color.Item2 : 0;
                    if (weight > maxColorWeight)
                    {
                        maxColorWeight = weight;
                        tmpSelectedColor = color != null ? color.Item1 : Colors[r.Next(Colors.Count)];
                    }
                }
                int aWeight = maxFiledWeight + maxColorWeight;
                if (actualMaxWeight < aWeight)
                {
                    actualMaxWeight = aWeight;
                    selectedColor = tmpSelectedColor;
                    selectedField = tmpSelectedFieldNumer;
                }
            }
            return new Tuple<int, int>(selectedField + 1, Colors.IndexOf(selectedColor));
        }
    }
}
