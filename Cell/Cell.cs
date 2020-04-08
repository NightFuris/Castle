using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{

    /// <summary>
    /// Позиция на которой находится
    /// </summary>
    public int posY = 0, posX = 0;
    [HideInInspector] public MechanicsMap maps; 
    protected SpriteRenderer material;

    [HideInInspector] public Types.Cell.Types Type 
    { 
        get 
        { 
            return type; 
        }
        set
        {
            if(value == Types.Cell.Types.Building)
            {
                CellBuilding cellBuilding = gameObject.AddComponent<CellBuilding>();
                CellNature cellNature = GetComponent<CellNature>();
                cellBuilding.posX = cellNature.posX;
                cellBuilding.posY = cellNature.posY;
                cellBuilding.maps = cellNature.maps;
                Destroy(GetComponent<CellNature>());
                maps.mapCells[cellBuilding.posY, cellBuilding.posX] = cellBuilding;
            }
            else
            {
                CellNature cellNature = gameObject.AddComponent<CellNature>();
                CellBuilding cellBuilding = GetComponent<CellBuilding>();
                cellNature.posX = cellBuilding.posX;
                cellNature.posY = cellBuilding.posY;
                cellNature.maps = cellBuilding.maps;
                cellNature.TypeNature = Types.Nature.Types.Meadow;
                cellNature.AddPoints();
                Destroy(GetComponent<CellBuilding>());
                maps.mapCells[cellNature.posY, cellNature.posX] = cellNature;
            }
            type = value;
        }
    }
    [SerializeField] protected Types.Cell.Types type = Types.Cell.Types.Nature;

    public Types.Cell.Visibility.Types TypeVisibility
    {
        get
        {
            return typeVisibility;
        }
        set
        {
            material.color = Types.Cell.Visibility.GetColor(value);
            typeVisibility = value;
        }
    }
    private Types.Cell.Visibility.Types typeVisibility = Types.Cell.Visibility.Types.CanNotSee;

    public Types.Cell.State TypeState 
    { 
        get 
        { 
            return typeState; 
        }
        set
        {
            if(value == Types.Cell.State.Disable)
            {
                material.material.SetFloat("_Outline", 0);
                transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            else
            {
                material.material.SetFloat("_Outline", 1);
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
            }
            typeState = value;
        }
    }
    private Types.Cell.State typeState = Types.Cell.State.Disable;

    private void Awake()
    {
        material = GetComponent<SpriteRenderer>();
    }
}
