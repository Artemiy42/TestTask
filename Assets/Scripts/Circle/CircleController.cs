using System;
using TestTask.Utility;
using UnityEngine;

namespace TestTask.Circle
{
    public class CircleController : MonoBehaviour
    {
        public Action<float> WalkedDistance;

        [SerializeField] private PathDrawer _pathDrawer;
        [SerializeField] private CircleView _circleView;
        [SerializeField] private Camera _camera;
        [SerializeField] private AnimationCurve _movingEase;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private float _acceleration = 20f;
        [SerializeField] private float _period = 0.05f;
        
        private CircleCatcher _circleCatcher;
        private CircleModel _circleModel;
        private Timer _timer;
        private Path _path;
        private float _cameraDepth;

        public void OnPointerDown()
        {
            if (_path.Count == 0)
            {
                AddPointToPath(_circleModel.Position);
            }
        }

        public void OnDragged(Vector3 screenPosition)
        {
            if (_timer.CheckTime(Time.time))
            {
                Vector3 worldPoint = _camera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _cameraDepth));
                AddPointToPath(worldPoint);
            }
        }

        public void OnPointerUp(Vector3 position)
        {
            if (_circleModel.IsMoving == false)
            {
                ChangeTarget();                
            }
            else if (_circleCatcher.IntersectWithCircle(position))
            {
                StopMove();
                _path.Clear();
            }
        }

        public void AddPointToPath(Vector3 worldPoint)
        {
            _path.Add(worldPoint);
        }

        public void StopMove()
        {
            _circleModel.StopMoving();
        }

        public void ChangeTarget()
        {
            if (_path.HasNextPoint())
            {
                _circleModel.SetTarget(_path.GetNextPoint());
            }
            else
            {
                StopMove();
                _path.Clear();
            }
        }

        private void Awake()
        {
            _path = new Path();
            _timer = new Timer(_period);
            _circleCatcher = new CircleCatcher(_camera);
            _circleModel = new CircleModel(Vector3.zero, _speed, _acceleration);

            _pathDrawer.Initialize(_path);
            _circleView.Init(_circleModel);
            _cameraDepth = -_camera.transform.position.z;
        }

        private void OnEnable()
        {
            _circleModel.WalkedDistance += OnCircleWalkDistance;
            _circleModel.EndedMove += OnEndedMove;
        }

        private void OnDisable()
        {
            _circleModel.WalkedDistance -= OnCircleWalkDistance;
            _circleModel.EndedMove -= OnEndedMove;
        }

        private void Update()
        {
            if (_circleModel.IsMoving)
            {
                float sqrMagnitude = _path.SqrMagnitude();
                float passedMagnitude = _path.PassedMagnitude(_circleModel.Position);
                Debug.Log(sqrMagnitude + " | " + passedMagnitude);
                float delta = Mathf.InverseLerp(0, sqrMagnitude, passedMagnitude);
                _circleModel.MoveToTarget(Time.deltaTime, _movingEase.Evaluate(delta));
                _circleView.UpdateView();
            }
        }

        private void OnCircleWalkDistance(float distance)
        {
            WalkedDistance?.Invoke(distance);
        }

        private void OnEndedMove()
        {
            ChangeTarget();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_path == null)
            {
                return;
            }
            
            foreach (Vector3 point in _path)
            {
                Gizmos.DrawSphere(point, 0.1f);
            }
        }
#endif
    }
}