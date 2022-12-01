using Grid;
using Piece;
using UnityEngine;

public class test : MonoBehaviour
{
    public PlayPiece movable;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MovementGrid>().AddGridMovableToGridWithCoordinate(new IntVector2(5, 5), movable);
    }
}
