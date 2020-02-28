using System.Collections.Generic;

namespace EFT_Launcher_12 
{
    public class ProfileExtended
    {
        public Info info;
        public Skills skills;
        public Hideout hideout;

        public class Info
        {
            public string nickname;
            public string side;
            public int experience;
            public string gameVersion;
        }

        public class Skills
        {
            public List<Skill> common;
            public class Skill
            {
                public string id;
                public decimal progress;
                public decimal pointsEarnedDuringSession;
                public int lastAcces;
            }
        }

        public class Hideout
        {
            public List<Area> areas;
            public partial class Area
            {
                public int type;
                public int level;
                public bool active;
                public bool passiveBonusesEnabled;
				public int completeTime;
				public bool constructing;
				public object[] slots;
            }
        }
    }
}
