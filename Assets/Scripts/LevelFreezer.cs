using System.Collections.Generic;
using UnityEngine;

public class LevelFreezer : MonoBehaviour
{
    [SerializeField] private GameObject LevelParent;
    [SerializeField] private List<GameObject> OtherFreezables;

    public void Freeze()
    {
        foreach(var freezable in LevelParent.transform.GetComponentsInChildren<IFreezable>())
        {
            freezable.Freeze();
        }
        foreach(var go in OtherFreezables)
        {
            IFreezable freezable = go.GetComponent<IFreezable>();
            if (freezable != null)
            {
                freezable.Freeze();
            }
        }
    }

    public void Unfreeze()
    {
        foreach (var freezable in LevelParent.transform.GetComponentsInChildren<IFreezable>())
        {
            freezable.Unfreeze();
        }
        foreach (var go in OtherFreezables)
        {
            IFreezable freezable = go.GetComponent<IFreezable>();
            if (freezable != null)
            {
                freezable.Unfreeze();
            }
        }
    }

}
