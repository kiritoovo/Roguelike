using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    AssetReference currentScene;
    public AssetReference map;

    public async void OnLoadRoomEvent(object roomType)
    {
        if(roomType is RoomDataOS)
        {
            var currentData=(RoomDataOS)roomType;
            currentScene=currentData.sceneToLoad;
        }
        await UnloadSceneTask();
        await LoadSceneTask();
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
