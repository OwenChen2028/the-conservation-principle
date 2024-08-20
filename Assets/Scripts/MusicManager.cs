using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource Player1;
    private AudioSource Player2;

    [SerializeField] AudioClip[] songList; // 0 is Waltz, 1 is Pulsar, 2 is Deep

    [SerializeField] float maxVolume;
    [SerializeField] float fadeSpeed;

    private AudioClip currentSong;
    private AudioClip nextSong;

    private bool player1Active = true;
    private Scene currentScene;

    private static MusicManager _instance;
    public static MusicManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        Player1 = transform.Find("Player 1").GetComponent<AudioSource>();
        Player2 = transform.Find("Player 2").GetComponent<AudioSource>();

        currentScene = SceneManager.GetActiveScene();

        currentSong = songList[0];
        nextSong = songList[0];

        Player1.clip = currentSong;
        Player2.clip = nextSong;

        Player1.volume = maxVolume;
        Player2.volume = 0;

        Player1.Play();
        Player2.Play();
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        ManagePlaying();
        DetermineSong();

        /*
        if (Input.GetKey(KeyCode.M))
        {
            nextSong = songList[2];
        }
        */
    }

    private void ManagePlaying()
    {
        if (currentSong != nextSong)
        {
            Crossfade();
        }
        if (Player1.volume < 0)
        {
            Player1.volume = 0;
        }
        if (Player2.volume > maxVolume)
        {
            Player2.volume = maxVolume;
        }
        if (Player2.volume < 0)
        {
            Player2.volume = 0;
        }
        if (Player1.volume > maxVolume)
        {
            Player1.volume = maxVolume;
        }

    }
    private void Crossfade()
    {

        if (player1Active)
        {
            Player2.clip = nextSong;

            Player1.volume = 0;
            Player1.Stop();

            Player2.volume = maxVolume;
            Player2.Play();

            currentSong = nextSong;
            player1Active = !player1Active;

        }
        else
        {
            Player1.clip = nextSong;

            Player2.volume = 0;
            Player2.Stop();

            Player1.volume = maxVolume;
            Player1.Play();

            currentSong = nextSong;
            player1Active = !player1Active;
        }
    }

    private void DetermineSong()
    {
        string room = currentScene.name;
        //0 for menus, 1 for levels 1-5, 2 for levels 6 
        switch(room)
        {
            case ("MainMenu"):
            case ("CreditsScreen"):
            case ("ControlsScreen"):
            {
                nextSong = songList[0];
                break;
            }

            case ("LevelA1"):
            case ("LevelA2"):
            case ("LevelA3"):
            case ("LevelA4"):
            case ("LevelA5"):
            case ("LevelB1"):
            case ("LevelB2"):
            case ("LevelB3"):
            case ("LevelB4"):
            case ("LevelB5"):
            {
                nextSong = songList[1];
                break;
            }

            case ("LevelA6"):
            case ("LevelB6"):

            {
                nextSong = songList[2];
                break;
            }
        }   
    }
   
}
