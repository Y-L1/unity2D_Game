using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManger : MonoBehaviour
{
    public static SoundManger instance;


    public AudioSource BGM;
    public AudioSource audioSource;
    public AudioSource EHurt;
    

    [SerializeField]
    private AudioClip btnDown;
    [SerializeField]
    private AudioClip jumpAudio, swordATK, fireATK;
    [SerializeField]
    private AudioClip enemyHurt;
    [SerializeField]
    private AudioClip openBox;
    [SerializeField]
    private AudioClip closeBox;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        BGM.Stop();
    }

    public void BtnDown()
    {
        audioSource.clip = btnDown;
        audioSource.Play();
    }

    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        audioSource.Play();
    }
    public void SwordAudio()
    {
        audioSource.clip = swordATK;
        audioSource.Play();
    }
    public void OpenBoxAudio()
    {
        audioSource.clip = openBox;
        audioSource.Play();
    }
    public void CloseBoxAudio()
    {
        audioSource.clip = closeBox;
        audioSource.Play();
    }
    public void FireAudio()
    {
        audioSource.clip = fireATK;
        audioSource.Play();
    }

    public void EnemyHurtPlay()
    {
        EHurt.clip = enemyHurt;
        EHurt.Play();
    }
}
