using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellers{

  public class CellSet{
    public List<Cell> Cells = new List<Cell>();
  }

  public class Cell{
    public CellSet Set;                                  
    public bool HasRightWall;
    public bool HasBottomWall;
  }

  private int width;
  private int height;

  private const int MaxBias = 64;
  private const int Bias = 32;

  private bool CreateWall{
    get => Random.Range(0, MaxBias + 1) > Bias;
  }

  private List<CellSet> sets;
  private List<Cell> row;

  private Cell[,] maze;

  public Ellers(int mazeWidth, int mazeHeight){
    width = mazeWidth;
    height = mazeHeight;

    maze = new Cell[width, height];
    sets = new List<CellSet>();
    row = new List<Cell>();

    for (int i = 0; i < width; i++)
      row.Add(new Cell());

    for (int rowIndex = 0; rowIndex < height - 1; rowIndex++){
      InitSets();

      for (int i = 0; i < row.Count - 1; i++)
        if (row[i].Set == row[i + 1].Set)
          row[i].HasRightWall = true;

      CreateRightWalls();

      CreateDownWalls();

      WriteRowIntoMaze(rowIndex);

      PrepareNextRow();
    }

    InitSets();

    foreach (Cell cell in row)
      cell.HasBottomWall = true;

    for (int i = 0; i < row.Count - 1; i++){
      if (row[i].Set != row[i + 1].Set)
        row[i].HasRightWall = false;
      else
        row[i].HasRightWall = true;
    }

    row[row.Count - 1].HasRightWall = true;

    WriteRowIntoMaze(height-1);

  }

  private void InitSets(){
    foreach (Cell cell in row){
      if (cell.Set == null){
        CellSet set = new CellSet();
        cell.Set = set;

        set.Cells.Add(cell);
        sets.Add(set);
      }
    }
  }

  private void CreateRightWalls(){
    for (int i = 0; i < row.Count - 1; i++){
      if (CreateWall){
        row[i].HasRightWall = true;
      }
      else if (row[i].Set == row[i + 1].Set){
        row[i].HasRightWall = true;
      }
      else{
        row[i + 1].Set.Cells.Remove(row[i + 1]);
        row[i].Set.Cells.Add(row[i + 1]);
        row[i + 1].Set = row[i].Set;
      }
    }
    row[row.Count - 1].HasRightWall = true;
  }

  private void CreateDownWalls(){
    foreach (CellSet set in sets.ToArray()){
      if (set.Cells.Count > 0){
        List<int> cellIndices = new List<int>();

        if (set.Cells.Count == 1){
          cellIndices.Add(0);
        }
        else{
          int downPaths = Random.Range(1, set.Cells.Count + 1);

          for (int i = 0; i < downPaths; i++)
          {
            int index;

            do{
              index = Random.Range(0, set.Cells.Count);
            } while (cellIndices.Contains(index));

            cellIndices.Add(index);
          }
        }

        for (int k = 0; k < set.Cells.Count; k++){
          if (!cellIndices.Contains(k))
            set.Cells[k].HasBottomWall = true;
          else
            set.Cells[k].HasBottomWall = false;
        }
      }
      else{
        sets.Remove(set);
      }
    }
  }

  private void WriteRowIntoMaze(int h){
    for (int i = 0; i < row.Count; i++){
      Cell cell = new Cell{
        HasBottomWall = row[i].HasBottomWall,
        HasRightWall = row[i].HasRightWall
      };

      maze[i, h] = cell;
    }
  }

  private void PrepareNextRow(){
    foreach (Cell cell in row){
      cell.HasRightWall = false;

      if (cell.HasBottomWall){
        cell.Set.Cells.Remove(cell);
        cell.Set = null;
        cell.HasBottomWall = false;
      }
    }
  }

  public void TranslateMazeIntoCubeFace(Vector3 groundPos, float groundCubeLength){
    GameObject wallbase = GameObject.Find("wall");
    Transform wallParent = wallbase.transform.parent;
    GameObject wall;
    Vector3 pos;
    for(int j = 0; j < width; j++){
      if(j==4||j==5)
        continue;
      pos = groundPos + new Vector3(-0.5f * groundCubeLength + 0.5f + (j) * 1f, 0.5f * groundCubeLength + 0.5f /* * razmer steny*/, 0.5f * groundCubeLength);
      wall = GameObject.Instantiate(wallbase, pos, Quaternion.Euler(Vector3.right  * 90f), wallParent);
    }

    for (int i = 0; i < height; i++)
    {

      if(i != 4 && i != 5){
        pos = groundPos + new Vector3(-0.5f * groundCubeLength, 0.5f * groundCubeLength + 0.5f, 0.5f * groundCubeLength - 0.5f - i * 1f);
        wall = GameObject.Instantiate(wallbase, pos, Quaternion.Euler((Vector3.right + Vector3.forward) * 90f), wallParent);
      }

      for (int j = 0; j < width; j++)
      {
        if (j!=width-1 || (j==width-1 && i != 4 && i != 5))
          if (maze[j, i].HasRightWall){
            pos = groundPos + new Vector3(-0.5f * groundCubeLength + (j + 1) * 1f, 0.5f * groundCubeLength + 0.5f, 0.5f * groundCubeLength - 0.5f - i * 1f);
            wall = GameObject.Instantiate(wallbase, pos, Quaternion.Euler((Vector3.right + Vector3.forward) * 90f), wallParent);
          }

        if (i!=height-1 || (i==height-1 && j != 4 && j != 5))
          if (maze[j, i].HasBottomWall){
            pos = groundPos + new Vector3(-0.5f * groundCubeLength + 0.5f + j * 1f, 0.5f * groundCubeLength + 0.5f, 0.5f * groundCubeLength - (i + 1) * 1f);
            wall = GameObject.Instantiate(wallbase, pos, Quaternion.Euler((Vector3.right) * 90f), wallParent);
          }

      }

    }
  }

}
