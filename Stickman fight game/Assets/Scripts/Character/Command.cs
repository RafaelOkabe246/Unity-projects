public abstract class Command
{
    public abstract void Execute();
    public abstract bool isFinished { get; }
}
