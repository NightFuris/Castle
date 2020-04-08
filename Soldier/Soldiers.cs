using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldiers : MonoBehaviour
{
    public Types.Soldier.Types Type { get { return type; } set { type = value; } }
    [SerializeField] private Types.Soldier.Types type;

    public int num;

    public Sprite spriteSoldier;
    public Sprite icon;

    public int rangeMoveMax;
    public int isActNum = 3;

    public Types.Cell.Visibility.Types typeVisisbilityCell;

    public GameObject claimUnit;

}
