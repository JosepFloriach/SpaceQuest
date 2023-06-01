using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void SetTrigger(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
