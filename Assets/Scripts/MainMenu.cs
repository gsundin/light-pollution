﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer titleText;

    [SerializeField]
    private SpriteRenderer wasdText;

    [SerializeField]
    private SpriteRenderer peckText;

    [SerializeField]
    private SpriteRenderer[] menuText;

    private float textWidth;
    private int cursor;
    private int prev;

    // Start is called before the first frame update
    void Start()
    {
        prev = 0;
        cursor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Obtain directional input
        if (Input.GetKeyDown(KeyCode.W) && cursor > 0)
        {
            prev = cursor;
            cursor--;
        }
        if (Input.GetKeyDown(KeyCode.S) && cursor < 1)
        {
            prev = cursor;
            cursor++;
        }

        AnimateText();

        // Player is selecting a menu option
        if (Input.GetKeyDown(KeyCode.Return) && cursor >= 0 && cursor <= 1)
        {
            Debug.Log("Selection has been made");
            Select();
        }
    }

    // Animates the text and makes it a little bigger
    void AnimateText()
    {
        menuText[prev].transform.localScale = new Vector3(90, 90, -10);
        menuText[cursor].transform.localScale = new Vector3(140, 140, -10);
    }

    void ZoomOutCamera()
    {
        VictoryTextCameraFollower.gameHasStarted = true;
        CompleteCameraController.gameHasStarted = true;
        CompleteCameraController.Instance.initializePlayerCamera();
        Camera.main.orthographicSize = 12;
    }

    void Select()
    {
        switch (cursor)
        {
            case 0:
                Debug.Log("Destroying menu...");
                foreach (SpriteRenderer sr in menuText)
                {
                    sr.enabled = false;
                }

                titleText.enabled = false;
                wasdText.enabled = true;
                peckText.enabled = true;
                PlayerController.startController = true;

                GameObject streetlamp = GameObject.FindWithTag("streetlamp");
                Sprite darkLamp = Resources.Load("Sprites/streetlamp0_dark", typeof(Sprite)) as Sprite;
                streetlamp.GetComponent<SpriteRenderer>().sprite = darkLamp;
                SoundManager.Instance.Play("glass_break");

                GameObject player = GameObject.Find("Player");
                player.gameObject.GetComponent<SpriteRenderer>().enabled = true;

                GameObject crickets = GameObject.FindWithTag("crickets");
                crickets.GetComponent<AudioSource>().mute = true;

                GameObject bgm = GameObject.FindWithTag("bgm");
                bgm.GetComponent<AudioSource>().Play();

                ZoomOutCamera();

                break;

            //case 1:
                //Debug.Log("Credits are not ready yet");
                //break;

            case 1:
                Debug.Log("Quitting game...");
                Application.Quit();
                break;
        }
    }

    void DisplayInstructions()
    {

    }
}
