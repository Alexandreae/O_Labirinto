using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int damage;
    public float bulletSpeed = 5f;
    private float wait = 0f;

    public Rigidbody2D theRB;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = PlayerController.instance.transform.position - transform.position;
        direction.Normalize();
        direction = direction * bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        wait += Time.deltaTime;
        theRB.velocity = direction * bulletSpeed;
        if(wait > 3){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            PlayerController.instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
