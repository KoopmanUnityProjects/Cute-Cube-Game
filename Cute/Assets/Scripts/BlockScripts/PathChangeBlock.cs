using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathChangeBlock : MonoBehaviour, ISpecialBlock
{
    public List<Ground> changeBlockList;
    public bool buttonOn = false;

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
        ToggleWalkableList();
        buttonOn = true;
    }

    private void TurnBlockOff()
    {
        this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0, 0);
        ToggleWalkableList();
        buttonOn = false;
    }

    private void ToggleWalkableList()
    {
        foreach (Ground groundBlock in changeBlockList)
        {
            groundBlock.ToggleWalkable();
        }
    }
}
