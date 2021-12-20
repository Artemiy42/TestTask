using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask.Utility
{
    public class Path : IEnumerable<Vector3>
    {
        public event Action<Vector3> AddedPoint;
        public event Action Cleared;
        
        public int Count => _points.Count;
        
        private List<Vector3> _points;
        private int _currentPointNumber;
        private float _sqrMagnitude;
        private int _amountPointsMagnitude;
        private float _passedMagnitude;
            
        public Path()
        {
            _points = new List<Vector3>();
        }

        public void Add(Vector3 worldPoint)
        {
            _points.Add(worldPoint);
            AddedPoint?.Invoke(worldPoint);
        }

        public bool HasNextPoint()
        {
            return _currentPointNumber < _points.Count;
        }

        public Vector3 GetNextPoint()
        {
            if (_currentPointNumber > 0)
            {
                _passedMagnitude += (_points[_currentPointNumber] - _points[_currentPointNumber - 1]).sqrMagnitude;
            }
            
            return _points[_currentPointNumber++];
        }

        public void Clear()
        {
            _amountPointsMagnitude = 0;
            _currentPointNumber = 0;
            _passedMagnitude = 0;
            _sqrMagnitude = 0;
            _points.Clear();
            Cleared?.Invoke();
        }

        public float SqrMagnitude()
        {
            if (_points.Count == 0)
            {
                return 0f;
            }

            for (int i = _amountPointsMagnitude; i < _points.Count - 1; i++)
            {
                _sqrMagnitude += (_points[i + 1] - _points[i]).sqrMagnitude;
                _amountPointsMagnitude++;
            }

            return _sqrMagnitude;
        }

        public float PassedMagnitude(Vector3 currentPosition)
        {
            if (_currentPointNumber == 0)
            {
                return 0f;
            }

            return _passedMagnitude + (currentPosition - _points[_currentPointNumber - 1]).sqrMagnitude;
        }

        public IEnumerator<Vector3> GetEnumerator()
        {
            return _points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}