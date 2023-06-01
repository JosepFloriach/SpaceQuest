using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VelocityModifier : MonoBehaviour
{
    public abstract Vector3 GetNewVelocity(Player affectedEntity);
}
