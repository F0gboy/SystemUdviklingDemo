using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        Debug.Log("Successfully quit");
        Application.Quit();
    }
}
