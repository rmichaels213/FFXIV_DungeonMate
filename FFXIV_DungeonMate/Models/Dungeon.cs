using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace FFXIV_DungeonMate.Models
{
    /// <summary>
    /// A class to handle Dungeon data.
    /// </summary>
    [Serializable]
    public class Dungeon
    {
        public int Id { get; set; }
        public char Symbol { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public string Description { get; set; }
        public string Loot { get; set; }
        public string Rewards { get; set; }

        public string BestTime { get; set; }

        public List<string> DynamicNotes { get; set; }

        public string Note1Name { get; set; }
        public string Note2Name { get; set; }
        public string Note3Name { get; set; }
        public string Note4Name { get; set; }
        public string Note5Name { get; set; }
        public string Note6Name { get; set; }
        public string Note7Name { get; set; }
        public string Note8Name { get; set; }
        public string Note9Name { get; set; }
        public string Note10Name { get; set; }

        public string Note1 { get; set; }
        public string Note2 { get; set; }
        public string Note3 { get; set; }
        public string Note4 { get; set; }
        public string Note5 { get; set; }
        public string Note6 { get; set; }
        public string Note7 { get; set; }
        public string Note8 { get; set; }
        public string Note9 { get; set; }
        public string Note10 { get; set; }

        public string FullName()
        {
            return Name + " (Lvl " + Level + ")";
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
