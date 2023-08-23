using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LightManager lightManager;
    public Player player;
    public CameraManager cameraManager;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void ParentObject(GameObject child, GameObject parent)
    {
        if (child == null)
            return;

        if (parent == null)
            child.transform.parent = null;
        else
            child.transform.parent = parent.transform;
    }
}
