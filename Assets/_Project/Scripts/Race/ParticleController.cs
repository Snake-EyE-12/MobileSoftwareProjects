using System;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private ParticleSystemStopBehavior behavior;

    private void Awake()
    {
        Stop();
    }

    protected bool playing;
    public virtual void Stop()
    {
        playing = false;
        particleSystem.Stop(true, behavior);
    }

    public virtual void Begin()
    {
        if(playing) return;
        playing = true;
        particleSystem.Play();
    }
}
