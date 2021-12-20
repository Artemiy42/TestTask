using UnityEngine;
using TMPro;

namespace TestTask.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountSquareCollected;
        [SerializeField] private TextMeshProUGUI _traveledDistance;

        public void SetAmountSquareCollected(int number)
        {
            _amountSquareCollected.text = number.ToString();
        }

        public void SetTraveledDistance(float distance)
        {
            _traveledDistance.text = distance.ToString("F2");
        }
    }
}