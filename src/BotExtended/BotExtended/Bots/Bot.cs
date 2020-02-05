using SFDGameScriptInterface;
using SFDScript.Library;
using System.Collections.Generic;
using static SFDScript.Library.Mocks.MockObjects;

namespace SFDScript.BotExtended.Bots
{
    public class Bot
    {
        private static readonly Bot none = new Bot();
        public static Bot None
        {
            get { return none;  }
        }

        public static Color DialogueColor
        {
            get { return new Color(128, 32, 32); }
        }
        public IPlayer Player { get; set; }
        public BotType Type { get; set; }
        public BotInfo Info { get; set; }
        public int UpdateInterval { get; set; }

        public Bot()
        {
            Player = null;
            Type = BotType.None;
            Info = new BotInfo();
            UpdateInterval = 100;
        }
        public Bot(IPlayer player)
        {
            Player = player;
            Type = BotType.None;
            Info = new BotInfo(player);
        }

        public void Decorate(IPlayer existingPlayer)
        {
            existingPlayer.CustomID = Player.CustomID;
            existingPlayer.SetProfile(Player.GetProfile());

            existingPlayer.GiveWeaponItem(Player.CurrentMeleeWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentMeleeMakeshiftWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentPrimaryWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentSecondaryWeapon.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentThrownItem.WeaponItem);
            existingPlayer.GiveWeaponItem(Player.CurrentPowerupItem.WeaponItem);

            existingPlayer.SetBotBehavior(Player.GetBotBehavior());

            existingPlayer.SetTeam(Player.GetTeam());
            existingPlayer.SetModifiers(Player.GetModifiers());
            existingPlayer.SetHitEffect(Player.GetHitEffect());
        }

        public void SaySpawnLine()
        {
            if (Info == null) return;

            var spawnLine = Info.SpawnLine;
            var spawnLineChance = Info.SpawnLineChance;

            if (!string.IsNullOrWhiteSpace(spawnLine) && RandomHelper.Between(0f, 1f) < spawnLineChance)
                GameScriptInterface.Game.CreateDialogue(spawnLine, DialogueColor, Player, duration: 3000f);
        }

        public void SayDeathLine()
        {
            if (Info == null) return;

            var deathLine = Info.DeathLine;
            var deathLineChance = Info.DeathLineChance;

            if (!string.IsNullOrWhiteSpace(deathLine) && RandomHelper.Between(0f, 1f) < deathLineChance)
                Game.CreateDialogue(deathLine, DialogueColor, Player, duration: 3000f);
        }

        private int m_lastUpdateElapsed;
        public void Update(float elapsed)
        {
            m_lastUpdateElapsed += (int)elapsed;

            if (m_lastUpdateElapsed >= UpdateInterval)
            {
                OnUpdate(m_lastUpdateElapsed + elapsed);
                m_lastUpdateElapsed = 0;
            }
        }

        private float m_bloodEffectElapsed = 0;
        protected virtual void OnUpdate(float elapsed)
        {
            if (Info.ZombieStatus == ZombieStatus.Infected && !Player.IsRemoved && !Player.IsBurnedCorpse)
            {
                m_bloodEffectElapsed += elapsed;

                if (m_bloodEffectElapsed > 300)
                {
                    var position = Player.GetWorldPosition();
                    Game.PlayEffect(EffectName.BloodTrail, position);
                    m_bloodEffectElapsed = 0;
                }
            }
        }
        public virtual void OnSpawn(List<Bot> bots) { }
        public virtual void OnMeleeDamage(IPlayer attacker, PlayerMeleeHitArg arg) { }
        public virtual void OnDamage(IPlayer attacker, PlayerDamageArgs args) { }
        public virtual void OnDeath(PlayerDeathArgs args) { }

        protected IPlayer FindClosestTarget()
        {
            var position = Player.GetWorldPosition();
            IPlayer target = null;

            foreach (var player in Game.GetPlayers())
            {
                var result = ScriptHelper.IsDifferentTeam(player, Player);
                if (player.IsDead || player.IsRemoved || !ScriptHelper.IsDifferentTeam(player, Player))
                    continue;

                if (target == null) target = player;

                var targetDistance = Vector2.Distance(target.GetWorldPosition(), position);
                var potentialTargetDistance = Vector2.Distance(player.GetWorldPosition(), position);

                if (potentialTargetDistance < targetDistance)
                {
                    target = player;
                }
            }

            return target;
        }
    }
}
