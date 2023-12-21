using System.Collections.Generic;
using UnityEngine;

public class Arrow
{
    public const float FORCE = 420f;
    public enum ArrowType
    {
        Left,
        Right,
        Up,
        Down,
        UpperLeft,
        UpperRight,
        DownLeft,
        DownRight
    }

    public static Dictionary<ArrowType, Vector2> Directions =
                            new Dictionary<ArrowType, Vector2>()
                            {
                                {ArrowType.Left, Vector2.left},
                                {ArrowType.Right, Vector2.right},
                                {ArrowType.Up, Vector2.up},
                                {ArrowType.Down, Vector2.down},
                                {ArrowType.UpperLeft, new Vector2(-1,1)},
                                {ArrowType.UpperRight, new Vector2(1,1)},
                                {ArrowType.DownLeft, new Vector2(-1,-1)},
                                {ArrowType.DownRight, new Vector2(1,-1)}
                            };

    public ArrowType Type { private set; get; }
    public bool Used { private set; get; }

    public void Launch(Rigidbody2D _rb)
    {
        if (Used) return;

        _rb.AddForce(FORCE * Directions[Type]);
        Used = true;
    }

    public Arrow()
    {
        Type = RandomEnum.GetRandom<ArrowType>();
        Used = false;
    }
}
