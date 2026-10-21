using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Chase2", story: "Chase [Target] at [Speed]", category: "Enemy", id: "10f26c3fa2b7fed02d1259103ff94ada")]
public partial class Chase2Action : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Target;
    [SerializeReference] public BlackboardVariable<float> Speed;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        Transform selfTransform = GameObject.transform;
        Transform targetTransform = Target.Value.transform;
        Vector3 toTarget = targetTransform.position - selfTransform.position;

        selfTransform.LookAt(targetTransform);

        selfTransform.position += toTarget.normalized * Speed.Value * Time.deltaTime;

        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}
