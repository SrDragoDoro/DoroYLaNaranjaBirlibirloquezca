using System.Collections;
using UnityEngine;

public class AttackEnemyThree : MonoBehaviour
{
    private GameObject Player;
    [SerializeField] private GameObject EnemyBulletPrefab; 
    [SerializeField] private float intervalo = 1.5f;
    private bool attacking = false; //Control de bucle, caso contrario las balas no se moverán

    void Start()
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

        ShootProyectileTriple();
        yield return new WaitForSeconds(intervalo);

        attacking = false;
    }

    public Vector2 PlayerLocate()
    {
        Vector2 direction = (Vector2)Player.transform.position - (Vector2)transform.position; //Calcular dirección hacia el jugador (Vector2)        
        direction = direction.normalized;

        return direction;
    }

    public void ShootProyectileTriple()
    {
        float spreadAngle = 10f; // rotación a cada lado

        SpawnBulletWithAngle(PlayerLocate(), -spreadAngle);  // Bala izquierda
        SpawnBulletWithAngle(PlayerLocate(), 0f);            // Bala central    
        SpawnBulletWithAngle(PlayerLocate(), spreadAngle);   // Bala derecha
    }

    public void SpawnBulletWithAngle(Vector2 direction, float angle)
    {
        GameObject bulletL = Instantiate(EnemyBulletPrefab, transform);
        bulletL.transform.position = gameObject.transform.position;                 
        Vector2 newDirL = Quaternion.Euler(0, 0, angle) * direction;         // Rotar la dirección
        bulletL.transform.up = newDirL;

        GameObject bulletR = Instantiate(EnemyBulletPrefab, transform);
        bulletR.transform.position = gameObject.transform.position;                
        Vector2 newDirR = Quaternion.Euler(0, 0, angle) * direction;         // Rotar la dirección
        bulletR.transform.up = newDirR;
    }

}
