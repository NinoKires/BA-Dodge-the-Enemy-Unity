using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManagerController : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void HowToPlay() 
    {
        SceneManager.LoadScene("InfoBoxScene");
    }

    public void Menu() 
    {
        SceneManager.LoadScene("MenuScene");
    }
}
