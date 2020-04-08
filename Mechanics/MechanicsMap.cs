using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsMap : MonoBehaviour
{
    public Sprite Nature;
    public GameObject[] prefabsCell = null;
    /// <summary>
    /// Сначала y, потом x
    /// </summary>
    public Cell[,] mapCells = null;
    public int maxX = 5, maxY = 5;
    [SerializeField] private Transform point = null;
    private void Generation()
    {
        float addX = 1.5f;//1.55f;
        float addY = 0;
        float addXY = 0;
        mapCells = new Cell[maxY, maxX];
        
        Cell obj;
        for (int y = 0; y < maxY; y++)
        {
            for (int x = 0; x < maxX; x++)
            {
                obj = mapCells[y, x] = Instantiate(prefabsCell[Random.Range(0, prefabsCell.Length)]).GetComponent<Cell>();
                obj.transform.position = addXY == 0 ? new Vector2(point.position.x + (addX * x), point.position.y + addY) : new Vector2(point.position.x + ((addX * x) + addXY), point.position.y + addY);
                obj.maps = this;
                obj.posX = x;
                obj.posY = y;
                obj.transform.SetParent(point);
            }
            addXY = addXY == 0 ? 0.75f/* 0.78f*/ : 0;
            addY += 1.14f;// 1.09f;
        }
    }
    private void Awake()
    {
        Generation();
    }
}
