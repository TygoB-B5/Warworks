using Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    IGridMovable movable;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = new GameObject();
        go.AddComponent<Piece.PlayPiece>();
        movable = go.GetComponent<IGridMovable>();


        FindObjectOfType<MovementGrid>().AddGridMovableToGridWithCoordinate(new IntVector2(15, 15), movable);
    }
    float t = 0;
    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if (t > 0.50f)
        {
            t = 0;
            FindObjectOfType<MovementGrid>().SetTileTypeWithCoordinate(new IntVector2(Random.Range(0, 10), Random.Range(0, 10)), Random.Range(0, 3));
        }
    }
}
