using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SoldiersType", menuName = "Cell/SoldiersType")]
public class SoldiersType : ScriptableObject
{
    public Sprite icon;
    public Sprite spriteSoldier;
    [HideInInspector] public int RangeMoveMax { get { return rangeMoveMax; } }
    [SerializeField] private int rangeMoveMax = 0;
    [HideInInspector] public string Name { get { return NameSoldier; } } 
    [SerializeField] private string NameSoldier = "";
    [HideInInspector] public string Info { get { return InfoSoldier; } }
    [SerializeField] [TextArea] private string InfoSoldier = "";
    public int Num { get { return num; } }
    [SerializeField] private int num = 0;
    public Types.Soldier.Types Soldier { get { return soldiers; } }
    [SerializeField] private Types.Soldier.Types soldiers = Types.Soldier.Types.None;
    [HideInInspector] public Types.Resources[] Resources { get { return resources; } }
    [Header("BUY")]
    [SerializeField] private Types.Resources.Types[] typesResources = null;
    [SerializeField] private int[] numsResources = null;
    [SerializeField] private Types.Resources[] resources = null;
    private void UpdateResourser(ref Types.Resources.Types[] types, ref int[] nums, ref Types.Resources[] res)
    {
        if (types.Length > nums.Length || types.Length < nums.Length)
        {
            int[] test = new int[types.Length];
            for (int i = 0; i < test.Length; i++)
            {
                if (i < nums.Length)
                    test[i] = nums[i];
                else
                    test[i] = 0;
            }
            nums = test;
        }
        res = new Types.Resources[types.Length];
        for (int i = 0; i < types.Length; i++)
        {
            res[i] = new Types.Resources(types[i], nums[i]);
        }
    }
    void Awake()
    {
        resources = new Types.Resources[typesResources.Length];
        for (int i = 0; i < typesResources.Length; i++)
        {
            resources[i] = new Types.Resources(typesResources[i], numsResources[i]);
        }
    }
    private void OnValidate()
    {
        UpdateResourser(ref typesResources, ref numsResources, ref resources);
    }
}
