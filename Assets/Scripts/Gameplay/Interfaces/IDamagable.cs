public interface IDamagable
{
    bool DealSkillDamage(float dmgAmt, bool dealLinkedDmg = true);
    void DealStatusEffectDamage(StatusEffect statusEffect, float dmgAmt);
}
