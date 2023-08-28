using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameData.LevelProgressionData;

[CreateAssetMenu(menuName = "Planetoids/LevelDataCollection")]
public class LevelDataCollection : ScriptableObject
{
    public bool DirtyFlag;
    public LevelInfo LevelInfo;
}
