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
        Ship.OnFire += () => { PlaySound(shoot, 0.1f); };
        Ufo.OnSpawn += (_) => { PlaySound(ufoSpawn); };
        Ship.OnDestroy += (_) => { PlaySound(playerDeath); };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
