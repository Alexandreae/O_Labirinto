using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool pause = false;
    private AudioSource as_audio;
    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        as_audio = GetComponent<AudioSource>();
        as_audio.Play();
        
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            UnlockCursor();
            pause = true;
        }
        if(Input.GetMouseButtonDown(0)){
            LockCursor();
            pause = false;
        }
    }

    private void LockCursor(){
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }

    private void UnlockCursor(){
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }    
}
