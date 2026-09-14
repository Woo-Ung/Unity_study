using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayer1 : MonoBehaviour
{
    private void Start()
    {
        // 참조타입의 동작

        MyClass c1 = new();
        c1.value = 5;
        MyClass c2 = c1;
        c2.value = 15;

        Debug.Log($"C1 : {c1.value}/ C2 : {c2.value}");

        string s1 = "aa";
        string s2 = s1;
        s2 = "bb";
        Debug.Log($"s1 : {s1}/ s2 : {s2}");
    }
}

public class MyClass
{
    public int value;
}
