using System;
using UnityEngine;

namespace TestTask.Service
{
    public class InputService : MonoBehaviour
    {
        public event Action<Vector3> PointerDown;
        public event Action<Vector3> Dragged;
        public event Action<Vector3> PointerUp; 
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                PointerDown?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Dragged?.Invoke(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                PointerUp?.Invoke(Input.mousePosition);
            }
        }
    }
}
