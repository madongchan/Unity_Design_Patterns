/// <summary>
/// Receiver (수신자): 실제로 작업을 수행하는 로직이 포함된 클래스입니다. ConcreteCommand에 의해 호출되는 메서드를 가지고 있습니다.
/// </summary>
using UnityEngine;
namespace CommandPatternExample
{
    public class MoveCommandReceiver : MonoBehaviour
    {
        public void MoveOperation(GameObject gameObjectToMove, MoveDirection direction, float distance)
        {
            Vector3 movement = Vector3.zero;
            switch (direction)
            {
                case MoveDirection.up:
                    movement.y += distance;
                    break;
                case MoveDirection.down:
                    movement.y -= distance;
                    break;
                case MoveDirection.left:
                    movement.x -= distance;
                    break;
                case MoveDirection.right:
                    movement.x += distance;
                    break;
            }
            gameObjectToMove.transform.position += movement;
        }
    }
}