using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{



    public static gameManager instance;
    private void Awake()
    {
        //Debug.Log(audioLevel);

        if (instance != null && instance != this)
        {

            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            StartCoroutine(playIntro());
            //audioLevel = .25f;
        }

        DontDestroyOnLoad(this.gameObject);
        
    }

    public int day = 1;

    public GameObject intro;
    public GameObject warnings;
    public GameObject logo;
    public GameObject back;
    public GameObject top;
    IEnumerator playIntro()
    {
        intro.SetActive(true);
        yield return new WaitForSeconds(2);
        logo.SetActive(true);
        fadeOut();
        yield return new WaitForSeconds(3);
        fadeIn();
        yield return new WaitForSeconds(2);
        logo.SetActive(false);
        warnings.SetActive(true);
        fadeOut();
        yield return new WaitForSeconds(3);
        fadeIn();
        yield return new WaitForSeconds(1);
        warnings.SetActive(false);
        back.SetActive(false);
        yield return new WaitForSeconds(1);
        fadeOut();
        yield return new WaitForSeconds(1);
        top.SetActive(false);
        intro.SetActive(false);


    }

    public void fadeIn()
    {
        top.GetComponent<RawImage>().CrossFadeAlpha(1f, 1f, false);
    }

    public void fadeOut()
    {
        top.GetComponent<RawImage>().CrossFadeAlpha(0f, 1f, false);
    }

    private bool secretDoor = false;

    //inventory items

    int kills = 0;

    bool skull = false;

    bool knife = false;
    int knifeLevel = 0;

    int dumbellUses = 0;

    bool rock = false;

    bool shovel = false;

    bool candles = false;
    int numCandles = 0;

    bool robe = false;

    bool hidoutKey = false;

    bool cageKey = false;

    bool present = false;
    bool presentOpen = false;
    string itemInPresent;

    // getter methods
    public bool getKnife() { return knife; }
    public int getKnifeLevel() { return knifeLevel; }
    public bool getPresent() { return present; }
    public bool getPresentOpen() { return presentOpen; }
    public string getItemInPresent() { return itemInPresent; }
    public bool getSkull() { return skull; }
    public int getKills() { return kills; }
    public bool getRock() { return rock; }
    public bool getShovel() { return shovel; }
    public bool getCandles() { return candles; }
    public bool getRobe() { return robe; }
    public int getDumbellUses() { return dumbellUses; }
    public int getNumCandles() { return numCandles; }
    public bool getHidoutKey() { return hidoutKey; }
    public bool getCageKey() { return cageKey; }

    private float audioLevel = .25f;

    // setter methods
    public void resetGame()
    {
        //reset all variables
        secretDoor = false;
        kills = 0;

        skull = false;

        knife = false;
        knifeLevel = 0;

        dumbellUses = 0;

        rock = false;

        shovel = false;

        candles = false;
        numCandles = 0;

        robe = false;

        hidoutKey = false;

        cageKey = false;

        present = false;
        presentOpen = false;

    }
    public void SetKnife(bool set) { knife = set; }
    public void addKnifeLevel()
    {
        knifeLevel++;
        FindObjectOfType<inventory>().knifeLevel(knifeLevel);

        //change image
    }
    public void addKills()
    {
        kills++;
        //Debug.Log("wow you have killed " + kills + " people");
        if (kills == 1)
        {
            addKnifeLevel();
        }
        else if (kills == 23)
        {
            addKnifeLevel();
        }
    }

    private bool birdsEnding = false;

    public void setBirdsEnding()
    {
        birdsEnding = true;
    }

    public bool getBirdsEnding()
    {
        return birdsEnding;
    }
    public void SetRock(bool set) { rock = set; }
    public void setShovel(bool set) { shovel = set; }
    public void setCandles(bool set) { candles = set; }
    public void setCandles(int set) { numCandles = set; }
    public void setRobe(bool set) { robe = set; }
    public void addDumbell() { dumbellUses++; }
    public void setHidoutKey(bool set) { hidoutKey = set; }
    public void setCageKey(bool set) { cageKey = set; }

    public float getAudioLevel()
    {
        return audioLevel;
    }

    public void setAudioLevel(float f)
    {
        audioLevel = f;
    }

    public void openSecretDoor()
    {
        secretDoor = true;
    }
    public bool isSecretDoor()
    {
        return secretDoor;
    }
}
