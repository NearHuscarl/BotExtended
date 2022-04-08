using BotExtended.Library;
using SFDGameScriptInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotExtended.Bots
{
    class CyborgBot : RobotBot
    {
        public CyborgBot(BotArgs args) : base(args) { }

        public override void OnSpawn()
        {
            base.OnSpawn();
            ChangeStatusColor("ClothingLightGreen");
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);
            UpdateHealthStatusColor();
        }

        private string m_healthColor = "";
        private void UpdateHealthStatusColor()
        {
            var mod = Player.GetModifiers();
            var healthLeft = mod.CurrentHealth / mod.MaxHealth;

            if (healthLeft > .4f && healthLeft <= .6f)
            {
                if (m_healthColor != "ClothingLightYellow") ChangeStatusColor("ClothingLightYellow");
            }
            else if (healthLeft > .2f && healthLeft <= .4f)
            {
                if (m_healthColor != "ClothingLightOrange") ChangeStatusColor("ClothingLightOrange");
            }
            else if (healthLeft <= .2f)
                if (m_healthColor != "ClothingLightRed") ChangeStatusColor("ClothingLightRed");
        }

        private void ChangeStatusColor(string color)
        {
            var profile = GetProfile();
            profile.Accesory.Color2 = color;
            Player.SetProfile(profile);
            m_healthColor = color;
        }
    }
}
