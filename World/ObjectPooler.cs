using UnityEngine;
using System.Collections;
using System.Data;
using System.Collections.Generic;

namespace Assets.Scripts.World
{
    [System.Serializable]
    public struct GameObjectInstance
    {
        public int NumberOfInstances;
        public GameObject objectToPool;
    }
    
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler INSTANCE;
        public GameObjectInstance[] gameObjectInstances;
        private Dictionary<GameObject, List<GameObject>> pool;
        public bool shouldExpand = true;

        // Use this for initialization
        void Start()
        {
            INSTANCE = this;

            pool = new Dictionary<GameObject, List<GameObject>>();
            foreach (GameObjectInstance instance in gameObjectInstances)
            {
                pool[instance.objectToPool] = new List<GameObject>();

                for (int i = 0; i < instance.NumberOfInstances; i++)
                {
                    GameObject obj = (GameObject)Instantiate(instance.objectToPool);
                    obj.SetActive(false);
                    pool[instance.objectToPool].Add(obj);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public GameObject GetObjectFromPool(GameObject gameObject)
        {
            if(!pool.ContainsKey(gameObject) && !shouldExpand)
            {
                return null;
            }

            if (pool.ContainsKey(gameObject))
            {
                foreach (GameObject obj in pool[gameObject])
                {
                    if (!obj.activeInHierarchy)
                    {
                        return obj;
                    }
                }
            }

            if(shouldExpand)
            {
                GameObject obj = (GameObject)Instantiate(gameObject);
                if (!pool.ContainsKey(gameObject)) 
                {
                    pool[gameObject] = new List<GameObject>();
                }
                obj.SetActive(false);
                pool[gameObject].Add(obj);
                return obj;
            } else
            {
                return null;
            }
        }
    }
}