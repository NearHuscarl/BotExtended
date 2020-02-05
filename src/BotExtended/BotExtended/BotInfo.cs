using SFDGameScriptInterface;
using System;

namespace SFDScript.BotExtended
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
            SpawnLineChance = 1f;
            DeathLine = "";
            DeathLineChance = 1f;
            ZombieStatus = ZombieStatus.Human;
            ImmuneToInfect = false;
        }

        public BotInfo(IPlayer player)
        {
            EquipWeaponChance = 0f;
            AIType = BotAI.None;
            SearchItems = player.GetBotBehaviorSet().SearchItems;
            Modifiers = player.GetModifiers();
            IsBoss = false;
            SpawnLine = "";
            SpawnLineChance = 0f;
            DeathLine = "";
            DeathLineChance = 0f;
            ZombieStatus = ZombieStatus.Human;
            ImmuneToInfect = false;
        }

        private float equipWeaponChance;
        public float EquipWeaponChance
        {
            get { return equipWeaponChance; }
            set { equipWeaponChance = MathHelper.Clamp(value, 0, 1); }
        }

        public BotAI AIType { get; set; }
        public SearchItems SearchItems { get; set; }
        public PlayerModifiers Modifiers { get; set; }
        public bool IsBoss { get; set; }
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
