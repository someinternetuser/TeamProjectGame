﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Index
{
	public Index(int xx, int yy)
	{
		this.x = xx;
		this.y = yy;

	}

	public int x;
	public int y;
}
public class CastleCoords
{
	public Index index;
	public List<int> area;
}
public class CastleData
{
	public bool isFinished;
	public List<CastleCoords> fields= new List<CastleCoords>();
}
public class ReturnPoints
{
    public ReturnPoints()
    {

    }
    public ReturnPoints(int p, List<Index> meeplesPositionss)
    {
        this.points = p;
        this.meeplesPositions = meeplesPositionss;
    }
    public int points;
	public List<Index> meeplesPositions = new List<Index>();
}
public class AreaTuple
{
    public AreaTuple()
    {
    }
    public AreaTuple(int xx, int yy, Area areaa, bool init)
    {
        this.area = areaa;
        this.x = xx;
        this.y = yy;
        this.initialized = init;
    }
    public int x;
	public int y;
	public Area area;
	public bool initialized;
}
public class AreaTupleTwo
{
    public AreaTupleTwo()
    {
    }
    public AreaTupleTwo(int xx, int yy, List<int> areaa, bool init)
    {
        this.area = areaa;
        this.x = xx;
        this.y = yy;
        this.initialized = init;
    }
    public int x;
    public int y;
    public List<int> area;
    public bool initialized;
}


public class PointsCounter : MonoBehaviour {
	private const int POINTS_FOR_FINISHED_CASTLE_WHEN_FIELD = 3;
	private const int POINTS_FOR_CASTLE_TILE = 2;
	private const int POINTS_FOR_CASTLE_SHIELD = 2;
	private const int POINTS_FOR_ROAD_FIELD = 1;
	private const int POINTS_FOR_MONASTERY_FIELD = 1;    
    private const int POINTS_FOR_CASTLE_TILE_GAME_END = 1;
    private const int POINTS_FOR_CASTLE_SHIELD_GAME_END = 1;

    GameManager GM;
    void Awake()
    {
        GM = GameManager.Instance;
    }
    public float[] getCoordinates(int row, int col)
    {
        float x = ((float)col - 100) * 10;
        float z = ((float)row - 100) * (-10);
        float[] array = new float[2] { x, z };
        return array;
    }
    public AreaTuple areaNeighbour(ref GameObject[,] board, int x, int y, int edge)
	{
//        Debug.Log("Sprawdzam dla krawedzi"  + edge);
		AreaTuple neighbour = new AreaTuple();
		neighbour.initialized = false;
		int[] correspondingEdges = new int[13];
		correspondingEdges[1] = 9;
		correspondingEdges[2] = 8;
		correspondingEdges[3] = 7;
		correspondingEdges[4] = 12;
		correspondingEdges[5] = 11;
		correspondingEdges[6] = 10;
		correspondingEdges[7] = 3;
		correspondingEdges[8] = 2;
		correspondingEdges[9] = 1;
		correspondingEdges[10] = 6;
		correspondingEdges[11] = 5;
		correspondingEdges[12] = 4;
		neighbour.x = x;
		neighbour.y = y;
		if (edge == 1 || edge == 2 || edge == 3)
		{
			(neighbour.x)--;
		}
		else if (edge == 4 || edge == 5 || edge == 6)
		{
			(neighbour.y)++;
		}
		else if (edge == 7 || edge == 8 || edge == 9)
		{
			(neighbour.x)++;
		}
		else if (edge == 10 || edge == 11 || edge == 12)
		{
			(neighbour.y)--;
		}
		if (board[neighbour.x, neighbour.y] != null)
		{
			foreach (var area in board[neighbour.x, neighbour.y].GetComponent<Tile>().Areas)
			{
				if (area.edges.Contains(correspondingEdges[edge]))
				{
					neighbour.area = area;
					break;
				}
			}
            //neighbour.initialized = true;
            //Debug.Log("Obszar klokcka sąsiada to : " + String.Join(" ", neighbour.area.edges.Select(item => item.ToString()).ToArray()));
		}

        

        return neighbour;
	}
    public AreaTuple areaNeighbour(ref TileAI[,] board, int x, int y, int edge)
    {
        AreaTuple neighbour = new AreaTuple();
        neighbour.initialized = false;
        int[] correspondingEdges = new int[13];
        correspondingEdges[1] = 9;
        correspondingEdges[2] = 8;
        correspondingEdges[3] = 7;
        correspondingEdges[4] = 12;
        correspondingEdges[5] = 11;
        correspondingEdges[6] = 10;
        correspondingEdges[7] = 3;
        correspondingEdges[8] = 2;
        correspondingEdges[9] = 1;
        correspondingEdges[10] = 6;
        correspondingEdges[11] = 5;
        correspondingEdges[12] = 4;
        neighbour.x = x;
        neighbour.y = y;
        if (edge == 1 || edge == 2 || edge == 3)
        {
            (neighbour.x)--;
        }
        else if (edge == 4 || edge == 5 || edge == 6)
        {
            (neighbour.y)++;
        }
        else if (edge == 7 || edge == 8 || edge == 9)
        {
            (neighbour.x)++;
        }
        else if (edge == 10 || edge == 11 || edge == 12)
        {
            (neighbour.y)--;
        }
        if ((object)board[neighbour.x, neighbour.y] != null)
        {
            foreach (var area in board[neighbour.x, neighbour.y].Areas)
            {
                if (area.edges.Contains(correspondingEdges[edge]))
                {
                    neighbour.area = area;
                    break;
                }
            }
        }
        //neighbour.initialized = true;
        return neighbour;
    }

    public List<AreaTuple> areaNeighbours(ref GameObject[,] board, int x, int y, List<int> edges)
	{
		List<AreaTuple> neighbours = new List<AreaTuple>();
		foreach (var edge in edges)
		{
			AreaTuple neighbour = areaNeighbour(ref board, x, y, edge);
			neighbours.Add(neighbour);
		}
        //foreach (var n in neighbours)
        //{
        //    Debug.Log("Dla "+x+" "+y+" Klocek sąsiad to : " + n.x + " " + n.y);
        //    Debug.Log("Obszar klokcka sąsiada to : " + String.Join(" ", n.area.edges.Select(item => item.ToString()).ToArray()));
        //}
        //Debug.Log("---");
		//Debug.Log("wielkość listy:"+neighbours.Count);
		return neighbours;
	}
    public List<AreaTuple> areaNeighbours(ref TileAI[,] board, int x, int y, List<int> edges)
    {
        List<AreaTuple> neighbours = new List<AreaTuple>();
        foreach (var edge in edges)
        {
            AreaTuple neighbour = areaNeighbour(ref board, x, y, edge);
            neighbours.Add(neighbour);
        }
        return neighbours;
    }

    public bool checkClosedArea(int x, int y, ref GameObject[,] board, ref List<AreaTupleTwo> checkedAreas, ref List<AreaTupleTwo> checkedGivenAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedGivenAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            return true;
        }
        checkedAreas.Add(currentTuple);
        checkedGivenAreas.Add(currentTuple);
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        foreach (var neighbour in an)
        {
            if (board[neighbour.x, neighbour.y] == null)
            {
                return false;
            }

            bool returnValue = checkClosedArea(neighbour.x, neighbour.y, ref board, ref checkedAreas, ref checkedGivenAreas, neighbour.area);
            if (returnValue == false)
            {
                return false;
            }
        }
        return true;
    }

    public bool checkClosedArea(int x, int y, ref TileAI[,] board, ref List<AreaTupleTwo> checkedAreas, ref List<AreaTupleTwo> checkedGivenAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedGivenAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            return true;
        }
        checkedAreas.Add(currentTuple);
        checkedGivenAreas.Add(currentTuple);
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        foreach (var neighbour in an)
        {
            if ((object)board[neighbour.x, neighbour.y] == null)
            {
                return false;
            }
            bool returnValue = checkClosedArea(neighbour.x, neighbour.y, ref board, ref checkedAreas, ref checkedGivenAreas, neighbour.area);
            if (returnValue == false)
            {
                return false;
            }
        }
        return true;
    }

    public void countMonasteryPoints(int[] coord, ref GameObject[,] board, ref GameObject[,] meeples)
	{
        if (board[coord[0], coord[1]].GetComponent<Tile>().Areas.All(a => a.player == null))
			return;
		for(int i = -1; i < 2; ++i)
			for(int j = -1; j < 2; ++j)
				if (board[coord[0]+i,coord[1]+j] == null)
					return;

        GM.AddScore(board[coord[0], coord[1]].GetComponent<Tile>().Areas.Find(a => a.player != null).player.color, 9);
        // board[coord[0], coord[1]].GetComponent<Tile>().Areas.Find (a => a.player != null).player.points += 9;
        Color col = board[coord[0], coord[1]].GetComponent<Tile>().Areas.Find(a => a.player != null).player.rgbaColor;
        GM.ReturnMeeple(board[coord[0], coord[1]].GetComponent<Tile>().Areas.Find(a => a.player != null).player.color);
        board [coord[0], coord[1]].GetComponent<Tile>().Areas.Find (a => a.player != null).player = null;
        Destroy(meeples[coord [0], coord [1]]);

        for (int i = -1; i < 2; ++i)
        {
            for (int j = -1; j < 2; ++j)
            {
                
                FloatingTextController.Initialize();
                FloatingTextController.CreateFloatingText2("+1".ToString(), getCoordinates(coord[0] + i, coord[1] + j)[0], getCoordinates(coord[0] + i, coord[1] + j)[1], col);
            }                
        }
           
        meeples[coord[0], coord[1]] = null;

    }
    public void countMonasteryPoints(int[] coord, ref TileAI[,] board, ref List<Player> players)
    {

        if (board[coord[0], coord[1]].Areas.All(a => a.player == null))
            return;
        for (int i = -1; i < 2; ++i)
            for (int j = -1; j < 2; ++j)
                if ((object)board[coord[0] + i, coord[1] + j] == null)
                    return;

        //PlayerColor color = board[coord[0], coord[1]].Areas.Find(a => a.player != null).player.color;
        PlayerColor color = PlayerColor.RED;
        int areaindex = 0;
        for (int i = 0; i < board[coord[0], coord[1]].Areas.Count; i++)
        {
            if (board[coord[0], coord[1]].Areas[i].player != null)
            {
                color = board[coord[0], coord[1]].Areas[i].player.color;
                areaindex = i;
                //Debug.Log(x + ", " + y + "   " + players[0].points + ", " + players[1].points);

            }
        }
        foreach (var area in board[coord[0], coord[1]].Areas)
        {
            if (area.player != null)
            {
                color = area.player.color;
            }
        }
        players.Find(a => a.color == color).ChangeScore(9);
        players.Find(a => a.color == color).meeples++;

        //board[coord[0], coord[1]].Areas.Find(a => a.player != null).player = null;
        board[coord[0], coord[1]].Areas[areaindex].player = null;
    }

    public void countMonasteryPointsEnd(int[] coord, ref GameObject[,] board, ref GameObject[,] meeples)
    {
        int value = 0;
        for (int i = -1; i < 2; ++i)
            for (int j = -1; j < 2; ++j)
                if (board[coord[0] + i, coord[1] + j] != null)
                    value++;
        
        GM.AddScore(board[coord[0], coord[1]].GetComponent<Tile>().Areas.Find(a => a.player != null).player.color, value);

        GM.ReturnMeeple(board[coord[0], coord[1]].GetComponent<Tile>().Areas.Find(a => a.player != null).player.color);
        board[coord[0], coord[1]].GetComponent<Tile>().Areas.Find(a => a.player != null).player = null;
        Destroy(meeples[coord[0], coord[1]]);
        meeples[coord[0], coord[1]] = null;
    }

    public void countMonasteryPointsEnd(int[] coord, ref TileAI[,] board, ref List<Player> players)
    {
        int value = 0;
        for (int i = -1; i < 2; ++i)
            for (int j = -1; j < 2; ++j)
                if ((object)board[coord[0] + i, coord[1] + j] != null)
                    value++;

        //PlayerColor color = board[coord[0], coord[1]].Areas.Find(a => a.player != null).player.color;
        PlayerColor color = PlayerColor.RED;
        int areaindex = 0;
        for (int i = 0; i < board[coord[0], coord[1]].Areas.Count; i++)
        {
            if (board[coord[0], coord[1]].Areas[i].player != null)
            {
                color = board[coord[0], coord[1]].Areas[i].player.color;
                areaindex = i;
                //Debug.Log(x + ", " + y + "   " + players[0].points + ", " + players[1].points);

            }
        }
        foreach (var area in board[coord[0], coord[1]].Areas)
        {
            if (area.player != null)
            {
                color = area.player.color;
            }
        }
        players.Find(a => a.color == color).ChangeScore(value);
        players.Find(a => a.color == color).meeples++;

        board[coord[0], coord[1]].Areas[areaindex].player = null;
    }

    public void countPointsAfterMove(ref GameObject[,] board, int x, int y, ref GameObject[,] meeples, bool endgame = false)
	{
       // Debug.Log("Jestem w counterze");
        foreach (var area in board[x, y].GetComponent<Tile>().Areas)
		{
         //   Debug.Log("Liczę dla roznych obszarów");
            CountAreaPoints(ref board, x, y, area, ref meeples, endgame);
		}
        //    Debug.Log("Liczę dla monastery");
        if (!endgame)
        {
            for (int i = -1; i < 2; ++i)
                for (int j = -1; j < 2; ++j)
                    if (board[x + i, y + j] != null && board[x + i, y + j].GetComponent<Tile>().Areas.Exists(a => a.terrain == terrainTypes.monastery))
                        countMonasteryPoints(new int[] { x + i, y + j }, ref board, ref meeples);
        }
    }

    public void countPointsAfterMove(ref TileAI[,] board, ref List<Player> players, int x, int y, bool endgame = false)
    {
        // Debug.Log("Jestem w counterze");
        foreach (var area in board[x, y].Areas)
        {
            //   Debug.Log("Liczę dla roznych obszarów");
            CountAreaPoints(ref board, x, y, area, endgame, ref players);
        }
        //    Debug.Log("Liczę dla monastery");
        if (!endgame)
        {
            for (int i = -1; i < 2; ++i)
                for (int j = -1; j < 2; ++j)
                    if ((object)board[x + i, y + j] != null && board[x + i, y + j].Areas.Exists(a => a.terrain == terrainTypes.monastery))
                        countMonasteryPoints(new int[] { x + i, y + j }, ref board, ref players);
        }
    }

    public void CountAreaPoints(ref GameObject[,] board, int x, int y, Area area, ref GameObject[,] meeples, bool endgame)
    {
        List<AreaTupleTwo> checkedAreas = new List<AreaTupleTwo>();
        ReturnPoints result = new ReturnPoints();
        switch (area.terrain)
        {
            case terrainTypes.monastery:
                if (endgame)
                {
                    countMonasteryPointsEnd(new int[] { x, y }, ref board, ref meeples);
                }
                break;
            case terrainTypes.castle:
                if (endgame)
                {
                    result = countCastleEnd(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref meeples))
                        {
                            GM.AddScore((PlayerColor)player, result.points);
                        }
                    }
                }
                else
                {
                    result = countCastle(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        if (result.points == (2 * POINTS_FOR_CASTLE_TILE)) result.points = POINTS_FOR_CASTLE_TILE;
                        int shift = 0;
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref meeples))
                        {
                            if (result.points == 2)
                            {
                                foreach (var tile in checkedAreas)
                                {
                                    FloatingTextController.Initialize();
                                    FloatingTextController.CreateFloatingText2("+1", getCoordinates(tile.x, tile.y)[0], getCoordinates(tile.x, tile.y)[1], GM.GetPlayerListCopy().Find(p => p.color == (PlayerColor)player).rgbaColor);
                                }
                            }
                            else
                            {
                                foreach (var tile in checkedAreas)
                                {
                                    if (board[tile.x, tile.y].GetComponent<Tile>().Plus == true)
                                    {
                                        FloatingTextController.Initialize();
                                        FloatingTextController.CreateFloatingText2("+4", getCoordinates(tile.x, tile.y)[0] + shift, getCoordinates(tile.x, tile.y)[1] + shift, GM.GetPlayerListCopy().Find(p => p.color == (PlayerColor)player).rgbaColor);
                                    }
                                    else
                                    {
                                        FloatingTextController.Initialize();
                                        FloatingTextController.CreateFloatingText2("+2", getCoordinates(tile.x, tile.y)[0] + shift, getCoordinates(tile.x, tile.y)[1] + shift, GM.GetPlayerListCopy().Find(p => p.color == (PlayerColor)player).rgbaColor);
                                    }
                                }
                            }
                            GM.AddScore((PlayerColor)player, result.points);
                            shift += 1;
                        }
                    }
                }
                break;
            case terrainTypes.road:
                if (endgame)
                {
                    result = countRoadsEnd(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref meeples))
                        {
                            GM.AddScore((PlayerColor)player, result.points);
                        }
                    }
                }
                else
                {
                    result = countRoads(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        int shift = 0;
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref meeples))
                        {

                            GM.AddScore((PlayerColor)player, result.points);

                            foreach (var tile in checkedAreas)
                            {
                                FloatingTextController.Initialize();
                                FloatingTextController.CreateFloatingText2("+1", getCoordinates(tile.x, tile.y)[0] + shift, getCoordinates(tile.x, tile.y)[1] + shift, GM.GetPlayerListCopy().Find(p => p.color == (PlayerColor)player).rgbaColor);

                            }
                            shift += 1;
                        }

                    }
                }

                break;
            case terrainTypes.grass:
                if (endgame)
                {
                    List<AreaTupleTwo> checkedCastles = new List<AreaTupleTwo>();
                    Debug.Log("LICZE DLA TRAWY!");
                    result = countGrass(ref board, x, y, result, ref checkedAreas, ref checkedCastles, area);
                    Debug.Log("Znaleziono meepli: " + result.meeplesPositions.Count);
                    Debug.Log("Punkty za zamknięte zamki (o ile znajdzie się meeple): " + result.points);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref meeples))
                        {
                            Debug.Log("Przyznane punkty za farmę: " + result.points);
                            GM.AddScore((PlayerColor)player, result.points);
                        }
                    }
                }
                break;
        }
    }
    public void CountAreaPoints(ref TileAI[,] board, int x, int y, Area area, bool endgame, ref List<Player> players)
    {
        List<AreaTupleTwo> checkedAreas = new List<AreaTupleTwo>();
        ReturnPoints result = new ReturnPoints();

        switch (area.terrain)
        {
            case terrainTypes.monastery:
                if (endgame)
                {
                    countMonasteryPointsEnd(new int[] { x, y }, ref board, ref players);
                }
                break;
            case terrainTypes.castle:
                if (endgame)
                {
                    result = countCastleEnd(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref players))
                        {
                            //GM.AddScore((PlayerColor)player, result.points);
                            players.Find(a => a.color == (PlayerColor)player).ChangeScore(result.points);
                        }
                    }
                }
                else
                {
                    result = countCastle(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        if (result.points == (2 * POINTS_FOR_CASTLE_TILE)) result.points = POINTS_FOR_CASTLE_TILE;
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref players))
                        {
                            // GM.AddScore((PlayerColor)player, result.points);
                            players.Find(a => a.color == (PlayerColor)player).ChangeScore(result.points);
                        }
                    }
                }
                break;
            case terrainTypes.road:
                if (endgame)
                {
                    result = countRoadsEnd(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref players))
                        {
                            //GM.AddScore((PlayerColor)player, result.points);
                            players.Find(a => a.color == (PlayerColor)player).ChangeScore(result.points);
                        }
                    }
                }
                else
                {
                    result = countRoads(ref board, x, y, result, ref checkedAreas, area);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref players))
                        {
                            //GM.AddScore((PlayerColor)player, result.points);
                            players.Find(a => a.color == (PlayerColor)player).ChangeScore(result.points);
                        }
                    }
                }
                break;
            case terrainTypes.grass:
                if (endgame)
                {
                    List<AreaTupleTwo> checkedCastles = new List<AreaTupleTwo>();
                    //Debug.Log("LICZE DLA TRAWY!");
                    result = countGrass(ref board, x, y, result, ref checkedAreas, ref checkedCastles, area);
                    //Debug.Log("Znaleziono meepli: " + result.meeplesPositions.Count);
                    //Debug.Log("Punkty za zamknięte zamki (o ile znajdzie się meeple): " + result.points);
                    if (result.meeplesPositions.Count == 0)
                    {
                        //do nothing
                    }
                    else
                    {
                        foreach (var player in RemoveMeeplesAndPickWinner(ref board, result.meeplesPositions, ref players))
                        {
                            //Debug.Log("Przyznane punkty za farmę: " + result.points);
                            //GM.AddScore((PlayerColor)player, result.points);
                            players.Find(a => a.color == (PlayerColor)player).ChangeScore(result.points);
                        }
                    }
                }
                break;
        }
    }
    public List<int> RemoveMeeplesAndPickWinner(ref GameObject[,] board, List<Index> meeplesPositions,ref GameObject[,] meeples)
	{
		const int MAX_PLAYER_SIZE = 5;
		int[] players = new int[5];

		foreach (Index index in meeplesPositions)
		{
			players [(int)board [index.x, index.y].GetComponent<Tile> ().Areas.Find (a => a.player != null).player.color]++;
            
            // return meeple to player
            GM.ReturnMeeple(board [index.x, index.y].GetComponent<Tile> ().Areas.Find (a => a.player != null).player.color);
            board[index.x, index.y].GetComponent<Tile>().Areas.Find(aa => aa.player != null).player = null;
			Destroy(meeples[index.x, index.y]);
            meeples[index.x, index.y] = null;
        }

		int max_count = players[0];
		List<int> maxList = new List<int> ();

		for (int i = 0; i < MAX_PLAYER_SIZE; ++i)
		{
			if (players[i] > max_count)
			{
				max_count = players[i];
				maxList = new List<int> ();
				maxList.Add (i);
			}
			else if(players[i] == max_count)
			{
				maxList.Add (i);
			}
		}
		return maxList;
	}
    public List<int> RemoveMeeplesAndPickWinner(ref TileAI[,] board, List<Index> meeplesPositions, ref List<Player> playersList)
    {
        const int MAX_PLAYER_SIZE = 5;
        int[] players = new int[5];

        foreach (Index index in meeplesPositions)
        {
            players[(int)board[index.x, index.y].Areas.Find(a => a.player != null).player.color]++;

            // return meeple to player

            //0-900----------------------- zwracamy meepele

            //GM.ReturnMeeple(board[index.x, index.y].Areas.Find(a => a.player != null).player.color);
            PlayerColor color = board[index.x, index.y].Areas.Find(a => a.player != null).player.color;
            playersList.Find(a => a.color == color).meeples++;

            board[index.x, index.y].Areas.Find(aa => aa.player != null).player = null;
        }

        int max_count = players[0];
        List<int> maxList = new List<int>();

        for (int i = 0; i < MAX_PLAYER_SIZE; ++i)
        {
            if (players[i] > max_count)
            {
                max_count = players[i];
                maxList = new List<int>();
                maxList.Add(i);
            }
            else if (players[i] == max_count)
            {
                maxList.Add(i);
            }
        }
        return maxList;
    }
    public ReturnPoints countRoads(ref GameObject[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
       // Debug.Log("--------------------------------------------------------------------------- liczymy dla drogi");

        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
       //     Debug.Log("Odwiedzony:" + x + " " + y + " Akumulator: " + accumulator.points + " więc exit.");
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
      //  Debug.Log("Dodajemy nowy:" + x + " " + y + " Otrzymany akumulator: " + accumulator.points);
       // Debug.Log("Wszystkie odwiedzone:");
        //wypisz checkedAreas
        foreach (var jararar in checkedAreas)
        {
        //    Debug.Log("x: " + jararar.x + " y: " + jararar.y + " Area: edges: " + String.Join(" ", jararar.area.Select(item => item.ToString()).ToArray()));
        }
        if (board[x, y].GetComponent<Tile>().Areas.Exists(a => a.player != null && a.terrain==terrainTypes.road && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        accumulator.points += POINTS_FOR_ROAD_FIELD;

        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
      //  Debug.Log("Sprawdzamy sąsiadów!");
        foreach (var neighbour in an)
        {
         //   Debug.Log("Sprawdzamy sąsiada: " + neighbour.x + " " + neighbour.y);
            if (board[neighbour.x, neighbour.y] == null)
            {
           //     Debug.Log("Ten: " + neighbour.x + " " + neighbour.y + " to null, wiec exit");
                return new ReturnPoints(0, new List<Index>());
            }
            accumulator = countRoads(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            if (accumulator.points == 0)
            {
       //         Debug.Log("Sąsiad: " + neighbour.x + " " + neighbour.y + " zwrócił nulla, więc my: " + x + " " + y + "  też!");
                return accumulator;
            }
    //        Debug.Log("Po sprawdzeniu sąsiada: " + neighbour.x + " " + neighbour.y + " akumulator to: " + accumulator.points);
        }
   //     Debug.Log("Sąsiedzi sprawdzeni, zwracamy akumulator: " + accumulator.points + "!");
        return accumulator;
    }
    public ReturnPoints countRoads(ref TileAI[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
        // Debug.Log("--------------------------------------------------------------------------- liczymy dla drogi");

        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            //     Debug.Log("Odwiedzony:" + x + " " + y + " Akumulator: " + accumulator.points + " więc exit.");
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
        //  Debug.Log("Dodajemy nowy:" + x + " " + y + " Otrzymany akumulator: " + accumulator.points);
        // Debug.Log("Wszystkie odwiedzone:");
        if (board[x, y].Areas.Exists(a => a.player != null && a.terrain == terrainTypes.road && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        accumulator.points += POINTS_FOR_ROAD_FIELD;

        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        //  Debug.Log("Sprawdzamy sąsiadów!");
        foreach (var neighbour in an)
        {
            //   Debug.Log("Sprawdzamy sąsiada: " + neighbour.x + " " + neighbour.y);
            if ((object)board[neighbour.x, neighbour.y] == null)
            {
                //     Debug.Log("Ten: " + neighbour.x + " " + neighbour.y + " to null, wiec exit");
                return new ReturnPoints(0, new List<Index>());
            }
            accumulator = countRoads(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            if (accumulator.points == 0)
            {
                //         Debug.Log("Sąsiad: " + neighbour.x + " " + neighbour.y + " zwrócił nulla, więc my: " + x + " " + y + "  też!");
                return accumulator;
            }
            //        Debug.Log("Po sprawdzeniu sąsiada: " + neighbour.x + " " + neighbour.y + " akumulator to: " + accumulator.points);
        }
        //     Debug.Log("Sąsiedzi sprawdzeni, zwracamy akumulator: " + accumulator.points + "!");
        return accumulator;
    }
    public ReturnPoints countRoadsEnd(ref GameObject[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
        if (board[x, y].GetComponent<Tile>().Areas.Exists(a => a.player != null && a.terrain == terrainTypes.road && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        accumulator.points += POINTS_FOR_ROAD_FIELD;
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);     
        foreach (var neighbour in an)
        {            
            if (board[neighbour.x, neighbour.y] != null)
            {            
                accumulator = countRoadsEnd(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            }           
        }
        return accumulator;
    }
    public ReturnPoints countRoadsEnd(ref TileAI[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
        if (board[x, y].Areas.Exists(a => a.player != null && a.terrain == terrainTypes.road && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        accumulator.points += POINTS_FOR_ROAD_FIELD;
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        foreach (var neighbour in an)
        {
            if ((object)board[neighbour.x, neighbour.y] != null)
            {
                accumulator = countRoadsEnd(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            }
        }
        return accumulator;
    }
    public ReturnPoints countGrass(ref GameObject[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas,ref List<AreaTupleTwo> checkedCastles, Area area)
    {
        //Debug.Log("Jestem na trawie w :" + x + " " + y+" "+ String.Join(" ", area.edges.Select(item => item.ToString()).ToArray()));
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {          
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
        if (board[x, y].GetComponent<Tile>().Areas.Exists(a => a.player != null && a.terrain == terrainTypes.grass && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            accumulator.meeplesPositions.Add(new Index(x, y));
            //Debug.Log("Znaleziono meepla :" + x + " " + y);
        }
        List<int> areaNeighboursOnTile = area.edges.Where(u => u != 0 && u !=12).Select(q => q + 1).ToList().Union(area.edges.Where(u => u == 12).Select(q => q = 1).ToList()).Union(area.edges.Where(u => u != 0 && u != 1).Select(q => q - 1).ToList()).Union(area.edges.Where(u => u == 1).Select(q => q = 12).ToList()).ToList();
        foreach (var castle in board[x, y].GetComponent<Tile>().Areas.Where(ar => ar.terrain == terrainTypes.castle && ar.edges.Intersect(areaNeighboursOnTile).ToList().Any())) //sprawdzamy każdy zamek na tilu
        {
            List<AreaTupleTwo> checkedAreasThisCastle = new List<AreaTupleTwo>();
            if (checkedCastles.Any(opt => opt.x == x && opt.y == y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), castle.edges.OrderBy(t => t))))
            {
                continue;
            }
            else if (checkClosedArea(x,y,ref board,ref checkedCastles,ref checkedAreasThisCastle,castle))
            {
                //Debug.Log("Dodano punkty za zamek: "+x + " " + y+ String.Join(" ", castle.edges.Select(item => item.ToString()).ToArray()));
                accumulator.points += POINTS_FOR_FINISHED_CASTLE_WHEN_FIELD;
            }
        }
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        foreach (var neighbour in an)
        {
            if (board[neighbour.x, neighbour.y] != null)
            {
                accumulator = countGrass(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, ref checkedCastles, neighbour.area);
            }
        }
        return accumulator;
    }
    public ReturnPoints countGrass(ref TileAI[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, ref List<AreaTupleTwo> checkedCastles, Area area)
    {
        //Debug.Log("Jestem na trawie w :" + x + " " + y + " " + String.Join(" ", area.edges.Select(item => item.ToString()).ToArray()));
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
        if (board[x, y].Areas.Exists(a => a.player != null && a.terrain == terrainTypes.grass && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            accumulator.meeplesPositions.Add(new Index(x, y));
            //Debug.Log("Znaleziono meepla :" + x + " " + y);
        }
        List<int> areaNeighboursOnTile = area.edges.Where(u => u != 0 && u != 12).Select(q => q + 1).ToList().Union(area.edges.Where(u => u == 12).Select(q => q = 1).ToList()).Union(area.edges.Where(u => u != 0 && u != 1).Select(q => q - 1).ToList()).Union(area.edges.Where(u => u == 1).Select(q => q = 12).ToList()).ToList();
        foreach (var castle in board[x, y].Areas.Where(ar => ar.terrain == terrainTypes.castle && ar.edges.Intersect(areaNeighboursOnTile).ToList().Any())) //sprawdzamy każdy zamek na tilu
        {
            List<AreaTupleTwo> checkedAreasThisCastle = new List<AreaTupleTwo>();
            if (checkedCastles.Any(opt => opt.x == x && opt.y == y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), castle.edges.OrderBy(t => t))))
            {
                continue;
            }
            else if (checkClosedArea(x, y, ref board, ref checkedCastles, ref checkedAreasThisCastle, castle))
            {
                //Debug.Log("Dodano punkty za zamek: " + x + " " + y + String.Join(" ", castle.edges.Select(item => item.ToString()).ToArray()));
                accumulator.points += POINTS_FOR_FINISHED_CASTLE_WHEN_FIELD;
            }
        }
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        foreach (var neighbour in an)
        {
            if ((object)board[neighbour.x, neighbour.y] != null)
            {
                accumulator = countGrass(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, ref checkedCastles, neighbour.area);
            }
        }
        return accumulator;
    }
    public ReturnPoints countCastle(ref GameObject[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x ==currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {

            //Debug.Log("Odwiedzony:" + x + " " + y + " Akumulator: " + accumulator.points + " więc exit.");
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
       // Debug.Log("Dodajemy nowy:" + x + " " + y + " Otrzymany akumulator: " + accumulator.points);
       // Debug.Log("Wszystkie odwiedzone:");
        //wypisz checkedAreas
        foreach(var jararar in checkedAreas)
        {
        //    Debug.Log("x: " + jararar.x + " y: " + jararar.y + " Area: edges: " + String.Join(" ", jararar.area.Select(item => item.ToString()).ToArray()));
        }
        if (board[x, y].GetComponent<Tile>().Areas.Exists(a => a.player != null && a.terrain==terrainTypes.castle && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        if (board[x, y].GetComponent<Tile>().Plus)
            accumulator.points += POINTS_FOR_CASTLE_SHIELD;
        accumulator.points += POINTS_FOR_CASTLE_TILE;

        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        /*
        foreach ()
        {

        }
        */
       // Debug.Log("Sprawdzamy sąsiadów!");
        foreach (var neighbour in an)
        {
        //    Debug.Log("Sprawdzamy sąsiada: " + neighbour.x + " " + neighbour.y);
            if (board[neighbour.x, neighbour.y] == null)
            {
           //     Debug.Log("Ten: " + neighbour.x + " " + neighbour.y + " to null, wiec exit");
                return new ReturnPoints(0, new List<Index>());
            }
            accumulator = countCastle(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            if (accumulator.points == 0)
            {
            //    Debug.Log("Sąsiad: " + neighbour.x + " " + neighbour.y + " zwrócił nulla, więc my: " + x + " " + y + "  też!");
                return accumulator;
            }
          //  Debug.Log("Po sprawdzeniu sąsiada: " + neighbour.x + " " + neighbour.y + " akumulator to: " + accumulator.points);
        }
      //  Debug.Log("Sąsiedzi sprawdzeni, zwracamy akumulator: " + accumulator.points + "!");
        return accumulator;
    }
    public ReturnPoints countCastle(ref TileAI[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {

            //Debug.Log("Odwiedzony:" + x + " " + y + " Akumulator: " + accumulator.points + " więc exit.");
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
        // Debug.Log("Dodajemy nowy:" + x + " " + y + " Otrzymany akumulator: " + accumulator.points);
        // Debug.Log("Wszystkie odwiedzone:");
        //wypisz checkedAreas
        //foreach (var jararar in checkedAreas)
        //{
        //    //    Debug.Log("x: " + jararar.x + " y: " + jararar.y + " Area: edges: " + String.Join(" ", jararar.area.Select(item => item.ToString()).ToArray()));
        //}
        if (board[x, y].Areas.Exists(a => a.player != null && a.terrain == terrainTypes.castle && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        if (board[x, y].Plus)
            accumulator.points += POINTS_FOR_CASTLE_SHIELD;
        accumulator.points += POINTS_FOR_CASTLE_TILE;

        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        /*
        foreach ()
        {

        }
        */
        // Debug.Log("Sprawdzamy sąsiadów!");
        foreach (var neighbour in an)
        {
            //    Debug.Log("Sprawdzamy sąsiada: " + neighbour.x + " " + neighbour.y);
            if ((object)board[neighbour.x, neighbour.y] == null)
            {
                //     Debug.Log("Ten: " + neighbour.x + " " + neighbour.y + " to null, wiec exit");
                return new ReturnPoints(0, new List<Index>());
            }
            accumulator = countCastle(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            if (accumulator.points == 0)
            {
                //    Debug.Log("Sąsiad: " + neighbour.x + " " + neighbour.y + " zwrócił nulla, więc my: " + x + " " + y + "  też!");
                return accumulator;
            }
            //  Debug.Log("Po sprawdzeniu sąsiada: " + neighbour.x + " " + neighbour.y + " akumulator to: " + accumulator.points);
        }
        //  Debug.Log("Sąsiedzi sprawdzeni, zwracamy akumulator: " + accumulator.points + "!");
        return accumulator;
    }
    public ReturnPoints countCastleEnd(ref GameObject[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            return accumulator;
        }
        checkedAreas.Add(currentTuple); 
        if (board[x, y].GetComponent<Tile>().Areas.Exists(a => a.player != null && a.terrain == terrainTypes.castle && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        if (board[x, y].GetComponent<Tile>().Plus)
            accumulator.points += POINTS_FOR_CASTLE_SHIELD_GAME_END;
        accumulator.points += POINTS_FOR_CASTLE_TILE_GAME_END;
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        foreach (var neighbour in an)
        {
            if (board[neighbour.x, neighbour.y] != null)
            {
                 accumulator = countCastleEnd(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            }
        }
        return accumulator;
    }
    public ReturnPoints countCastleEnd(ref TileAI[,] board, int x, int y, ReturnPoints accumulator, ref List<AreaTupleTwo> checkedAreas, Area area)
    {
        AreaTupleTwo currentTuple = new AreaTupleTwo(x, y, area.edges, true);
        if (checkedAreas.Any(opt => opt.x == currentTuple.x && opt.y == currentTuple.y && Enumerable.SequenceEqual(opt.area.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
        {
            return accumulator;
        }
        checkedAreas.Add(currentTuple);
        if (board[x, y].Areas.Exists(a => a.player != null && a.terrain == terrainTypes.castle && Enumerable.SequenceEqual(a.edges.OrderBy(t => t), currentTuple.area.OrderBy(t => t))))
            accumulator.meeplesPositions.Add(new Index(x, y));
        if (board[x, y].Plus)
            accumulator.points += POINTS_FOR_CASTLE_SHIELD_GAME_END;
        accumulator.points += POINTS_FOR_CASTLE_TILE_GAME_END;
        List<AreaTuple> an = areaNeighbours(ref board, x, y, area.edges);
        foreach (var neighbour in an)
        {
            if ((object)board[neighbour.x, neighbour.y] != null)
            {
                accumulator = countCastleEnd(ref board, neighbour.x, neighbour.y, accumulator, ref checkedAreas, neighbour.area);
            }
        }
        return accumulator;
    }
}
