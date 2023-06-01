using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCallbacks : MonoBehaviour
{
    private Cockpit cockpit;

    private void Awake()
    {
        cockpit = FindObjectOfType<Cockpit>();
    }

    public void FreezeShip()
    {
        cockpit.Freeze();
    }

    public void UnFreezeShip()
    {
        cockpit.UnFreeze();
    }

    public void StopShip()
    {
        cockpit.PhysicsBody.ClearAllForces();
    }
}
