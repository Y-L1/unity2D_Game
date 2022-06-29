using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    private GameObject myBag;
    [SerializeField]
    private GameObject BoxUI;
    bool isOpen;


    void Start()
    {
        myBag = GameObject.FindGameObjectWithTag("Bag");
        myBag.SetActive(false);
        BoxUI.SetActive(false);
        
    }


    void Update()
    {
        UseKeyBoardOpenAndCloseMyBag();

        //长按左alt显示鼠标
        ShowMouse();
        
    }

    void ShowMouse()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
        }
        else if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.visible = false;
        }
    }

    void UseKeyBoardOpenAndCloseMyBag()
    {
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            isOpen = !isOpen;
            myBag.SetActive(isOpen);

            MouseController();

        }

    }

    void MouseController()
    {
        if (isOpen)
        {
            Cursor.visible = true;
        }
        else
        {
            Cursor.visible = false;
        }

    }
    public void UseBtnOpenAndCloseMyBag()
    {
        isOpen = !isOpen;
        myBag.SetActive(isOpen);
        MouseController();
    }
}
