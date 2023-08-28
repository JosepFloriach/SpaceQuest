using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Planetoids/GravityFieldsParameters")]
public class GravityFieldsParameters : ScriptableObject
{
    public AnimationCurve MaxForceByRadiusCurve;
}
