using System.Collections.Generic;
using UnityEngine;

namespace Thatvrkidds.ScratchLab
{
    internal class ObjectPool
    {
        private readonly Queue<GameObject> pool = new Queue<GameObject>();

        public GameObject Get(GameObject prefab)
        {
            while (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();

                if (obj != null)
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            return Object.Instantiate(prefab);
        }

        public void Return(GameObject obj)
        {
            if (obj == null)
                return;

            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        public void Clear()
        {
            while (pool.Count > 0)
            {
                GameObject obj = pool.Dequeue();

                if (obj != null)
                    Object.Destroy(obj);
            }
        }
    }
}
