using System;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
/*Contenido
Control de movimento (tipo MOBA)
Creación de disparo y acción de disparo
Vida del player
*/

[Serializable] 
public struct PlayerStatistics //Sin asignar
{
    public float Speed;
    public float lifePlayer;
    public PlayerStatistics(float _speed, float _lifePlayer)
    {
        Speed = _speed;
        lifePlayer = _lifePlayer;
        
    }
}

public class PlayerController : MonoBehaviour
{
    public PlayerStatistics Statistics;

    [SerializeField] private Animator PlayerWalk;
    private bool move = false; //Simula un swichs de cambio para no hacer un movimiento constante al dar click
    private Vector2 destiny;

    [SerializeField] private float lifePlayer;
    [SerializeField] private float Speed;
 
    private void Start()
    {
        Statistics = new PlayerStatistics(Speed, lifePlayer);
    }
    void Update()
    {        
        if (move) //Si es TRUE hay movimiento 
        {
            MoveToDirection();
            PlayerWalk.SetBool("Caminar", true);
        }
        else
        {
            PlayerWalk.SetBool("Caminar", false);
        }

        if (Input.GetMouseButton(0))
        {
            SetDestiny();
        }
    }  

    public void SetDestiny() //Método para obtener las coordenadas del mouse
    {
        destiny = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        move = true;

        if (destiny.x < transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);   // izquierda
        else
            transform.localScale = new Vector3(1, 1, 1);    // derecha
    }

    public void MoveToDirection()
    {      
        Vector3 moveDirection = (destiny - (Vector2)transform.position).normalized; 
        transform.position += moveDirection * Statistics.Speed * Time.deltaTime;

        if (Vector2.Distance(transform.position, destiny) <= 0.1f)
            move = false;
    }   

    public void PlayerLife(float damage)
    {
        bool txt = false;

        Statistics.lifePlayer -= damage;
        print("Tu vida actual es: " + Statistics.lifePlayer);
        if (Statistics.lifePlayer <= 0)
        {
            print("Has muerto D:");
            print("¡Tu universo ha sido invadido!");
            if (!txt)
            {
                OhNo.CreateTextFile();
                txt = true;
            }            
        }
    }

    public float GetLifePlayer()
    {
        return Statistics.lifePlayer;
    }
}
