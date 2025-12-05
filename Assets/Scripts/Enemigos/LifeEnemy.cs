using UnityEngine;

public class LifeEnemy : MonoBehaviour
{
    [SerializeField] private int lifeEnemy = 4;
    private static int NumberEnemyDeath;       //Contador estatico para el numero de enemigos muertos
    private const int RequiredEnemyDeath = 25; //Numero requerido de enemigos muertos para activar la invocacion del jefe

    public void CountEnemyDeath()
    {
        NumberEnemyDeath++;
        print("Numero de enemigos muertos: " + NumberEnemyDeath);
    }

    public void WinCondition()
    {
        if (NumberEnemyDeath >= RequiredEnemyDeath)
        {
            print("¡Ahora debemos acabar con el m�s fuerte!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("trigger entro: " + collision.tag);
        if (collision.CompareTag("ShotPlayer"))
        {
            lifeEnemy--;
            //print("Enemigo 1 tiene de vida: " + lifeEnemy);
            if (lifeEnemy <= 0)
            {
                Destroy(gameObject);
                CountEnemyDeath();
                WinCondition();
            }
        }
    }

    public static int NumberEnemyDeathGet()
    {
        return NumberEnemyDeath;
    }

    public static int RequiredEnemyDeathGet()
    {
        return RequiredEnemyDeath;
    }
}