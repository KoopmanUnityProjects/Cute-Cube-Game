using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool walkable; // Set on front end
    public bool moves; // St on front end
    public int rowNumber;
    public int columnNumber;

    private void OnMouseDown()
    {
        if (walkable)
        {
            GameManager.instance.player.TryToMoveToPoint(this);
        }
    }

    public void StepOn()
    {
        if (gameObject.tag == "SpecialBlock")
        {
            var special = this.gameObject.GetComponent<ISpecialBlock>();
            special.OnInteract();
        }
    }

    public void SetWalkableOnOrOff(bool canWalk)
    {
        walkable = canWalk;
    }

    public void ToggleWalkable()
    {
        walkable = !walkable;

        if (walkable)
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 0);
        }
        else
        {
            this.gameObject.GetComponent<Renderer>().material.color = new Color(1, 1, 1);
        }
    }
}
