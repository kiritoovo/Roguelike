
using UnityEngine;

[System.Serializable]
public class SerializeVecotr3
{
   public float x,y,z;

    public SerializeVecotr3(Vector3 pos)
    {
        x=pos.x;
        y=pos.y;
        z=pos.z;
    }

    public Vector3 ToVector()
    {
        return new Vector3(x,y,z);
    }

    public Vector2Int ToVector2Int()
    {
        return new Vector2Int((int)x, (int)y);
    }

}