﻿using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Text;
using static ChallengePlus.SFD;

namespace ChallengePlus
{
    public abstract class IChallenge
    {
        public virtual ChallengeName Name { get; protected set; }
        public virtual string Description { get; protected set; }

        public virtual void OnSpawn(IPlayer[] players) { }
        public virtual void OnUpdate(float e) { }

        public virtual void OnPlayerCreated(Player player) { }
        public virtual void OnPlayerKeyInput(Player player, VirtualKeyInfo[] keyInfos) { }
        public virtual void OnUpdate(float e, Player player) { }
        public virtual void OnPlayerDamage(Player player, PlayerDamageArgs args, Player attacker) { }
        public virtual void OnPlayerWeaponAdded(Player player, PlayerWeaponAddedArg args) { }
        public virtual void OnPlayerDealth(Player player, PlayerDeathArgs args) { }

        public virtual void OnObjectCreated(IObject obj) { }
        public virtual void OnObjectDamage(IObject o, ObjectDamageArgs args) { }
        public virtual void OnObjectTerminated(IObject obj) { }

        public virtual void OnProjectileCreated(IProjectile projectile) { }
        public virtual void OnUpdate(float e, IProjectile projectile) { }
        public virtual void OnProjectileHit(IProjectile projectile, ProjectileHitArgs args) { }
    }
}
