using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using UnityEngine.ResourceManagement.AsyncOperations;

public class CardManager : MonoBehaviour
{
    public PoolTool pool;
    public List<CardSO> cardSOList;

    public CardLibrarySO startLibrary;
    public CardLibrarySO holdLibrary;
    
    private void Awake() {
        InitializeCardDataList();
        foreach(CardLibraryEntry entry in startLibrary.cardList)
        {
            holdLibrary.cardList.Add(entry);
        }
    }

   private void InitializeCardDataList(){
    Addressables.LoadAssetsAsync<CardSO>("CardData",null).Completed+=OnCardDataLoaded;
   }

    private void OnCardDataLoaded(AsyncOperationHandle<IList<CardSO>> handle)
    {
        if(handle.Status==AsyncOperationStatus.Succeeded){
        cardSOList=new List<CardSO>(handle.Result);
        }else{
            Debug.Log("no");
        }
    }


    public GameObject GetCard()
    {
        return pool.GetPoolObj();
    }

    public void ReleaseCard(GameObject card)
    {
        pool.ReleasePoolObj(card);
    }
}
