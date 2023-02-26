using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Livia
{
    public class SettingsManager : MonoBehaviour
    {
        public AudioMixer audioMixer;
        private string volume_string;

        public void SetVolumeString(string v)
        {
            volume_string = v;
        }

        public void SetVolume(float volume)
        {
            audioMixer.SetFloat(volume_string,volume);
        }
    }
}
