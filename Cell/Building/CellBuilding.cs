using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellBuilding : Cell
{
    public GameObject claimUnit;
    public Types.Building.Types TypeBuilding { get { return typeBuilding; } set { typeBuilding = value; } }
    [SerializeField] private Types.Building.Types typeBuilding = Types.Building.Types.None;
    public Types.Resources[] ResourcesGive { get; set; } = null;
    /// <summary>
    /// Уничтожение
    /// </summary>
    public void Destroy()
    {
        if (typeBuilding == Types.Building.Types.Castle)
        {
            List<Cell> cells = SearchCellsAll(1, this);
            foreach (Cell cell in cells)
            {
                if (cell.Type == Types.Cell.Types.Building)
                {
                    cell.TypeVisibility = Types.Cell.Visibility.Types.CanNotSee;
                    cell.Type = Types.Cell.Types.Nature;
                    cell.transform.Find("Build").GetComponent<SpriteRenderer>().sprite = null;
                }
                else
                {
                    ((CellNature)cell).Points.Remove(Types.Nature.PointNature.Castle);
                    ((CellNature)cell).Points.Remove(Types.Nature.PointNature.Field);
                }
            }
        }
        else if(Type == Types.Cell.Types.Building)
        {
            TypeVisibility = Types.Cell.Visibility.Types.CanNotSee;
            Type = Types.Cell.Types.Nature;
            transform.Find("Build").GetComponent<SpriteRenderer>().sprite = null;
        }
    }
    private void Check(Types.Building.Types building, Types.Building.Types buildingSearch, Types.Nature.Types natureSearchEdit, Types.Nature.PointNature pointNatureAdd)
    {
        CellNature typeNature;
        if (building == buildingSearch)
        {
            if (posY + 1 < maps.maxY)
            {
                if (posY % 2 != 0 && posX + 1 < maps.maxX)
                {
                    if (maps.mapCells[posY + 1, posX + 1].Type == Types.Cell.Types.Nature)
                    {
                        typeNature = maps.mapCells[posY + 1, posX + 1].GetComponent<CellNature>();
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                else if (posX - 1 >= 0)
                {
                    if (maps.mapCells[posY + 1, posX - 1].Type == Types.Cell.Types.Nature)
                    {
                        typeNature = maps.mapCells[posY + 1, posX - 1].GetComponent<CellNature>();
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                if (maps.mapCells[posY + 1, posX].Type == Types.Cell.Types.Nature)
                {
                    typeNature = maps.mapCells[posY + 1, posX].GetComponent<CellNature>();
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
            if (posY - 1 >= 0)
            {
                if (posY % 2 != 0 && posX + 1 < maps.maxX)
                {
                    if (maps.mapCells[posY - 1, posX + 1].Type == Types.Cell.Types.Nature)
                    {
                        typeNature = maps.mapCells[posY - 1, posX + 1].GetComponent<CellNature>();
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                else if (posX - 1 >= 0)
                {
                    if (maps.mapCells[posY - 1, posX - 1].Type == Types.Cell.Types.Nature)
                    {
                        typeNature = maps.mapCells[posY - 1, posX - 1].GetComponent<CellNature>();
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                if (maps.mapCells[posY - 1, posX].Type == Types.Cell.Types.Nature)
                {
                    typeNature = maps.mapCells[posY - 1, posX].GetComponent<CellNature>();
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
            if (posX + 1 < maps.maxX)
            {
                if (maps.mapCells[posY, posX + 1].Type == Types.Cell.Types.Nature)
                {
                    typeNature = maps.mapCells[posY, posX + 1].GetComponent<CellNature>();
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
            if (posX - 1 >= 0)
            {
                if (maps.mapCells[posY, posX - 1].Type == Types.Cell.Types.Nature)
                {
                    typeNature = maps.mapCells[posY, posX - 1].GetComponent<CellNature>();
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
        }
    }
    private void CheckTypeAndAdd(CellNature nature, Types.Nature.Types natureSearchEdit, Types.Nature.PointNature pointNatureAdd)
    {
        if (nature.TypeNature == natureSearchEdit)
        {
            if (nature.Points.Count > 0)
            {
                if (!nature.Points.Contains(pointNatureAdd))
                {
                    nature.Points.Add(pointNatureAdd);
                }
            }
            else
            {
                nature.Points.Add(pointNatureAdd);
            }
        }
    }
    private void OnValidate()
    {
        type = Types.Cell.Types.Building;
    }
    private void Start()
    {
        type = Types.Cell.Types.Building;
        Backlight();
        Check(TypeBuilding, Types.Building.Types.Castle, Types.Nature.Types.Meadow, Types.Nature.PointNature.Castle);
        Check(TypeBuilding, Types.Building.Types.Farmhouse, Types.Nature.Types.Meadow, Types.Nature.PointNature.Field);
    }
    private void Backlight()
    {
        List<Cell> cells;
        if (TypeBuilding == Types.Building.Types.Castle)
        {
            cells = SearchCellsAll(2, this);
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].TypeVisibility == Types.Cell.Visibility.Types.CanNotSee)
                {
                    cells[i].TypeVisibility = Types.Cell.Visibility.Types.Passed;
                }
            }
            cells = SearchCellsAll(1, this);
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].TypeVisibility == Types.Cell.Visibility.Types.Passed)
                {
                    cells[i].TypeVisibility = Types.Cell.Visibility.Types.Medium;
                }
            }

            this.TypeVisibility = Types.Cell.Visibility.Types.Good;
        }
        else
        {
            cells = SearchCellsAll(1, this);
            for (int i = 0; i < cells.Count; i++)
            {
                if (cells[i].Type == Types.Cell.Types.Nature)
                {
                    if (cells[i].TypeVisibility == Types.Cell.Visibility.Types.Passed)
                    {
                        cells[i].TypeVisibility = Types.Cell.Visibility.Types.Poorly;
                    }
                    else if (cells[i].TypeVisibility == Types.Cell.Visibility.Types.CanNotSee)
                    {
                        cells[i].TypeVisibility = Types.Cell.Visibility.Types.Poorly;
                    }
                }
            }
            this.TypeVisibility = Types.Cell.Visibility.Types.Medium;
        }
    }
    private static List<Cell> SearchCellsAll(int range, Cell cell)
    {
        List<Cell> cells = new List<Cell>(); //= new Cell[((range + 1) * (3 * range)) + 1 + (range * 2) + 2];
        int rangeY = range;
        int rangeX;
        cells.Add(cell);
        for (int x = 1; x <= range; x++)
        {
            rangeX = 0;
            for (int y = 0; y <= range; y++)
            {
                if (cell.posY + y < cell.maps.maxY)
                {
                    if (cell.posY % 2 == 0)
                    {
                        if ((cell.posX + x) - rangeX < cell.maps.maxX)
                        {
                            if(cells.Contains(cell.maps.mapCells[cell.posY + y, (cell.posX + x) - rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY + y, (cell.posX + x) - rangeX]);
                            }
                        }
                        if (y % 2 == 0)
                        {
                            rangeX++;
                        }
                    }
                    else if (cell.posY % 2 != 0)
                    {
                        if (y % 2 != 0)
                        {
                            rangeX++;
                        }
                        if ((cell.posX - x) + rangeX >= 0)
                        {
                            if(cells.Contains(cell.maps.mapCells[cell.posY + y, (cell.posX - x) + rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY + y, (cell.posX - x) + rangeX]);
                            }
                        }
                    }
                }
            }
            rangeX = 0;
            for (int y = 0; y <= range; y++)
            {
                if (cell.posY + y < cell.maps.maxY)
                {
                    if (cell.posY % 2 == 0)
                    {
                        if ((cell.posX - x) + rangeX >= 0)
                        {
                            if (cells.Contains(cell.maps.mapCells[cell.posY + y, (cell.posX - x) + rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY + y, (cell.posX - x) + rangeX]);
                            }
                        }
                        if (y % 2 != 0)
                        {
                            rangeX++;
                        }
                    }
                    else if (cell.posY % 2 != 0)
                    {
                        if ((cell.posX + x) - rangeX < cell.maps.maxX)
                        {
                            if (cells.Contains(cell.maps.mapCells[cell.posY + y, (cell.posX + x) - rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY + y, (cell.posX + x) - rangeX]);
                            }
                        }
                        if (y % 2 != 0)
                        {
                            rangeX++;
                        }
                    }
                }
            }
            rangeX = 0;
            for (int y = 0; y <= range; y++)
            {
                if (cell.posY - y >= 0)
                {
                    if (cell.posY % 2 == 0)
                    {
                        if ((cell.posX + x) - rangeX < cell.maps.maxX)
                        {
                            if (cells.Contains(cell.maps.mapCells[cell.posY - y, (cell.posX + x) - rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY - y, (cell.posX + x) - rangeX]);
                            }
                        }
                        if (y % 2 == 0)
                        {
                            rangeX++;
                        }
                    }
                    else if (cell.posY % 2 != 0)
                    {
                        if (y % 2 != 0)
                        {
                            rangeX++;
                        }
                        if ((cell.posX - x) + rangeX >= 0)
                        {
                            if (cells.Contains(cell.maps.mapCells[cell.posY - y, (cell.posX - x) + rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY - y, (cell.posX - x) + rangeX]);
                            }
                        }
                    }
                }
            }
            rangeX = 0;
            for (int y = 0; y <= range; y++)
            {
                if (cell.posY - y >= 0)
                {
                    if (cell.posY % 2 == 0)
                    {
                        if ((cell.posX - x) + rangeX >= 0)
                        {
                            if (cells.Contains(cell.maps.mapCells[cell.posY - y, (cell.posX - x) + rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY - y, (cell.posX - x) + rangeX]);
                            }
                        }
                        if (y % 2 != 0)
                        {
                            rangeX++;
                        }
                    }
                    else if (cell.posY % 2 != 0)
                    {
                        if ((cell.posX + x) - rangeX < cell.maps.maxX)
                        {
                            if(cells.Contains(cell.maps.mapCells[cell.posY - y, (cell.posX + x) - rangeX]) == false)
                            {
                                cells.Add(cell.maps.mapCells[cell.posY - y, (cell.posX + x) - rangeX]);
                            }
                        }
                        if (y % 2 != 0)
                        {
                            rangeX++;
                        }
                    }
                }
            }
            rangeY--;
        }
        for (int i = 0; i < cells.Count; i++)
        {
            for (int a = 0; a < cells.Count; a++)
            {
                if (cells[i] == cells[a] && i != a)
                {
                    List<Cell> cellsOld = cells;
                    cells = new List<Cell>();
                    for (int s = 0; s < cells.Count; s++)
                    {
                        if (i <= s)
                            cells.Add(cellsOld[s + 1]);
                        else
                            cells.Add(cellsOld[s]);
                    }
                    i = 0;
                }
            }
        }
        return cells;
    }
}
