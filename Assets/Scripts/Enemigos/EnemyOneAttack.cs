using System.Collections;
using UnityEngine;

public class EnemyOneAttack : MonoBehaviour
{
    public GameObject BulletPrefabEnemy;
    public Transform Player;
    [SerializeField] private float intervalo = 1f;
    private bool attacking = false;

    void Update()
    {
        if (!attacking)
        {
            StartCoroutine(CoroutineAttack());
        }
    }

    IEnumerator CoroutineAttack()
    {
        attacking = true;

        Vector2 direction = (Vector2)Player.position - (Vector2)transform.position; // 1. Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;                                           // 2. Normalizar la dirección

        GameObject bullet = Instantiate(BulletPrefabEnemy);                         //-> Crear bala
        bullet.transform.position = transform.position;                             //Coloca la bala en la posición del enemigo
        bullet.transform.up = direction;                                            //Ajusta el ángulo de la bala a la ubicación del player
       
        yield return new WaitForSeconds(intervalo);

        attacking = false;
    }
}
