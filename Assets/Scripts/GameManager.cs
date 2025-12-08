using UnityEngine;

public struct GameData
{
    public PlayerStatistics player;
    public int currentEnemyCount;
    public int numberEnemyDeath;
}

public class GameManager : MonoBehaviour
{         
    public GameData GameData;
    public static int NumberEnemyDeath;  //Contador estatico para el numero de enemigos muertos    

    void Start()
    {
        NumberEnemyDeath = 0;
    }

    void Update()
    {
        FinalBattle();
        SaveSystemControll();
    }

    public void FinalBattle()
    {
        if (NumberEnemyDeath >= LifeEnemy.RequiredEnemyDeathGet())
        {
            print("¡Ahora debemos acabar con el más fuerte!");
        }
    }

    public void SaveSystemControll()
    {
        if (Input.GetKeyDown(KeyCode.S)) //Guardar juego
        {
            CollectSaveData();
            SaveSystem.SaveGame(GameData);
        }

        if (Input.GetKeyDown(KeyCode.L)) //Cargar juego
        {
            ApplyLoadedData();
            GameData = SaveSystem.LoadGame();
        }
    }

    private void CollectSaveData() //Recolecta los datos actuales del juego para guardarlos
    {
        GameData.currentEnemyCount = EnemyController.currentQuantity;
        GameData.numberEnemyDeath = NumberEnemyDeath;
    }

    private void ApplyLoadedData() //Aplica los datos cargados del guardado al juego
    {
        EnemyController.currentQuantity = GameData.currentEnemyCount;
        NumberEnemyDeath = GameData.numberEnemyDeath;       
    }



}
