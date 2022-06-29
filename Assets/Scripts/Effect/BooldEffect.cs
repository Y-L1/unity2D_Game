using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooldEffect : MonoBehaviour
{
    ParticleSystem booldEffect;
    void Start()
    {
        booldEffect = GetComponent<ParticleSystem>();
        booldEffect.Stop();
    }


    public void EffectPlay()
    {
        booldEffect.Play();
    }
}
