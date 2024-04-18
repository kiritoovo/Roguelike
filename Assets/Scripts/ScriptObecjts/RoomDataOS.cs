using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "RoomDataOS", menuName = "RoomDataOS", order = 0)]
public class RoomDataOS : ScriptableObject {
    public Sprite roomIcon;
    public RoomType roomType;
    public AssetReference sceneToLoad;
}