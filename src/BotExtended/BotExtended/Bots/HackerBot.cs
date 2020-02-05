namespace SFDScript.BotExtended.Bots
{
    public class HackerBot : Bot
    {
        public HackerBot() : base()
        {
            UpdateInterval = 200;
        }

        protected override void OnUpdate(float elapsed)
        {
            if (Player.IsRemoved) return;

            base.OnUpdate(elapsed);

            var profile = Player.GetProfile();
            var currentColor = profile.Head.Color2;
            var newColor = "";

            switch (currentColor)
            {
                case "ClothingLightRed":
                    newColor = "ClothingLightOrange";
                    break;
                case "ClothingLightOrange":
                    newColor = "ClothingLightYellow";
                    break;
                case "ClothingLightYellow":
                    newColor = "ClothingLightGreen";
                    break;
                case "ClothingLightGreen":
                    newColor = "ClothingLightCyan";
                    break;
                case "ClothingLightCyan":
                    newColor = "ClothingLightBlue";
                    break;
                case "ClothingLightBlue":
                    newColor = "ClothingLightPurple";
                    break;
                case "ClothingLightPurple":
                    newColor = "ClothingLightRed";
                    break;
                default:
                    newColor = "ClothingLightCyan";
                    break;
            }
            profile.Head.Color2 = newColor;
            profile.ChestOver.Color2 = newColor;
            profile.Feet.Color1 = newColor;
            Player.SetProfile(profile);
        }
    }
}
