using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : InteractableBase
{
    private CheckPointManager checkPointManager;

    private void Awake()
    {
        checkPointManager = FindObjectOfType<CheckPointManager>();
        ReferenceValidator.NotNull(checkPointManager);
    }

    protected override void OnStart()
    {
        checkPointManager.AddCheckPoint(this);
    }

    public override void StartInteraction(Player player, IPhysicsBody cockpit, Transform transform)
    {
        checkPointManager.CurrentCheckPoint = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        GizmosCustom.DrawCircle(transform.position, 2.0f, 5);
    }
}
