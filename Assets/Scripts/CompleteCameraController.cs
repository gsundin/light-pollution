using UnityEngine;
using System.Collections;

public class CompleteCameraController : MonoBehaviour
{

    public GameObject player;       //Public variable to store a reference to the player game object
    private float offsetX;         //Private variable to store the offset distance between the player and camera
    private float offsetY;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        //offsetX = transform.position.x - player.transform.position.x;
        //offsetY = transform.position.y - player.transform.position.y;

        offsetX = player.transform.position.x;
        offsetY = player.transform.position.y;
        transform.position = new Vector3(offsetX, offsetY, -10);
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        
        if (player.transform.position.x > Globals.LEVEL_BOUNDARIES_X[0] && player.transform.position.x < Globals.LEVEL_BOUNDARIES_X[1])
        {
            offsetX = player.transform.position.x;
        }

        if (player.transform.position.y > Globals.LEVEL_BOUNDARIES_Y[0] && player.transform.position.y < Globals.LEVEL_BOUNDARIES_Y[1])
        {
            offsetY = player.transform.position.y;
        }

        transform.position = new Vector3(offsetX, offsetY, -10);

    }
}
