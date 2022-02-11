using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlanet : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool UIisDisabled = false;
    public GameObject createPlanetUI;
    public GameObject PanelUI;

    void Awake()
    {
        createPlanetUI.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (Input.GetKeyDown("f"))
        {
            if (UIisDisabled)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }
    }

    void Disable()
    {
        PanelUI.SetActive(false);
        UIisDisabled = true;
    }

    void Enable()
    {
        PanelUI.SetActive(true);
        UIisDisabled = false;
    }

    void Resume()
    {
        createPlanetUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        createPlanetUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
}
