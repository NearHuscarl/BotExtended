using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;
using BotExtended.Factions;
using System;

namespace BotExtended.Bots
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
        public BotFaction Faction { get; set; }
        public BotInfo Info { get; set; }
        public int UpdateInterval { get; set; }

        public delegate void PlayerDropWeaponCallback(IPlayer previousOwner, IObjectWeaponItem weaponObj);
        public event PlayerDropWeaponCallback PlayerDropWeaponEvent;

        public delegate void PlayerPickUpWeaponCallback(IPlayer newOwner, IObjectWeaponItem weaponObj);
        public event PlayerPickUpWeaponCallback PlayerPickUpWeaponEvent;

        public Bot()
        {
            Player = null;
            Type = BotType.None;
            Faction = BotFaction.None;
            Info = new BotInfo();
            UpdateInterval = 100;
        }
        public Bot(IPlayer player, BotFaction faction = BotFaction.None)
        {
            Player = player;
            Type = BotType.None;
            Faction = faction;
            Info = new BotInfo(player);
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

        private List<WeaponItem> m_prevWeapons = new List<WeaponItem>()
        {
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
        };

        private List<float> m_prevAmmo = new List<float>()
        {
            0, // melee 'ammo' is durability of melee weapon
            0,
            0,
            0,
        };

        private float m_lastUpdateElapsed;
        public void Update(float elapsed)
        {
            m_lastUpdateElapsed += elapsed;

            if (m_lastUpdateElapsed >= UpdateInterval)
            {
                OnUpdate(m_lastUpdateElapsed);
                m_lastUpdateElapsed = 0;
            }
            UpdateWeaponStatus();
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

        private WeaponItem CurrentWeapon(int index)
        {
            switch (index)
            {
                case 0:
                    return Player.CurrentMeleeWeapon.WeaponItem;
                case 1:
                    return Player.CurrentPrimaryWeapon.WeaponItem;
                case 2:
                    return Player.CurrentSecondaryWeapon.WeaponItem;
                case 3:
                    return Player.CurrentThrownItem.WeaponItem;
                case 4:
                    return Player.CurrentPowerupItem.WeaponItem;
            }
            return WeaponItem.NONE;
        }
        private float CurrentAmmo(int index)
        {
            switch (index)
            {
                case 0:
                    return Player.CurrentMeleeWeapon.Durability;
                case 1:
                    return Player.CurrentPrimaryWeapon.TotalAmmo;
                case 2:
                    return Player.CurrentSecondaryWeapon.TotalAmmo;
                case 3:
                    return Player.CurrentThrownItem.CurrentAmmo;
            }
            return 0;
        }

        private IObjectWeaponItem[] m_nearbyWeapons;
        private void UpdateWeaponStatus()
        {
            m_nearbyWeapons = Game.GetObjectsByArea<IObjectWeaponItem>(Player.GetAABB());

            for (var i = 0; i < m_prevAmmo.Count; i++)
            {
                if (CurrentAmmo(i) != m_prevAmmo[i])
                {
                    if (m_prevWeapons[i] == CurrentWeapon(i))
                        CheckFireWeaponEvent(i, WeaponEvent.Refill);
                    m_prevAmmo[i] = CurrentAmmo(i);
                }
            }

            for (var i = 0; i < m_prevWeapons.Count; i++)
            {
                if (CurrentWeapon(i) != m_prevWeapons[i])
                {
                    if (m_prevWeapons[i] == WeaponItem.NONE && CurrentWeapon(i) != WeaponItem.NONE)
                        CheckFireWeaponEvent(i, WeaponEvent.Pickup);
                    if (m_prevWeapons[i] != WeaponItem.NONE && CurrentWeapon(i) == WeaponItem.NONE)
                        CheckFireWeaponEvent(i, WeaponEvent.Drop);
                    if (m_prevWeapons[i] != WeaponItem.NONE && CurrentWeapon(i) != WeaponItem.NONE)
                        CheckFireWeaponEvent(i, WeaponEvent.Swap);
                    m_prevWeapons[i] = CurrentWeapon(i);
                }
            }
        }

        private enum WeaponEvent
        {
            Pickup,
            Drop,
            Swap,
            Refill,
        }
        private void CheckFireWeaponEvent(int weaponIndex, WeaponEvent weaponEvent)
        {
            // TODO: event will not fire if player pick up the same weapon when have max ammo!
            // https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3946
            IObjectWeaponItem droppedWeaponObj = null;
            IObjectWeaponItem pickedupWeaponObj = null;

            foreach (var weapon in m_nearbyWeapons)
            {
                if (weapon.WeaponItem == m_prevWeapons[weaponIndex])
                {
                    if (weaponEvent == WeaponEvent.Drop || weaponEvent == WeaponEvent.Swap)
                    {
                        droppedWeaponObj = weapon;
                    }
                }
                if (weapon.WeaponItem == CurrentWeapon(weaponIndex))
                {
                    if (weaponEvent == WeaponEvent.Pickup || weaponEvent == WeaponEvent.Swap || weaponEvent == WeaponEvent.Refill)
                    {
                        pickedupWeaponObj = weapon;
                    }
                }
            }

            // defer firing events until now to make sure drop event always fired before pickup event
            if (droppedWeaponObj != null && PlayerDropWeaponEvent != null)
                PlayerDropWeaponEvent.Invoke(Player, droppedWeaponObj);

            if (pickedupWeaponObj != null && PlayerPickUpWeaponEvent != null)
                PlayerPickUpWeaponEvent.Invoke(Player, pickedupWeaponObj);
        }

        public virtual void OnSpawn(IEnumerable<Bot> bots)
        {
            SaySpawnLine();
        }
        public virtual void OnMeleeDamage(IPlayer attacker, PlayerMeleeHitArg arg) { }
        public virtual void OnDamage(IPlayer attacker, PlayerDamageArgs args) { }
        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args) { }
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
