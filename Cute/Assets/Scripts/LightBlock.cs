using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : MonoBehaviour, ISpecialBlock
{
    public bool buttonOn = true;

    public void OnInteract()
    {
        if (buttonOn)
        {
            TurnBlockOff();
        }
        else
        {
            TurnBlockOn();
        }
    }

    private void TurnBlockOn()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(0, 1, 0);
        GameManager.instance.lightManager.ChangeToSunLight();
        buttonOn = true;
    }

    private void TurnBlockOff()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        GameManager.instance.lightManager.ChangeToOverHeadSpotLight();
        buttonOn = false;
    }
}
