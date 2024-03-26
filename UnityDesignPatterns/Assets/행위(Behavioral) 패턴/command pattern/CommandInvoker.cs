/// <summary>
/// Invoker(호출자): ConcreteCommand 객체를 보유하고, ConcreteCommand 객체의 execute 메서드를 호출하여 요청을 처리합니다.
/// </summary>
namespace CommandPatternExample
{
    public class CommandInvoker
    {
        private ICommand command;

        public void SetCommand(ICommand command)
        {
            this.command = command;
        }

        public void ExecuteCommand()
        {
            command.Execute();
        }

        public void UnExecuteCommand()
        {
            command.UnExecute();
        }
    }
}
