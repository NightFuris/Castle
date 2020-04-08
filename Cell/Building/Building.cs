using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Building", menuName = "Cell/Building")]
public class Building : ScriptableObject
{
    public Sprite icon = null;
    [TextArea] public string nameBuild = null;
    [TextArea] public string infoBuild = null;
    [HideInInspector] public Types.Resources[] ResourcesGives { get { return resourcesGives; } }
    [Header("GIVE")]
    [SerializeField] private Types.Resources.Types[] typesResourcesGives = null;
    [SerializeField] private int[] numsResourcesGives = null;
    [SerializeField] private Types.Resources[] resourcesGives = null; 
    [HideInInspector] public Types.Resources[] Resources { get { return resources; } }
    [Header("BUY")]
    [SerializeField] private Types.Resources.Types[] typesResources = null;
    [SerializeField] private int[] numsResources = null;
    [SerializeField] private Types.Resources[] resources = null;
    [HideInInspector] public Types.Building.Types TypeBuilding { get { return typeBuilding; } }
    [Header("Что за постройка?")]
    [SerializeField] Types.Building.Types typeBuilding = Types.Building.Types.None;
    /// <summary>
    /// На какой земле можно поставить
    /// </summary>
    [HideInInspector] public Types.Nature.Types[] WhatLandPut { get { return whatLandPut; } }
    [Header("На какой земле может построена")]
    [SerializeField] Types.Nature.Types[] whatLandPut = null;
    [HideInInspector] public Types.Nature.PointNature[] PointNatures { get { return pointNatures; } }
    [Header("На каких метках она может построена")]
    [SerializeField] Types.Nature.PointNature[] pointNatures = null;

    [HideInInspector] public Types.Nature.PointNature[] NoPointNatures { get { return noPointNatures; } }
    [Header("При каких условие нельзя строить")]
    [SerializeField] Types.Nature.PointNature[] noPointNatures = null;

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
    private void Awake()
    {
        resources = new Types.Resources[typesResources.Length];
        for (int i = 0; i < typesResources.Length; i++)
        {
            resources[i] = new Types.Resources(typesResources[i], numsResources[i]);
        }
        resourcesGives = new Types.Resources[typesResourcesGives.Length];
        for (int i = 0; i < typesResourcesGives.Length; i++)
        {
            resourcesGives[i] = new Types.Resources(typesResourcesGives[i], numsResourcesGives[i]);
        }
    }
    private void OnValidate()
    {
        UpdateResourser(ref typesResources, ref numsResources, ref resources);
        UpdateResourser(ref typesResourcesGives, ref numsResourcesGives, ref resourcesGives);
    }
}
