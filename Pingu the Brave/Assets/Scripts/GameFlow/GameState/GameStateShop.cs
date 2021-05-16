using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStateShop : GameState
{
    public GameObject shopUI;
    public TextMeshProUGUI totalFish;
    public TextMeshProUGUI currentHatName;
    public HatLogic hatLogic;
    private bool isInit = false;
    private int hatCount;
    private int unlockedHatCount;
    //shop item
    public GameObject hatPrefab;
    public Transform hatContainer;
    private Hat[] hats;
    //completion circle
    public Image completionCircle;
    public TextMeshProUGUI completionText;

    public GameObject notEnoughTable;

    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCamera.shop);
        hats = Resources.LoadAll<Hat>("Hat");
        shopUI.SetActive(true);
        if (!isInit)
        {
            totalFish.text = SaveManager.Instance.save.Fish.ToString("000");
            currentHatName.text = hats[SaveManager.Instance.save.CurrentHatIndex].ItemName;
            PopulateShop();
            isInit = true;
        }
        ResetCompletionCirlce();
    }

    public override void Destruct()
    {
        shopUI.SetActive(false);
    }

    public void OnHomeClick()
    {
        AudioManager.Instance.PlaySFX(GameManager.Instance.clickSound);
        brain.ChangeState(GetComponent<GameStateInit>());
    }

    private void PopulateShop()
    {
        for (int i = 0; i < hats.Length; i++)
        {
            int index = i;
            GameObject go = Instantiate(hatPrefab, hatContainer) as GameObject;
            //button
            go.GetComponent<Button>().onClick.AddListener(() => OnHatClick(index));

            //thumbnail
            go.transform.GetChild(0).GetComponent<Image>().sprite = hats[index].Thumbnail;

            //itemname
            go.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = hats[index].ItemName;

            //price
            if (SaveManager.Instance.save.UnlockedHatFlag[i] == 0)
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = hats[index].ItemPrice.ToString();
            else
            {
                go.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
                unlockedHatCount++;
            }


        }
    }

    private void OnHatClick(int i)
    {
        if (SaveManager.Instance.save.UnlockedHatFlag[i] == 1)
        {
            SaveManager.Instance.save.CurrentHatIndex = i;
            hatLogic.SelectHat(i);
            currentHatName.text = hats[i].ItemName;
            SaveManager.Instance.Save();
        }
        //if we don't have it, can we buy it?
        else if (hats[i].ItemPrice <= SaveManager.Instance.save.Fish)
        {
            SaveManager.Instance.save.Fish -= hats[i].ItemPrice;
            SaveManager.Instance.save.UnlockedHatFlag[i] = 1;
            SaveManager.Instance.save.CurrentHatIndex = i;
            currentHatName.text = hats[i].ItemName;
            hatLogic.SelectHat(i);
            totalFish.text = SaveManager.Instance.save.Fish.ToString("000");
            SaveManager.Instance.Save();
            hatContainer.GetChild(i).transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "";
            unlockedHatCount++;
            ResetCompletionCirlce();
        }
        //
        else
        {
            notEnoughTable.SetActive(true);
            StartCoroutine(LateCall());
            //Debug.Log("Not enough fish");
        }
    }

    public int sec = 2;
    IEnumerator LateCall()
    {
        yield return new WaitForSeconds(sec);
        notEnoughTable.SetActive(false);
    }

    private void ResetCompletionCirlce()
    {
        int hatCount = hats.Length-1;
        int currentlyUnlockedCount = unlockedHatCount - 1;

        completionCircle.fillAmount = (float)currentlyUnlockedCount / (float)hatCount;
        completionText.text = currentlyUnlockedCount + " / " + hatCount;
    }
}
