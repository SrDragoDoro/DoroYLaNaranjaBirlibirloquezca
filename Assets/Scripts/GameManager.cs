using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int NumberEnemyDeath;  //Contador estatico para el numero de enemigos muertos    

    void Start()
    {
        NumberEnemyDeath = 0;
    }

    void Update()
    {
        FinalBattle();
    }

    public void FinalBattle()
    {
        if (NumberEnemyDeath >= LifeEnemy.RequiredEnemyDeathGet())
        {
            print("¡Ahora debemos acabar con el más fuerte!");
        }
    }


}
