using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : CharacterModel
{
    [SerializeField]protected StatLinkerModel _statLinkerModel;

    public StatLinkerModel GetStatLinkerModel(){
        return _statLinkerModel;
    }
    public override bool DealSkillDamage(float dmgAmt, bool dealLinkedDmg = true)
    {
        bool dmgDealtSuccesfully = base.DealSkillDamage(dmgAmt, dealLinkedDmg);
        if(dealLinkedDmg) _statLinkerModel.DealDmgToAllLinkedPlayers(dmgAmt);
        return dmgDealtSuccesfully;
    }

    public void DealDmgToAllLinkedPlayers(float dmgAmt)
    {
        throw new System.NotImplementedException();
    }

    public CharacterStat GetLinkedStatBuff()
    {
        throw new System.NotImplementedException();
    }
}
