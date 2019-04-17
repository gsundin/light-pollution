using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private AudioClip tap, glass_break, flap, last_flap, party_whistle;
    static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        tap = Resources.Load<AudioClip>("Sounds/tap");
        glass_break = Resources.Load<AudioClip>("Sounds/glass_break");
        flap = Resources.Load<AudioClip>("Sounds/flap");
        last_flap = Resources.Load<AudioClip>("Sounds/last_flap");
        party_whistle = Resources.Load<AudioClip>("Sounds/party_whistle");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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

    public void Play(string clip)
    {
        switch (clip)
        {
            case "tap":
                audioSrc.PlayOneShot(tap);
                break;

            case "glass_break":
                audioSrc.PlayOneShot(glass_break);
                break;

            case "flap":
                audioSrc.PlayOneShot(flap);
                break;

            case "last_flap":
                audioSrc.PlayOneShot(last_flap);
                break;

            case "party_whistle":
                audioSrc.PlayOneShot(party_whistle);
                break;
        }
    }
}
