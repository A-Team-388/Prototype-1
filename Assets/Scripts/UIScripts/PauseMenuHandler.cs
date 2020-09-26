using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseMenuCurtain;

    // Start is called before the first frame update
    public void HamburgerMenuButton()
    {
        SoundManager.PlaySound("menu");
        pauseMenuCurtain.SetActive(true);
    }

    public void ResumeGame()
    {
        SoundManager.PlaySound("menu");
        pauseMenuCurtain.SetActive(false);
    }

    public void HelpMenu()
    {
        SoundManager.PlaySound("menu");
    }

    public void ExitGame()
    {
        SoundManager.PlaySound("menu");
        Application.Quit();
    }

}
