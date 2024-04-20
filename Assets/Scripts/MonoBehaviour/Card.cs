using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Card : MonoBehaviour
{
    public SpriteRenderer cardSprite;
    public TextMeshPro costText,description,typeText;
    public CardSO cardSO;

    private void Start() {
        Init(cardSO);
    }

    public void Init(CardSO cardSO)
    {
        this.cardSO = cardSO;
        cardSprite.sprite=cardSO.cardSprite;
        costText.text=cardSO.cost.ToString();
        description.text=cardSO.cardText;
        typeText.text=cardSO.cardType.ToString();
    }
}
