using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController2D controller;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private int pecks;

    private SpriteRenderer otherSprite;
    private Light otherLight;

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Attempting to jump...");
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Attempting to peck...");
            Peck();
        }
    }

    void FixedUpdate()
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<SpriteRenderer>() != null)
        {
            otherSprite = other.gameObject.GetComponent<SpriteRenderer>();
        }

        if (other.GetComponentInChildren<Light>() != null)
        {
            otherLight = other.GetComponentInChildren<Light>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        otherSprite = null;
    }

    void Peck()
    {
        if (otherSprite != null)
        {
            string name = otherSprite.sprite.name;

            string[] split = name.Split('_');

            if (split[0].Contains("neon") && split[1].Equals("lit"))
            {
                SoundManager.Instance.Play("glass_break");
            }
            else if (split[0].Contains("window"))
            {
                SoundManager.Instance.Play("tap");
            }

            if (split[1].Equals("lit"))
            {
                Sprite darkSprite = Resources.Load("Sprites/" + split[0] + "_dark", typeof(Sprite)) as Sprite;
                otherSprite.sprite = darkSprite;
                //otherLight.enabled = false;
                pecks++;
            }
        }

        if (pecks == Globals.LIGHTS_PER_LEVEL[0])
        {

            Invoke("nothing", 0.3f);
            SoundManager.Instance.Play("party_whistle");
            // ADVANCE TO NEXT LEVEL
        }
    }
}