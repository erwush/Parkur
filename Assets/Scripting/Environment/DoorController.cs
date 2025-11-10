using System.Collections;
using UnityEngine;

public class DoorController : MonoBehaviour
{

    public BoxCollider2D triggerArea;
    public CapsuleCollider2D doorCollider;
    [SerializeField]private bool isOpen;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        triggerArea = GetComponent<BoxCollider2D>();
        doorCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerArea.IsTouchingLayers(LayerMask.GetMask("Player")) && Input.GetKeyUp(KeyCode.E) && isOpen == false)
        {
            doorCollider.isTrigger = true;
            isOpen = true;

        }
        else if (triggerArea.IsTouchingLayers(LayerMask.GetMask("Player")) && Input.GetKeyUp(KeyCode.E) && isOpen == true)
        {
            doorCollider.isTrigger = false;
            isOpen = false;

        }
    }

    //lerp for the door open and close
    public IEnumerator DoorOpenClose()
    {
        yield return new WaitForSeconds(0.1f);
    }
}
