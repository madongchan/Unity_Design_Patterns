using UnityEngine;
using System.Collections.Generic;
using CommandPatternExample;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    // Receiver 객체 생성
    private MoveCommandReceiver moveCommandReceiver = new MoveCommandReceiver();
    // Invoker 객체 생성
    private CommandInvoker commandInvoker = new CommandInvoker();
    // Command 객체 생성
    private List<MoveCommand> commands = new List<MoveCommand>();
    public float moveDistance;
    public GameObject objectToMove;
    private int currentCommandNum = 0;
    private Dictionary<string, MoveDirection> moveDirectionDict = new Dictionary<string, MoveDirection>()
    {
        { "Up", MoveDirection.up },
        { "Down", MoveDirection.down },
        { "Right", MoveDirection.right },
        { "Left", MoveDirection.left }
    };

    public void Undo()
    {
        if (currentCommandNum > 0)
        {
            currentCommandNum--;
            MoveCommand moveCommand = commands[currentCommandNum];
            commandInvoker.SetCommand(moveCommand);
            commandInvoker.UnExecuteCommand();
        }
    }

    public void Redo()
    {
        if (currentCommandNum < commands.Count)
        {
            MoveCommand moveCommand = commands[currentCommandNum];
            commandInvoker.SetCommand(moveCommand);
            currentCommandNum++;
            commandInvoker.ExecuteCommand();
        }
    }

    public void Move()
    {
        string buttonName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.GetComponent<Button>().name; //클릭한 버튼의 이름
        string[] parts = buttonName.Split(' '); // 문자열을 공백을 기준으로 분할합니다.
        string result = parts[0]; // 분할된 첫 번째 요소를 가져옵니다.
        foreach (var direction in moveDirectionDict)
        {
            if (result == direction.Key)
            {
                MoveCommand moveCommand = new MoveCommand(moveCommandReceiver, direction.Value, moveDistance, objectToMove);
                commandInvoker.SetCommand(moveCommand);
                commandInvoker.ExecuteCommand();
                commands.Add(moveCommand);
                currentCommandNum++;
            }
        }
    }
    //Shows what's going on in the command list
    void OnGUI()
    {
        string label = "   start";
        if (currentCommandNum == 0)
        {
            label = ">" + label;
        }
        label += "\n";

        for (int i = 0; i < commands.Count; i++)
        {
            if (i == currentCommandNum - 1)
                label += "> " + commands[i].ToString() + "\n";
            else
                label += "   " + commands[i].ToString() + "\n";

        }
        GUI.Label(new Rect(0, 0, 400, 800), label);
    }
}

