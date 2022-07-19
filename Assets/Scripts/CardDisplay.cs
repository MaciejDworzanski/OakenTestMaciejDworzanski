using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CardDisplay : MonoBehaviour
{
    public Card card;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI upgradeCostText;
    public TextMeshProUGUI destroyProfitText;
    public Image spriteColor;
    public GameObject upgradeNotPossible;
    public GameObject upgradeCostGraphic;
    public GameObject destroyProfitGraphic;
    [HideInInspector]
    public Deck deck;
    [HideInInspector]
    public float x;
    [HideInInspector]
    public float y;
    //[HideInInspector]
    public int cardID;
    private bool upgradePossible;

    void Start()
    {
        SetDisplay();
    }

    void SetDisplay()
    {
        nameText.text = card.cardName;
        spriteColor.color = card.color;
        name = card.cardName;
        upgradeCostText.text = "-" + card.upgradeCost.ToString();
        destroyProfitText.text = "+" + card.destroyProfit.ToString();
    }

    public void CheckIfUpgradePossible()
    {
        if (Global.Instance.lumi < card.upgradeCost || !card.isUpgradeable)
        {
            upgradeNotPossible.SetActive(true);
            upgradePossible = false;
        }
        else
        {
            upgradeNotPossible.SetActive(false);
            upgradePossible = true;
        }
    }

    public void SetColorsToNormal()
    {
        upgradeNotPossible.SetActive(false);
        upgradePossible = false;
    }

    public void Upgrade()
    {
        if (Global.Instance.upgradeTime && upgradePossible)
        {
            deck.ChangeCard(cardID - 1, card.upgradeCard, this);
            Global.Instance.upgradeTime = false;
            Global.Instance.lumi -= card.upgradeCost;
            Global.Instance.ResetUI();
            Destroy(gameObject);
        }
        else
        {
            Global.Instance.upgradeTime = false;
            //Global.Instance.ResetUI();
        }
    }

    public void Destroy()
    {
        Debug.Log("TryDestroy");
        if (Global.Instance.destroyTime)
        {
            Debug.Log("Destroy");
            Global.Instance.destroyTime = false;
            Global.Instance.lumi += card.destroyProfit;
            Global.Instance.ResetUI();
            deck.DestroyCard(cardID);
            Destroy(gameObject);
        }
        else
        {
            Global.Instance.destroyTime = false;
            Global.Instance.ResetUI();
        }
    }

    public void MouseEnter()
    {
        Global.Instance.mouseIsOverCard = true;
        if (Global.Instance.upgradeTime && upgradePossible) upgradeCostGraphic.SetActive(true);
        else if (Global.Instance.destroyTime) destroyProfitGraphic.SetActive(true);
    }

    public void MouseExit()
    {
        Global.Instance.mouseIsOverCard = false;
        if (Global.Instance.upgradeTime && upgradePossible) upgradeCostGraphic.SetActive(false);
        else if (Global.Instance.destroyTime) destroyProfitGraphic.SetActive(false);
    }
}
