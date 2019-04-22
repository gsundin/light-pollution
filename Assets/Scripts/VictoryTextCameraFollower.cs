using UnityEngine;
using System.Collections;

public class VictoryTextCameraFollower : MonoBehaviour
{
    public static VictoryTextCameraFollower Instance { get; private set; }

    public GameObject player;       //Public variable to store a reference to the player game object
    public GameObject mainCamera;

    public static bool gameHasStarted = false;
    public static bool victory = false;

    private float offsetX;
    private float offsetY;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        if (gameHasStarted)
        {
            //offsetX = player.transform.position.x;
            //offsetY = player.transform.position.y;

            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, 3);
        }

        if (victory)
        {
            //transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1, 3);
            transform.position = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, 3);
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            Instance = this;
        }
    }
}
