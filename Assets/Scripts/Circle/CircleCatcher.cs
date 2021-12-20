using UnityEngine;

namespace TestTask.Circle
{
    public class CircleCatcher
    {
        private const float RaycastDistance = 100f;
        private readonly int CircleLayerMask;
        
        private Camera _camera;

        public CircleCatcher(Camera camera)
        {
            _camera = camera;
            CircleLayerMask = LayerMask.GetMask("Circle");
        }

        public bool IntersectWithCircle(Vector3 screenPosition)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, RaycastDistance, 
                CircleLayerMask);

            return hit.collider != null;
        }
    }
}