using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{
    //public GameObject scene;

    //resets the current scene
    public void resetGame()
    {
        Debug.Log("Reset");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
