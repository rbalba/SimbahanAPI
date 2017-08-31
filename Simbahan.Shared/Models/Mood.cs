using System.Collections.Generic;

namespace Simbahan.Models
{
    public class Mood
    {
        public static readonly int ANGRY = 1;
        public static readonly int ALONE = 2;
        public static readonly int CONFUSED = 3;
        public static readonly int FINANCIALLYSTRESSED = 4;
        public static readonly int REJECTED = 5;
        public static readonly int WORRIED = 6;
        public static readonly int WEAK = 7;
        public static readonly int BITTER = 8;
        public static readonly int TEMPTED = 9;
        public static readonly int GRATEFUL = 10;
        public static readonly int PEACEFUL = 11;
        public static readonly int PEACE = 11;
        public static readonly int JOYFUL = 12;
        public static readonly int FAITHFUL = 13;
        public static readonly int HOPEFUL = 14;
        public static readonly int LOVE = 15;
        private readonly Dictionary<int, string> MoodDictionary;
        private readonly Dictionary<string, int> InverseMoodDictionary;


        public Mood()
        {
            MoodDictionary = new Dictionary<int, string>();
            InverseMoodDictionary = new Dictionary<string, int>();

            foreach (var property in GetType().GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static))
            {
                var key = (int) property.GetValue(this);

                if (MoodDictionary.ContainsKey(key))
                    continue;

                MoodDictionary.Add(key, property.Name.ToLower());
                InverseMoodDictionary.Add(property.Name.ToLower(), key);
            }
        }

        public string GetValue(int mood)
        {
            return MoodDictionary[mood];
        }

        public int GetKey(string val)
        {
            var value = val.ToLower();

            return InverseMoodDictionary[value];
        }
    }
}