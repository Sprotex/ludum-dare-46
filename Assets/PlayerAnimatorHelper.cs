using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorHelper : MonoBehaviour
{
    public PlayerAttack attack;
    public void Fight(int count)
    {
        attack.Attack(count);
    }

    public void Pickup()
    {
        attack.Pickup();
    }
}
