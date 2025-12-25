using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class cutceen : MonoBehaviour
{
    // Start is called before the first frame update
    public float time;
    public string level;
    void Start()
    {
        StartCoroutine(delay());
    }

    // Update is called once per frame
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(time);
        FindObjectOfType<sceneManager>().loadLevel(level);
    }
}
