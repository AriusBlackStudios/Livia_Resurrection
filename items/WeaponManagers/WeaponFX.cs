using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFX : MonoBehaviour
{
    [Header("Weapon FX")]
    public ParticleSystem normalWeaponTrail;
    public ParticleSystem fireWeaponTrail;
    public ParticleSystem darkWeaponTrail;
    public ParticleSystem lightninghWeaponTrail;

    public AudioSource swooshSource;
    public void PlayWeaponFX()
    {
        normalWeaponTrail.Stop();
        swooshSource.Stop();
        if (normalWeaponTrail.isStopped)
        {
            normalWeaponTrail.Play();
            
        }
        if (!swooshSource.isPlaying)
        {
            swooshSource.Play();
        }
    }
}
