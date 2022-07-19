using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandle : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI lumi;
    [SerializeField]
    private LineRenderer lineUpgrade;
    [SerializeField]
    private LineRenderer lineDestroy;
    private LineRenderer lineUsed;
    [SerializeField]
    private GameObject updateButton;
    [SerializeField]
    private GameObject destroyButton;
    private bool mouseClick;
    private bool mouseDrag;
    public Action endAction;
    public Deck deck;

    private void Start()
    {
        endAction = UpgradeEnd;
        Global.Instance.ui = this;
        LumiUpdate();
    }

    private void Update()
    {
        CheckForUnclickAction();
    }

    private void CheckForUnclickAction()
    {
        if (mouseDrag)
        {
            Vector3 pos = Input.mousePosition;
            lineUsed.SetPosition(1, Camera.main.ScreenToWorldPoint(pos));
            if (Input.GetMouseButtonUp(0))
            {
                endAction();
            }
        }
        if (mouseClick)
        {
            Vector3 pos = Input.mousePosition;
            lineUsed.SetPosition(1, Camera.main.ScreenToWorldPoint(pos));
            if (Input.GetMouseButtonDown(0))
            {
                if (!Global.Instance.mouseIsOverCard)
                {
                    endAction();
                }
            }
        }
    }

    public void UpgradeEnd()
    {
        Global.Instance.upgradeTime = false;
        mouseClick = false;
        mouseDrag = false;
        deck.SetColorsToNormal();
        lineUpgrade.gameObject.SetActive(false);
        LumiUpdate();
    }

    public void DestroyEnd()
    {
        Global.Instance.destroyTime = false;
        mouseClick = false;
        mouseDrag = false;
        lineDestroy.gameObject.SetActive(false);
        LumiUpdate();
    }

    public void LumiUpdate()
    {
        lumi.text = Global.Instance.lumi.ToString();
    }


    public void UpgradeClick()
    {
        Global.Instance.upgradeTime = true;
        mouseClick = true;
    }

    public void UpgradeDrag()
    {
        Global.Instance.upgradeTime = true;
        mouseDrag = true;
    }

    public void DestroyClick()
    {
        Global.Instance.destroyTime = true;
        mouseClick = true;
    }

    public void DestroyDrag()
    {
        Global.Instance.destroyTime = true;
        mouseDrag = true;
    }

    public void UpgradeDown()
    {
        endAction();
        lineUsed = lineUpgrade;
        lineUpgrade.SetPosition(0, updateButton.transform.position);
        Global.Instance.upgradeTime = true;
        Vector3 pos = Input.mousePosition;
        lineUpgrade.SetPosition(1, Camera.main.ScreenToWorldPoint(pos));
        lineUpgrade.gameObject.SetActive(true);
        deck.CheckCardsForUpgrades();
        endAction = UpgradeEnd;
    }

    public void DestroyDown()
    {
        endAction();
        lineUsed = lineDestroy;
        lineDestroy.SetPosition(0, destroyButton.transform.position);
        Global.Instance.destroyTime = true;
        Vector3 pos = Input.mousePosition;
        lineDestroy.SetPosition(1, Camera.main.ScreenToWorldPoint(pos));
        lineDestroy.gameObject.SetActive(true);
        endAction = DestroyEnd;
    }
}
