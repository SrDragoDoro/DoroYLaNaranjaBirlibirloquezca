using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] levelTheme;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (levelTheme.Length > 0) // Verifica que haya clips en el array
        {
            AudioClip randomClip = levelTheme[Random.Range(0, levelTheme.Length)];
            PlayMusic(randomClip);
        }
    }

    private void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;

        audioSource.clip = clip;
        audioSource.loop = true;  
        audioSource.volume = 0.45f;
        audioSource.Play();
    }
}
