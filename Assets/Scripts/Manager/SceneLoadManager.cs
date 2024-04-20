using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    AssetReference currentScene;
    public AssetReference map;
    public ObjectEventSO updateRoomStateSo;
    private Vector2Int currentRoomVector;

    public async void OnLoadRoomEvent(object roomType)
    {
        if(roomType is Room)
        {
            Room room = roomType as Room;
            var currentData=room.roomDataOS;
            currentRoomVector=new(room.colunm,room.line);
            currentScene=currentData.sceneToLoad;
        }
        

        await UnloadSceneTask();
        await LoadSceneTask();
        updateRoomStateSo.RaiseEvent(currentRoomVector,this);
    }

    private async Awaitable LoadSceneTask()
    {
        var s=currentScene.LoadSceneAsync(LoadSceneMode.Additive);
        await s.Task;
        if(s.Status==AsyncOperationStatus.Succeeded)
        {
            SceneManager.SetActiveScene(s.Result.Scene);
        }
    } 

    private async Awaitable UnloadSceneTask()
    {
        await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
    }

    public async void LoadMap()
    {
        await UnloadSceneTask();
        currentScene=map;
        await LoadSceneTask();
    }
}
