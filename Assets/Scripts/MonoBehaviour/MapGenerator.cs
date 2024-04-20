using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class MapGenerator : MonoBehaviour
{
   public Room roomPrefab;
   public MapConfigOS mapConfigOS;
   public LineRenderer linePrefab;
    private List<Room> rooms;
    private List<LineRenderer> lines=new();
    public MapLayoutOS mapLayoutOS;

    public List<RoomDataOS> roomDatas=new();
    public Dictionary<RoomType,RoomDataOS> roomDataDict=new();

     float ScreenHeight;
     float ScreenWidth;
     float ColumnWidth;
     private Vector3 generatePoint;
     public float border;

    private void Awake() {
        ScreenHeight=Camera.main.orthographicSize*2;
        ScreenWidth=ScreenHeight*Camera.main.aspect;
        ColumnWidth=ScreenWidth/(mapConfigOS.rooms.Count()+1);
        rooms=new List<Room>();
        foreach(var roomdata in roomDatas) {
            roomDataDict.Add(roomdata.roomType,roomdata);
        }
    }

     private void OnEnable() {
        if(mapLayoutOS.mapRoomDataList.Count>0)
        {
             LoadMap();
        }
        else{
            CreateMap();
        }
    }

    public void CreateMap()
    {
        List<Room> preRooms=new List<Room>();  
        for(int colunm=0; colunm < mapConfigOS.rooms.Count(); colunm++)
        {
            var bluePrint=mapConfigOS.rooms[colunm];
            var amount=UnityEngine.Random.Range(bluePrint.min,bluePrint.max);
            var ColunmHeight=ScreenHeight/(amount+1);

            var startHeight=ScreenHeight/2-(ColunmHeight);
            generatePoint =new Vector3(-ScreenWidth/2+border+ColumnWidth*colunm,startHeight,0);
            var newPoint=generatePoint;
            List<Room> currentRooms=new List<Room>();
            for(int i=0;i<amount;i++)
            {
                if(colunm==mapConfigOS.rooms.Count()-1)
                {
                    newPoint.x=ScreenWidth/2-(border*2);
                }
                else if(colunm!=0)
                {
                    newPoint.x=generatePoint.x+UnityEngine.Random.Range(-(border/2),(border/2));
                }
                newPoint.y=startHeight-i*ColunmHeight;
               var room= Instantiate(roomPrefab,newPoint,Quaternion.identity,transform);
               
               RoomType newType=GetRandomRoomType(mapConfigOS.rooms[colunm].roomType);
               if(colunm==0)
               {
                room.roomState=RoomState.gaind;
               }else{
                room.roomState=RoomState.Locked;
               }
                room.SetupRoom(colunm,i,GetRoomDataOS(newType) );
               rooms.Add(room);
               currentRooms.Add(room);
            }
            if(preRooms.Count>0)
            {
                CreateConnections(preRooms,currentRooms);
            }
            preRooms=currentRooms;
        }
        SaveMap();
    }

    private void CreateConnections(List<Room> preRooms, List<Room> currentRooms)
    {
        HashSet<Room> hashSetRoom=new();
        foreach(Room room in preRooms)
        {
            var targetRoom=ConnectRandomRoom(room,currentRooms,false);
            hashSetRoom.Add(targetRoom);
        }
        foreach(Room room in currentRooms)
        {
            if(!hashSetRoom.Contains(room))
            {
                var targetRoom =ConnectRandomRoom(room,preRooms,true);
                hashSetRoom.Add(room);
            }
        }
    }

    private Room ConnectRandomRoom(Room room, List<Room> currentRooms,bool check)
    {
        Room targetRoom;
        targetRoom=currentRooms[UnityEngine.Random.Range(0,currentRooms.Count-1)];
        if(check)
        {
            targetRoom.linkTo.Add(new(room.colunm,room.line));
        }
        else{
            room.linkTo.Add(new(targetRoom.colunm,targetRoom.line));
        }
        LineRenderer line=Instantiate(linePrefab,transform);
        line.SetPosition(0,room.transform.position);
        line.SetPosition(1,targetRoom.transform.position);
        lines.Add(line);
        return targetRoom;
    }



    [ContextMenu(itemName:"ReGenerate")]
    public void ReGenerate()
    {
        foreach(var item in rooms)
        {
            Destroy(item.gameObject);
        }
        foreach(var line in lines)
        {
            Destroy(line.gameObject);
        }
        rooms.Clear();
        lines.Clear();
        CreateMap();
    }

    public RoomDataOS GetRoomDataOS(RoomType roomType)
    {
        return roomDataDict[roomType];
    }

    public RoomType GetRandomRoomType(RoomType flag)
    {
        string[] options=flag.ToString().Split(',');
        string randomOption=options[UnityEngine.Random.Range(0,options.Length)];
        RoomType roomType=(RoomType)Enum.Parse(typeof(RoomType),randomOption);
        return roomType;
    }

    public void SaveMap()
    {
        mapLayoutOS.mapRoomDataList=new();
        for(int i=0;i<rooms.Count;i++)
        {
            var room=new MapRoomData(){
                PosX=rooms[i].transform.position.x,
                PosY=rooms[i].transform.position.y,
                colunm=rooms[i].colunm,
                line=rooms[i].line,
                roomDataOS=rooms[i].roomDataOS,
                roomState=rooms[i].roomState,
                linkTo=rooms[i].linkTo
            };
            mapLayoutOS.mapRoomDataList.Add(room);
            
        }
        mapLayoutOS.linePositionList=new();
        for(int i=0;i<lines.Count;i++)
        {
            var linePostion=new LinePosition{
                startPos=new SerializeVecotr3(lines[i].GetPosition(0)),
                endPos=new SerializeVecotr3(lines[i].GetPosition(1))
            };
            mapLayoutOS.linePositionList.Add(linePostion);
        }
    }

    public void LoadMap()
    {
        for(int i=0;i<mapLayoutOS.mapRoomDataList.Count;i++)
        {
            var newPosion=new Vector3(mapLayoutOS.mapRoomDataList[i].PosX,mapLayoutOS.mapRoomDataList[i].PosY);
            var newRoom=Instantiate(roomPrefab,newPosion,quaternion.identity,transform);
            newRoom.roomState=mapLayoutOS.mapRoomDataList[i].roomState;
            
            newRoom.SetupRoom(mapLayoutOS.mapRoomDataList[i].colunm,mapLayoutOS.mapRoomDataList[i].line,mapLayoutOS.mapRoomDataList[i].roomDataOS);
            newRoom.linkTo = mapLayoutOS.mapRoomDataList[i].linkTo;
            rooms.Add(newRoom);
        }
        for(int i=0;i<mapLayoutOS.linePositionList.Count;i++)
        {
            var line=Instantiate(linePrefab,transform);
            line.SetPosition(0,mapLayoutOS.linePositionList[i].startPos.ToVector());
            line.SetPosition(1,mapLayoutOS.linePositionList[i].endPos.ToVector());
            lines.Add(line);
        }
    }
}
