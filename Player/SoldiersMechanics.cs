using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoldiersMechanics : MonoBehaviour
{
    private SwitchButton switchButton;
    private ClickAct cellAct;
    private Unit unit;
    [SerializeField] private CellMechanicsSoldiers mechanicsSoldiers;
    [SerializeField] private Sprite iconCreateSoldier;
    [HideInInspector] public Soldiers soldier;
    [HideInInspector] public State state = State.None;
    [Space]
    [SerializeField] private Sprite SoldierMove;
    [SerializeField] private Sprite SoldierAttacke;
    [SerializeField] private Sprite Back;
    [SerializeField] private Sprite Nature;

    public enum State
    {
        None,
        Move
    }
    
    private void Start()
    {
        switchButton = GetComponent<SwitchButton>();
        cellAct = GetComponent<ClickAct>();
        unit = GetComponent<Unit>();
    }

    /// <summary>
    /// Вкл
    /// </summary>
    public void Enable()
    {
        if (cellAct.actCell != null)
        {
            switchButton.Clear();

            if(cellAct.actCell.Type == Types.Cell.Types.Building)
            {
                if(((CellBuilding)cellAct.actCell).TypeBuilding == Types.Building.Types.Castle && mechanicsSoldiers.IsBuyCreate(unit, mechanicsSoldiers.soldiersTypes[0]))
                {
                    switchButton.AddButton((unit, soldiersType, cell) => mechanicsSoldiers.BuyCreate(unit, soldiersType, cell), 
                        iconCreateSoldier, unit, mechanicsSoldiers.soldiersTypes[0], cellAct.actCell);
                }
            }
            
            if (cellAct.actCell.transform.Find("Soldiers").childCount > 0)
            {
                Soldiers[] soldiers = cellAct.actCell.transform.Find("Soldiers").GetComponentsInChildren<Soldiers>();
                for (int i = 0; i < soldiers.Length; i++)
                {
                    switchButton.AddButton((id) => Buttons(id), soldiers[i].icon, i);
                }
            }
        }
    }
    
    /// <summary>
    /// Все действие которые может Unit
    /// </summary>
    /// <param name="id"></param>
    public void Buttons(int id)
    {
        soldier = cellAct.actCell.transform.Find("Soldiers").GetComponentsInChildren<Soldiers>()[id];
        switchButton.Clear();
        if(soldier.isActNum > 0)
        {
            switchButton.AddButton(ButtonMove, SoldierMove);
        }
        if(cellAct.actCell.Type == Types.Cell.Types.Building)
        {
            switchButton.AddButton(ButtonAttacke, SoldierAttacke);
        }
        switchButton.AddButton(Enable, Back);
    }
    
    /// <summary>
    /// Кнока атаки
    /// </summary>
    public void ButtonAttacke()
    {
        Attacke();
    }
    
    /// <summary>
    /// Кнока передвижения
    /// </summary>
    public void ButtonMove()
    {
        state = State.Move;
    }
    
    /// <summary>
    /// Видимост
    /// </summary>
    private void VisibilityMove()
    {
        if(soldier.typeVisisbilityCell == Types.Cell.Visibility.Types.CanNotSee)
        {
            cellAct.actCell.TypeVisibility = Types.Cell.Visibility.Types.Passed;
        }
        else
        {
            cellAct.actCell.TypeVisibility = soldier.typeVisisbilityCell;
        }
        //if (cellAct.actCell.Type == Types.Cell.Types.Building)
        //{
        //    if (((CellBuilding)cellAct.actCell).TypeBuilding == Types.Building.Types.Castle)
        //    {
        //        cellAct.actCell.TypeVisibility = Types.Cell.Visibility.Types.Good;
        //        //cellAct.actCell.GetComponent<SpriteRenderer>().color = Types.People.VisibilityColor.Get(Type.People.Visibility.Good);
        //    }
        //    else
        //    {
        //        cellAct.actCell.TypeVisibility = Types.Cell.Visibility.Types.Medium;
        //        //cellAct.actCell.GetComponent<SpriteRenderer>().color = Type.People.VisibilityColor.Get(Type.People.Visibility.Medium);
        //    }
        //}
        //else
        //{
        //    List<Types.Nature.PointNature> points = ((CellNature)cellAct.actCell).Points;
        //    //bool isCastle = false;

        //    //for (int i = 0; i < points.Length; i++)
        //    //{
        //    //    if (points[i] == Type.PointNature.Castle)
        //    //    { isCastle = true; break; }
        //    //}
        //    if (points.Contains(Types.Nature.PointNature.Castle))
        //    {
        //        cellAct.actCell.TypeVisibility = Types.Cell.Visibility.Types.Medium;
        //        //cellAct.actCell.GetComponent<SpriteRenderer>().color = Type.People.VisibilityColor.Get(Type.People.Visibility.Medium);
        //    }
        //    else
        //    {
        //        List<Cell> cells = SearchCellsAll(1, cellAct.actCell);
        //        bool isRangeCastle = false;
        //        for (int i = 0; i < cells.Count; i++)
        //        {
        //            if (cells[i].Type == Types.Cell.Types.Building && isRangeCastle)
        //            {
        //                isRangeCastle = true;
        //                break;
        //            }
        //            if (cells[i].TryGetComponent<CellNature>(out CellNature cellNature))
        //            {
        //                if (cellNature.Points.Count > 0)
        //                {
        //                    if (cellNature.Points.Contains(Types.Nature.PointNature.Castle))
        //                    {
        //                        isRangeCastle = true;
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //        if (isRangeCastle) 
        //        {
        //            cellAct.actCell.TypeVisibility = Types.Cell.Visibility.Types.Passed;
        //            //cellAct.actCell.GetComponent<SpriteRenderer>().color = Type.People.VisibilityColor.Get(Type.People.Visibility.Poorly); 
        //        }
        //        else
        //        {
        //            cellAct.actCell.TypeVisibility = Types.Cell.Visibility.Types.Passed;
        //            //cellAct.actCell.GetComponent<SpriteRenderer>().color = Type.People.VisibilityColor.Get(Type.People.Visibility.Passed);
        //        }
        //    }
        //}
    }
    
    /// <summary>
    /// Передвижение
    /// </summary>
    public void Move(Cell cellMove)
    {
        VisibilityMove();
        soldier.transform.SetParent(cellMove.transform.Find("Soldiers"));
        soldier.transform.localPosition = new Vector3(0, 0.5f, -2);
        soldier.isActNum--;
        soldier.typeVisisbilityCell = cellMove.TypeVisibility;
        cellMove.TypeVisibility = Types.Cell.Visibility.Types.Medium;
        state = State.None;
        Enable();
    }
    
    /// <summary>
    /// Атака
    /// </summary>
    public void Attacke()
    {
        if (cellAct.actCell.TryGetComponent<CellBuilding>(out CellBuilding cellBulding))
        {
            if(cellBulding.claimUnit != soldier.claimUnit)
            {
                cellAct.actCell.TypeState = Types.Cell.State.Disable;
                cellBulding.Destroy();
                soldier.typeVisisbilityCell = Types.Cell.Visibility.Types.CanNotSee;
                soldier.isActNum--;
            }
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
                            if (cells.Contains(cell.maps.mapCells[cell.posY + y, (cell.posX + x) - rangeX]) == false)
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
                            if (cells.Contains(cell.maps.mapCells[cell.posY + y, (cell.posX - x) + rangeX]) == false)
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
                            if (cells.Contains(cell.maps.mapCells[cell.posY - y, (cell.posX + x) - rangeX]) == false)
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
