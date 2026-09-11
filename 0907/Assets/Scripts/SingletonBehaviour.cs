using OpenCover.Framework.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T: MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected void SetSingleton()
    {
        // 1. 게임 내에 '단 하나'만 존재해야 함.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // 2. 전역적인 접근 지원.
            _instance = (this as T);//GetComponent<T>();

            // 3. Scene 전환 시에도 유지.
            DontDestroyOnLoad(gameObject);
        }
    }
}