using UnityEngine;

namespace NewUtils
{
    public abstract class DontDestroySingleton<T> : Singleton<T> where T : Component
    {
        public override void Init()
        {
            if (Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }
}
