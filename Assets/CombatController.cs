using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    float Luck;
    // Start is called before the first frame update
    void Start()
    {
        Luck = 0.2f;
    }

    public void DoCombat(UnitState attacker, UnitState defender)
    {
        float AttackerDamage = CalculateDamage(attacker.AttackPower, attacker.Health);
        float DefenderHealth = defender.Health - AttackerDamage;
        float DefenderDamage = CalculateDamage(defender.AttackPower, DefenderHealth);

        // inflict damage
        attacker.Health -= DefenderDamage;
        defender.Health -= AttackerDamage;
    }

    float CalculateDamage(float APower, float AHealth)
    {
        float LuckMult = 1 + Random.Range(-Luck, Luck);
        float HealthMult = Mathf.Ceil(AHealth) / 10;
        float Damage = APower * (AHealth / 10f) * LuckMult;
        return Damage;
    }
}
