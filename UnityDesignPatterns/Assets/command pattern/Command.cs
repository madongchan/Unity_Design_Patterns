namespace CommandPatternExample
{
    /// <summary>
    /// command: 실행될 작업에 대한 인터페이스
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
