using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class PersistentMusicPlayer : MonoBehaviour
{
    public AudioClip musicClip;
    private AudioSource audioSource;
    private static PersistentMusicPlayer instance;

    void Awake()
    {
        // Singleton pattern to prevent duplicates
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();

        if (musicClip != null)
        {
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No music clip assigned to PersistentMusicPlayer.");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop music and destroy the object when entering "PlayScene"
        if (scene.name == "PlayScene")
        {
            audioSource.Stop();
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        // Clean up listener if object is destroyed
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
