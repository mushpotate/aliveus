using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;
//using static UnityEditor.Progress;

public class itemCode : MonoBehaviour
{

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        //this.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isSelected() && FindAnyObjectByType<inventory>().isOpen())
        {
            //Debug.Log("selected");
            FindObjectOfType<inventoryText>().setActive(this.name);
        }
    }

    int pos = 0;

    

    public bool isSelected()
    {
        
        if (pos == FindObjectOfType<inventory>().getSelected())
        {
            return true;
        }
        return false;
    }

    public void changePos(int newPos)
    {
        pos = newPos;
        this.transform.position = FindFirstObjectByType<playerMovment>().getPos() + (new Vector2(pos - 5 , 5.6f));
        
    }

    public int getPos() { return pos; }
}
