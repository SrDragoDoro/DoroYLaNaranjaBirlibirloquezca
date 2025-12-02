using System.Collections.Generic;
using UnityEngine;

/*Contenido
Velodidad de la bala
Tiempo de existencia sin colisiones específicas
Condiciones de comparación por colisión por Trigger (Destrucción por única colisión)
*/

public class Bullet : MonoBehaviour
{
    private Transform targetEnemy;
    [SerializeField] private float Speed;
    [SerializeField] float minDistance = 2f; // rango para detectar enemigos    
    private List<string> tagslist = new List<string>()
    {     "Enemy", "EnemyTwo", "BulletEnemyOne" };
    void Start()
    {     
        Destroy(gameObject, 5);
    }

    void Update()
    {
        EnemyNearby();
        if (targetEnemy != null)                                                                //Si se detecta que uno de los tags está cerca エネミー
        {
            Vector2 direction = (targetEnemy.position - transform.position).normalized;         // 1. Encontrar dirección          
            transform.up = Vector2.Lerp(transform.up, direction, Time.deltaTime * (Speed + 3));   // 2. Girar bala hacia el enemigo
            transform.position += transform.up * Speed * Time.deltaTime;                        // 3. Avanzar
        }
        else
        {
            transform.position += transform.up * Speed * Time.deltaTime; //DIRECCION VELOCIDAD TIME DELTA TIME // Movimiento normal
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in tagslist)
        {
            if (collision.CompareTag(tag))
            {
                Destroy(gameObject);
            }
        }
        //if (collision.CompareTag("Enemy")     || collision.CompareTag("EnemyTwo") ||
        //    collision.CompareTag("MuroNegro"))
        //    Destroy(gameObject);
    }
    public void EnemyNearby()
    {        
        Transform nearest = null; //un método del GameObject vacío エネミー

        foreach (string tag in tagslist)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag); //Array, contiene los tags

            foreach (GameObject tags in enemies)
            {
                float dist = Vector2.Distance(transform.position, tags.transform.position);

                if ( dist < minDistance)
                {                    
                    nearest = tags.transform; //Guardamos la ubicación encontrada en Transform vacío　エネミー
                }
            }
        }
        targetEnemy = nearest; // si es null, no hay enemigos en rango エネミー
    }
}
