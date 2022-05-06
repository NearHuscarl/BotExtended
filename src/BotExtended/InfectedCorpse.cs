using BotExtended.Factions;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using static BotExtended.Library.SFD;

namespace BotExtended
{
    class InfectedCorpse
    {
        public readonly int TimeToTurnIntoZombie;
        public int UniqueID { get; private set; }
        public BotType Type { get; set; }
        public BotFaction Faction { get; set; }
        public IPlayer Body { get; set; }
        public float DeathTime { get; private set; }
        public bool IsTurningIntoZombie { get; private set; }
        public bool CanTurnIntoZombie { get; private set; }
        public bool IsZombie { get; private set; }

        public InfectedCorpse(IPlayer player, BotType botType, BotFaction faction)
        {
            UniqueID = player.UniqueID;
            Type = botType;
            Faction = faction;
            Body = player;
            IsTurningIntoZombie = false;
            IsZombie = false;
            CanTurnIntoZombie = true;
            DeathTime = Game.TotalElapsedGameTime;
            TimeToTurnIntoZombie = RandomHelper.BetweenInt(4000, 5000);
        }

        private bool TurnIntoZombie()
        {
            if (Body.IsRemoved || Body.IsBurnedCorpse) return false;

            var player = Game.CreatePlayer(Body.GetWorldPosition());
            var zombieType = BotHelper.GetZombieType(Type);
            var oldProfile = Body.GetProfile();
            var oldWeapons = BotHelper.GetWeaponSet(Body); // TODO: test gun with lazer once gurt fixed https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=4000

            ScriptHelper.LogDebug(Type, "->", zombieType);
            player.SetBotName(Body.Name); // NOTE: set right now so SpawnLine dialogue will show the bot name correctly

            var zombie = BotManager.SpawnBot(zombieType, Faction, player, BotManager.GetBot(Body).InfectTeam);
            var zombieBody = zombie.Player;

            var modifiers = Body.GetModifiers();
            // Survivor has fake MaxHealth to have blood effect on the face
            if (Enum.GetName(typeof(BotType), BotManager.GetBot(Body).Type).StartsWith("Survivor"))
                modifiers.CurrentHealth = modifiers.MaxHealth = 50;
            else
                modifiers.CurrentHealth = modifiers.MaxHealth * 0.75f;

            zombieBody.SetModifiers(modifiers);
            zombieBody.SetProfile(BotHelper.ToZombieProfile(oldProfile));
            BotHelper.Equip(zombieBody, oldWeapons);

            Body.Remove();
            Body = zombieBody;
            Body.SetBotBehaivorActive(false);
            Body.AddCommand(new PlayerCommand(PlayerCommandType.StartCrouch));
            IsTurningIntoZombie = true;
            return true;
        }

        public void Update()
        {
            if (ScriptHelper.IsElapsed(DeathTime, TimeToTurnIntoZombie))
            {
                if (!IsTurningIntoZombie)
                {
                    CanTurnIntoZombie = TurnIntoZombie();
                }
                if (!IsZombie)
                {
                    UpdateTurningIntoZombieAnimation();
                }
            }
        }

        private bool isKneeling;
        private float kneelingTime;
        private void UpdateTurningIntoZombieAnimation()
        {
            if (!isKneeling)
            {
                kneelingTime = Game.TotalElapsedGameTime;
                isKneeling = true;
            }
            else
            {
                if (ScriptHelper.IsElapsed(kneelingTime, 700))
                {
                    Body.AddCommand(new PlayerCommand(PlayerCommandType.StopCrouch));
                    Body.SetBotBehaivorActive(true);
                    IsZombie = true;
                }
            }
        }
    }
}
