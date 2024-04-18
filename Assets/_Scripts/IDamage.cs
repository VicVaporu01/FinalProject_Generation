using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void TakeDamage(float damage, DamageType damageType);
}

public enum DamageType
{
    Physical,
    Magical
}
