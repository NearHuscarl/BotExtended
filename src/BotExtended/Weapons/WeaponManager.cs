using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;

namespace BotExtended.Weapons
{
    static class WeaponManager
    {
        private static List<IWeapon> m_weapons = new List<IWeapon>();

        public static void Iniialize()
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

        private static void OnUpdate(float elapsed)
        {
            foreach(var weapon in m_weapons)
            {
                weapon.Update(elapsed);
            }
        }

        private static void OnObjectDamage(IObject obj, ObjectDamageArgs arg)
        {
            ScriptHelper.LogDebug(string.Format("{0} is damaged", obj.Name));
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
    }
}
