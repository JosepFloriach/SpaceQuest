using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CenterMessageController : MonoBehaviour
{
    private Player player;
    private Animator animator;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        animator = GetComponent<Animator>();
        ReferenceValidator.NotNull(player, animator);
    }

    private void OnEnable()
    {
        player.PlayerKilled += OnPlayerKilled;
        player.PlayerWon += OnPlayerWon;
    }

    private void OnDisable()
    {
        player.PlayerKilled -= OnPlayerKilled;
        player.PlayerWon -= OnPlayerWon;
    }

    private void OnPlayerKilled()
    {
        animator.SetTrigger("MissionFailed");
    }

    private void OnPlayerWon()
    {
        animator.SetTrigger("MissionComplete");
    }

}
