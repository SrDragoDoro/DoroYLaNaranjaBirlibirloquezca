using System.Collections;
using UnityEngine;

/*Contenido
Cortinas para intervalos en ataques enemigos
Creación de objetos de disparo
*/

public class EnemyOneAttack : MonoBehaviour
{
    public GameObject BulletPrefabEnemy;
    private GameObject Player;
    [SerializeField] private float intervalo = 1f;
    private bool attacking = false; //Control de bucle, caso contrario las balas no se moverán

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

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

        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position; //Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;                                                     //Normalizar la dirección

        GameObject bullet = Instantiate(BulletPrefabEnemy);                         //-> Crear bala
        bullet.transform.position = transform.position;                             //Coloca la bala en la posición del enemigo
        bullet.transform.up = direction;                                            //Ajusta el ángulo de la bala a la ubicación del player
       
        yield return new WaitForSeconds(intervalo);

        attacking = false;
    }
}
