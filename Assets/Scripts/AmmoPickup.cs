using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 25;
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
        if(other.tag == "Player"){
            as_audio = GetComponent<AudioSource>();
            as_audio.Play();
            PlayerController.instance.bulletAmmo += ammoAmount;
            PlayerController.instance.ammoText.text = PlayerController.instance.bulletAmmo.ToString();
            Invoke("destruir",.5f);
            
        }
    }

    void destruir(){
        Destroy(gameObject);
    }
}