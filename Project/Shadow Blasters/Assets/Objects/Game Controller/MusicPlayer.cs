using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct Music
{

    public bool repeatOnRestart;
    public string musicPath;

    public Music(bool repeatOnRestart, string musicPath)
    {
        this.repeatOnRestart = repeatOnRestart;
        this.musicPath = musicPath;
    }
}

public class MusicPlayer : MonoBehaviour
{

    private Dictionary<string, Music> musicScenes;
    private Music? playing;
    private string lastScene;
    private AudioSource source;

    void Start()
    {
        SceneManager.sceneLoaded += OnLoad;
        source = GetComponent<AudioSource>();
		source.loop = true;
		musicScenes = new() {
            { "Menu", new Music(true, "Musics/Ambient")},
            { "TESTE", new Music(true, "Musics/Ambient")},
            { "BossFight", new Music(true, "Musics/Boss")}
        };

        OnLoad(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

	public void OnLoad(Scene scene, LoadSceneMode mode)
	{
        if (playing == null)
        {
            Music musicToPlay = musicScenes[scene.name];
            playing = musicToPlay;

            source.Stop();
            source.clip = Resources.Load<AudioClip>(musicToPlay.musicPath);
            source.Play();
        }
        else
        {
            if (scene.name != lastScene)
            {
				if (playing.Value.musicPath != musicScenes[scene.name].musicPath)
                {
					Music musicToPlay = musicScenes[scene.name];
					playing = musicToPlay;

					source.Stop();
					source.clip = Resources.Load<AudioClip>(musicToPlay.musicPath);
					source.Play();
				}
                else
                {
                }
			}
            else
            {
            }
		}
		lastScene = scene.name;
	}

	void Update()
    {
        source.volume = GameController.masterVolume * GameController.musicVolume;
    }
}
