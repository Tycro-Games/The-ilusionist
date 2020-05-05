using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{
    public AudioMixer audioMixer = null;

    public Slider vol = null;

    public void SetVolume (float volume)
    {
        audioMixer.SetFloat ("volume", volume);
    }
    private void Update ()
    {
        audioMixer.GetFloat ("volume", out float value);
        if (vol.value!= value)
        {
            vol.value = value;
        }
    }
}
