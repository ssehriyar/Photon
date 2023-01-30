using UnityEngine;


namespace NewUtils
{
    public abstract class Singleton<T> : SingletonBase where T : Component
    {
        public static T Instance { get; protected set; }

        public override void Init()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}

