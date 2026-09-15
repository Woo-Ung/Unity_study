using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineTest : MonoBehaviour
{
    [SerializeField] private float _delay;
    private WaitForSeconds _wait;
    private Coroutine _routine;

    private void Awake()
    {
        // YieldInstruction 반복적으로 사용될거라면 캐싱해두기.
        _wait = new WaitForSeconds(_delay);
        
    }

    private void Start()
    {
        Debug.Log("Start 시작");

        //// 시작 O : StartCoroutine(MyRoutine());
        ////      X : MyRoutine();
        //StartCoroutine(MyRoutine());
        //// 멈출 때: StopCoroutine();

        Debug.Log("Start 종료");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) Run();
        if (Input.GetKeyDown(KeyCode.Alpha2)) Stop();
    }

    private void Run()
    {
        if (_routine != null) return;

        _routine = StartCoroutine(MyRoutine());
    }

    private void Stop()
    {
        if (_routine == null) return;

        StopCoroutine(_routine);
        _routine = null;
    }

    private void OnDrawGizmos()
    {
        Debug.Log("----------------");
    }

    // 함수의 반환형은 'IEnumerator'

   
    private IEnumerator MyRoutine()
    {
        while (true)
        {
            // 반환할 때는 'yield return'
            // yield return 000 : 000이(가) 충족되는 상황까지 함수를 종료하고 대기할 것.
            yield return _wait;

            Debug.Log("Coroutine");
        }
        //yield break; //루틴을 아예 멈출 때
    }
}