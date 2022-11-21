using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioPlayer;
    [SerializeField]
    private List<AudioClip> shoot;
    [SerializeField]
    private List<AudioClip> lazer;
    [SerializeField]
    private List<AudioClip> ufoSpawn;
    [SerializeField]
    private List<AudioClip> destroyEnemy;
    [SerializeField]
    private List<AudioClip> playerDeath;
    [SerializeField]
    private List<AudioClip> bgMusic;


    private void PlaySound(List<AudioClip> from, float volume = 1f)
    {
        audioPlayer.PlayOneShot(from[Random.Range(0, from.Count)], volume);
    }

    void Start()
    {
        Gun.OnFire += () => { PlaySound(shoot, 0.7f); };
        Gun.OnLazer += () => { PlaySound(lazer); };
        Ufo.OnSpawn += (_) => { PlaySound(ufoSpawn); };
        Ufo.OnDestroy += (_) => { PlaySound(destroyEnemy); };
        Asteroid.OnDestroy += (_) => { PlaySound(destroyEnemy); };
        Ship.OnDestroy += (_) => { PlaySound(playerDeath, 1.2f); };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
