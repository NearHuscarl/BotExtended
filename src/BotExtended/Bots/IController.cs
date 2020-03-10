namespace BotExtended.Bots
{
    public interface IController<T> where T : Bot
    {
        T Actor { get; set; }
        void OnUpdate(float elapsed);
    }
}
