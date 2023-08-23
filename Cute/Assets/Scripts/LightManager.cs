using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public GameObject sunLight;
    public GameObject overHeadSpotLight;

    public GameObject activeLightSource;

    // Start is called before the first frame update
    void Start()
    {
        TurnOffAllLights();
        sunLight.SetActive(true);
        activeLightSource = sunLight;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    public void ChangeToSunLight()
    {
        activeLightSource.SetActive(false);
        sunLight.SetActive(true);
        activeLightSource = sunLight;
        GameManager.instance.cameraManager.ChangeBackgroundColor(Color.cyan);

    }

    public void ChangeToOverHeadSpotLight()
    {
        activeLightSource.SetActive(false);
        overHeadSpotLight.SetActive(true);
        activeLightSource = overHeadSpotLight;
        GameManager.instance.cameraManager.ChangeBackgroundColor(Color.black);
    }

    public void TurnOffAllLights()
    {
        sunLight.SetActive(false);
        overHeadSpotLight.SetActive(false);
    }

}
