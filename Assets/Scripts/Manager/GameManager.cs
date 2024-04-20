using UnityEngine;
using UnityEngine.UIElements;

public class GameManager1 : MonoBehaviour
{
    public MapLayoutOS mapLayoutOS;

    public void UpdateRoomState(object value)
    {
        var vectorInt2=(Vector2Int)value;
        var currentRoom=mapLayoutOS.mapRoomDataList.Find(r=>r.colunm==vectorInt2.x&&r.line==vectorInt2.y);
        currentRoom.roomState=RoomState.visited;
        var sameColumnRoom=mapLayoutOS.mapRoomDataList.FindAll(r=>r.colunm==currentRoom.colunm);
        foreach(var room in sameColumnRoom)
        {
            if(room.line!=currentRoom.line)
            {
                room.roomState=RoomState.visited;
            }
        }
        foreach(var room in currentRoom.linkTo)
        {
            var nextRoom=mapLayoutOS.mapRoomDataList.Find(r=>r.colunm==room.x&&r.line==room.y);
            nextRoom.roomState=RoomState.gaind;
        }
    }   
}
