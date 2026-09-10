using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StimpakController : MonoBehaviour, IInteractable
{
    [SerializeField] private float _upSpeed;
    [SerializeField] private int _duration;
    public GameObject GameObject => gameObject;
    private Outline _outline;    

    public void Awake()
    {
        _outline = gameObject.GetComponent<Outline>();       
    }
    public void Start() => _outline.enabled = false;

    public void Update()
    {        
        transform.Rotate(Vector3.up, 100 * Time.deltaTime,Space.World);
    }

    public void Targeting()
    {
        _outline.enabled = true;
    }

    public void Untargeting()
    {
        _outline.enabled = false;
    }

    public void Interact(IInteractor owner)
    {
        if(!(owner is PlayerController))
        {
            return;
        }

        PlayerController player = (PlayerController)owner;
        
        player.GetComponent<PlayerMovement>().SetMoveSpeed(_upSpeed);
        player.GetComponent<PlayerMovement>().SetDuration(_duration);
        player.GetComponent<PlayerState>().TakeDamage(15);
        player.GetComponentInChildren<PlayerWeapon>().SetCooldown(0.1f);

        Destroy(gameObject);
    }       
}