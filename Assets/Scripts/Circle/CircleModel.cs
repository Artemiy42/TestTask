using System;
using UnityEngine;

namespace TestTask.Circle
{
    public class CircleModel
    {
        public Action<float> WalkedDistance;
        public Action EndedMove;
        
        private Vector3 _position;
        private Vector3 _target;
        private float _speed;
        private float _acceleration;
        private bool _isMoving;

        public Vector3 Position => _position;
        public bool IsMoving => _isMoving;

        public CircleModel(Vector3 startPosition, float speed, float acceleration)
        {
            _position = startPosition;
            _speed = speed;
            _acceleration = acceleration;
        }

        public void SetTarget(Vector3 target)
        {
            _target = target;
            _isMoving = true;
        }
        
        public void StopMoving()
        {
            _isMoving = false;
        }
        
        public void MoveToTarget(float delta, float easy)
        {
            if (_isMoving)
            {
                Vector3 newPosition = Vector3.MoveTowards(_position, _target, (_speed + _acceleration * easy) * delta);
                WalkedDistance?.Invoke((_position - newPosition).magnitude);
                _position = newPosition;
                
                if (_position == _target)
                {
                    StopMoving();
                    EndedMove?.Invoke();
                }
            }
        }
    }
}
