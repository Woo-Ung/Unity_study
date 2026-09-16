using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class tempPlayer : MonoBehaviour
{
    //IntChange : 반환형이 없고, int  매개변수를 1개 받는 함수를 담아둘 수 있는 타입이다.
    //public delegate void IntChange(int value);

    public UnityEvent TempEvent;
    private int _health;
    public int Health
    {
        get => _health;
        private set
        {
            _health = value;
            OnHealthChange?.Invoke(_health);
        }
    }
    public event Action<int> OnHealthChange; // On+이름

    public ObservablePorperty<float> Exp = new(0);

    private void OnEnable()
    {
        //TempEvent.AddListener(Foo);
        //TempEvent.RemoveListener(Foo);
        //TempEvent.RemoveAllListeners();
    }

    //private void TryLoadData(Action s, Action f) 
    //{
    //    // 로드~~
    //    if (성공했다면?)
    //    {
    //        s.Invoke(); 
    //    }
    //    else
    //    {
    //        f.Invoke();
    //    }
    //} // 콜백 구조

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeDamage(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Heal(10);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Exp.value += 20.5f;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            TempEvent?.Invoke();
        }
    }

    public void TakeDamage(int damage)
    {        
        Health -= damage;
        Debug.Log("데미지 받음");
    }

    public void Heal(int heal)
    {
        Health += heal;
        Debug.Log("회복했다");
    }
}
