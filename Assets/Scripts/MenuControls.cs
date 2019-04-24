using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuControls : MonoBehaviour
{
    public void FighterLevel()
    {
        SceneManager.LoadScene("FighterLevel");
    }
    public void GunShipLevel()
    {
        SceneManager.LoadScene("GSLevel");
    }

    public void Escape()
    {
        Application.Quit();
    }

}
