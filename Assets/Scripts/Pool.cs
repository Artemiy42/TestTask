using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TestTask
{
    public class Pool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] private int _baseCapacity = 16;
        [SerializeField] private int _additionCapacity = 16;
        [SerializeField] private GameObject _template;

        private bool _isInitialize;
        private List<PullElement> _pool;

        private void OnValidate()
        {
            if (_template != null && _template.GetComponent<T>() == null)
            {
                _template = null;
                Debug.LogError("Template must contain " + typeof(T) + " component");
            }

            if (_baseCapacity < 1)
            {
                Debug.LogError("Base Capacity cannot be less 1");
            }

            if (_additionCapacity < 1)
            {
                Debug.LogError("AdditionCapacity cannot be less 1");
            }
        }

        public void Initialize()
        {
            _pool = new List<PullElement>(_baseCapacity);
            CreateElements(_baseCapacity);
            _isInitialize = true;
        }

        public void CreateElements(int count)
        {
            for (int i = 0; i < count; i++)
            {
                GameObject element = Instantiate(_template, transform);
                element.SetActive(false);
                _pool.Add(new PullElement(element.GetComponent<T>()));
            }
        }
        
        public T GetElement()
        {
            PullElement pullElement = _pool.FirstOrDefault(e => e.IsUsed == false);
            
            if (pullElement != null)
            {
                pullElement.IsUsed = true;
                T element = pullElement.Object;
                element.gameObject.SetActive(true);
                return element;
            }

            CreateElements(_additionCapacity);
            return GetElement();
        }

        public void ReturnToPool(T element)
        {
            PullElement pullElement = _pool.FirstOrDefault(e => e.Object == element); 
            
            if (pullElement != null)
            {
                pullElement.IsUsed = false;
                element.gameObject.SetActive(false);
                CheckSize();
            }
            else
            {
                Debug.LogError("Object is not element from pool");
            }
        }

        private void CheckSize()
        {
            if (_pool.Count > _baseCapacity)
            {
                PullElement[] freePool = _pool.FindAll(e => e.IsUsed == false).ToArray();
                if (freePool.Length > _additionCapacity)
                {
                    for (int i = 0; i < _additionCapacity; i++)
                    {
                        _pool.Remove(freePool[i]);
                        Destroy(freePool[i].Object.gameObject);
                    }

                    _pool.Capacity = _pool.Count;
                }
            }
        }

        private class PullElement
        {
            public T Object;
            public bool IsUsed;

            public PullElement(T obj)
            {
                Object = obj;
            }
        }
    }
}