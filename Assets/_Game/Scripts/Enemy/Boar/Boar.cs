using System;
using Unity.VisualScripting;
using UnityEngine;

public class Boar:Enemy
{
    public override void Move()
    {
        base.Move();
        Anim.SetBool("isWalk",true);
    }
}
