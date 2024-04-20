using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public CardManager cardManager;

    private List<CardSO> drawDeck=new();
    private List<CardSO> discardDeck=new();
    private List<Card> handCardList=new();

    private void Start() {
        InitialzeDeck();
    }

    public void InitialzeDeck()
    {
        drawDeck.Clear();
        foreach(var car in cardManager.holdLibrary.cardList)
        {
            for(int i=0;i<car.count;i++)
            {
                drawDeck.Add(car.cardSO);
            }
        }
    }

    [ContextMenu("test")]
    public void TestDrawCard()
    {
        DrawCard(1);
    }

    public void DrawCard(int count)
    {
        for(int i=0;i<count;i++)

    {
        if(drawDeck.Count==0)
        {

        }
        CardSO cardSO=drawDeck[0];
        drawDeck.RemoveAt(0);
        var card=cardManager.GetCard().GetComponent<Card>();
        card.Init(cardSO);  
        handCardList.Add(card);  }
    }
}
