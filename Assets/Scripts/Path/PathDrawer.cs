using TestTask.Utility;
using UnityEngine;

public class PathDrawer : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    
    private Path _path;
    
    public void Initialize(Path path)
    {
        _path = path;
        _path.AddedPoint += OnAddedPoint;
        _path.Cleared += OnCleared;
    }
    
    public void OnDisable()
    {
        _path.AddedPoint -= OnAddedPoint;
        _path.Cleared -= OnCleared;
    }

    private void OnAddedPoint(Vector3 point)
    {
        _lineRenderer.positionCount = _path.Count;
        _lineRenderer.SetPosition(_path.Count - 1, point);
    }

    private void OnCleared()
    {
        _lineRenderer.positionCount = 0;
    }
}
