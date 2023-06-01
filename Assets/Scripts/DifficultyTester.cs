using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DifficultyTester : MonoBehaviour
{
    /*private Player player;
    private Launcher launcher;
    private PlayerPathDrawer pathDrawer;
    private RandomPlaysGenerator playsGenerator;

    private int currentPlayouts = 0;
    private int totalPlayouts = 0;
    private bool makeScreenshot = false;*/

    private void Awake()
    {
        /*launcher = GameObject.FindObjectOfType<Launcher>();
        player = GameObject.FindObjectOfType<Player>();
        pathDrawer = GameObject.FindObjectOfType<PlayerPathDrawer>();*/
    }

    private void Start()
    {
        //StartTest(15, true);
    }

    public void StartTest(int playouts, bool makeScrenshot)
    {
        /*this.makeScreenshot = makeScrenshot;
        pathDrawer.active = makeScrenshot;
        totalPlayouts = playouts;

        player.PlayerKilled += OnKilled;
        player.PlayerWon += OnWin;
        launcher.LaunchedEnabled += OnLaunchEnabled;

        playsGenerator = new RandomPlaysGenerator(120.0f, 15.0f, 60.0f, 25.0f, totalPlayouts, 3, 0);*/
    }

    private void OnKilled()
    {
        // Increment statistics.
    }

    private void OnWin()
    {
        // Increment statistics.
    }

    private void OnLaunchEnabled()
    {
        /*if (currentPlayouts >= totalPlayouts)
        {
            Finish();
        }
        else
        {
            DoPlayout();
        }*/
    }

    private void DoPlayout()
    {
        /*currentPlayouts++;
        Vector3 nextPlay = playsGenerator.GetNextPlay();
        launcher.Launch(nextPlay);*/
    }

    private void Finish()
    {
        /*if (makeScreenshot)
        {
            string fileName = Directory.GetCurrentDirectory() + "\\DifficultyTests\\" + SceneManager.GetActiveScene().name + "_" + System.DateTime.Now.ToString("yyyy_dd_MM_HH_mm") + ".png";
            ScreenCapture.CaptureScreenshot(fileName);
        }
        player.PlayerKilled -= OnKilled;
        player.PlayerWon -= OnWin;*/
    }

}
