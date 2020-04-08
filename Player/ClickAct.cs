using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickAct : MonoBehaviour
{
    private SwitchButton switchButton;
    private CellMechanics mechanics;
    private SoldiersMechanics mechanicsSoldiers;
    private MoveCam moveCam;


    private bool isTimeAct = false;
    public bool isAct = true;
    public float time = 0;
    public Cell actCell = null;
    private void Start()
    {
        moveCam = GetComponent<MoveCam>();
        mechanics = GetComponent<CellMechanics>();
        switchButton = GetComponent<SwitchButton>();
        mechanicsSoldiers = GetComponent<SoldiersMechanics>();
    }

    void Update()
    {
        if (isAct) 
        { 
            if (Input.GetMouseButtonDown(0))
            {
                isTimeAct = true;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                if(time <= 0.2f)
                {
                    Act();
                }
                time = 0;
                isTimeAct = false;
            }
            if (isTimeAct)
            {
                time += Time.deltaTime;
            }
        }
    }
    private void Act()
    {
        RaycastHit2D hit = Physics2D.Raycast(moveCam.cameraPlayer.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if(hit.collider != null)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            Cell cell = hit.collider.GetComponent<Cell>();
            if (mechanicsSoldiers.state == SoldiersMechanics.State.Move && cell != actCell)
            {
                int y = Mathf.Abs(actCell.posY - cell.posY);
                int x;

                if (y <= mechanicsSoldiers.soldier.rangeMoveMax)
                {
                    if (y != 0)
                    {
                        if (actCell.posY % 2 != 0)
                        {
                            if (actCell.posX >= cell.posX)
                            {
                                x = Mathf.Abs(Mathf.Abs(actCell.posX) - Mathf.Abs(cell.posX - 1));
                            }
                            else
                            {
                                x = Mathf.Abs(Mathf.Abs(actCell.posX) - Mathf.Abs(cell.posX));
                            }
                        }
                        else
                        {
                            if (actCell.posX <= cell.posX)
                            {
                                x = Mathf.Abs(Mathf.Abs(actCell.posX) - Mathf.Abs(cell.posX + 1));
                            }
                            else
                            {
                                x = Mathf.Abs(Mathf.Abs(actCell.posX) - Mathf.Abs(cell.posX));
                            }
                        }
                    }
                    else
                    {
                        x = Mathf.Abs(Mathf.Abs(actCell.posX) - Mathf.Abs(cell.posX));
                    }

                    if (x <= mechanicsSoldiers.soldier.rangeMoveMax && x != 0)
                    {
                        mechanicsSoldiers.Move(cell);
                    }
                }
            }
            else
            {
                if (cell.TypeState == Types.Cell.State.Disable)
                {
                    if (actCell != null)
                    {
                        switchButton.Clear();
                        actCell.TypeState = Types.Cell.State.Disable;
                    }
                    actCell = cell;
                    if (switchButton.Type == Types.GUI.SwitchButton.Building)
                    {
                        mechanics.Enable();
                    }
                    else
                    {
                        mechanicsSoldiers.Enable();
                    }
                    cell.TypeState = Types.Cell.State.Enable;
                    //moveCam.isAct = false;
                }
                else
                {
                    DisableAct(cell);
                }
            }
        }
        //if(hit.collider != null)
        //{
        //    if (EventSystem.current.IsPointerOverGameObject())
        //        return;
        //    Cell cell = hit.collider.GetComponent<Cell>();
        //    if(cell.TypeState == Types.Cell.State.Disable)
        //    {
        //        if (actCell != null)
        //        {
        //            switchButton.Clear();
        //            actCell.TypeState = Types.Cell.State.Disable;
        //        }
        //        actCell = cell;
        //        if(switchButton.Type == Types.GUI.SwitchButton.Building)
        //        {
        //            mechanics.Enable();
        //        }
        //        else
        //        {
        //            mechanicsSoldiers.Enable();
        //        } 
        //        cell.TypeState = Types.Cell.State.Enable;
        //        moveCam.isAct = false;
        //    }
        //    else
        //    {
        //        DisableAct(cell);
        //    }
        //}
    }
    public void DisableAct(Cell cell)
    {
        //moveCam.isAct = true;
        actCell = null;
        switchButton.Clear();
        cell.TypeState = Types.Cell.State.Disable;
    }
}
