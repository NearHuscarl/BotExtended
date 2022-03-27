﻿using BotExtended.Bots;
using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BotExtended.Library.SFD;

namespace BotExtended.Powerups.MeleeWeapons
{
    class SeriousPowerup : MeleeWpn
    {
        public SeriousPowerup(IPlayer owner, WeaponItem name) : base(owner, name, MeleeWeaponPowerup.Serious) { }

        private float _cooldownTime = 0f;

        protected override void OnMeleeActionChanged(MeleeAction meleeAction, Vector2 hitPosition)
        {
            base.OnMeleeActionChanged(meleeAction, hitPosition);

            if (Owner.IsDead || meleeAction != MeleeAction.Three || !ScriptHelper.IsElapsed(_cooldownTime, Game.IsEditorTest ? 0 : 12000)) return;

            _cooldownTime = Game.TotalElapsedGameTime;

            var dir = Owner.GetFaceDirection();
            var velocity = ScriptHelper.GetDirection(dir == 1 ? MathExtension.PIOver4 : MathExtension.PI - MathExtension.PIOver4) * 5;

            var pos = Owner.GetWorldPosition();
            var area = ScriptHelper.Area(pos, pos + Vector2.UnitX * dir * 110 + Vector2.UnitY * 60);
            var objects = Game.GetObjectsByArea(area);

            if (objects.Length == 0) return;

            Game.RunCommand("/settime 0.1");
            ScriptHelper.Timeout(() => Game.RunCommand("/settime 1"), 800);

            var thrownObjects = new List<IObject>();
            foreach (var o in objects)
            {
                if (o.UniqueID == Owner.UniqueID || o.Name.Contains("Debris")) continue;

                if (ScriptHelper.IsDynamicObject(o))
                    o.Destroy();
                
                if (ScriptHelper.IsPlayer(o))
                {
                    var direction = dir == -1 ? RandomHelper.Direction(100, 170) : RandomHelper.Direction(10, 80);
                    var player = (IPlayer)o;
                    var profile = player.GetProfile();
                    var stripeableClothingTypes = ScriptHelper.StrippeableClothingTypes(profile);

                    thrownObjects.Add(player);
                    player.SetLinearVelocity(direction * RandomHelper.Between(10, 25));
                    BotManager.GetBot(player).DisarmAll();
                    ScriptHelper.Fall2(player);

                    if (!stripeableClothingTypes.Any() && !ScriptHelper.HaveUnderwear(profile)) continue;

                    if (!stripeableClothingTypes.Any())
                    {
                        var strippedProfile = ScriptHelper.StripUnderwear(profile);
                        player.SetProfile(strippedProfile);
                    }
                    else
                    {
                        var clothingTypes = RandomHelper.Boolean() ?
                            stripeableClothingTypes :
                            stripeableClothingTypes.Take((int)Math.Ceiling(stripeableClothingTypes.Count / 2d));
                        foreach (var ct in clothingTypes)  profile = ScriptHelper.Strip(profile, ct);
                        player.SetProfile(profile);
                    }
                }
            }

            ScriptHelper.Timeout(() =>
            {
                var debrisObjects = Game.GetObjectsByArea(area)
                .Where(x => x.GetLinearVelocity().Length() <= 30 && (x.Name.Contains("Debris") || x.Name.StartsWith("Wpn"))).ToList();

                foreach (var o in debrisObjects)
                {
                    thrownObjects.Add(o);
                    var direction = dir == -1 ? RandomHelper.Direction(100, 170) : RandomHelper.Direction(10, 80);
                    o.SetLinearVelocity(direction * RandomHelper.Between(10, 25));
                    o.SetAngularVelocity(RandomHelper.Between(-20, 20));
                }
            }, 0);

            var isElapsedPlayEffect = ScriptHelper.WithIsElapsed(75);
            ScriptHelper.RunUntil(() =>
            {
                if (!isElapsedPlayEffect()) return;
                
                foreach (var o in thrownObjects.ToList())
                {
                    if (o.IsRemoved || o.GetLinearVelocity() == Vector2.Zero) thrownObjects.Remove(o);
                    else
                        Game.PlayEffect(EffectName.ChainsawSmoke, o.GetWorldPosition());
                }
            }, () => thrownObjects.Count == 0);
        }
    }
}
