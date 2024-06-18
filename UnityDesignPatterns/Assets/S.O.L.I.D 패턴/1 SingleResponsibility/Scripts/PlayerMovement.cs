using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DesignPatterns.SRP
{
    // sample code to demo single-responsibility
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float positionMultiplier;
        private float yPosition;

        public void Move(float delta)
        {
            // Mathf.Clamp 함수를 사용하여 _yPosition의 값을 -1과 1 사이로 제한합니다.
            yPosition = Mathf.Clamp(yPosition + delta, -1, 1);

            transform.position = new Vector3(transform.position.x, yPosition * positionMultiplier, transform.position.z);
        }
    }
}
