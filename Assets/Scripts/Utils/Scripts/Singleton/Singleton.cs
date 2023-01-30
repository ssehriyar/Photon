﻿using UnityEngine;

namespace MyUtils.Singleton
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static bool _shuttingDown = false;
		private static readonly object _lock = new object();
		private static T _instance = null;

		public static T Instance
		{
			get
			{
				if (_shuttingDown)
				{
					Debug.LogWarning("[Singleton] Instance' " + typeof(T) + "' already destroyed. Returning null.");
					return null;
				}
				lock (_lock)
				{
					if (_instance == null)
					{
						_instance = (T)FindObjectOfType(typeof(T));
						if (_instance == null)
						{
							GameObject singletonObject = new GameObject();
							_instance = singletonObject.AddComponent<T>();
							singletonObject.name = typeof(T).ToString() + "(Singleton)";
							DontDestroyOnLoad(singletonObject);
						}
					}
					return _instance;
				}
			}
		}

		private void OnApplicationQuit() => _shuttingDown = true;
		private void OnDestroy() => _shuttingDown = true;
	}
}