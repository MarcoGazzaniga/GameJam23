using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.Tilemaps;

public class Root : MonoBehaviour
{
    [Header ("Tile Map")]
    [SerializeField] Tilemap _bodyTilemap;

    [Header ("Body Tile Parameters")]
    [SerializeField] TileBase _horizontalTile;
    [SerializeField] TileBase _verticalTile;
    [SerializeField] TileBase _bendLeftUpTile;
    [SerializeField] TileBase _bendLeftDownTile;
    [SerializeField] TileBase _bendRightUpTile;
    [SerializeField] TileBase _bendRightDownTile;
    [SerializeField] TileBase _tTile;
    [SerializeField] TileBase _crossTile;

    private Vector3Int _prevPosTile;
    private Vector3Int _currentPosTile;

    void OnMovement(Vector2 offset)
    {
        _prevPosTile = _currentPosTile;
        _currentPosTile = _bodyTilemap.WorldToCell(transform.position); 
        
        ChoiseBend(_prevPosTile, _currentPosTile, offset);
    }

    void OnAttack(Vector2 offset) {
        _prevPosTile = _currentPosTile;
        _currentPosTile = _bodyTilemap.WorldToCell(transform.position);

        ChoiseBend(_prevPosTile, _currentPosTile, offset);
    }

    private void PlaceTale(Vector3Int pos, TileBase tile)
    { 
        _bodyTilemap.SetTile(pos, tile);
    }

    private void ChoiseBend(Vector3Int prevTile, Vector3Int curTile, Vector2 offset)
    {
        Vector3Int previousDirection = curTile - prevTile;
        previousDirection.Clamp(Vector3Int.one * -1, Vector3Int.one);
        //arriviamo da destra
        if (previousDirection == Vector3Int.left)
        {
            if (offset.x < 0)
            {
                PlaceTale(curTile, _horizontalTile);
            }
            if(offset.y < 0)
            {
                PlaceTale(curTile, _bendRightDownTile);
            }
            if(offset.y > 0)
            {
                PlaceTale(curTile, _bendRightUpTile);
            }
        }

        if (previousDirection == Vector3Int.right)
        {
            if (offset.x > 0)
            {
                PlaceTale(curTile, _horizontalTile);
            }
            if (offset.y < 0)
            {
                PlaceTale(curTile, _bendLeftDownTile);
            }
            if (offset.y > 0)
            {
                PlaceTale(curTile, _bendLeftUpTile);
            }
        }

        if (previousDirection == Vector3Int.up)
        {
            if (offset.y > 0)
            {
                PlaceTale(curTile, _verticalTile);
            }
            if (offset.x > 0)
            {
                PlaceTale(curTile, _bendRightDownTile);
            }
            if (offset.x < 0)
            {
                PlaceTale(curTile, _bendLeftDownTile);
            }
        }

        if (previousDirection == Vector3Int.down)
        {
            if (offset.y < 0)
            {
                PlaceTale(curTile, _verticalTile);
            }
            if (offset.x > 0)
            {
                PlaceTale(curTile, _bendRightUpTile);
            }
            if (offset.x < 0)
            {
                PlaceTale(curTile, _bendLeftUpTile);
            }
        }




        //    if (offset.x < 0 || offset.x > 0)
        //{
        //    PlaceTale(curTile, _horizontalTile);
        //}

        //if (offset.y < 0)
        //{

        //    {
        //        PlaceTale(curTile, _bendLeftUpTile);
        //    }
        //    else
        //    {
        //        PlaceTale(curTile, _verticalTile);
        //    }

        //    //se va su o giù ed il precedente è orizzontale mette la curva

        //}
        //offset.y > 0



    }
}
