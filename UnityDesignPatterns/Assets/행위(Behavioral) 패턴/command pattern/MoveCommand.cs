/// <summary>
/// ConcreteCommand(구체적인 명령): 명령 패턴의 구체적인 명령을 나타냅니다. 
/// 이 경우 MoveCommand 클래스는 게임 오브젝트를 특정 방향으로 특정 거리만큼 이동하는 로직을 캡슐화한 구체적인 명령입니다. 
/// 이 클래스는 각각 이동 작업과 실행 취소를 수행하기 위해 Execute 및 UnExecute 메서드를 구현합니다. 
/// MoveCommand 클래스는 MoveDirection 열거형을 문자열 표현으로 변환하고 디버그 출력에 명령 세부 정보를 표시하는 메서드도 제공합니다.
/// </summary>

using UnityEngine;

namespace CommandPatternExample
{
    // A basic enum to describe our movement
    public enum MoveDirection { up, down, left, right };

    class MoveCommand : ICommand
    {
        private MoveCommandReceiver _receiver;
        private MoveDirection _direction;
        private float _distance;
        private GameObject _gameObject;
        private MoveDirection value;
        private float moveDistance;
        private GameObject objectToMove;

        //Constructor
        public MoveCommand(MoveCommandReceiver receiver , MoveDirection direction, float distance, GameObject gameObjectToMove)
        {
            _receiver = receiver;
            _direction = direction;
            _distance = distance;
            _gameObject = gameObjectToMove;
        }

        //Execute new command
        public void Execute()
        {
            _receiver.MoveOperation(_gameObject, _direction, _distance);
        }


        //Undo last command
        public void UnExecute()
        {
            _receiver.MoveOperation(_gameObject, InverseDirection(_direction), _distance);
        }


        //invert the direction for undo
        private MoveDirection InverseDirection(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.up:
                    return MoveDirection.down;
                case MoveDirection.down:
                    return MoveDirection.up;
                case MoveDirection.left:
                    return MoveDirection.right;
                case MoveDirection.right:
                    return MoveDirection.left;
                default:
                    Debug.LogError("Unknown MoveDirection");
                    return MoveDirection.up;
            }
        }

        //화면에 표시될 명령 세부 정보를 문자열로 반환
        public override string ToString()
        {
            return _gameObject.name + " : " + MoveDirectionString(_direction) + " : " + _distance.ToString();
        }


        //Convert the MoveDirection enum to a string for debug
        public string MoveDirectionString(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.up:
                    return "up";
                case MoveDirection.down:
                    return "down";
                case MoveDirection.left:
                    return "left";
                case MoveDirection.right:
                    return "right";
                default:
                    return "unkown";
            }
        }
    }
}

