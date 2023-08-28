using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectListEnabler : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;

    int currentIdx = 0;

    private void Start()
    {
        foreach(var obj in objects)
        {
            obj.SetActive(false);
        }
        objects[0].SetActive(true);
        currentIdx = 0;
    }

    public void Next()
    {
        objects[currentIdx].SetActive(false);
        currentIdx = (currentIdx + 1) % objects.Count;
        objects[currentIdx].SetActive(true);
    }

    public void Previous()
    {
        objects[currentIdx].SetActive(false);
        currentIdx = currentIdx == 0 ? objects.Count - 1 : currentIdx - 1;
        objects[currentIdx].SetActive(true);
    }
}
