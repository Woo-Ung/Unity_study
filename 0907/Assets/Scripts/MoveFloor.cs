using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    private Animator _animator;

    private void Awake() => CacheComponents();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("isTrigger", true);
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _animator.SetBool("isTrigger", false);
            other.transform.SetParent(null);
        }
    }
    private void CacheComponents()
    {
        _animator = GetComponent<Animator>();  
    }
}
