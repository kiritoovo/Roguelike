using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MapConfigOS", menuName = "MapConfigOS", order = 0)]
public class MapConfigOS : ScriptableObject {
    public RoomBluePrint[] rooms;
}

[Serializable]
public class RoomBluePrint{
    public int min,max;
    public RoomType roomType;
}