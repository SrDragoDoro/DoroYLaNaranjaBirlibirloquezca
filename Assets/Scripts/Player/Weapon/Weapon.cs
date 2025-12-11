/*
Contenido:
Control de disparo del arma del jugador
Creación de proyectiles en la dirección del mouse
Contenedor de balas
*/

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering;

public enum ShotType 
{ 
    None,
    Normal, 
    Doble, 
    Triple 
}

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletParent; //Padre de las balas
        [SerializeField] private GameObject BulletPrefab;  //Hija de las bulletParent
        [SerializeField] private GameObject BulletPrefab2; //Hija de las bulletParent
        [SerializeField] private GameObject BulletPrefab3; //Hija de las bulletParent

    [SerializeField] private GameObject ultiPrefabL; // Ulti/rayo
    [SerializeField] private GameObject ultiPrefabR; // Ulti/rayo   
    private bool ultiAvailable = true;
    [SerializeField] private float ultiCooldown = 20f;


    [SerializeField] private AudioClip shotSound; 
    [SerializeField] private AudioClip ultiSound;       


    private GameObject currentRayL;                  // Rayo actualmente activo, empieza nulo
    private GameObject currentRayR;                  // Rayo actualmente activo, empieza nulo


    [SerializeField] private Transform weaponLeft;
    [SerializeField] private Transform FirePointLeft;
    [SerializeField] private Transform weaponRight;
    [SerializeField] private Transform FirePointRight;

   

    public ShotType currentShot = ShotType.Normal; // Tipo de disparo actual

    private void Start()
    {             
        bulletParent = GameObject.Find("BulletContainer");
    }

    void Update()
    {
        // Siempre apunta las armas al mouse
        RotateWeaponsToMouse();
        UpdateRayDirection();

        if (Input.GetKeyDown(KeyCode.Q) && ultiAvailable)
        {
            StartCoroutine(UseUlti());            
        }            

        ShotControll();
        if (Input.GetMouseButtonDown(1))
        {
            StartCoroutine(PlayAudioForDuration(shotSound, 0.7f, 0.1f, 0.9f));
            Fire();
        }       
    }

    //Sección audio
    IEnumerator PlayAudioForDuration(AudioClip clip, float start, float duration, float volume)
    {
        GameObject tempGO = new GameObject("TempAudio");        //Crea un objeto temporal
        AudioSource temp = tempGO.AddComponent<AudioSource>();  //Agrega un componente AudioSource al objeto temporal

        temp.volume = volume;
        temp.clip = clip;
        temp.time = start;
        temp.Play();

        yield return new WaitForSeconds(duration);

        Destroy(tempGO);   //Para no ocupar memoria 
    }

    //Sección rotación de armas
    private void RotateWeaponsToMouse()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Posición del mouse en el mundo

        // Dirección hacia el mouse
        Vector2 dirLeft = mousePos - (Vector2)weaponLeft.position;
        Vector2 dirRight = mousePos - (Vector2)weaponRight.position;

        // Rota armas apuntando hacia el mouse
        weaponLeft.up = dirLeft.normalized;
        weaponRight.up = dirRight.normalized;
    }


    //Sección intercambio de disparos
    public void Fire()
    {
        switch (currentShot)
        {
            case ShotType.Normal:
                ShootProyectile();
                break;

            case ShotType.Doble:
                ShootProyectileDoble();
                break;

            case ShotType.Triple:
                ShootProyectileTriple();
                break;
        }
    }

    public void ShotControll()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentShot = ShotType.Normal;
            print("Disparo Normal Activado");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)) print("Disparo Normal bloqueado");

        if (Input.GetKeyDown(KeyCode.Alpha2) && EnemyController.currentQuantity >= EnemyController.Maxquantrity / 2)
        {
            currentShot = ShotType.Doble;
            print("Disparo Doble Activado");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) print("Disparo doble bloqueado");

        if (Input.GetKeyDown(KeyCode.Alpha3) && BossController.BossHave)
        {
            currentShot = ShotType.Triple;
            print("Disparo Triple Activado");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) print("Disparo triple bloqueado");
    }

    public Vector2 GetMousePosition()
    {
        Vector2 mousePosition = Input.mousePosition;                                         //-> obtener posicon del mouse respecto a la resolucion de la patanlla
        Vector2 worldPositon = Camera.main.ScreenToWorldPoint(mousePosition);                // convirtiendo la posicon del mouse a la posicion en el mundo
        Vector2 shootDirection = (worldPositon - (Vector2)transform.position).normalized;    //calculdo la direccion de disparo

        return shootDirection;
    }


    //Sección de disparos
    public void ShootProyectile()
    {
        GameObject bulletLeft = Instantiate(BulletPrefab, bulletParent.transform);           //-> Crear 
        bulletLeft.transform.position = (Vector2)FirePointLeft.position;                     //Coloca la bala en la posición del player
        bulletLeft.transform.up = GetMousePosition();                                        //Ajusta el ángulo de la bala a la ubicación del mouse

        GameObject bulletRight = Instantiate(BulletPrefab, bulletParent.transform);          //-> Crear 
        bulletRight.transform.position = (Vector2)FirePointRight.position;                   //Coloca la bala en la posición del player
        bulletRight.transform.up = GetMousePosition();                                       //Ajusta el ángulo de la bala a la ubicación del mouse
    }

    public void ShootProyectileDoble()
    {
        int bullets = 2; // Numero de balas a disparar por cada arma       

        for (int i = 0; i < bullets; i++)
        {
            Vector2 offset = new Vector2(0, Random.Range(-0.4f, 0.4f));

            GameObject bulletL = Instantiate(BulletPrefab2, bulletParent.transform);
            bulletL.transform.position = (Vector2)FirePointLeft.position + offset;
            bulletL.transform.up = GetMousePosition();

            GameObject bulletR = Instantiate(BulletPrefab2, bulletParent.transform);
            bulletR.transform.position = (Vector2)FirePointRight.position + offset;
            bulletR.transform.up = GetMousePosition();
        }
    }

    public void ShootProyectileTriple()
    {
        float spreadAngle = 10f; // rotación a cada lado

        SpawnBulletWithAngle(GetMousePosition(), -spreadAngle);  // Bala izquierda
        SpawnBulletWithAngle(GetMousePosition(), 0f);            // Bala central    
        SpawnBulletWithAngle(GetMousePosition(), spreadAngle);   // Bala derecha
    }

    public void SpawnBulletWithAngle(Vector2 direction, float angle)
    {
        GameObject bulletL = Instantiate(BulletPrefab3, bulletParent.transform);
        bulletL.transform.position = FirePointLeft.position;                 // Usa el FirePointLeft 
        Vector2 newDirL = Quaternion.Euler(0, 0, angle) * direction;         // Rotar la dirección
        bulletL.transform.up = newDirL;

        GameObject bulletR = Instantiate(BulletPrefab3, bulletParent.transform);
        bulletR.transform.position = FirePointRight.position;                // Usa el FirePointLeft 
        Vector2 newDirR = Quaternion.Euler(0, 0, angle) * direction;         // Rotar la dirección
        bulletR.transform.up = newDirR;
    }


    //Sección de ulti/rayo
    private void UpdateRayDirection()
    {
        if (currentRayL != null)
        {
            currentRayL.transform.up = GetMousePosition();
            currentRayL.transform.Rotate(0, 0, -90f); // corrige desfase
        }

        if (currentRayR != null)
        {
            currentRayR.transform.up = GetMousePosition();
            currentRayR.transform.Rotate(0, 0, -90f);
        }
    }

    public void Ulti()
    {
        // Instanciar rayo izquierdo
        currentRayL = Instantiate(ultiPrefabL, weaponLeft);
        currentRayL.transform.position = FirePointLeft.position;
        Destroy(currentRayL, 6f); // Duración del rayo

        // Instanciar rayo derecho
        currentRayR = Instantiate(ultiPrefabR, weaponRight);
        currentRayR.transform.position = FirePointRight.position;
        Destroy(currentRayR, 6f); // Duración del rayo
    }

    IEnumerator UseUlti()
    {
        ultiAvailable = false;  // bloquea el uso
        
        StartCoroutine(PlayAudioForDuration(ultiSound, 0.2f, 6f, 0.25f));// Sonido
        Ulti();                                                          // Lanzar ulti

        yield return new WaitForSeconds(ultiCooldown);        
        ultiAvailable = true;   // se vuelve a habilitar
        StartCoroutine(PlayAudioForDuration(shotSound, 0.1f, 0.2f, 0.25f));
        print("Ulti lista!");
    }
}
