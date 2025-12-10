using System.Collections;
using UnityEngine;

public class BossFollow : MonoBehaviour
{
    private GameObject player;                               // Referencia del Player
    [SerializeField] private GameObject enemyDead;
    [SerializeField] private AudioClip deadSound;

    [SerializeField] public static int lifeBoss = 300;
    [SerializeField] private float speed = 1f;      
                                               

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;                // 1. Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;                                                                    // 2. Normalizar la direcci�n
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;               // 3. Mover al enemigo     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("trigger entro: " + collision.tag);
        if (collision.CompareTag("ShotPlayer"))
        {
            lifeBoss--;
            //print("Enemigo 1 tiene de vida: " + lifeEnemy);
            if (lifeBoss <= 0)
            {
                GameObject death = Instantiate(enemyDead);
                death.transform.position = gameObject.transform.position;                
                Destroy(gameObject);
                StartCoroutine(PlayAudioForDuration(death, deadSound, 0.01f, 2.8f, 1f)); //Debe ser hijo del Death para que no se quede en jerarquia
                Destroy(death, 1f);
                print("¡Has derrotado al Boss Final!");
                print("¡Salvaste tu mundo!");
            }
        }        
    }

    //Sección audio
    IEnumerator PlayAudioForDuration(GameObject parent, AudioClip clip, float start, float duration, float volume)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.SetParent(parent.transform);                           //Hace que el audio sea hijo del objeto death

        AudioSource temp = tempGO.AddComponent<AudioSource>();
        temp.volume = volume;
        temp.clip = clip;
        temp.time = start;
        temp.Play();

        yield return new WaitForSeconds(duration);

        Destroy(tempGO);
    }
}