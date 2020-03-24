using SFDGameScriptInterface;

namespace BotExtended.Bots
{
    public abstract class Controller<T> where T : Bot
    {
        public T Actor { get; set; }
        public IPlayer Player { get { return Actor.Player; } }
        public abstract void OnUpdate(float elapsed);
        public virtual void OnDamage(IPlayer attacker, PlayerDamageArgs args) { }
    }
}
