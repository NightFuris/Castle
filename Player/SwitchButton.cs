using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    private CellMechanics mechanicsCell;
    private SoldiersMechanics mechanicsSoldiers;
    public Sprite BackButton;

    public RectTransform pointButton = null; // куда закидывают префабы кнопок
    public GameObject prefabButton = null; // кнопка интерейса каторая используется для постройки здания или взаимодейсвие с зданием

    public Types.GUI.SwitchButton Type
    {
        get
        {
            return type;
        }
        set
        {
            switch (value)
            {
                case Types.GUI.SwitchButton.Building:
                    Clear();
                    mechanicsCell.Enable();
                    break;
                case Types.GUI.SwitchButton.Soldiers:
                    Clear();
                    mechanicsSoldiers.Enable();
                    break;
            }
            type = value;
        }
    }
    [SerializeField] private Types.GUI.SwitchButton type = Types.GUI.SwitchButton.Building;
    #region Add
    public void AddButton(Action function, Sprite icon)
    {
        GameObject obj = Instantiate(prefabButton, pointButton);
        obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() => function());
        obj.transform.Find("Button").Find("Icon").GetComponent<Image>().sprite = icon;
        obj.name = "" + pointButton.childCount;
    }
    public void AddButton(Action<Unit, SoldiersType, Cell> function, Sprite icon, Unit unit, SoldiersType soldiersType, Cell cell)
    {
        GameObject obj = Instantiate(prefabButton, pointButton);
        obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            function(unit, soldiersType, cell);
        });
        obj.transform.Find("Button").Find("Icon").GetComponent<Image>().sprite = icon;
        obj.name = "" + pointButton.childCount;
    }
    public void AddButton(Action<int> function, Sprite icon, int id)
    {
        GameObject obj = Instantiate(prefabButton, pointButton);
        obj.transform.Find("Button").GetComponent<Button>().onClick.AddListener(() =>
        {
            function(id);
        });
        obj.transform.Find("Button").Find("Icon").GetComponent<Image>().sprite = icon;
        obj.name = "" + pointButton.childCount;
    }
    #endregion
    public void Clear()
    {
        Transform[] objs = pointButton.GetComponentsInChildren<Transform>();
        for (int i = 1; i < objs.Length; i++)
            Destroy(objs[i].gameObject);
    }
    public void Switch()
    {
        if (Type == Types.GUI.SwitchButton.Soldiers)
        {
            Type = Types.GUI.SwitchButton.Building;
        }
        else
        {
            Type = Types.GUI.SwitchButton.Soldiers;
        }
    }
    private void Start()
    {
        mechanicsCell = GetComponent<CellMechanics>();
        mechanicsSoldiers = GetComponent<SoldiersMechanics>();
    }
}
