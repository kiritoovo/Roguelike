using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class FinishRoom : MonoBehaviour
{
    private Button button;
   public ObjectEventSO objectEventSO;

   public void OnMouseDown() {
            objectEventSO.RaiseEvent(null,this);
   }
     
     public void fuck()
     {
        Debug.Log("fuck u");
     }
}

