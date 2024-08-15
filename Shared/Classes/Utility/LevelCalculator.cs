using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Classes.Utility
{
    public static class LevelCalculator
    {
        public readonly static Dictionary<int, int> _levelToExp;
        static LevelCalculator() 
        {
            _levelToExp = InitLevelList();

        }
        private static Dictionary<int, int> InitLevelList()
        {
            //Sheet: https://docs.google.com/spreadsheets/d/10KfFQnJeBR9OxjmC0g2SIfghLot2xVLj77clQ6GA9t8/edit#gid=0
            //formula = (level/x)^y
            var x = 0.2;
            var y = 2;
            var levelList = new Dictionary<int, int>();
            for (int i = 0; i <= 100; i++)
            {
                int exp = (int)Math.Pow((i / x), y);
                levelList.Add(i, exp);
            }
            return levelList;
        }
        /// <summary>
        /// Returns level for exp amount in the level -> exp dictionary.
        /// Returns 0 if exp amount is negative.
        /// For values >= 0 returns a positive, nonzero integer
        /// Max level is 100 - 08/15/2024
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>


        public static int CalculateLevel(int exp)
        {
            int level = 0;
            if (exp < 0)
            {
                // If exp is a weird, unexpected value, just return zero.
                // The default value for level is 1 as long as the value is zero or above
                return level;
            }
            
            foreach(KeyValuePair<int, int> levelToExp in _levelToExp)
            {
               if(exp >= levelToExp.Value)
               {
                    level = levelToExp.Key;
               }
               else
               {
                    break;
               }
            }
            return level;
        }
    }
}
