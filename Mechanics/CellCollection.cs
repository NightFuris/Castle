using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollection : MonoBehaviour
{
    private MechanicsMap mechanicsMap = null;
    [SerializeField] private Unit unit = null;
    private void Start()
    {
        mechanicsMap = GetComponent<MechanicsMap>();
    }
    public void Collection()
    {
        for(int y = 0; y < mechanicsMap.maxY; y++)
        {
            for (int x = 0; x < mechanicsMap.maxX; x++)
            {
                if(mechanicsMap.mapCells[y,x].Type == Types.Cell.Types.Building)
                {
                    CellBuilding building = (CellBuilding)mechanicsMap.mapCells[y, x];
                    for(int i = 0; i < building.ResourcesGive.Length; i++)
                    {
                        for(int h = 0; h < unit.Resources.Length; h++)
                        {
                            if(building.ResourcesGive[i].Name == unit.Resources[h].Name)
                            {
                                unit.EditResouser(building.ResourcesGive[i].type, unit.Resources[h].num + building.ResourcesGive[i].num);
                            }
                        }
                    }
                }
            }
        }
    }
}
