using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BotExtended.Weapons
{
    class TurretPlaceholderInfo
    {
        public TurretPlaceholder Placeholder;
        public IPlayer Builder;
    }

    static class WeaponManager
    {
        private static List<IWeapon> m_weapons = new List<IWeapon>();
        private static Dictionary<int, TurretPlaceholderInfo> m_turretPlaceholders = new Dictionary<int, TurretPlaceholderInfo>();

        public static void Initialize()
        {
            Events.UpdateCallback.Start(OnUpdate);
            Events.ObjectDamageCallback.Start(OnObjectDamage);
        }

        public static void SpawnTurret(IPlayer owner)
        {
            var position = owner.GetWorldPosition();
            var direction = owner.FacingDirection > 0 ? TurretDirection.Right : TurretDirection.Left;
            m_weapons.Add(new Turret(position, direction, owner));
        }

        public static IEnumerable<T> GetWeapons<T>() where T : class, IWeapon
        {
            foreach (var weapon in m_weapons)
            {
                var w = weapon as T;
                if (w != null) yield return w;
            }
        }

        private static void OnUpdate(float elapsed)
        {
            foreach(var weapon in m_weapons)
            {
                weapon.Update(elapsed);
            }
        }

        private static void OnObjectDamage(IObject obj, ObjectDamageArgs arg)
        {
            //ScriptHelper.LogDebug(string.Format("{0} is damaged", obj.Name));
            foreach (var weapon in m_weapons)
            {
                foreach (var component in weapon.Components)
                {
                    if (obj.UniqueID == component.UniqueID)
                    {
                        weapon.OnDamage(obj);
                    }
                }
            }
        }

        public static TurretPlaceholder CreateTurretPlaceholder(IPlayer player)
        {
            var turretDirection = player.GetFaceDirection() == 1 ? TurretDirection.Right : TurretDirection.Left;
            var placeholder = new TurretPlaceholder(player.GetWorldPosition(), turretDirection, player);

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
