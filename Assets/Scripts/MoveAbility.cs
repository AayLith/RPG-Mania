using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAbility : Ability
{
    int rangeLeft = 0;
    bool arrow;

    private void Reset ()
    {
        arrow = true;
    }

    public override void init ( Character c )
    {
        range = c.moveLeft;
    }

    public override void end ()
    {

    }

    public override List<Tile> getTilesInRange ( Tile start )
    {
        List<Tile> tiles = Board.instance.getTilesForMove ( start , range , dontAddIfOccupied , stopSearchIfOccupied , true );
        tiles.RemoveAt ( 0 );
        return tiles;
    }

    public override List<Tile> getTargetableTiles ( List<Tile> tiles , Monster origin )
    {
        return tiles;
    }

    protected override IEnumerator execution ( Monster origin , List<Tile> tiles )
    {
        Debug.Log ( "Start moving" );
        rangeLeft -= ( tiles.Count - 1 );
        for ( int i = 0 ; i < tiles.Count ; i++ )
        {
            // Orient the sprite in the right direction
            if ( tiles[ i ].transform.position.x > origin.transform.position.x || tiles[ i ].transform.position.y > origin.transform.position.y )
                origin.transform.localScale = Vector3.one;
            else
                origin.transform.localScale = Vector3.one + ( 2 * Vector3.left );

            // Move the character
            while ( Vector3.Distance ( origin.transform.position , tiles[ i ].transform.position ) > 0.05f )
            {
                origin.transform.position += ( tiles[ i ].transform.position - origin.transform.position ).normalized * origin.moveSpeed * Time.deltaTime;
                yield return null;
            }
            origin.transform.position = tiles[ i ].transform.position;
            origin.tile.setContent ( null );
            origin.tile = tiles[ i ];
            origin.tile.setContent ( origin );
            yield return null;
        }
        Debug.Log ( "End moving" );
    }
}
