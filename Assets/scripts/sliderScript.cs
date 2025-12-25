using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using UnityEngine.UI;

public class sliderScript : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private float sLevel;

    // Start is called before the first frame update
    void Start()
    {
        sLevel = FindObjectOfType<gameManager>().getAudioLevel();
        _slider.SetValueWithoutNotify(sLevel);
        FindObjectOfType<gameManager>().setAudioLevel(sLevel);
        FindObjectOfType<musicManager>().changeMaxVolume(sLevel);

        _slider.onValueChanged.AddListener((v) =>
        {
            FindObjectOfType<gameManager>().setAudioLevel(v);
            FindObjectOfType<musicManager>().changeMaxVolume(v);
        });
    }

    
}
