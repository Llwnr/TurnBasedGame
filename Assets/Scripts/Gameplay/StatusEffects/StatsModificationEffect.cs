using UnityEngine;

//MAIN PURPOSE OF THESE EFFECTS IS TO MODIFY STATS. THEY WON'T BE USED AND THEIR STACKS DECREASE PER TURN.
public abstract class StatsModificationEffect : StatusEffect
{
    public abstract float GetStatModificationData(StatModifierType modType);//Returns a percentage value

    public override void Execute(CharacterModel targetModel, int stacks)
    {
        //Do nothing duh!! because this class of status effect will only modify the stats
    }
}
public enum StatModifierType{
    Attack,
    Defense,
    Speed
}