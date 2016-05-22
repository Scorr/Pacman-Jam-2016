using UnityEngine;
using System.Collections.Generic;
using Pathfinding;

public class TileManager : MonoBehaviour {

	public class Tile
	{
		public int x { get; set; }
		public int y { get; set; }
		public bool occupied {get; set;}
		public int adjacentCount {get; set;}
		public bool isIntersection {get; set;}
		
		public Tile left,right,up,down;
		
		public Tile(int x_in, int y_in)
		{
			x = x_in; y = y_in;
			occupied = false;
			left = right = up = down = null;
		}


	};
	
	public List<Tile> tiles = new List<Tile>();
    public GameObject pacdot;
    private List<GameObject> pacdots = new List<GameObject>();
	
	// Use this for initialization
	void Start() 
	{
        ReadTiles();
	}

    // Update is called once per frame
	void Update () 
	{
		DrawNeighbors();
	}

    void OnLevelWasLoaded() {
        ReadTiles();
    }

    //-----------------------------------------------------------------------
    // hardcoded tile data: 1 = free tile, 0 = wall
    void ReadTiles()
    {

        GridGraph gridGraph = AstarPath.active.astarData.gridGraph;
        GridNode[] nodes = gridGraph.nodes;

        //using (StringReader reader = new StringReader(data))
        //{
        //    string line;
        //    while ((line = reader.ReadLine()) != null)
        //    {
        for (int y = 0; y < gridGraph.Depth; y++) {
            for (int x = 0; x < gridGraph.width; ++x)
            {
                var newTile = new Tile(x + 1, y + 1);
                GridNode node = nodes[y*gridGraph.width + x];
                if (node.Walkable) {
                    if (x != 0 && nodes[y*gridGraph.width + (x - 1)].Walkable) {
                        // assign each tile to the corresponding side of other tile
                        newTile.left = tiles[tiles.Count - 1];
                        tiles[tiles.Count - 1].right = newTile;

                        // adjust adjcent tile counts of each tile
                        newTile.adjacentCount++;
                        tiles[tiles.Count - 1].adjacentCount++;
                    }
                }
                else {
                    newTile.occupied = true;
                }

                // check for up-down neighbor
                int downNeighbour = tiles.Count - gridGraph.width; // up neighbor index
                if (y > 0 && !newTile.occupied && !tiles[downNeighbour].occupied) {
                    tiles[downNeighbour].up = newTile;
                    newTile.down = tiles[downNeighbour];

                    // adjust adjcent tile counts of each tile
                    newTile.adjacentCount++;
                    tiles[downNeighbour].adjacentCount++;
                }

                tiles.Add(newTile);
            }
        }

        // after reading all tiles, determine the intersection tiles
        foreach (Tile tile in tiles)
        {
            if (tile.adjacentCount > 2)
                tile.isIntersection = true;
        }

        AddPacdots();
    }

    private void AddPacdots() {
        if (pacdots.Count > 0) return; // Prevent bug that spawns pacdots more than once.
        foreach (Tile tile in tiles) {
            if (!tile.occupied)
                pacdots.Add((GameObject)Instantiate(pacdot, new Vector3(tile.x, tile.y), Quaternion.identity));
        }
    }

    /*void OnDrawGizmos() {
        foreach (Tile tile in tiles) {
            if (tile.occupied) Gizmos.DrawSphere(new Vector3(tile.x, tile.y, 0), 0.25f);
        }
    }*/

	//-----------------------------------------------------------------------
	// Draw lines between neighbor tiles (debug)
	void DrawNeighbors()
	{
		foreach(Tile tile in tiles) {
			Vector3 pos = new Vector3(tile.x, tile.y, 0);
			Vector3 up = new Vector3(tile.x+0.1f, tile.y+1, 0);
			Vector3 down = new Vector3(tile.x-0.1f, tile.y-1, 0);
			Vector3 left = new Vector3(tile.x-1, tile.y+0.1f, 0);
			Vector3 right = new Vector3(tile.x+1, tile.y-0.1f, 0);
			
			if(tile.up != null)		Debug.DrawLine(pos, up);
			if(tile.down != null)	Debug.DrawLine(pos, down);
			if(tile.left != null)	Debug.DrawLine(pos, left);
			if(tile.right != null)	Debug.DrawLine(pos, right);
		}
		
	}


	//----------------------------------------------------------------------
	// returns the index in the tiles list of a given tile's coordinates
	public int Index(int X, int Y) {
	    Tile retTile = tiles.Find(tile => tile.x == X && tile.y == Y);
        if (retTile != null)
            return tiles.IndexOf(retTile);
	    return 0;
		// if the requsted index is in bounds
		//Debug.Log ("Index called for X: " + X + ", Y: " + Y);
		if(X>=1 && X<=28 && Y<=31 && Y>=1)
			return (31-Y)*28 + X-1;

		// else, if the requested index is out of bounds
		// return closest in-bounds tile's index 
	    if(X<1)		X = 1;
	    if(X>28) 	X = 28;
	    if(Y<1)		Y = 1;
	    if(Y>31)	Y = 31;

	    return (31-Y)*28 + X-1;
	}
	
	public int Index(Tile tile)
	{
        return tiles.IndexOf(tile);
		return (31-tile.y)*28 + tile.x-1;
	}

	//----------------------------------------------------------------------
	// returns the distance between two tiles
	public float distance(Tile tile1, Tile tile2)
	{
		return Mathf.Sqrt( Mathf.Pow(tile1.x - tile2.x, 2) + Mathf.Pow(tile1.y - tile2.y, 2));
	}
}
