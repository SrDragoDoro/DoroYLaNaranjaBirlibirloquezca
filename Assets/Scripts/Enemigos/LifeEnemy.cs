using UnityEngine;

public class LifeEnemy : MonoBehaviour
{   
    [SerializeField] private GameObject enemyDead;
    [SerializeField] private int lifeEnemy = 4;    
    private const int RequiredEnemyDeath = 60; //Numero requerido de enemigos muertos para activar la invocacion del jefe
      

    public void CountEnemyDeath()
    {
        GameManager.NumberEnemyDeath++;
        print("Enemigos muertos: " + GameManager.NumberEnemyDeath);
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
                GameObject death = Instantiate(enemyDead);
                death.transform.position = gameObject.transform.position;
                Destroy(gameObject);
                Destroy(death, 1.5f);                
                CountEnemyDeath();                               
            }
        }
    }    

    //Condiciones para el boss
    public static int NumberEnemyDeathGet() 
    {
        return GameManager.NumberEnemyDeath;
    }

    public static int RequiredEnemyDeathGet()
    {
        return RequiredEnemyDeath;
    }
}