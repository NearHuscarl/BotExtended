using SFDGameScriptInterface;

namespace Animations
{
    public partial class Program : GameScriptInterface
    {
        public Program() : base(null) { }

        public void OnStartup()
        {
            var colorObj = (IObjectText)Game.GetSingleObjectByCustomID("color");
            if (colorObj != null) colorObj.SetTextColor(Color.Green);
        }

        private Curve _curve = Curves.Linear;

        public void OnLinear(TriggerArgs args) { _curve = Curves.Linear; }
        public void OnEaseInOutBack(TriggerArgs args) { _curve = Curves.EaseInOutBack; }
        public void OnSlowMiddle(TriggerArgs args) { _curve = Curves.SlowMiddle; }
        public void OnEaseOutQuint(TriggerArgs args) { _curve = Curves.EaseOutQuint; }
        public void OnEaseInCirc(TriggerArgs args) { _curve = Curves.EaseInCirc; }

        public void OnAnimate(TriggerArgs args)
        {
            var transformObj = Game.GetSingleObjectByCustomID("transform");
            var rotateObj = Game.GetSingleObjectByCustomID("rotate");
            var colorObj = (IObjectText)Game.GetSingleObjectByCustomID("color");

            Animations.AnimateTo(value => transformObj.SetWorldPosition(value), new AnimationOptions<Vector2>
            {
                Curve = _curve,
                Start = transformObj.GetWorldPosition(),
                End = transformObj.GetWorldPosition() + Vector2.UnitY * 25,
                Duration = 2000,
            });

            Animations.AnimateTo(value => rotateObj.SetAngle(value), new AnimationOptions<float>
            {
                Curve = _curve,
                Start = rotateObj.GetAngle(),
                End = rotateObj.GetAngle() + MathHelper.TwoPI,
                Duration = 2000,
            });

            Animations.AnimateTo(value => colorObj.SetTextColor(value), new AnimationOptions<Color>
            {
                Curve = _curve,
                Start = Color.Green,
                End = Color.Red,
                Duration = 2000,
            });
        }

        public void OnAnimateBack(TriggerArgs args)
        {
            var transformObj = Game.GetSingleObjectByCustomID("transform");
            var rotateObj = Game.GetSingleObjectByCustomID("rotate");
            var colorObj = (IObjectText)Game.GetSingleObjectByCustomID("color");

            Animations.AnimateTo(value => transformObj.SetWorldPosition(value), new AnimationOptions<Vector2>
            {
                Curve = _curve,
                Start = transformObj.GetWorldPosition(),
                End = transformObj.GetWorldPosition() - Vector2.UnitY * 25,
                Duration = 2000,
            });

            Animations.AnimateTo(value => rotateObj.SetAngle(value), new AnimationOptions<float>
            {
                Curve = _curve,
                Start = rotateObj.GetAngle(),
                End = rotateObj.GetAngle() - MathHelper.TwoPI,
                Duration = 2000,
            });

            Animations.AnimateTo(value => colorObj.SetTextColor(value), new AnimationOptions<Color>
            {
                Curve = _curve,
                Start = Color.Red,
                End = Color.Green,
                Duration = 2000,
            });
        }
    }
}