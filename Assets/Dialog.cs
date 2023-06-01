using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    [SerializeField] private DialogSetup setup;

    private DialogController dialogController;
    private bool seen = false;

    private void Awake()
    {
        dialogController = FindObjectOfType<DialogController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !seen)
        {
            dialogController.OpenDialog(setup);
            seen = true;
        }
    }
}
