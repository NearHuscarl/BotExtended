using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using static BotExtended.Library.SFD;

namespace BotExtended.Weapons
{
    class TurretPlaceholderInfo
    {
        public TurretPlaceholder Placeholder;
        public IPlayer Builder;
    }

    static class WeaponManager
    {
        private static List<Weapon> m_weapons = new List<Weapon>();
        private static Dictionary<int, TurretPlaceholderInfo> m_turretPlaceholders = new Dictionary<int, TurretPlaceholderInfo>();

        public static void Initialize()
        {
            Events.UpdateCallback.Start(OnUpdate);
            Events.ObjectDamageCallback.Start(OnObjectDamage);
            Events.ObjectTerminatedCallback.Start(OnObjectTerminated);
        }

        public static IEnumerable<T> GetWeapons<T>() where T : Weapon
        {
            foreach (var weapon in m_weapons)
            {
                var w = weapon as T;
                if (w != null) yield return w;
            }
        }

        public static Weapon SpawnWeapon(BeWeapon weapon, object arg)
        {
            var wpn = WeaponFactory.Create(weapon, arg);
            m_weapons.Add(wpn);
            return wpn;
        }

        private static float m_lastUpdateTime = 0f;
        private static void OnUpdate(float elapsed)
        {
            foreach (var weapon in m_weapons.ToList())
            {
                weapon.Update(Game.TotalElapsedGameTime - m_lastUpdateTime);

                if (weapon.IsDestroyed)
                    m_weapons.Remove(weapon);
            }

            foreach (var placeholderInfo in m_turretPlaceholders.Values.ToList())
            {
                var placeholder = placeholderInfo.Placeholder;

                placeholder.Update(Game.TotalElapsedGameTime - m_lastUpdateTime);

                if (placeholder.IsRemoved)
                    placeholder.Remove();
            }

            m_lastUpdateTime = Game.TotalElapsedGameTime;
        }

        private static void OnObjectDamage(IObject obj, ObjectDamageArgs arg)
        {
            if (string.IsNullOrEmpty(obj.CustomID)) return;

            foreach (var weapon in m_weapons)
            {
                foreach (var component in weapon.Components.ToList())
                {
                    if (obj.UniqueID == component.UniqueID)
                    {
                        weapon.OnDamage(obj, arg);
                    }
                }
            }
        }

        private static void OnObjectTerminated(IObject[] objects)
        {
            foreach (var o in objects)
            {
                // improve performance
                if (string.IsNullOrEmpty(o.CustomID)) continue;

                foreach (var weapon in m_weapons.ToList())
                {
                    foreach (var component in weapon.Components.ToList())
                    {
                        if (o.UniqueID == component.UniqueID)
                        {
                            weapon.OnComponentTerminated(o);
                        }
                    }
                }
            }
        }

        public static TurretPlaceholder CreateTurretPlaceholder(IPlayer player, TurretDirection direction)
        {
            var placeholder = new TurretPlaceholder(player.GetWorldPosition(), direction, player);

            m_turretPlaceholders.Add(placeholder.UniqueID, new TurretPlaceholderInfo()
            {
                Builder = player,
                Placeholder = placeholder,
            });
            return placeholder;
        }

        public static IEnumerable<KeyValuePair<int, TurretPlaceholderInfo>> GetUntouchedTurretPlaceholders()
        {
            return m_turretPlaceholders.Where((p) => p.Value.Builder == null);
        }
        public static void RemoveBuilderFromTurretPlaceholder(int uniqueID)
        {
            if (m_turretPlaceholders.ContainsKey(uniqueID))
                m_turretPlaceholders[uniqueID].Builder = null;
        }
        public static void AddBuilderToTurretPlaceholder(int uniqueID, IPlayer builder) { m_turretPlaceholders[uniqueID].Builder = builder; }
        public static void RemoveTurretPlaceholder(int uniqueID) { m_turretPlaceholders.Remove(uniqueID); }
    }
}
