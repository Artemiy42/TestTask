using UnityEngine;

namespace TestTask.Circle
{
    public class CircleView : MonoBehaviour
    {
        private CircleModel _circleModel;

        public void Init(CircleModel circleModel)
        {
            _circleModel = circleModel;
        }

        public void UpdateView()
        {
            if (_circleModel.IsMoving)
            {
                transform.localPosition = _circleModel.Position;
            }
        }
    }
}