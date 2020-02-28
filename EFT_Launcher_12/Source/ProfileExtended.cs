using System.Collections.Generic;

namespace EFT_Launcher_12
{
    public class Settings
    {
        public string Role;
        public string BotDifficulty;
        public int Experience;
    }

    public class Info
    {
        public string Nickname;
        public string LowerNickname;
        public string Side;
        public string Voice;
        public int Level;
        public int Experience;
        public int RegistrationDate;
        public string GameVersion;
        public int AccountType;
        public string MemberCategory;
        public bool lockedMoveCommands;
        public int SavageLockTime;
        public int LastTimePlayedAsSavage;
        public Settings Settings;
        public bool NeedWipe;
        public bool GlobalWipe;
        public int NicknameChangeDate;
        public List<object> Bans;
    }

    public class Customization
    {
        public string Head;
        public string Body;
        public string Feet;
        public string Hands;
    }

    public class Hydration
    {
        public int Current;
        public int Maximum;
    }

    public class Energy
    {
        public int Current;
        public int Maximum;
    }

    public class Health2
    {
        public int Current;
        public int Maximum;
    }

    public class Head
    {
        public Health2 Health;
    }

    public class Health3
    {
        public int Current;
        public int Maximum;
    }

    public class Chest
    {
        public Health3 Health;
    }

    public class Health4
    {
        public int Current;
        public int Maximum;
    }

    public class Stomach
    {
        public Health4 Health;
    }

    public class Health5
    {
        public int Current;
        public int Maximum;
    }

    public class LeftArm
    {
        public Health5 Health;
    }

    public class Health6
    {
        public int Current;
        public int Maximum;
    }

    public class RightArm
    {
        public Health6 Health;
    }

    public class Health7
    {
        public int Current;
        public int Maximum;
    }

    public class LeftLeg
    {
        public Health7 Health;
    }

    public class Health8
    {
        public int Current;
        public int Maximum;
    }

    public class RightLeg
    {
        public Health8 Health;
    }

    public class BodyParts
    {
        public Head Head;
        public Chest Chest;
        public Stomach Stomach;
        public LeftArm LeftArm;
        public RightArm RightArm;
        public LeftLeg LeftLeg;
        public RightLeg RightLeg;
    }

    public class Health
    {
        public Hydration Hydration;
        public Energy Energy;
        public BodyParts BodyParts;
        public int UpdateTime;
    }

    public class Foldable
    {
        public bool Folded;
    }

    public class Tag
    {
        public int Color;
        public string Name;
    }

    public class Upd
    {
        public int StackObjectsCount;
        public Foldable Foldable;
        public Tag Tag;
    }

    public class Item
    {
        public string _id;
        public string _tpl;
        public string parentId;
        public string slotId;
        public Upd upd;
        public object location;
    }

    public class FastPanel
    {
		// intentionally empty
	}

	public class Inventory
    {
        public List<Item> items;
        public string equipment;
        public string stash;
        public string questRaidItems;
        public string questStashItems;
        public FastPanel fastPanel;
    }

    public class Common
    {
        public string Id;
        public decimal Progress;
        public decimal MaxAchieved;
        public int LastAccess;
    }

    public class Skills
    {
        public List<Common> Common;
        public List<object> Mastering;
        public int Points;
    }

    public class OverallCounters
    {
       public List<object> Items;
    }

    public class Stats
    {
        public object SessionCounters;
        public OverallCounters OverallCounters;
        public double SessionExperienceMult;
        public double TotalSessionExperience;
        public int LastSessionDate;
        public object Aggressor;
        public List<object> DroppedItems;
        public List<object> FoundInRaidItems;
        public List<object> Victims;
        public List<object> CarriedQuestItems;
        public int TotalInGameTime;
        public string Survivorclass;
    }

    public class Encyclopedia
    {
		// intentionally empty
	}

	public class ConditionCounters
    {
        public List<object> Counters;
    }

    public class BackendCounters
    {
		// intentionally empty
    }

    public class Production
    {
        public int Progress;
        public bool inProgress;
        public string RecipeId;
        public List<object> Products;
        public int StartTime;
    }

    public class Area
    {
        public int type;
        public int level;
        public bool active;
        public bool passiveBonusesEnabled;
        public int completeTime;
        public bool conclassing;
        public List<object> slots;
    }

    public class Hideout
    {
        public Production Production;
        public List<Area> Areas;
    }

    public class Bonus
    {
        public string type;
        public string templateId;
    }

    public class Notes
    {
        private List<object> notes;

        public List<object> GetNotes()
        {
            return notes;
        }

        public void SetNotes(List<object> value)
        {
            notes = value;
        }
    }

    public class TraderStandings
    {
		// intentionally empty
    }

    public class RagfairInfo
    {
        public double rating;
        public bool isRatingGrowing;
        public List<object> offers;
    }

    public class ProfileExtended
    {
        public string _id;
        public int aid;
        public string savage;
        public Info Info;
        public Customization Customization;
        public Health Health;
        public Inventory Inventory;
        public Skills Skills;
        public Stats Stats;
        public Encyclopedia Encyclopedia;
        public ConditionCounters ConditionCounters;
        public BackendCounters BackendCounters;
        public List<object> InsuredItems;
        public Hideout Hideout;
        public List<Bonus> Bonuses;
        public Notes Notes;
        public List<object> Quests;
        public TraderStandings TraderStandings;
        public RagfairInfo RagfairInfo;
        public List<object> WishList;
    }
}
