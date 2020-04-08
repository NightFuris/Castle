using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ClickAct))]
[RequireComponent(typeof(Unit))]
public class CellMechanics : MonoBehaviour
{
    public CellMechanicsBuilding cellMechanicsBuilding;

    //[SerializeField] private GameObject GUIDialogWindow = null;
    [Space]
    [SerializeField] private MoveCam cameraMove = null;
    [Space]
    [SerializeField] private GameObject CreateUI = null;

    private SwitchButton switchButton;
    private ClickAct clickAct;
    private Unit unit;
    
    /// <summary>
    /// ВКЛ Скрипта
    /// </summary>
    public void Enable()
    {
        switchButton.Clear();
        if (clickAct.actCell == null)
            return;
        Cell cell = clickAct.actCell;
        if(cell.Type == Types.Cell.Types.Nature)
        {
            Types.Nature.Types natureType = clickAct.actCell.GetComponent<CellNature>().TypeNature;
            List<Types.Nature.PointNature> pointNatures = clickAct.actCell.GetComponent<CellNature>().Points;
            int isPoint = 0;
            for (int i = 0; i < cellMechanicsBuilding.buildings.Length; i++)
            {
                Building building = cellMechanicsBuilding.buildings[i];
                isPoint = building.PointNatures.Length;
                // Проверка на возможно строить ли на этой земле здание
                for(int p = 0; p < building.WhatLandPut.Length; p++)
                {
                    if(building.PointNatures.Length != 0)
                    {
                        for (int l = 0; l < building.PointNatures.Length; l++)
                        {
                            for (int k = 0; k < pointNatures.Count; k++)
                            {
                                if (building.PointNatures[l] == pointNatures[k])
                                    isPoint--;
                            }
                        }
                    }
                    if (building.NoPointNatures != null)
                    {
                        if (building.NoPointNatures.Length != 0)
                        {
                            for (int l = 0; l < building.NoPointNatures.Length; l++)
                            {
                                for (int k = 0; k < pointNatures.Count; k++)
                                {
                                    if (building.NoPointNatures[l] == pointNatures[k])
                                        isPoint++;
                                }
                            }
                        }
                    }
                    if(natureType == building.WhatLandPut[p] && isPoint == 0)
                    {
                        //GameObject obj = Instantiate(switchButton.prefabButton, switchButton.pointButton.transform);
                        if (cellMechanicsBuilding.IsBuyCreate(unit, building.TypeBuilding))
                        {
                            switchButton.AddButton(WindowBuyBuilding, building.icon, i);
                            //obj.transform.Find("Button").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => WindowBuyBuilding(obj));
                        }
                        //else
                        //{
                        //    switchButton.AddButton(WindowNoBuy, building.icon, i);
                        //    //obj.transform.Find("Button").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => WindowNoBuy(obj));
                        //}
                        //obj.transform.Find("Button").Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = building.icon;
                        break;
                    }
                }

            }
        }
    }
    
    public void WindowBuyBuilding(int objId)
    {
        cameraMove.isAct = false;
        clickAct.isAct = false;
        CreateUI.SetActive(true);
        int id = objId;

        UICreateText uICreate = new UICreateText(CreateUI);
        uICreate.Name.text = cellMechanicsBuilding.buildings[id].nameBuild;
        uICreate.Info.text = cellMechanicsBuilding.buildings[id].infoBuild;
        uICreate.Button.onClick.RemoveAllListeners();
        uICreate.Button.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Построить";
        uICreate.Button.onClick.AddListener(() => BuyBuild(cellMechanicsBuilding.buildings[id].TypeBuilding));
    }
    
    public void CloseWindow()
    {
        clickAct.actCell.TypeState = Types.Cell.State.Disable;
        CreateUI.SetActive(false);
        clickAct.isAct = true;
        cameraMove.isAct = true;
        switchButton.Clear();
    }
    
    public void BuyBuild(Types.Building.Types building)
    {
        cellMechanicsBuilding.BuyCreate(unit, building, clickAct.actCell);
        CloseWindow();
    }
    
    public void WindowNoBuy(int objId)
    {
        cameraMove.isAct = false;
        clickAct.isAct = false;
        Debug.LogError("123");
        //GUIDialogWindow.SetActive(true);
        Building building = cellMechanicsBuilding.buildings[objId];
    }
    
    private void Start()
    {
        switchButton = GetComponent<SwitchButton>();
        clickAct = GetComponent<ClickAct>();
        unit = GetComponent<Unit>();
    }
    /*
    [SerializeField] MoveCam cam = null;

    [Space]
    //[SerializeField] private Building[] buildings = null; // ТИП ЗДАНИЙ
    [SerializeField] private Actions[] actions = null; // ТИП ДЕЙСТВИЙ СО ЗДАНИЕМ
    [SerializeField] private SoldiersType[] soldiersTypes = null;
    [Space]

    [Space]
    [SerializeField] private GameObject GUIDialogWindow = null; 
    [Space]

    //[SerializeField] private GameObject pointUIBuilding = null;
    [SerializeField] private GameObject CreateUI = null;
    private SoldiersMechanics soldiersMechanics;
    private SwitchButton switchButton;
    private CellActPlayer player;
    private Unit unit;
    
    private void Start()
    {
        player = GetComponent<CellActPlayer>();
        unit = GetComponent<Unit>();
        switchButton = GetComponent<SwitchButton>();
        soldiersMechanics = GetComponent<SoldiersMechanics>();
    }
    
    /// <summary>
    /// Диалогое окно покупки здания
    /// </summary>
    /// <param name="obj"></param>
    //public void BuildingCreateBuy(GameObject obj)
    //{
    //    //cam.isAct = false;
    //    //player.isAct = false;
    //    //CreateUI.SetActive(true);
    //    //int id = System.Convert.ToInt32(obj.name);
    //    //UICreateText uICreate = new UICreateText(CreateUI);
    //    //uICreate.Name.text = buildings[id].nameBuild;
    //    //uICreate.Info.text = buildings[id].infoBuild;
    //    //uICreate.Button.onClick.RemoveAllListeners();
    //    //uICreate.Button.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Построить";
    //    //uICreate.Button.onClick.AddListener(() => CreateBuilding(id));
    //}
    
    /// <summary>
    /// Закрыть "диалогое окно покупки здания"
    /// </summary>
    public void DisableCreateUI()
    {
        cam.isAct = true;
        player.isAct = true;
        CreateUI.SetActive(false);
    }
    
    /// <summary>
    /// Создания здания
    /// </summary>
    /// <param name="id"></param>
    //public void CreateBuilding(int id)
    //{
    //    Building building = buildings[id];
    //    for (int s = 0; s < building.Resources.Length; s++)
    //    {
    //        for (int h = 0; h < unit.Resources.Length; h++)
    //        {
    //            if (unit.Resources[h].type == building.Resources[s].type)
    //            {
    //                unit.EditResouser(building.Resources[s].type, unit.Resources[h].num - building.Resources[s].num);
    //                break;
    //            }
    //        }
    //    }
    //    player.actCell.transform.Find("Build").GetComponent<SpriteRenderer>().sprite = buildings[id].icon;
    //    player.actCell.gameObject.AddComponent<CellTypeBuilding>();
    //    player.actCell.GetComponent<CellTypeBuilding>().Type = buildings[id].TypeBuilding;
    //    player.actCell.GetComponent<CellTypeBuilding>().Resources = buildings[id].ResourcesGives;
    //    player.actCell.EditTypeCell(Type.Cell.Building);
    //    player.actCell.EditState(Type.People.State.Disable);
    //    DisableCreateUI();
    //    switchButton.Clear();
    //}
    
    /// <summary>
    /// ВКЛ Скрипта
    /// </summary>
    public void Enable()
    {
        switchButton.Clear();
        if (player.actCell == null)
            return;
        Type.Cell cell = player.actCell.GetComponent<CellTypeBuilding>() != null ? Type.Cell.Building : Type.Cell.Nature;
        if (cell == Type.Cell.Nature)
        {
            Type.Nature natureType = player.actCell.GetComponent<CellTypeNature>().Type;
            Type.PointNature[] pointNatures = player.actCell.GetComponent<CellTypeNature>().Points;
            int isPoint = 0;
            for (int i = 0; i < buildings.Length; i++)
            {
                isPoint = buildings[i].PointNatures.Length;
                for (int a = 0; a < buildings[i].WhatLandPut.Length; a++)
                {
                    if(buildings[i].PointNatures.Length != 0)
                    {
                        for (int l = 0; l < buildings[i].PointNatures.Length; l++)
                        {
                            for (int k = 0; k < pointNatures.Length; k++)
                            {
                                if (buildings[i].PointNatures[l] == pointNatures[k])
                                    isPoint--;
                            }
                        }
                    }
                    if(buildings[i].NoPointNatures.Length != 0)
                    {
                        for (int l = 0; l < buildings[i].NoPointNatures.Length; l++)
                        {
                            for (int k = 0; k < pointNatures.Length; k++)
                            {
                                if (buildings[i].NoPointNatures[l] == pointNatures[k])
                                    isPoint++;
                            }
                        }
                    }
                    if (natureType == buildings[i].WhatLandPut[a] && isPoint == 0)
                    {
                        GameObject obj = Instantiate(switchButton.prefabButton, switchButton.pointButton.transform);
                        obj.name = "" + i;
                        bool isBuy = true;
                        for (int s = 0; s < buildings[i].Resources.Length; s++)
                        {
                            for (int h = 0; h < unit.Resources.Length; h++)
                            {
                                if (unit.Resources[h].type == buildings[i].Resources[s].type && unit.Resources[h].num < buildings[i].Resources[s].num)
                                {
                                    isBuy = false;
                                    break;
                                }
                            }
                        }
                        if (isBuy)
                        {
                            obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Building(obj));
                        }
                        else
                        {
                            obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => BuildingCreateBuyNo(obj));
                        }
                        obj.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = buildings[i].icon;
                        break;
                    }

                }
            }
        }
        else
        {
            Type.Building buildingType = player.actCell.GetComponent<CellTypeBuilding>().Type;
            for (int i = 0; i < actions.Length; i++)
            {
                for (int a = 0; a < actions[i].Building.Length; a++)
                {
                    if (buildingType == actions[i].Building[a])
                    {
                        GameObject obj = Instantiate(switchButton.prefabButton, switchButton.pointButton.transform);
                        obj.name = "" + i;
                        obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => FunctionAction(obj));
                        obj.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = actions[i].icon;
                    }
                }
            }
        }
    }
    
    /// <summary>
    /// Не хватате каторого ресурса
    /// </summary>
    /// <param name="obj"></param>
    //private void BuildingCreateBuyNo(GameObject obj)
    //{
    //    GUIDialogWindow.SetActive(true);
    //    Building building = buildings[System.Convert.ToInt32(obj.name)];
    //    UIDialog uIDialog = new UIDialog(GUIDialogWindow);
    //    string textResourseNoBuy = "";
    //    for (int s = 0; s < building.Resources.Length; s++)
    //    {
    //        for (int h = 0; h < unit.Resources.Length; h++)
    //        {
    //            if (unit.Resources[h].type == building.Resources[s].type && unit.Resources[h].num < building.Resources[s].num)
    //            {
    //                textResourseNoBuy += $"{building.Resources[s].Name} - {building.Resources[s].num - unit.Resources[s].num}\n";
    //                break;
    //            }
    //        }
    //    }
    //    uIDialog.Name.text = "Не хватает ресурсов";
    //    uIDialog.Info.text = "Для постройки данного здание не хватает: \n" +
    //                         $"{textResourseNoBuy}";
    //    player.actCell.EditState(Type.People.State.Disable);
    //}
    
    /// <summary>
    /// Действия со зданием
    /// </summary>
    /// <param name="obj"></param>
    private void FunctionAction(GameObject obj)
    {
        switch (actions[System.Convert.ToInt32(obj.name)].Action)
        {
            case Type.People.Action.Info:
                GUIDialogWindow.SetActive(true);
                UIDialog uIDialog = new UIDialog(GUIDialogWindow);
                for (int i = 0; i < buildings.Length; i++)
                {
                    if(buildings[i].TypeBuilding == player.actCell.GetComponent<CellTypeBuilding>().Type)
                    {
                        uIDialog.Name.text = buildings[i].nameBuild;
                        uIDialog.Info.text = buildings[i].infoBuild;
                    }
                }
                break;
            case Type.People.Action.CreateSoldier:
                GUISoldierCreate();
                break;
        }
    }
    
    public void SoldierCreateBuy(GameObject obj)
    {
        cam.isAct = false;
        player.isAct = false;
        CreateUI.SetActive(true);
        int id = System.Convert.ToInt32(obj.name);
        UICreateText uICreate = new UICreateText(CreateUI);
        uICreate.Name.text = soldiersTypes[id].Name;
        uICreate.Info.text = soldiersTypes[id].Info;
        uICreate.Button.onClick.RemoveAllListeners();
        uICreate.Button.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "Нанять";
        uICreate.Button.onClick.AddListener(() => SoldierCreate(id));
    }
    
    public void SoldierCreateBuyNo(GameObject obj)
    {
        GUIDialogWindow.SetActive(true);
        SoldiersType soldier = soldiersTypes[System.Convert.ToInt32(obj.name)];
        UIDialog uIDialog = new UIDialog(GUIDialogWindow);
        string textResourseNoBuy = "";
        for (int s = 0; s < soldier.Resources.Length; s++)
        {
            for (int h = 0; h < unit.Resources.Length; h++)
            {
                if (unit.Resources[h].type == soldier.Resources[s].type && unit.Resources[h].num < soldier.Resources[s].num)
                {
                    textResourseNoBuy += $"{soldier.Resources[s].Name} - {soldier.Resources[s].num - unit.Resources[s].num}\n";
                    break;
                }
            }
        }
        uIDialog.Name.text = "Не хватает ресурсов";
        uIDialog.Info.text = "Для найма данного солдата не хватает: \n" +
                             $"{textResourseNoBuy}";
        player.actCell.EditState(Type.People.State.Disable);
    }
    
    public void SoldierCreate(int id)
    {
        SoldiersType soldiers = soldiersTypes[id];
        for (int s = 0; s < soldiers.Resources.Length; s++)
        {
            for (int h = 0; h < unit.Resources.Length; h++)
            {
                if (unit.Resources[h].type == soldiers.Resources[s].type)
                {
                    unit.EditResouser(soldiers.Resources[s].type, unit.Resources[h].num - soldiers.Resources[s].num);
                    break;
                }
            }
        }
        soldiersMechanics.Create(soldiersTypes[id], player.actCell);
        player.isAct = true;
        CreateUI.SetActive(false);
    }
    
    private void GUISoldierCreate()
    {
        switchButton.Clear();
        for (int i = 0; i< soldiersTypes.Length;i++)
        {
            GameObject obj = Instantiate(switchButton.prefabButton, switchButton.pointButton.transform);
            obj.name = "" + i;
            obj.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = soldiersTypes[i].icon;
            bool isBuy = true;
            for (int s = 0; s < soldiersTypes[i].Resources.Length; s++)
            {
                for (int h = 0; h < unit.Resources.Length; h++)
                {
                    if (unit.Resources[h].type == soldiersTypes[i].Resources[s].type && unit.Resources[h].num < soldiersTypes[i].Resources[s].num)
                    {
                        isBuy = false;
                        break;
                    }
                }
            }
            if(isBuy)
                obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SoldierCreateBuy(obj));
            else
                obj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => SoldierCreateBuyNo(obj));
        }
        GameObject objBack = Instantiate(switchButton.prefabButton, switchButton.pointButton.transform);
        objBack.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => Enable());
        objBack.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = switchButton.BackButton;
    }*/
}
public class UIElemnt
{
    public UnityEngine.UI.Text Name { get; set; }
    public UnityEngine.UI.Text Info { get; set; }
    public UnityEngine.UI.Button Button { get; set; }
}
public class UICreateText : UIElemnt
{
    public UICreateText(GameObject point)
    {
        Name = point.transform.Find("Image").Find("Name").Find("Text").GetComponent<UnityEngine.UI.Text>();
        Info = point.transform.Find("Image").Find("Info").Find("Text").GetComponent<UnityEngine.UI.Text>();
        Button = point.transform.Find("Image").Find("Buttons").Find("CreateBuildingButton").GetComponent<UnityEngine.UI.Button>();
    }
}
public class UIDialog : UIElemnt
{
    public UIDialog(GameObject point)
    {
        Name = point.transform.Find("Image").Find("Name").Find("Text").GetComponent<UnityEngine.UI.Text>();
        Info = point.transform.Find("Image").Find("Info").Find("Text").GetComponent<UnityEngine.UI.Text>();
    }
}
