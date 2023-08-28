using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleManagement : MonoBehaviour
{
    private void Update()
    {
        DialogController.GetInstance().Update(Time.deltaTime);
    }
}
