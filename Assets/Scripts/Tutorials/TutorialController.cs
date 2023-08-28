using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    [SerializeField] private List<GameObject> tutorials;

    private Player player;

    int currentTutorialIndex = 0;
    
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        ReferenceValidator.NotNull(player, tutorials);
    }

    private void OnEnable()
    {
        player.PlayerKilled += ResetTutorials;
    }

    private void OnDisable()
    {
        player.PlayerKilled -= ResetTutorials;
    }

    private void ResetTutorials()
    {
        currentTutorialIndex = 0;
        foreach(var tutorial in tutorials)
        {
            tutorial.SetActive(false);
        }
        DialogController.GetInstance().CancelAllRequests();
        tutorials[0].SetActive(true);        
    }

    public void NextTutorial()
    {
        if (currentTutorialIndex >= 0)
        {
            tutorials[currentTutorialIndex].gameObject.SetActive(false);
        }
        tutorials[++currentTutorialIndex].gameObject.SetActive(true);
    }
}
