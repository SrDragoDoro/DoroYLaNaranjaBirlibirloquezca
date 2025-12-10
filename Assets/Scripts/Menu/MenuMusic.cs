using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public static MenuMusic instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip menu;


    void Start()
    {
        audioSource.PlayOneShot(menu, 0.25f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
