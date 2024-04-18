using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public RoomDataOS roomDataOS;
    public RoomState roomState;
    public List<Vector2Int> linkTo;
    public ObjectEventSO roomEvent;
    public int colunm;
    public int line;

    private void Awake() {
        spriteRenderer=GetComponentInChildren<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetupRoom(0,0,roomDataOS);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        if(roomState == RoomState.gaind)
        roomEvent.RaiseEvent(roomDataOS,this);
    }

   public void SetupRoom(int colunm,int line,RoomDataOS roomDataOS)
    {
        this.colunm=colunm;
        this.line=line;
        this.roomDataOS=roomDataOS;

        spriteRenderer.sprite=roomDataOS.roomIcon;
    }
}
