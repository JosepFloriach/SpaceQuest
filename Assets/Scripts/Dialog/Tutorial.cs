using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Tutorial : MonoBehaviour
{
    [SerializeField] DialogController dialogController;
    [SerializeField] private DialogSetup setup;

    bool triggered = false;

    private void Start()
    {
        dialogController = FindObjectOfType<DialogController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !triggered)
        {
            dialogController.OpenDialog(setup);
            triggered = true;
        }
    }
}
