using SFDGameScriptInterface;

namespace BotExtended.Bots
{
    class FunnymanBot : Bot
    {
        private Controller<FunnymanBot> m_controller;

        public FunnymanBot(BotArgs args, Controller<FunnymanBot> controller) : base(args)
        {
            if (controller != null)
            {
                m_controller = controller;
                m_controller.Actor = this;
            }
        }

        protected override void OnUpdate(float elapsed)
        {
            base.OnUpdate(elapsed);

            if (m_controller != null)
                m_controller.OnUpdate(elapsed);
        }
    }
}
