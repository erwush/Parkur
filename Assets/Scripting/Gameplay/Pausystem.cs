using UnityEngine;

public class Pausystem : MonoBehaviour
{

    public bool isPaused = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPaused && Input.GetButtonDown("Pause")){
            Time.timeScale = 1f;
            isPaused = false;
        } else if(!isPaused && Input.GetButtonDown("Pause")){
            Time.timeScale = 0f;
            isPaused = true;
        }
    }
}
