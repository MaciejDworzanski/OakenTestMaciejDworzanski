using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New card", menuName = "New card/Normal card")]
public class Card : ScriptableObject
{
    public string cardName;
    public Card upgradeCard;
    public bool isUpgradeable;
    public int upgradeCost;
    public int destroyProfit;
    public Color color;
    //[HideInInspector]
    //public int cardIndex;
}
