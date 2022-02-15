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

    void Awake()
    {
        createPlanetUI.SetActive(false);
        controlsUI.SetActive(true);
        PanelUI.SetActive(true);
        PlanetStatUI.SetActive(false);
    }


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

        if (Input.GetKeyDown("t"))
        {
            if (PlanetStat)
            {
                DisablePlanetStat();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100000.0f))
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
        /*radiusText.text = selectedBody.radius.ToString();
        orbitRadiusText.text = selectedBody.orbitRadius.ToString();
        surfaceGravityText.text = selectedBody.surfaceGravity.ToString();
        massText.text = selectedBody.mass.ToString();
        velocityText.text = velocity.ToString();
        nameText.text = selectedBody.bodyName;*/
        radiusText.text = "Radius: " + selectedBody.radius;
        orbitRadiusText.text = "Orbit Radius: " + selectedBody.orbitRadius;
        surfaceGravityText.text = "Surface Gravity: " + selectedBody.surfaceGravity;
        massText.text = "Mass: " + selectedBody.mass;
        velocityText.text = "Velocity: " + velocity;
        nameText.text = selectedBody.bodyName;
    }
}