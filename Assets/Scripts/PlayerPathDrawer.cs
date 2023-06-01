using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPathDrawer : MonoBehaviour
{
    /*public bool active = false;

    Launcher launcher;
    Player player;
    LineRenderer testLine = null;
    int frameCount = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        launcher = GameObject.FindObjectOfType<Launcher>();
        player = GameObject.FindObjectOfType<Player>();
        
        launcher.Launched += OnLaunched;
        player.PlayerWon += OnPlayerFinish;
        player.PlayerKilled += OnPlayerFinish;
    }

    private void OnDestroy()
    {
        launcher.Launched -= OnLaunched;
    }

    private void OnLaunched(Vector3 direction)
    {
        testLine = null;
        frameCount = 0;
    }

    private void OnPlayerFinish()
    {
        testLine = null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!active)
        {
            return;
        }

        if (testLine == null)
        {
            var newObject = new GameObject("TestPath");
            newObject.transform.parent = transform;
            testLine = newObject.AddComponent<LineRenderer>();
            testLine.startWidth = 0.1f;
            testLine.endWidth = 0.1f;
        }

        if (frameCount % 10 == 0)
        {
            testLine.positionCount++;
            testLine.SetPosition(testLine.positionCount - 1, player.transform.position);
        }
        frameCount++;
    }*/
}
