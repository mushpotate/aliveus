using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class delete : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public GameObject obj2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        obj.SetActive(false);
        obj2.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
