using System.Collections;
using UnityEngine;

public struct GameData
{    
    public PlayerStatistics player;
    public int currentEnemyCount;
    public int numberEnemyDeath;
}

public class GameManager : MonoBehaviour
{           
    private PlayerController playerController;
    public GameData GameData;       
    public static int NumberEnemyDeath;  //Contador estatico para el numero de enemigos muertos    

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(Autosave());
        NumberEnemyDeath = 0;
    }

    void Update()
    {
        ShotDesbloqueate();
        FinalBattle();
        SaveSystemControll();       
    }

    public void ShotDesbloqueate()
    {
        if (EnemyController.currentQuantity >= EnemyController.Maxquantrity / 2)
        {
            print("¡Has desbloqueado el disparo doble!");
        }

        if (BossController.BossHave)
        {
            print("¡Has desbloqueado el disparo triple!");
        }
    }

    public void FinalBattle()
    {
        if (NumberEnemyDeath >= LifeEnemy.RequiredEnemyDeathGet())
        {
            print("¡Ahora debemos acabar con el más fuerte!");
        }
    }

    public  void winCondition()
    {
        if (BossFollow.lifeBoss <= 0 && EnemyController.currentQuantity == EnemyController.Maxquantrity)
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

        if (Input.GetKeyDown(KeyCode.F5)) //Cargar autoguardado juego
        {            
            GameData = AutoSaveSystem.LoadGame();
            ApplyLoadedData();
        }

        if (Input.GetKeyDown(KeyCode.L)) //Cargar juego
        {            
            GameData = SaveSystem.LoadGame();
            ApplyLoadedData();
        }        
    }

    IEnumerator Autosave()
    {
        while (true)
        {
            yield return new WaitForSeconds(100);
            CollectSaveData();
            AutoSaveSystem.SaveGame(GameData);
            print("Autosave realizado");
        }
    }

    private void CollectSaveData() //Recolecta los datos actuales del juego para guardarlos
    {
        GameData.currentEnemyCount = EnemyController.currentQuantity;
        GameData.numberEnemyDeath = NumberEnemyDeath;
        GameData.player = playerController.Statistics;
    }

    private void ApplyLoadedData() //Aplica los datos cargados del guardado al juego
    {
        EnemyController.currentQuantity = GameData.currentEnemyCount;
        NumberEnemyDeath = GameData.numberEnemyDeath;
        playerController.Statistics = GameData.player;
    }
}
