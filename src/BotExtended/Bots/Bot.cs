using SFDGameScriptInterface;
using BotExtended.Library;
using System.Collections.Generic;
using static BotExtended.Library.Mocks.MockObjects;
using BotExtended.Factions;
using System;
using BotExtended.Projectiles;
using System.Linq;

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
        public Vector2 Position
        {
            get { return Player.GetWorldPosition(); }
            set { Player.SetWorldPosition(value); }
        }

        private Bot_GravityGunAI m_botGravityGunAI;

        private Bot()
        {
            m_botGravityGunAI = new Bot_GravityGunAI(this);
        }
        public Bot(IPlayer player = null, BotFaction faction = BotFaction.None) : this()
        {
            Player = player;
            Type = BotType.None;
            Faction = faction;
            Info = new BotInfo(player);
            UpdateInterval = 100;
        }
        public Bot(BotArgs args) : this()
        {
            Player = args.Player;
            Type = args.BotType;
            Faction = args.BotFaction;
            Info = args.Info;
        }

        public void SaySpawnLine()
        {
            if (Info == null) return;

            var spawnLine = Info.SpawnLine;
            var spawnLineChance = Info.SpawnLineChance;

            if (!string.IsNullOrWhiteSpace(spawnLine) && RandomHelper.Percentage(spawnLineChance))
                Game.CreateDialogue(spawnLine, DialogueColor, Player, duration: 3000f);
        }

        public void SayDeathLine()
        {
            if (Info == null) return;

            var deathLine = Info.DeathLine;
            var deathLineChance = Info.DeathLineChance;

            if (!string.IsNullOrWhiteSpace(deathLine) && RandomHelper.Percentage(deathLineChance))
                Game.CreateDialogue(deathLine, DialogueColor, Player, duration: 3000f);
        }

        private List<WeaponItem> m_prevWeapons = new List<WeaponItem>()
        {
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
            WeaponItem.NONE,
        };

        private List<float> m_prevAmmo = new List<float>()
        {
            0, // makeshift
            0, // melee 'ammo' is durability of melee weapon
            0, // primary
            0, // secondary
            0, // throwable
            0, // powerup - should always be 0
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
            UpdateCustomWeaponAI(elapsed);
            UpdateInfectedEffect(elapsed);

            if (IsStunned && !Player.IsDeathKneeling)
                Player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));
        }

        public void LogDebug(params object[] messages)
        {
            if (Game.IsEditorTest)
            {
                var ph = ScriptHelper.GetDefaultFormatter(messages);
                Game.DrawText(string.Format(ph, messages), Position);
            }
        }

        protected virtual void OnUpdate(float elapsed) { }

        public virtual void OnPickedupWeapon(PlayerWeaponAddedArg arg) { }
        public virtual void OnDroppedWeapon(PlayerWeaponRemovedArg arg)
        {
            var gun = GetGravityGun();
            if (gun != null)
                m_botGravityGunAI.OnDroppedWeapon(arg);
        }

        private int CurrentWeaponIndex
        {
            get
            {
                switch (Player.CurrentWeaponDrawn)
                {
                    case WeaponItemType.Melee:
                        if (Player.CurrentMeleeMakeshiftWeapon.WeaponItem != WeaponItem.NONE)
                            return 0;
                        return 1;
                    case WeaponItemType.Rifle:
                        return 2;
                    case WeaponItemType.Handgun:
                        return 3;
                    case WeaponItemType.Thrown:
                        return 4;
                    case WeaponItemType.Powerup:
                        return 5;
                }
                return -1;
            }
        }
        public WeaponItem CurrentWeapon
        {
            get { return GetCurrentWeapon(CurrentWeaponIndex); }
        }
        public float CurrentAmmo
        {
            get { return GetCurrentAmmo(CurrentWeaponIndex); }
            set { SetCurrentAmmo(CurrentWeaponIndex, value); }
        }

        private WeaponItem GetCurrentWeapon(int index)
        {
            switch (index)
            {
                case 0:
                    return Player.CurrentMeleeMakeshiftWeapon.WeaponItem;
                case 1:
                    return Player.CurrentMeleeWeapon.WeaponItem;
                case 2:
                    return Player.CurrentPrimaryWeapon.WeaponItem;
                case 3:
                    return Player.CurrentSecondaryWeapon.WeaponItem;
                case 4:
                    return Player.CurrentThrownItem.WeaponItem;
                case 5:
                    return Player.CurrentPowerupItem.WeaponItem;
            }
            return WeaponItem.NONE;
        }
        public float GetCurrentAmmo(int index)
        {
            switch (index)
            {
                case 0:
                    return Player.CurrentMeleeMakeshiftWeapon.Durability;
                case 1:
                    return Player.CurrentMeleeWeapon.Durability;
                case 2:
                    return Player.CurrentPrimaryWeapon.TotalAmmo;
                case 3:
                    return Player.CurrentSecondaryWeapon.TotalAmmo;
                case 4:
                    return Player.CurrentThrownItem.CurrentAmmo;
            }
            return 0;
        }
        public void SetCurrentAmmo(int currentWeaponIndex, float value)
        {
            switch (currentWeaponIndex)
            {
                case 0:
                    Player.SetCurrentMeleeMakeshiftDurability(value);
                    break;
                case 1:
                    Player.SetCurrentMeleeDurability(value);
                    break;
                case 2:
                    Player.SetCurrentPrimaryWeaponAmmo((int)value);
                    break;
                case 3:
                    Player.SetCurrentSecondaryWeaponAmmo((int)value);
                    break;
                case 4:
                    Player.SetCurrentThrownItemAmmo((int)value);
                    break;
            }
        }

        private void UpdateWeaponStatus()
        {
            for (var i = 0; i < m_prevWeapons.Count; i++)
            {
                if (GetCurrentWeapon(i) != m_prevWeapons[i])
                {
                    m_prevWeapons[i] = GetCurrentWeapon(i);
                }
            }

            for (var i = 0; i < m_prevAmmo.Count; i++)
            {
                m_prevAmmo[i] = GetCurrentAmmo(i);
            }
        }

        private GravityGun GetGravityGun()
        {
            var playerWeapon = ProjectileManager.GetOrCreatePlayerWeapon(Player);
            if (playerWeapon == null) return null;

            if (playerWeapon.Primary.Powerup == RangedWeaponPowerup.Gravity
                || playerWeapon.Primary.Powerup == RangedWeaponPowerup.GravityDE)
                return (GravityGun)playerWeapon.Primary;
            if (playerWeapon.Secondary.Powerup == RangedWeaponPowerup.Gravity
                || playerWeapon.Secondary.Powerup == RangedWeaponPowerup.GravityDE)
                return (GravityGun)playerWeapon.Secondary;

            return null;
        }

        private void UpdateCustomWeaponAI(float elapsed)
        {
            if (!Player.IsBot) return;

            // TODO: Avoid disabling input because it's error-prone
            // Equip the weapon and ammo manually and remove the IObjectWeaponItem
            // if gurt adds this https://www.mythologicinteractiveforums.com/viewtopic.php?f=31&t=3986
            //foreach (var nearbyWeapon in m_nearbyWeapons)
            //{
            //    Game.DrawArea(nearbyWeapon.GetAABB(), Color.Grey);
            //    if (Info.SpecificSearchItems.Contains(nearbyWeapon.WeaponItem)
            //        && !Player.IsStaggering && !Player.IsStunned && Player.IsOnGround)
            //    {
            //        Player.SetInputEnabled(false);
            //        Player.AddCommand(new PlayerCommand(PlayerCommandType.Activate, nearbyWeapon.UniqueID));
            //        ScriptHelper.Timeout(() =>
            //        {
            //            Player.ClearCommandQueue();
            //            Player.SetInputEnabled(true);
            //        }, 10);
            //        break;
            //    }
            //}

            var botBehaviorSet = Player.GetBotBehaviorSet();

            if (botBehaviorSet.RangedWeaponMode != BotBehaviorRangedWeaponMode.HipFire)
            {
                var gun = GetGravityGun();
                if (gun != null)
                    m_botGravityGunAI.Update(elapsed, gun);
            }
        }

        private float m_bloodEffectElapsed = 0;
        private void UpdateInfectedEffect(float elapsed)
        {
            if (Info.ZombieStatus == ZombieStatus.Infected && !Player.IsRemoved && !Player.IsBurnedCorpse)
            {
                m_bloodEffectElapsed += elapsed;

                if (m_bloodEffectElapsed > 300)
                {
                    Game.PlayEffect(EffectName.BloodTrail, Position);
                    m_bloodEffectElapsed = 0;
                }
            }
        }

        public virtual void OnSpawn(IEnumerable<Bot> bots)
        {
            SaySpawnLine();
        }
        public virtual void OnMeleeDamage(IPlayer attacker, PlayerMeleeHitArg arg) { }
        public virtual void OnDamage(IPlayer attacker, PlayerDamageArgs args)
        {
            UpdateInfectedStatus(attacker, args);
        }

        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args)
        {
            var player = Game.GetPlayer(projectile.InitialOwnerPlayerID);

            if (player == null) return;

            var bot = BotManager.GetBot(player) as CowboyBot;

            if (bot != null)
            {
                if (args.IsCrit)
                {
                    var destroyWeapon = RandomHelper.Percentage(bot.DestroyWeaponWhenCritDisarmChance);
                    if (RandomHelper.Percentage(bot.CritDisarmChance))
                        Disarm(projectile.Direction, destroyWeapon);
                }
                else
                {
                    var destroyWeapon = RandomHelper.Percentage(bot.DestroyWeaponWhenDisarmChance);
                    if (RandomHelper.Percentage(bot.DisarmChance))
                        Disarm(projectile.Direction, destroyWeapon);
                }
            }
        }
        public virtual void OnDeath(PlayerDeathArgs args)
        {
            if (args.Killed)
            {
                SayDeathLine();
            }
        }

        public virtual void OnPlayerKeyInput(VirtualKeyInfo[] keyInfos) { }

        public bool CanInfect { get { return Info.ZombieStatus != ZombieStatus.Human; } }
        public bool CanBeInfected { get { return !CanInfect && !Info.ImmuneToInfect && !Player.IsBurnedCorpse; } }
        public bool IsInfectedByZombie { get { return Info.ZombieStatus == ZombieStatus.Infected; } }
        private void UpdateInfectedStatus(IPlayer attacker, PlayerDamageArgs args)
        {
            if (CanBeInfected && attacker != null)
            {
                var directContact = args.DamageType == PlayerDamageEventType.Melee
                    && attacker.CurrentWeaponDrawn == WeaponItemType.NONE
                    && !attacker.IsKicking && !attacker.IsJumpKicking;
                var attackerBot = BotManager.GetBot(attacker);

                if (attackerBot.CanInfect && directContact)
                {
                    Infect();
                    Game.ShowChatMessage(attacker.Name + " infected " + Player.Name);
                }
            }
        }

        public void Infect()
        {
            if (CanBeInfected)
            {
                Game.PlayEffect(EffectName.CustomFloatText, Position, "infected");
                Info.ZombieStatus = ZombieStatus.Infected;
            }
        }

        public void Disarm(Vector2 projDirection, bool destroyWeapon = false)
        {
            if (Player.CurrentWeaponDrawn == WeaponItemType.NONE) return;

            var velocity =
                    Vector2.UnitX * RandomHelper.Between(.25f, 2.5f) * Math.Sign(projDirection.X) +
                    Vector2.UnitY * RandomHelper.Between(.25f, 2.5f);

            if (Math.Sign(projDirection.X) == Math.Sign(Player.GetLinearVelocity().X))
                velocity += Vector2.UnitX * (Player.GetLinearVelocity().X / 2);

            Game.PlayEffect(EffectName.CustomFloatText, Position + Vector2.UnitY * 15, "Disarmed");

            // TODO: check if gurt fixed grenade diarm bug
            // https://www.mythologicinteractiveforums.com/viewtopic.php?f=18&t=3991
            var weapon = Player.Disarm(Player.CurrentWeaponDrawn, velocity, false);
            if (destroyWeapon)
                weapon.SetHealth(0);
        }

        public bool IsStunned { get; private set; }
        private Events.UpdateCallback m_effect;
        public void Stun(uint stunnedTime, Action<float> effect, uint effectTime = 0)
        {
            IsStunned = true;
            Player.SetInputEnabled(false);
            Player.AddCommand(new PlayerCommand(PlayerCommandType.DeathKneelInfinite));

            m_effect = Events.UpdateCallback.Start(effect, effectTime);

            ScriptHelper.Timeout(() =>
            {
                Player.AddCommand(new PlayerCommand(PlayerCommandType.StopDeathKneel));
                Player.SetInputEnabled(true);
                IsStunned = false;
                Events.UpdateCallback.Stop(m_effect);
                m_effect = null;
            }, stunnedTime);
        }

        // set modifiers without changing current health and energy
        public void SetModifiers(PlayerModifiers modifiers)
        {
            modifiers.CurrentHealth = Player.GetHealth();
            modifiers.CurrentEnergy = Player.GetEnergy();
            Player.SetModifiers(modifiers);
        }
    }
}
