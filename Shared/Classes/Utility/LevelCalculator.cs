using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestGiver.Shared.Classes.Utility
{
    public static class LevelCalculator
    {
        static Dictionary<int, int> _levelToExp;
        static LevelCalculator() 
        {
            _levelToExp = InitLevelList();

        }
        private static Dictionary<int, int> InitLevelList()
        {
            //formula = (level/x)^y
            var x = 0.2;
            var y = 2;
            var levelList = new Dictionary<int, int>();
            for (int i = 0; i <= 100; i++)
            {
                var exp = (int)Math.Pow((i / x), y);
                levelList.Add(i, exp);
            }
            return levelList;
        }

        public static int CalculateLevel(int exp)
        {
            int level = 0;
            if (exp < 0)
            {
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
