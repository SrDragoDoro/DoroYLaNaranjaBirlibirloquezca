using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public GameObject BulletPrefabEnemy;
    private GameObject Player;
    [SerializeField] private float intervalo = 1f;
    [SerializeField] private float intervalo2 = 3f;

    private bool attacking = false; //Control de bucle, caso contrario las balas no se mover�n

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (!attacking)
        {
            StartCoroutine(CoroutineAttack());
            StartCoroutine(CoroutineAttack2());
        }
    }


    IEnumerator CoroutineAttack()
    {
        attacking = true;        

        GameObject bullet = Instantiate(BulletPrefabEnemy, transform);                         //-> Crear bala
        bullet.transform.position = transform.position;                             //Coloca la bala en la posicion del enemigo
        bullet.transform.up = PlayerLocate();                                            //Ajusta el �ngulo de la bala a la ubicaci�n del player

        yield return new WaitForSeconds(intervalo);

        attacking = false;
    }

    IEnumerator CoroutineAttack2()
    {
        attacking = true;
        
        GameObject bullet = Instantiate(BulletPrefabEnemy, transform);                         //-> Crear bala
        bullet.transform.localScale = new Vector3(3, 3, 0);
        bullet.transform.position = transform.position;                             //Coloca la bala en la posicion del enemigo
        bullet.transform.up = PlayerLocate();                                            //Ajusta el angulo de la bala a la ubicaci�n del player

        yield return new WaitForSeconds(intervalo2);

        attacking = false;
    }
    public Vector2 PlayerLocate()
    {
        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position; //Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;

        return direction;
    }
}