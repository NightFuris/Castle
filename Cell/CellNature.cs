using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellNature : Cell
{
    public Types.Nature.Types TypeNature { get { return typeNature; } set { typeNature = value; } }
    [SerializeField] private Types.Nature.Types typeNature = Types.Nature.Types.Forest;

    public List<Types.Nature.PointNature> Points = null;
    private void CheckTypeAndAdd(CellNature nature, Types.Nature.Types natureSearchEdit, Types.Nature.PointNature pointNatureAdd)
    {
        if(nature.TypeNature == natureSearchEdit)
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
    private void Check(Types.Nature.Types nature, Types.Nature.Types natureSearch, Types.Nature.Types natureSearchEdit, Types.Nature.PointNature pointNatureAdd)
    {
        CellNature typeNature;
        if (nature == natureSearch)
        {
            if (posY + 1 < maps.maxY)
            {
                if (posY % 2 != 0 && posX + 1 < maps.maxX)
                {
                    if (maps.mapCells[posY + 1, posX + 1].Type == Types.Cell.Types.Nature)
                    {
                        typeNature = (CellNature)maps.mapCells[posY + 1, posX + 1];
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                else if (posX - 1 >= 0)
                {
                    if (maps.mapCells[posY + 1, posX - 1].Type == Types.Cell.Types.Nature)
                    {
                        typeNature = (CellNature)maps.mapCells[posY + 1, posX - 1];
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                if (maps.mapCells[posY + 1, posX].Type == Types.Cell.Types.Nature)
                {
                    typeNature = (CellNature)maps.mapCells[posY + 1, posX];
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
            if (posY - 1 >= 0)
            {
                if (posY % 2 != 0 && posX + 1 < maps.maxX)
                {
                    if (maps.mapCells[posY - 1, posX + 1].Type == Types.Cell.Types.Nature)
                    { 
                        typeNature = (CellNature)maps.mapCells[posY - 1, posX + 1];
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                else if (posX - 1 >= 0)
                {
                    if (maps.mapCells[posY - 1, posX - 1].Type == Types.Cell.Types.Nature)
                    {
                        typeNature = (CellNature)maps.mapCells[posY - 1, posX - 1];
                        CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                    }
                }
                if (maps.mapCells[posY - 1, posX].Type == Types.Cell.Types.Nature)
                {
                    typeNature = (CellNature)maps.mapCells[posY - 1, posX];
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
            if (posX + 1 < maps.maxX)
            {
                if (maps.mapCells[posY, posX + 1].Type == Types.Cell.Types.Nature)
                {
                    typeNature = (CellNature)maps.mapCells[posY, posX + 1];
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
            if (posX - 1 >= 0)
            {
                if (maps.mapCells[posY, posX - 1].Type == Types.Cell.Types.Nature)
                {
                    typeNature = (CellNature)maps.mapCells[posY, posX - 1];
                    CheckTypeAndAdd(typeNature, natureSearchEdit, pointNatureAdd);
                }
            }
        }
    }

    private void OnValidate()
    {
        type = Types.Cell.Types.Nature;
    }
    private void Start()
    {
        type = Types.Cell.Types.Nature;
        TypeVisibility = Types.Cell.Visibility.Types.CanNotSee;
        AddPoints();
    }
    public void AddPoints()
    {
        Check(typeNature, Types.Nature.Types.Forest, Types.Nature.Types.Meadow, Types.Nature.PointNature.Forest);
        Check(typeNature, Types.Nature.Types.Mountains, Types.Nature.Types.Meadow, Types.Nature.PointNature.Mine);
    }
}
