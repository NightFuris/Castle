using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Types.Resources[] resources = null;
    [Header("CLAIM")]
    public int claim = 0; 
    [HideInInspector] public Types.Resources[] Resources 
    {
        set
        {
            resources = value;
        }
        get
        {
            return resources;
        }
    }
    [Header("RESOURCES")]
    [SerializeField] private Types.Resources.Types[] typesResources = null;
    [SerializeField] private int[] numsResources = null;
    [SerializeField] private Sprite[] iconsResources = null;
    [Header("RESOURCES GUI")]
    [SerializeField] private Transform GUIResources = null;
    [SerializeField] private GameObject PrefabsGUIResoures = null;

    /// <summary>
    /// Изменение Интерфейса ресурс с обновление их
    /// </summary>
    /// <param name="resType"></param>
    /// <param name="num"></param>
    public void EditResouser(Types.Resources.Types resType, int num)
    {
        for (int h = 0; h < Resources.Length; h++)
        {
            if (Resources[h].type == resType)
            {
                Resources[h].num = num;
                UpdateResouserGUI(Resources[h]);
                break;
            }
        }
    }

    /// <summary>
    /// Обновление интерфейса ресурс
    /// </summary>
    /// <param name="resources"></param>    
    public void UpdateResouserGUI(Types.Resources resources)
    {
        Transform[] transform = GUIResources.GetComponentsInChildren<Transform>();
        for(int i = 1; i < transform.Length; i++)
        {
            if(transform[i].name == resources.Name)
            {
                transform[i].Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + resources.num;
                break;
            }
        }
    }

    /// <summary>
    /// Создание интерфейса ресурс
    /// </summary>
    /// <param name="resources"></param>
    private void CreateResouserGUI(Types.Resources resources)
    {
        GameObject obj = Instantiate(PrefabsGUIResoures, GUIResources);
        obj.name = resources.Name;
        obj.transform.Find("Text").GetComponent<UnityEngine.UI.Text>().text = "" + resources.num;
        obj.transform.Find("Icon").GetComponent<UnityEngine.UI.Image>().sprite = resources.icon;
    }
    
    private void Start()
    {
        resources = new Types.Resources[typesResources.Length];
        for (int i = 0; i < typesResources.Length; i++)
            resources[i] = new Types.Resources(typesResources[i], numsResources[i], iconsResources[i]);

        for (int i = 0; i < resources.Length; i++)
            CreateResouserGUI(resources[i]);
    }
    
    private void OnValidate()
    {
        if (typesResources.Length > numsResources.Length || typesResources.Length < numsResources.Length)
        {
            int[] test = new int[typesResources.Length];
            for (int i = 0; i < test.Length; i++)
            {
                if (i < numsResources.Length)
                    test[i] = numsResources[i];
                else
                    test[i] = 0;
            }
            numsResources = test;
        }
        if (typesResources.Length > iconsResources.Length || typesResources.Length < iconsResources.Length)
        {
            Sprite[] test = new Sprite[typesResources.Length];
            for (int i = 0; i < test.Length; i++)
            {
                if (i < iconsResources.Length)
                    test[i] = iconsResources[i];
                else
                    test[i] = null;
            }
            iconsResources = test;
        }
    }
    //public int money = 0;     // Деньги
    ////public int wheat = 0;   // Пщеница
    //public int bread = 0;     // Хлеб   
    //public int tree = 0;      // Дерево
    //public int stone = 0;     // Камень
    ////public int iron = 0;    // Железо
}
