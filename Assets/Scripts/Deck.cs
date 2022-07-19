using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public GameObject cardDisplayPrefab;
    public List<Card> deckCards;
    private List<CardDisplay> deck;
    public Canvas canv;
    public float deckXPosition;
    public float deckYPosition;
    public float distanceBetweenCardsX;
    public float distanceBetweenCardsY;
    public int numberOfCardsInRow;

    void Start()
    {
        deck = new List<CardDisplay>();
        ShowCards();
    }

    void ShowCards()
    {
        float x = deckXPosition;
        float y = deckYPosition;
        int cardNumber = 0;
        int cardID = 0;
        foreach (Card card in deckCards)
        {
            GameObject newCard = Instantiate(cardDisplayPrefab, canv.transform);
            newCard.GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
            if (!newCard.GetComponent<CardDisplay>()) newCard.AddComponent<CardDisplay>();
            CardDisplay cardDisp = newCard.GetComponent<CardDisplay>();
            cardDisp.card = card;
            cardDisp.x = x;
            cardDisp.y = y;
            cardDisp.deck = this;
            cardDisp.cardID = cardID;
            cardID++;
            deck.Add(cardDisp);
            cardNumber++;
            if(cardNumber >= numberOfCardsInRow)
            {
                x = deckXPosition;
                y -= distanceBetweenCardsY;
                cardNumber = 0;
            }
            else
            {
                x += distanceBetweenCardsX;
            }
        }
    }

    public void CheckCardsForUpgrades()
    {
        foreach (CardDisplay card in deck)
        {
            card.CheckIfUpgradePossible();
        }
    }

    public void SetColorsToNormal()
    {
        foreach (CardDisplay card in deck)
        {
            card.SetColorsToNormal();
        }
    }

    public void ChangeCard(int cardID, Card newCard, CardDisplay oldCard)
    {
        GameObject cardAfterChange = Instantiate(cardDisplayPrefab, canv.transform);
        cardAfterChange.GetComponent<RectTransform>().anchoredPosition = new Vector2(oldCard.x, oldCard.y);

        if (!cardAfterChange.GetComponent<CardDisplay>()) cardAfterChange.AddComponent<CardDisplay>();
        CardDisplay cardDisp = cardAfterChange.GetComponent<CardDisplay>();
        cardDisp.card = newCard;
        cardDisp.x = oldCard.x;
        cardDisp.y = oldCard.y;
        cardDisp.cardID = oldCard.cardID;
        cardDisp.deck = this;
        cardID++;
        deck[cardID] = cardAfterChange.GetComponent<CardDisplay>();
    }

    public void DestroyCard(int cardID)
    {
        deck.RemoveAt(cardID);
        foreach (CardDisplay card in deck)
            if (card.cardID >= cardID) card.cardID--;
    }
}
