using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;   // Referencia del Player
    public float speed = 1f;   // Velocidad del enemigo

    void Update()
    {        
        Vector2 direction = (Vector2)player.position - (Vector2)transform.position;           // 1. Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;                                                     // 2. Normalizar la dirección
        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;// 3. Mover al enemigo usando solo Vector2        
    }
}