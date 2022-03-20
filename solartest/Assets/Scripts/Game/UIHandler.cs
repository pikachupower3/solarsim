using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool UIisDisabled = false;
    public static bool PlanetStat = false;
    public GameObject createPlanetUI;
    public GameObject controlsUI;
    public GameObject PlanetStatUI;
    public GameObject PanelUI;
    public TMPro.TextMeshProUGUI radiusText;
    public TMPro.TextMeshProUGUI orbitRadiusText;
    public TMPro.TextMeshProUGUI surfaceGravityText;
    public TMPro.TextMeshProUGUI massText;
    public TMPro.TextMeshProUGUI velocityText;
    public TMPro.TextMeshProUGUI nameText;

    /*Makes sure that the UI is visible and the Create Planet and Planet Stat UI are invisile*/
    void Awake()
    {
        createPlanetUI.SetActive(false);
        controlsUI.SetActive(true);
        PanelUI.SetActive(true);
        PlanetStatUI.SetActive(false);
    }

    /*Makes the Create Planet UI invisible or visible if the F key is pressed based on if it is already visible or not*/
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            if (!UIisDisabled)
            {
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    if (PlanetStat)
                    {
                        DisablePlanetStat();
                    }
                    Pause();
                }
            }
        }

        /*Makes all the UI invisible or visible if the F key is pressed based on if it is already visible or not*/
        if (Input.GetKeyDown("f"))
        {
            if (UIisDisabled)
            {
                if (GameIsPaused)
                {
                    Time.timeScale = 0f;
                }
                EnableAllUI();
            }
            else
            {
                DisableAllUI();
            }
        }

        /*Makes the Planet Stat UI invisbile if it is visible when the T key is pressed*/
        if (Input.GetKeyDown("t"))
        {
            if (PlanetStat)
            {
                DisablePlanetStat();
            }
        }

        /*Checks on which Body is pressed and get's the stat for that and passes it to the Planet Stat UI*/
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10000000.0f))
            {
                if (hit.transform != null)
                {
                    ShowBodyStats(hit.transform.gameObject);
                    if (!UIisDisabled)
                    {
                        EnablePlanetStat();
                    }
                }
            }
        }
    }

    void DisableAllUI()
    {
        PanelUI.SetActive(false);
        UIisDisabled = true;
        Time.timeScale = 1f;
    }

    void EnableAllUI()
    {
        PanelUI.SetActive(true);
        UIisDisabled = false;
    }

    void EnablePlanetStat()
    {
        PlanetStatUI.SetActive(true);
        createPlanetUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        PlanetStat = true;
    }

    void DisablePlanetStat()
    {
        PlanetStatUI.SetActive(false);
        PlanetStat = false;
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

    public void ShowBodyStats(GameObject go)
    {
        GameObject cb = GameObject.Find(go.name);
        Rigidbody rbSelectedBody = cb.GetComponent<Rigidbody>();
        float velocity = rbSelectedBody.velocity.magnitude;
        CelestialBody selectedBody = cb.GetComponent<CelestialBody>();
        radiusText.text = "Radius: " + selectedBody.radius;
        orbitRadiusText.text = "Orbit Radius: " + selectedBody.orbitRadius;
        surfaceGravityText.text = "Surface Gravity: " + selectedBody.surfaceGravity;
        massText.text = "Mass: " + selectedBody.mass;
        velocityText.text = "Velocity: " + velocity;
        nameText.text = selectedBody.bodyName;
    }
}
