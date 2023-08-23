using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    Camera currentCam;
    Player player;
    float xOffset;
    float yOffset;
    float zOffset;
    bool followPlayer;

    // Start is called before the first frame update
    void Start()
    {
        currentCam = Camera.main;
        player = GameManager.instance.player;
        FollowPlayer(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            Vector3 newCamPos = new Vector3(
                player.transform.position.x + xOffset,  // xpos
                player.transform.position.y + yOffset,  // ypos
                player.transform.position.z + zOffset); // zpos

            currentCam.transform.position = newCamPos;
        }
    }

    public void ChangeBackgroundColor(Color color)
    {
        Camera.main.backgroundColor = color;
    }

    public void FollowPlayer(bool follow)
    {
        followPlayer = follow;
        xOffset = currentCam.transform.position.x - player.transform.position.x;
        yOffset = currentCam.transform.position.y - player.transform.position.y;
        zOffset = currentCam.transform.position.z - player.transform.position.z;
    }
}
