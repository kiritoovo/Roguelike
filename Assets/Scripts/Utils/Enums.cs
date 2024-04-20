using System;

[Flags]
public enum RoomType{
    nomalEnemy=1,eleteEnemy=2,bossEnemy=4,tresure=8,store=16,relax=32
}

public enum RoomState{
    Locked,visited,gaind
}

public enum CardType{
    attack,denden,ability
}