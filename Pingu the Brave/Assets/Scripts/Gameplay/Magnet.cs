using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DentedPixel;

//SUBSCRIBE TO VIN CODES FOR MORE FREE SCRIPTS IN FUTURE VIDEOS :)

public class Magnet : MonoBehaviour
{
    //public float BarHP;
   // private float barHP;
    public int time;
    //private float aveHP { get { return barHP / BarHP; } }
    public GameObject magnetTimer;
    public Image fillBar;
    public GameObject coinDetectorObj;
    public Animator anim;
    public float BarDMG =5f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (go.name == "MagnetTimer")
            {
                magnetTimer = go;
                fillBar = magnetTimer.GetComponent<Image>();
                //fillBar.fillAmount = 1;
            }
            if (go.name == "Coin Detector")
                coinDetectorObj = go;
        }
        anim?.SetTrigger("Idle");
    }
   /* void ReduceBar(float dmg)
    {
        barHP -= dmg;
        fillBar.fillAmount = aveHP;
    }*/
    private void OnTriggerEnter(Collider other)
    {
        if (coinDetectorObj.activeSelf == false)
        {
            Debug.Log("AM INTRAT");
            anim = GetComponent<Animator>();
            if (other.gameObject.tag == "Player")
            {
                anim?.SetTrigger("Pickup");
                //StartCoroutine(ActivateCoin());
                ActivateCoin();

            }
            //magnetTimer.SetActive(false);
            //coinDetectorObj.SetActive(false);

            DestroyGameObject();
        }
    }
    IEnumerator waitabit()
    {
        yield return new WaitForSecondsRealtime(5f);
    }
    IEnumerator DestroyGameObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(transform.GetChild(0).gameObject);
    }
    public void ActivateCoin()
    {
       // barHP = BarHP;
        
        magnetTimer.SetActive(true);
        coinDetectorObj.SetActive(true);

        //for (int i = 0; i < 600; i++)
        /* while (magnetTimer.activeSelf==true)
         {
             //yield return new WaitForSeconds(0.005f);
             ReduceBar(BarDMG);
             yield return new WaitForSecondsRealtime(0.01f);
             ReduceBar(BarDMG);
             //StartCoroutine(waitabit());
             Debug.Log("Test");
             //ReduceBar(BarDMG);
             if (aveHP <= 0) { Debug.Log("M am oprit"); magnetTimer.SetActive(false); break; }
         }*/
        //LeanTween.scaleX(magnetTimer, 0, 5f).setOnComplete(setMagnetOff);
        //LeanTween.moveX(magnetTimer,0f,5f).setOnComplete(setMagnetOff).setEase(LeanTweenType.easeInQuad).setDelay(1f);
        LeanTween.value(magnetTimer, 1f, 0.0f, 5f).setOnComplete(setMagnetOff).setOnUpdate( (value) => { fillBar.fillAmount = value; }) ;
        //StartCoroutine(waitabit());
        Debug.Log("au trecut secundele, dezactivez");
        //coinDetectorObj.SetActive(false);
        //magnetTimer.SetActive(false); 
    }
    public void setMagnetOff()
    {
        magnetTimer.SetActive(false);
        coinDetectorObj.SetActive(false);
    }
}
