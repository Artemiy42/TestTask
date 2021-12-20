using System;
using UnityEngine;
using TestTask.Circle;

namespace TestTask.Square
{
    public class SquareView : MonoBehaviour
    {
        private Action<SquareView> _deathCallback;

        public void SetDeathCallback(Action<SquareView> callback)
        {
            _deathCallback = callback;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out CircleView _))
            {
                _deathCallback?.Invoke(this);
            }
        }
    }
}
