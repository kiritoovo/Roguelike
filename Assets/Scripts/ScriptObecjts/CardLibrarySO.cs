using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardLibrarySO", menuName = "CardLibrarySO", order = 0)]
public class CardLibrarySO : ScriptableObject {
    public List<CardLibraryEntry> cardList;
}

[System.Serializable]
public class CardLibraryEntry{
    public CardSO cardSO;
    public int count;
}