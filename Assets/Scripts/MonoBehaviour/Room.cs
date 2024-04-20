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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        if(roomState == RoomState.gaind)
        roomEvent.RaiseEvent(this,this);
    }

   public void SetupRoom(int colunm,int line,RoomDataOS roomDataOS)
    {
        this.colunm=colunm;
        this.line=line;
        this.roomDataOS=roomDataOS;

        spriteRenderer.sprite=roomDataOS.roomIcon;

        spriteRenderer.color= roomState switch
        {
            RoomState.Locked => new Color(0.5f, 0.5f, 0.5f, 1f),
           RoomState.visited => new Color(0.5f, 0.8f, 0.5f, 0.5f),
           RoomState.gaind => Color.white,
            _ => throw new System.NotImplementedException(),
        };
    }
}
