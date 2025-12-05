using System.Collections.Generic;
using UnityEngine;
/*Contenido
Velodidad de la bala
Tiempo de existencia sin colisiones espec�ficas
Condiciones de comparaci�n por colision por Trigger (Destrucci�n por �nica colisi�n)
*/

public class EnemyOneBullet : MonoBehaviour
{
    [SerializeField] private PlayerController playerController; //Referencia a PlayerController para acceder al m�todo de vida
    [SerializeField] private float Speed = 3f;
    [SerializeField] private float Damage = 10f;
    [SerializeField] private float DestroyBullet = 5f;
    private List<string> tagslistE = new List<string>
    {     "Player", "ShotPlayer" };

    void Start()
    {
        Destroy(gameObject, DestroyBullet);
    }

    void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime; //DIRECCION VELOCIDAD TIME DELTA TIME
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (string tag in tagslistE)
        {
            if (collision.CompareTag(tag))
            {
                Destroy(gameObject);
                if (collision.CompareTag("Player"))
                {
                    playerController.PlayerLife(Damage); //Causar dano al jugador al colisionar con la bala
                }
            }
        }
    }
}