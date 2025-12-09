using System.Collections;
using UnityEngine;

public class AttackEnemyFour : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject BulletPrefabEnemy;
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

        ShootProyectile();
        yield return new WaitForSeconds(intervalo);

        attacking = false;
    }

    public void ShootProyectile()
    {
        GameObject bullet = Instantiate(BulletPrefabEnemy, transform);                   //-> Crear bala
        bullet.transform.localScale = new Vector3(2, 2, 0);
        bullet.transform.position = transform.position;                                  //Coloca la bala en la posición del enemigo
        bullet.transform.up = PlayerLocate();                                            //Ajusta el ángulo de la bala a la ubicación del player
    }

    public Vector2 PlayerLocate()
    {
        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position; //Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;

        return direction;
    }
}
