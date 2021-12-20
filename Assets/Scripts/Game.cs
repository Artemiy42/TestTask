using UnityEngine;
using TestTask.Circle;
using TestTask.Service;
using TestTask.Square;
using TestTask.UI;

namespace TestTask
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private CircleController _circleController;
        [SerializeField] private SquareController _squareController;
        [SerializeField] private InputService _inputService;
 
        private CircleCatcher _circleCatcher;
        
        private PlayerData _playerData;

        private void OnEnable()
        {
            _circleController.WalkedDistance += OnCircleWalkDistance;
            _squareController.SquareDeath += OnSquareDeath;
            _inputService.PointerDown += OnPointerDown;
            _inputService.Dragged += OnDragged;
            _inputService.PointerUp += OnPointerUp;
        }

        private void OnDisable()
        {
            _circleController.WalkedDistance -= OnCircleWalkDistance;
            _squareController.SquareDeath -= OnSquareDeath;
            _inputService.PointerDown -= OnPointerDown;
            _inputService.Dragged -= OnDragged;
            _inputService.PointerUp -= OnPointerUp;
        }

        private void Start()
        {
            _playerData = new PlayerData();

            _squareController.Initialize();
        }

        private void OnPointerDown(Vector3 obj)
        {
            _circleController.OnPointerDown();
        }

        private void OnDragged(Vector3 screenPosition)
        {
            _circleController.OnDragged(screenPosition);
        }

        private void OnPointerUp(Vector3 screenPosition)
        {
            _circleController.OnPointerUp(screenPosition);
        }

        private void OnSquareDeath()
        {
            _playerData.AmountSquareCollected++;
            _hud.SetAmountSquareCollected(_playerData.AmountSquareCollected);
        }

        private void OnCircleWalkDistance(float distance)
        {
            _playerData.TraveledDistance += distance;
            _hud.SetTraveledDistance(_playerData.TraveledDistance);
        }
    }
}