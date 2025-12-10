using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager musicManager;      // Solo puede haber una instancia de MusicManager
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] levelTheme;

    private void Start()
    {
        // Evitar duplicados al cambiar de escena
        if (musicManager != null)
        {
            Destroy(gameObject);
            return;
        }

        musicManager = this; // Asignar la instancia actual

        // Esto permite que la música NO se corte al cambiar de escena
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        PlayMusic(levelTheme);
    }

    public void PlayMusic(AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        audioSource.clip = clips[Random.Range(0, clips.Length)];
        audioSource.loop = true;
        audioSource.volume = 0.45f;
        audioSource.Play();
    }

    //ELiminar musica
    public void StopMusic()
    {
        audioSource.Stop();
    }

    public void ChangeVolume(float newVolume)
    {
        audioSource.volume = newVolume;
    }
}
