using System;
using TestTask.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TestTask.Square
{
    public class SquareController : MonoBehaviour
    {
        public event Action SquareDeath;
        
        [SerializeField] private SquarePool _squarePool;
        [SerializeField] private Transform _container;
        [SerializeField] private SquareView _squareView;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _spawnPeriod = 1;
        [SerializeField] private float _maxAmount = 10;

        private Timer _timer;
        private float _amountSpawned;
        private float _verticalExtent;
        private float _horizontalExtent;

        public void Initialize()
        {
            CalculateCameraBounds();
            _squarePool.Initialize();
        }

        private void Awake()
        {
            _timer = new Timer(_spawnPeriod);
        }

        private void Update()
        {
            if (_amountSpawned < _maxAmount && _timer.CheckTime(Time.time))
            {
                SpawnSquare();
            }
        }

        private void SpawnSquare()
        {
            SquareView squareView = _squarePool.GetElement();
            squareView.SetDeathCallback(OnSquareDeath);
            squareView.transform.position = GetRandomPositionInCamera();
            _amountSpawned++;
        }

        private void OnSquareDeath(SquareView squareView)
        {
            _squarePool.ReturnToPool(squareView);
            _amountSpawned--;
            SquareDeath?.Invoke();
        }

        private Vector3 GetRandomPositionInCamera()
        {
            Vector3 spawnPosition;
            
            spawnPosition.x = Random.Range(-_horizontalExtent, _horizontalExtent);
            spawnPosition.y = Random.Range(-_verticalExtent, _verticalExtent);
            spawnPosition.z = 0f;
            
            return spawnPosition;
        }

        private void CalculateCameraBounds()
        {
            _verticalExtent = _camera.orthographicSize;    
            _horizontalExtent = _verticalExtent * Screen.width / Screen.height;
        }
    }
}
