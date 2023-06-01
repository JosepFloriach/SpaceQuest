using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<Tutorial> BackwardsTutorialAlternatives;


    public void DisableBackwardsTutorials()
    {
        foreach(var tutorial in BackwardsTutorialAlternatives)
        {
            tutorial.gameObject.SetActive(false);
        }
    }
}
