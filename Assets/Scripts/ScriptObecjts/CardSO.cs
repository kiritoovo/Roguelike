using UnityEngine;

[CreateAssetMenu(fileName = "CardSO", menuName = "CardSO", order = 0)]
public class CardSO : ScriptableObject {
    public string cardName;
    public Sprite cardSprite;
    public int cost;
    public CardType cardType;
    [TextArea]
    public string cardText;
}