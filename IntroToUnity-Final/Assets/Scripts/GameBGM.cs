using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameBGM : MonoBehaviour
{
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.Play();
    }
}
