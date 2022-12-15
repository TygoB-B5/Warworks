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
        movable.SetOwnerIndex(0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            FindObjectOfType<MovementGrid>().SetGridMovablePositionWithCoordinate(new IntVector2(Random.Range(0, 10), Random.Range(0, 10)), movable);
    }
}
