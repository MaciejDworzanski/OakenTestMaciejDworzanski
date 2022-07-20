using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance { get; private set; }

    public int lumi;
    public bool upgradeTime;
    public bool destroyTime;
    public bool mouseIsOverCard;
    public UIHandle ui;

    private void Awake()
    {
        if (Instance == null) 
        {
            Instance = this;
            lumi = 15;
            upgradeTime = false;
            mouseIsOverCard = false;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetUI()
    {
        if(ui != null) ui.endAction();
    }

}
