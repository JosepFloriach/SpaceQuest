using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePickup : BasePickup
{
    private IPhysicsBody body;

    public override string ID
    {
        get
        {
            return "FreezePickup";
        }
    }

    public override void Pickup(Player player, Cockpit cockpit)
    {
        base.Pickup(player, cockpit);
    }

    public override void StartInteraction(Player player, IPhysicsBody body, Transform transform)
    {
        base.StartInteraction(player, body, transform);
        body.SetLinearSpeed(0.0f);
        //this.body = body;
        //Invoke("Unfreeze", 1.0f);
    }

    private void Unfreeze()
    {
        //body.UnFreeze();
    }
}
