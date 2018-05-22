using UnityEngine;

public class Roadinfo : MonoBehaviour {
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum Type
    {
        Start,
        End,
        Straight,
        Turn,
        CrossRoad,
        Intersection
    }

    public Direction dirc;
    public Type type;
    

	void Start () {
        float rotate = 0;
		switch(dirc)
        {
            case Direction.Down:
                rotate = 0;
                break;
            case Direction.Up:
                rotate = 180;
                break;
            case Direction.Right:
                rotate = -90;
                break;
            case Direction.Left:
                rotate = 90;
                break;
        }

        this.transform.Rotate(new Vector3(0, 0, rotate));
    }

}
