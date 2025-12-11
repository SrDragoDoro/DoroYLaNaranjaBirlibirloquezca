using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct GameData
{    
    public PlayerStatistics player;
    public int currentEnemyCount;
    public int numberEnemyDeath;
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject Loading;
    private PlayerController playerController;
    public GameData GameData;       
    public static int NumberEnemyDeath;  //Contador estatico para el numero de enemigos muertos    
    private bool isSaving = false;

    void Start()
    {
        
        print("¡Bienvenido a la aventura!");
        Loading.SetActive(false);
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        StartCoroutine(Autosave());
        NumberEnemyDeath = 0;
    }

    void Update()
    {
        ShotUnlocked();
        FinalBattle();
        SaveSystemControll();       
    }

    public void ShotUnlocked()
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

    public void BossCondition()
    {
        if (BossFollow.lifeBoss <= 0 && EnemyController.currentQuantity == EnemyController.Maxquantrity)
        {
            print("¡Ahora debemos acabar con el más fuerte!");
        }            
    }

    public void SaveSystemControll()
    {
        if (Input.GetKeyDown(KeyCode.S) && !isSaving) //Guardar juego
        {            
            StartCoroutine(ManualSave());
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

    IEnumerator ManualSave()
    {
        isSaving = true;

        CollectSaveData();
        SaveSystem.SaveGame(GameData);

        Loading.SetActive(true);
        yield return new WaitForSeconds(2);
        Loading.SetActive(false);

        isSaving = false;
    }

    IEnumerator Autosave()
    {
        while (true)
        {
            yield return new WaitForSeconds(100);
            CollectSaveData();
            AutoSaveSystem.SaveGame(GameData);
            print("Autosave realizado");

            Loading.SetActive(true);
            yield return new WaitForSeconds(2);
            Loading.SetActive(false);
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
