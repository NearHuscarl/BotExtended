using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EffectName
{
    public partial class EffectName : GameScriptInterface
    {
        /// <summary>
        /// Placeholder constructor that's not to be included in the ScriptWindow!
        /// </summary>
        public EffectName() : base(null) { }

        public class EffectData
        {
            public EffectData(string id, string name, object[] args = null) { ID = id; Name = name; Args = args ?? new object[] { }; }
            public string ID;
            public string Name;
            public object[] Args;
        }

        private List<EffectData> Effects = new List<EffectData>
        {
            new EffectData("ACS", "AcidSplash"),
            new EffectData("Block", "Block"),
            new EffectData("BLD", "Blood"),
            new EffectData("TR_B", "BloodTrail"),
            new EffectData("BulletHit", "BulletHit"),
            new EffectData("BulletHitCloth", "BulletHitCloth"),
            new EffectData("BulletHitDefault", "BulletHitDefault"),
            new EffectData("BulletHitDirt", "BulletHitDirt"),
            new EffectData("BulletHitMetal", "BulletHitMetal"),
            new EffectData("BulletHitMoney", "BulletHitMoney"),
            new EffectData("BulletHitPaper", "BulletHitPaper"),
            new EffectData("BulletHitWood", "BulletHitWood"),
            new EffectData("BST_S", "BulletSlowmoTrace", new object[] { 0, 0 }),
            new EffectData("CAM_S", "CameraShaker"),
            new EffectData("CSW", "ChainsawSmoke"),
            new EffectData("CL_H", "ClothHit"),
            new EffectData("DISS", "CloudDissolve"),
            new EffectData("CFTXT", "CustomFloatText", new object[] { "floating text" }),
            new EffectData("DestroyCloth", "DestroyCloth"),
            new EffectData("DestroyDefault", "DestroyDefault"),
            new EffectData("DestroyGlass", "DestroyGlass"),
            new EffectData("DestroyMetal", "DestroyMetal"),
            new EffectData("DestroyPaper", "DestroyPaper"),
            new EffectData("DestroyWood", "DestroyWood"),
            new EffectData("DIG", "Dig"),
            new EffectData("TR_D", "DustTrail"),
            new EffectData("Electric", "Electric"),
            new EffectData("EXP", "Explosion"),
            new EffectData("FIRE", "Fire"),
            new EffectData("FNDTRA", "FireNodeTrailAir"),
            new EffectData("FNDTRG", "FireNodeTrailGround"),
            new EffectData("TR_F", "FireTrail"),
            new EffectData("GIB", "Gib"),
            new EffectData("G_P", "GlassParticles"),
            new EffectData("GR_D", "GrenadeDud"),
            new EffectData("ImpactDefault", "ImpactDefault"),
            new EffectData("ImpactPaper", "ImpactPaper"),
            new EffectData("GLM", "ItemGleam"),
            new EffectData("HIT_B", "MeleeHitBlunt"),
            new EffectData("HIT_S", "MeleeHitSharp"),
            new EffectData("HIT_U", "MeleeHitUnarmed"),
            new EffectData("MZLED", "MuzzleFlashAssaultRifle"),
            new EffectData("MZLED", "MuzzleFlashBazooka"),
            new EffectData("MZLED", "MuzzleFlashL"),
            new EffectData("MZLED", "MuzzleFlashM"),
            new EffectData("MZLED", "MuzzleFlashS"),
            new EffectData("MZLED", "MuzzleFlashShotgun"),
            new EffectData("PPR_D", "PaperDestroyed"),
            new EffectData("PLRB", "PlayerBurned"),
            new EffectData("F_S", "PlayerFootstep"),
            new EffectData("H_T", "PlayerLandFull"),
            new EffectData("Smack", "Smack"),
            new EffectData("TR_S", "SmokeTrail"),
            new EffectData("S_P", "Sparks"),
            new EffectData("STM", "Steam"),
            new EffectData("TR_SPR", "TraceSpawner"),
            new EffectData("WS", "WaterSplash"),
            new EffectData("W_P", "WoodParticles"),
        };

        public void OnStartup()
        {
            _topLeft = Game.GetCameraMaxArea().TopLeft + Vector2.UnitX * 180 - Vector2.UnitY * 80;
            _muzzleAssualtRifleObject = Game.CreateObject("InvisibleBlockNoCollision");
            _muzzleBazookaObject = Game.CreateObject("InvisibleBlockNoCollision");
            _muzzleFlashLObject = Game.CreateObject("InvisibleBlockNoCollision");
            _muzzleFlashMObject = Game.CreateObject("InvisibleBlockNoCollision");
            _muzzleFlashSObject = Game.CreateObject("InvisibleBlockNoCollision");
            _muzzleShotgunObject = Game.CreateObject("InvisibleBlockNoCollision");
            _traceSpawnerObject = Game.CreateObject("InvisibleBlockNoCollision");

        Events.UpdateCallback.Start(OnUpdate);
            Events.PlayerKeyInputCallback.Start(OnInput);
        }

        private bool _isPressingSprint = false;
        private void OnInput(IPlayer arg1, VirtualKeyInfo[] arg2)
        {
            var isPressingSprint = arg2.Any(x => x.Event == VirtualKeyEvent.Pressed && x.Key == VirtualKey.SPRINT);
            var isReleasingSprint = arg2.Any(x => x.Event == VirtualKeyEvent.Released && x.Key == VirtualKey.SPRINT);

            if (isPressingSprint) _isPressingSprint = true;
            if (isReleasingSprint) _isPressingSprint = false;

            foreach (var a in arg2)
            {
                if (a.Event != VirtualKeyEvent.Pressed) continue;

                var moveSpeed = 10;
                if (_isPressingSprint)
                    moveSpeed = 40;

                if (a.Key == VirtualKey.AIM_RUN_LEFT) _topLeft -= Vector2.UnitX * moveSpeed;
                if (a.Key == VirtualKey.AIM_RUN_RIGHT) _topLeft += Vector2.UnitX * moveSpeed;
                if (a.Key == VirtualKey.AIM_CLIMB_UP) _topLeft += Vector2.UnitY * moveSpeed;
                if (a.Key == VirtualKey.AIM_CLIMB_DOWN) _topLeft -= Vector2.UnitY * moveSpeed;
                if (a.Key == VirtualKey.SHEATHE) _playEffectNow = true;
            }
        }

        private Vector2 _topLeft;
        private bool _playEffectNow = false;

        private float _showEffectTime = 0f;
        private IObject _muzzleAssualtRifleObject;
        private IObject _muzzleBazookaObject;
        private IObject _muzzleFlashLObject;
        private IObject _muzzleFlashMObject;
        private IObject _muzzleFlashSObject;
        private IObject _muzzleShotgunObject;
        private IObject _traceSpawnerObject;
        private void OnUpdate(float e)
        {
            var playEffect = false;
            var itemsPerRow = 8;
            var player = Game.GetPlayers()[0];

            if (_playEffectNow)
            {
                playEffect = true;
                _playEffectNow = false;
            }
            if (Game.TotalElapsedGameTime - _showEffectTime > 800)
            {
                playEffect = true;
                _showEffectTime = Game.TotalElapsedGameTime;
            }

            for (var i = 0; i < Effects.Count; i++)
            {
                var x = (i % itemsPerRow) * 100;
                var y = (i / itemsPerRow) * 100;
                var effect = Effects[i];
                var effectPos = new Vector2(_topLeft.X + x, _topLeft.Y - y);

                if (playEffect)
                {
                    switch (effect.ID)
                    {
                        case "MZLED":
                        {
                            IObject muzzleObj = null;
                            switch (effect.Name)
                            {
                                case "MuzzleFlashAssaultRifle":
                                    muzzleObj = _muzzleAssualtRifleObject;
                                    break;
                                case "MuzzleFlashBazooka":
                                    muzzleObj = _muzzleBazookaObject;
                                    break;
                                case "MuzzleFlashL":
                                    muzzleObj = _muzzleFlashLObject;
                                    break;
                                case "MuzzleFlashM":
                                    muzzleObj = _muzzleFlashMObject;
                                    break;
                                case "MuzzleFlashS":
                                    muzzleObj = _muzzleFlashSObject;
                                    break;
                                case "MuzzleFlashShotgun":
                                    muzzleObj = _muzzleShotgunObject;
                                    break;
                            }
                            muzzleObj.SetWorldPosition(effectPos);
                            Game.PlayEffect(effect.ID, Vector2.Zero, muzzleObj.UniqueID, effect.Name);
                            break;
                        }
                        case "TR_SPR":
                            Game.PlayEffect(effect.ID, Vector2.Zero, player.UniqueID, "Smack", 10000);
                            break;
                        default:
                            Game.PlayEffect(effect.ID, effectPos, effect.Args);
                            break;
                    }
                }
                Game.DrawText(effect.Name, effectPos - Vector2.UnitY * 20);
            }
        }
    }
}