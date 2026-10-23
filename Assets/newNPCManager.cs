using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newNPCManager : MonoBehaviour
{
    public static newNPCManager instance;
    private gameManager gameManager;
    private void Awake()
    {
        gameManager = FindAnyObjectByType<gameManager>();

        if (instance != null && instance != this)
        {

            Destroy(this.gameObject);
        }
        else
        {
            instance = this;

        }

        DontDestroyOnLoad(this.gameObject);

    }

    [SerializeField] private GameObject[] NPCs;

    private void Start()
    {
        setUpNPCs();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O) && Input.GetKeyDown(KeyCode.P))
        {
            {
                gameManager.nextDay();
                setUpNPCs();
            }
        }
    }

    private void setUpNPCs()
    {
        Debug.Log("setting up");
        int day = gameManager.day;
        foreach (GameObject npc in NPCs)
        {
            npc.GetComponent<npcTrigger>().SetNPC(day);
        }
    }
    
}
