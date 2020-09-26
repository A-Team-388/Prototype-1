using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip deleted, gamePlayBackground, manuNav, placed1, placed2;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        deleted = Resources.Load<AudioClip>("Audio/Deleted");
        gamePlayBackground = Resources.Load<AudioClip>("Audio/Gameplay_backgrounds");
        manuNav = Resources.Load<AudioClip>("Audio/Menu Nav_01");
        placed1 = Resources.Load<AudioClip>("Audio/Placed");
        placed2 = Resources.Load<AudioClip>("Audio/Placed_v2");

        audioSrc = GetComponent<AudioSource>();
        audioSrc.volume = .2f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "delete":
                audioSrc.PlayOneShot(deleted);
                Debug.Log("sound");
                break;
            case "menu":
                audioSrc.PlayOneShot(manuNav);
                break;
            case "place1":
                audioSrc.PlayOneShot(placed1);
                break;
            case "place2":
                audioSrc.PlayOneShot(placed2);
                break;
            case "background":
                audioSrc.PlayOneShot(gamePlayBackground);
                audioSrc.loop = true;
                break;
        }
    }
}
