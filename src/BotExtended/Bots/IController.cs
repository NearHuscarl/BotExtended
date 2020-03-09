namespace BotExtended.Bots
{
    public interface IController<T>
    {
        T Actor { get; set; }
        void OnUpdate(float elapsed);
    }
}
