using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New attack", menuName = "Attack")]
public class Attack : ScriptableObject
{
    new public string name = "Attack name";

    public int damage;

    public Vector2 moveDirection;
}
