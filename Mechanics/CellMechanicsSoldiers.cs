using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellMechanicsSoldiers : MonoBehaviour
{
    public SoldiersType[] soldiersTypes;
    [SerializeField] private GameObject prefabSoldier;

    public void Create(Cell cell, SoldiersType soldiersType, Unit unit)
    {
        GameObject obj = Instantiate(prefabSoldier, cell.transform.Find("Soldiers"));
        obj.GetComponent<SpriteRenderer>().sprite = soldiersType.spriteSoldier;

        Soldiers soldiers = obj.AddComponent<Soldiers>();
        soldiers.Type = soldiersType.Soldier;
        soldiers.num = soldiersType.Num;
        soldiers.spriteSoldier = soldiersType.spriteSoldier;
        soldiers.rangeMoveMax = soldiersType.RangeMoveMax;
        soldiers.icon = soldiersType.icon;
        soldiers.typeVisisbilityCell = cell.TypeVisibility;
        soldiers.claimUnit = unit.gameObject;
        unit.GetComponent<SwitchButton>().Clear();
        unit.GetComponent<SoldiersMechanics>().Enable();
    }
    public void BuyCreate(Unit unit, SoldiersType soldiersType, Cell point)
    {
        if (IsBuyCreate(unit, soldiersType) == true)
        {
            Buy(unit, soldiersType);
            Create(point, soldiersType, unit);
        }
        else
        {

        }
    }
    private void Buy(Unit unit, SoldiersType soldiersType)
    {
        for (int s = 0; s < soldiersType.Resources.Length; s++)
        {
            for (int h = 0; h < unit.Resources.Length; h++)
            {
                if (unit.Resources[h].type == soldiersType.Resources[s].type)// && unit.Resources[h].num < buildings[id].Resources[s].num)
                {
                    unit.EditResouser(unit.Resources[h].type, unit.Resources[h].num - soldiersType.Resources[s].num);
                }
            }
        }
    }
    public bool IsBuyCreate(Unit unit, SoldiersType soldiersType)
    {
        bool isBuy = true;
        for (int s = 0; s < soldiersType.Resources.Length; s++)
        {
            for (int h = 0; h < unit.Resources.Length; h++)
            {
                if (unit.Resources[h].type == soldiersType.Resources[s].type && unit.Resources[h].num < soldiersType.Resources[s].num)
                {
                    isBuy = false;
                    break;
                }
            }
        }
        return isBuy;
    }
}
