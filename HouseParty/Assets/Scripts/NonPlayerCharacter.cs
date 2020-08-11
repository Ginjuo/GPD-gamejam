using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public GameObject DialogBox;
    // Start is called before the first frame update
    void Start()
    {
        DialogBox.SetActive(false);
    }

    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Enter dialog displaying!");
        DialogBox.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        Debug.Log("Enter dialog not displaying!");
        DialogBox.SetActive(false);
    }

    public void DisplayDialog()
    {
        Debug.Log("Dialog displaying!");
        DialogBox.SetActive(true);
    }
}
