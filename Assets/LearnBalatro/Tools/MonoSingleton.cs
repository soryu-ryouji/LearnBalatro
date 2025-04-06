using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RyoujiTools
{
    public class MonoSingleton : MonoBehaviour
    {
        private static MonoSingleton mInstance;
        private Dictionary<string, Coroutine> mActiveCoroutines = new();

        public static MonoSingleton Instance
        {
            get
            {
                if (mInstance == null)
                {
                    var objs = FindObjectsByType<MonoSingleton>(FindObjectsSortMode.None);
                    if (objs.Length > 0)
                    {
                        mInstance = objs[0];
                    }

                    if (mInstance == null)
                    {
                        var singletonObject = new GameObject();
                        mInstance = singletonObject.AddComponent<MonoSingleton>();
                        singletonObject.name = "MonoSingleton";
                    }
                }

                return mInstance;
            }
        }

        private void Awake()
        {
            if (mInstance == null)
            {
                mInstance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (mInstance != this)
            {
                Destroy(gameObject);
            }
        }

        public void Run(UnityAction action)
        {
            action();
        }

        /// <summary>
        /// Start a coroutine and store its reference with a unique identifier.
        /// </summary>
        /// <param name="action">The IEnumerator coroutine to run.</param>
        /// <param name="key">The unique identifier for this coroutine.</param>
        public void RunSync(string key, IEnumerator action)
        {
            if (mActiveCoroutines.ContainsKey(key))
            {
                Debug.LogWarning($"MonoSingleton: Coroutine with key '{key}' is already running.");
                return;
            }

            Coroutine coroutine = StartCoroutine(action);
            mActiveCoroutines[key] = coroutine;
        }

        /// <summary>
        /// Stop a coroutine using its unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier for the coroutine to stop.</param>
        public void StopSync(string key)
        {
            var value = mActiveCoroutines[key];
            StopCoroutine(value);
            mActiveCoroutines.Remove(key);
        }

        /// <summary>
        /// Try Stop a coroutine using its unique identifier.
        /// </summary>
        /// <param name="key">The unique identifier for the coroutine to stop.</param>
        public void TryStopSync(string key)
        {
            if (mActiveCoroutines.TryGetValue(key, out Coroutine coroutine))
            {
                StopCoroutine(coroutine);
                mActiveCoroutines.Remove(key);
            }
        }

        /// <summary>
        /// Stop all active coroutines managed by this class.
        /// </summary>
        public void StopAllSyncs()
        {
            foreach (var coroutine in mActiveCoroutines.Values)
            {
                StopCoroutine(coroutine);
            }
            mActiveCoroutines.Clear();
        }
    }
}
