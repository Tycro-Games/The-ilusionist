using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    public AudioMixer audioMixer = null;
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat ("volume", volume);
    }
}
