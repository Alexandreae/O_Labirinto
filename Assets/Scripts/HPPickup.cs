using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPPickup : MonoBehaviour
{
    public int hp = 30;
    private AudioSource as_audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other){
        as_audio = GetComponent<AudioSource>();
        as_audio.Play();
        PlayerController.instance.Heal(hp);
        Invoke("destruir",.5f);
        
    }
    void destruir(){
        Destroy(gameObject);
    }
}