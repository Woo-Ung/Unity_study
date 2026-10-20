using UnityEngine;
using System;

public interface IStateEvent
{
    public Action<StateBase> OnStateChange { get; set; }
}
