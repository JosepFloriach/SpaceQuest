using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CenterMessageController : MonoBehaviour
{
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        player.PlayerKilled += OnPlayerKilled;
        player.PlayerWon += OnPlayerWon;
    }

    private void OnPlayerKilled()
    {
        GetComponent<Animator>().SetTrigger("MissionFailed");
    }

    private void OnPlayerWon()
    {
        GetComponent<Animator>().SetTrigger("MissionComplete");
    }

}
