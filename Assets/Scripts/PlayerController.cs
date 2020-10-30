using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D theRB;
    public float moveSpeed = 5f;
    private Vector2 moveInput;
    private Vector2 mouseInput;

    public float mouseSensitivity = 1f;
    public Camera viewCam;
    public GameObject bulletImpact;
    public int bulletAmmo = 25;
    public Animator gunAnim;
    public int currentHealth;
    public int maxHealth = 100;
    public GameObject gOver;
    private bool ded = false;
    public Text hpText;
    public Text ammoText;
    public Animator anim;
    private AudioSource as_audio;

    private void Awake(){
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hpText.text = currentHealth.ToString() + "%";
        ammoText.text = bulletAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(!ded){

            
            //movimento
            moveInput = new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
            Vector3 moveHorizontal = -transform.up * moveInput.x;
            Vector3 moveVertical = transform.right * moveInput.y;
            theRB.velocity = (moveHorizontal + moveVertical) * moveSpeed;

            //camera
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z - mouseInput.x);
            viewCam.transform.localRotation = Quaternion.Euler(viewCam.transform.localRotation.eulerAngles + new Vector3(0f,mouseInput.y,0f));

            //atirar
            if(Input.GetMouseButtonDown(0)) {
                if(bulletAmmo>0){
                    bulletAmmo-=1;
                    ammoText.text = bulletAmmo.ToString();
                    Ray ray = viewCam.ViewportPointToRay(new Vector3(.5f,.5f,0f));
                    RaycastHit hit;
                    if(Physics.Raycast(ray,out hit)){
                        Instantiate(bulletImpact,hit.point,transform.rotation);
                        if(hit.transform.tag == "Monster"){
                            hit.transform.parent.GetComponent<EnemyController>().TakeDamage();
                        }
                        if(hit.transform.tag == "Boss"){
                            hit.transform.parent.GetComponent<Boss>().TakeDamage();
                        }
                    }
                    gunAnim.SetTrigger("Shoot");
                }
            }
            if(moveInput != Vector2.zero){
                anim.SetBool("moving",true);
            }else{
                anim.SetBool("moving",false);
            }
        }
    }

    public void TakeDamage(int damage){
        currentHealth -= damage;
        as_audio = GetComponent<AudioSource>();
        as_audio.Play();
        if(currentHealth <= 0){
            AudioListener.volume = 0;
            gOver.SetActive(true);
            ded = true;
        }
        hpText.text = currentHealth.ToString() + "%";
    }

    public void Heal(int amount){
        currentHealth += amount;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        hpText.text = currentHealth.ToString() + "%";        
    }
}

