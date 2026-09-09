using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerTest : MonoBehaviour
{
    [SerializeField] private float _range;
    public LayerMask TargetLayer;   

    private void Start()
    {
        TargetLayer = TargetLayer.Everything();
    }

    private void OnTriggerEnter(Collider other)
    {       
        if (TargetLayer.Contains(other))
        {
            Debug.Log("찾음");
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    int layer = (1 << other.gameObject.layer);
    //    if((TargetLayer.value & layer) != 0)
    //    {
    //        Debug.Log("찾음");
    //    }
    //}

    private bool ContainsLayer(LayerMask mask, Collider layer)
    {
        return 0 != (mask.value & (1 << layer.gameObject.layer));
    }

    //public void Update()
    //{
    //    Ray ray = new Ray(transform.position, Vector3.forward);
    //    RaycastHit hit;
    //    if(Physics.Raycast(ray, out hit, _range, TargetLayer))
    //    {
    //        Debug.Log(hit.transform.name);
    //    }
    //}
}
