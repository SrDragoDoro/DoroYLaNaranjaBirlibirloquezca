using UnityEngine;

//DIRECCION VELOCIDAD TIME DELTA TIME
public class EnemyOneBullet : MonoBehaviour
{
    public float Speed = 3f;
    void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += transform.up * Speed * Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            Destroy(gameObject);
    }
}
