using UnityEngine;

public class BossFollow : MonoBehaviour
{
    private GameObject player;                      // Referencia del Player    
    [SerializeField] private float speed = 1f;    // Velocidad del enemigo
    [SerializeField] private int lifeBoss = 100; // Vida del Boss                                             

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Vector2 direction = (Vector2)player.transform.position - (Vector2)transform.position;                // 1. Calcular direcci�n hacia el jugador (Vector2)        
        direction = direction.normalized;                                                                    // 2. Normalizar la direcci�n
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;              // 3. Mover al enemigo     
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
                Destroy(gameObject);
                print("¡Has derrotado al Boss Final!");
                print("¡Salvaste tu mundo!");
            }
        }
    }
}