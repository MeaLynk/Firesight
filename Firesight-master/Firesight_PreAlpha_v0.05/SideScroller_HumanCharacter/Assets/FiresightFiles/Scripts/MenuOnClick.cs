//Code help from https://answers.unity.com/questions/1502836/add-button-script-to-3d-gameobject.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; // this namespace makes the magic, tho

public class MenuOnClick : MonoBehaviour
{
    public MenuControllerScript menuController;
    public MenuControllerScript.MenuStates menuStateNeeded;
    [SerializeField] UnityEvent anEvent; // puts an easy to setup event in the inspector and anEvent references it so you can .Invoke() it

    private bool isInArea = false;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && menuController.GetCurrentMenu() == menuStateNeeded && isInArea == true)
        {
            anEvent.Invoke();
        }
    }

    //Makes sure mouse is in correct area
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Fireball")
        {
            isInArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Fireball")
        {
            isInArea = false;
        }
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(10, 50, 80, 30), isInArea.ToString());
    }
}

