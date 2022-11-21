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


    private void PlaySound(List<AudioClip> from, float volume = 1f)
    {
        audioPlayer.PlayOneShot(from[Random.Range(0, from.Count)], volume);
    }

    void Start()
    {
        Gun.OnFire += () => { PlaySound(shoot, 0.7f); };
        Gun.OnLazer += () => { PlaySound(lazer); };
        Ufo.OnSpawn += (_) => { PlaySound(ufoSpawn, 20f); };
        Ufo.OnDestroy += (_) => { PlaySound(destroyEnemy, 10f); };
        Asteroid.OnDestroy += (_) => { PlaySound(destroyEnemy, 5f); };
        Ship.OnDestroy += (_) => { PlaySound(playerDeath, 10f); };
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
