using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class inventory : MonoBehaviour
{
    List<GameObject> inventoryList = new List<GameObject>();

    public Animator animator;
    public GameObject knifeObj;
    public Image  knifeImage;

    public Sprite knife1;
    public Sprite knife2;
    public Sprite knife3;
    public Sprite knife4;

    public GameObject rockObj;
    
    public GameObject shovelObj;
    
    public GameObject candleObj;
    
    public GameObject robeObj;
    
    public GameObject hidoutKeyObj;
    
    public GameObject cageKeyObj;
    
    public GameObject skullObj;
    
    public GameObject presentObj;
    
    private bool open = false;
    
    private int items = 0;
    private int selected = 0;

    public GameObject curser;

    public AudioSource pickupSound;

    public Animator knife;
    public Animator shovel;

    public void addRobe() { addObject( robeObj); }

    // Start is called before the first frame update
    void Start()
    {
        inventoryList.Add(knifeObj);
        inventoryList.Add(rockObj);
        inventoryList.Add(shovelObj);
        inventoryList.Add(candleObj);
        inventoryList.Add(robeObj);
        inventoryList.Add(hidoutKeyObj);
        inventoryList.Add(cageKeyObj);
        inventoryList.Add(skullObj);
        inventoryList.Add(presentObj);


        bool presentOpen = FindObjectOfType<gameManager>().getPresentOpen();
        string itemInPresent = FindObjectOfType<gameManager>().getItemInPresent();

        bool skull = FindObjectOfType<gameManager>().getSkull();

        bool knife = FindObjectOfType<gameManager>().getKnife();
        int knifeLevel = FindObjectOfType<gameManager>().getKnifeLevel();
        
        int kills = FindObjectOfType<gameManager>().getKills();

        int dumbellUses = FindObjectOfType<gameManager>().getDumbellUses();

        bool rock = FindObjectOfType<gameManager>().getRock();

        bool shovel = FindObjectOfType<gameManager>().getShovel();

        bool candles = FindObjectOfType<gameManager>().getCandles();
        int numCandles = FindObjectOfType<gameManager>().getNumCandles();

        bool robe = FindObjectOfType<gameManager>().getRobe();

        bool hidoutKey = FindObjectOfType<gameManager>().getHidoutKey();

        bool cageKey = FindObjectOfType<gameManager>().getCageKey();

        //addObject(knifeObj);
        //addObject(robeObj); 
        //addObject(shovelObj);
        //addObject(candleObj);
        //addObject(skullObj);
        //addObject(rockObj);

        

    }

    public void knifeLevel(int level)
    {
        if (level == 1)
        {
            knifeImage.sprite = knife1;
        }
        else if (level == 2)
        {
            knifeImage.sprite = knife2;
        }
        else if (level == 3)
        {
            knifeImage.sprite = knife3;
        }
        else if (level == 4)
        {
            knifeImage.sprite = knife4;
        }
    }

    public void knifeRange(bool b)
    {
        if(FindObjectOfType<killable>().doesKnife())
        {
            knife.SetBool("open", b);
        }
        
    }

    public void digRange(bool b)
    {
        if (FindObjectOfType<digable>().canDigs())
        {
            shovel.SetBool("open", b);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!open)
            {
                selected = 1;
                curser.transform.position = FindFirstObjectByType<playerMovment>().getPos() + (new Vector2(selected - 5, 6f));
                FindObjectOfType<playerMovment>().SetWalk(false);
                animator.SetBool("open", true);
                open = true;
                
            }
            else
            {
                FindAnyObjectByType<inventoryText>().setUnactive();
                FindObjectOfType<playerMovment>().SetWalk(true);
                animator.SetBool("open", false);
                open = false;
            }
        }

        if (open)
        {
            if (Input.GetKeyDown(KeyCode.D) && items > 1)
            {
                FindAnyObjectByType<inventoryText>().setUnactive();
                if (selected == items)
                {
                    selected = 1;
                }
                else
                {
                    selected++;
                }

                curser.transform.position = FindFirstObjectByType<playerMovment>().getPos() + (new Vector2(selected - 5, 4));
            }
            else if (Input.GetKeyDown(KeyCode.A) && items > 1)
            {
                FindAnyObjectByType<inventoryText>().setUnactive();
                if (selected == 1)
                {
                    selected = items;
                }
                else
                {
                    selected--;
                }

                curser.transform.position = FindFirstObjectByType<playerMovment>().getPos() + (new Vector2(selected - 5, 4));
            }
        }

        
    }

    
    public int getSelected()
    {
        //Debug.Log(selected);
        return selected;
    }

    public void addObject(GameObject obj)
    {
        pickupSound.Play(0);

        //Debug.Log(obj.name);
        items++;
        obj.SetActive(true);
        obj.GetComponent<itemCode>().changePos(items);

        if(obj.Equals(shovelObj))
        {
            foreach (digable k in FindObjectsOfType<digable>())
            {
                k.gotShovel();
            }
        }
        else if (obj.Equals(knifeObj))
        {
            FindObjectOfType<suisideEnding>().gotKnife();

            foreach(killable k in FindObjectsOfType<killable>())
            {
                k.gotKnife();
            }
        }
        else if (obj.Equals(robeObj))
        {
            FindObjectOfType<playerMovment>().gotRobe();
        }
        else if(obj.Equals(hidoutKeyObj))
        {
            FindObjectOfType<cultDoor>().unlockable();
        }


    }

    public void removeObject(GameObject obj)
    {

        int del = obj.GetComponent<itemCode>().getPos();
        obj.SetActive(false);
        obj.GetComponent<itemCode>().changePos(0);
        foreach (GameObject objec in inventoryList) 
        {
            if(objec.GetComponent<itemCode>().getPos() > del)
            {
                objec.GetComponent<itemCode>().changePos(objec.GetComponent<itemCode>().getPos() - 1);
            }
        }

        items--;
    }

    public bool isOpen()
    {
        return open;
    }

    
}
