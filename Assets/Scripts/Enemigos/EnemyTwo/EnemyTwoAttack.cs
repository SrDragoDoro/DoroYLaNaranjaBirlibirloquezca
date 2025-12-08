using System.Collections;
using UnityEngine;

/*Contenido
Hacer uso del script para hacer explotar al enenigo al colisionar con el player
*/

public class EnemyTwoAttack : MonoBehaviour
{    
    [SerializeField] private GameObject deathEnemy;
    [SerializeField] private float Damage = 15f;

    public void OnCollisionEnter2D(Collision2D collision)
    {   
        if (collision.gameObject.CompareTag("Player")) //Comparar colisión para realizar la detonación del enemigo y causar daño
        {
            GameObject death = Instantiate(deathEnemy);
            death.transform.position = gameObject.transform.position;
            Destroy(gameObject);
            Destroy(death,1);
            collision.gameObject.GetComponent<PlayerController>().PlayerLife(Damage);            
        }
    }
}
