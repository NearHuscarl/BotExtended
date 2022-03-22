using SFDGameScriptInterface;
using System;
using System.Collections.Generic;

namespace BotExtended
{
    public class BotInfo
    {
        public BotInfo()
        {
            EquipWeaponChance = 1f;
            AIType = BotAI.Debug;
            SearchItems = SearchItems.None;
            IsBoss = false;
            SpawnLine = "";
            DeathLine = "";
            SpawnLineChance = 1f;
            DeathLineChance = 1f;
            ZombieStatus = ZombieStatus.Human;
            ImmuneToInfect = false;
            SearchRange = WpnSearchRange.Infinite;
            SpecificSearchItems = new HashSet<WeaponItem>();
        }

        public BotInfo(IPlayer player) : this()
        {
            EquipWeaponChance = 0f;
            AIType = BotAI.None;
            SearchItems = player == null ? SearchItems.None : player.GetBotBehaviorSet().SearchItems;
            Modifiers = player == null ? new PlayerModifiers() : player.GetModifiers();
            SpawnLineChance = .1f;
            DeathLineChance = .1f;
        }

        private float equipWeaponChance;
        public float EquipWeaponChance
        {
            get { return equipWeaponChance; }
            set { equipWeaponChance = MathHelper.Clamp(value, 0, 1); }
        }

        public string Name { get; set; }
        public BotAI AIType { get; set; }
        public SearchItems SearchItems { get; set; }
        public HashSet<WeaponItem> SpecificSearchItems { get; set; }
        public PlayerModifiers Modifiers { get; set; }
        public bool IsBoss { get; set; }
        public float SearchRange { get; set; }
        public string SpawnLine { get; set; }
        public float SpawnLineChance { get; set; }
        public string DeathLine { get; set; }
        public float DeathLineChance { get; set; }

        private ZombieStatus zombieStatus;
        public ZombieStatus ZombieStatus
        {
            get { return zombieStatus; }
            set
            {
                if (ImmuneToInfect && value != ZombieStatus.Human)
                    throw new Exception("if ImmuneToInfect == true, ZombieStatus must be Human");
                zombieStatus = value;
            }
        }

        private bool immuneToInfect;
        public bool ImmuneToInfect
        {
            get { return immuneToInfect; }
            set
            {
                if (value == true && ZombieStatus != ZombieStatus.Human)
                    throw new Exception("if ImmuneToInfect == true, ZombieStatus must be Human");
                immuneToInfect = value;
            }
        }
    }
}
