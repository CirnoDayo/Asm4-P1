using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void RunGame()
    {
        Debug.Log("Game Over!");
        SceneManager.LoadScene("Lan_Scene"); // Replace "GameOverScene" with the actual name of your game over scene.
    }
}
