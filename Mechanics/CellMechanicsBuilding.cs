using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMechanicsBuilding : MonoBehaviour
{
    public Building[] buildings = null; // ТИП ЗДАНИЙ

    public void Create(Cell cell, Types.Building.Types building, GameObject unit)
    {
        int id = 0;
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].TypeBuilding == building)
            { id = i; break; }
        }
        cell.transform.Find("Build").GetComponent<SpriteRenderer>().sprite = buildings[id].icon;
        cell.Type = Types.Cell.Types.Building;

        CellBuilding typeBuilding = cell.GetComponent<CellBuilding>();
        typeBuilding.TypeBuilding = buildings[id].TypeBuilding;
        typeBuilding.ResourcesGive = buildings[id].ResourcesGives;
        typeBuilding.claimUnit = unit;
    }
    private void Buy(Unit unit, Types.Building.Types building)
    {
        int id = 0;
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].TypeBuilding == building)
            { id = i; break; }
        }
        for (int s = 0; s < buildings[id].Resources.Length; s++)
        {
            for (int h = 0; h < unit.Resources.Length; h++)
            {
                if (unit.Resources[h].type == buildings[id].Resources[s].type)// && unit.Resources[h].num < buildings[id].Resources[s].num)
                {
                    unit.EditResouser(unit.Resources[h].type, unit.Resources[h].num - buildings[id].Resources[s].num);
                }
            }
        }
    }
    public void BuyCreate(Unit unit, Types.Building.Types building, Cell point)
    {
        if(IsBuyCreate(unit, building) == true)
        {
            Buy(unit, building);
            Create(point, building, unit.gameObject);
        }
    }
    public bool IsBuyCreate(Unit unit, Types.Building.Types building)
    {
        bool isBuy = true;
        int id = 0;
        for (int i = 0; i < buildings.Length; i++)
        {
            if (buildings[i].TypeBuilding == building)
            { id = i; break; }
        }
        for (int s = 0; s < buildings[id].Resources.Length; s++)
        {
            for (int h = 0; h < unit.Resources.Length; h++)
            {
                if (unit.Resources[h].type == buildings[id].Resources[s].type && unit.Resources[h].num < buildings[id].Resources[s].num)
                {
                    isBuy = false;
                    break;
                }
            }
        }
        return isBuy;
    }
    ///// <summary>
    ///// Bot
    ///// </summary>
    ///// <param name="unit"></param>
    ///// <param name="building"></param>
    ///// <param name="point"></param>
    //public void BuyCreate(Bot unit, Type.Building building, Cell point)
    //{
    //    if (IsBuyCreate(unit, building) == true)
    //    {
    //        Create(point, building, unit.gameObject);
    //    }
    //}
    //public bool IsBuyCreate(Bot unit, Type.Building building)
    //{
    //    bool isBuy = true;
    //    int id = 0;
    //    for (int i = 0; i < buildings.Length; i++)
    //    {
    //        if (buildings[i].TypeBuilding == building)
    //        { id = i; break; }
    //    }
    //    for (int s = 0; s < buildings[id].Resources.Length; s++)
    //    {
    //        for (int h = 0; h < unit.Resources.Length; h++)
    //        {
    //            if (unit.Resources[h].type == buildings[id].Resources[s].type && unit.Resources[h].num < buildings[id].Resources[s].num)
    //            {
    //                isBuy = false;
    //                break;
    //            }
    //        }
    //    }
    //    return isBuy;
    //}
}
