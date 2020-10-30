using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health = 3;
    public GameObject explosion;
    public float playerRange = 10f;
    public Rigidbody2D theRB;
    public float moveSpeed;
    public bool shoot = true;
    public float fireRate = .5f;
    private float wait;
    public GameObject bullet;
    public Transform firePoint;
    private AudioSource as_audio;
    public GameObject victory;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,PlayerController.instance.transform.position) < playerRange){
            Vector3 playerDirection = PlayerController.instance.transform.position - transform.position;
            theRB.velocity = playerDirection.normalized * moveSpeed;

            if(shoot){
                wait -= Time.deltaTime;
                if(wait <= 0){
                    Instantiate(bullet,firePoint.position,firePoint.rotation);
                    wait = fireRate;
                }
            }

        }else{
            theRB.velocity = Vector2.zero;
        }

    }

    public void TakeDamage(){
        health--;
        if(health <= 0){
            AudioListener.volume = 0;
            victory.SetActive(true);

        }
    }
    void destruir(){
        Destroy(gameObject);
    }
}
