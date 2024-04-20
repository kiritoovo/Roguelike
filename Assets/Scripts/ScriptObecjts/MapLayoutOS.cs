using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "MapLayoutOS", menuName = "MapLayoutOS", order = 0)]
public class MapLayoutOS : ScriptableObject {
    public List<MapRoomData> mapRoomDataList=new();
    public List<LinePosition> linePositionList=new();
}

[Serializable]
public class MapRoomData{
    public float PosX,PosY;
    public int colunm,line;
    public RoomDataOS roomDataOS;
    public RoomState roomState;
    public List<Vector2Int> linkTo;
}

[Serializable]
public class LinePosition{
    public SerializeVecotr3 startPos,endPos;
}