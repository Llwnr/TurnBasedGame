using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SkillAction : ScriptableObject
{
    public List<EffectInflictData> SelfStatusEffects, EnemyStatusEffects;
    public bool IsAoe = false;
    public Sprite Icon;
    [TextArea(3,10)]
    public string Description;
    public float SkillDmgMultiplier;
    public AnimationClip SkillAnim;
    public async Task Execute(CharacterModel skillUser, Func<CharacterModel> targetFinder){
        //DONT EXECUTE SKILL IF THE SKILL USER IS DEAD, cuz dead ppl cant attack
        if(!skillUser.gameObject.activeSelf) return;
        Vector2 origPos = skillUser.transform.position;
        CharacterModel target = targetFinder.Invoke();
        await Task.Delay(200);
        //Move to target
        await MoveToTarget(skillUser.transform, target.transform);
        //Start animation
        await AnimationManager.instance.PlayAnimation(SkillAnim.name, target.transform.position);
        if(!IsAoe){
            ExecuteSkill(skillUser, target);
            InflictSelfEffects(skillUser);
        }else{
            //THIS SKILL IS AOE SO:
            if(target is PlayerModel){
                foreach(CharacterModel currTarget in TargetManager.GetAllPlayerModels()){
                    ExecuteSkill(skillUser, currTarget);
                }
            }else{
                foreach(CharacterModel currTarget in TargetManager.GetAllEnemyModels()){
                    ExecuteSkill(skillUser, currTarget);
                }
            }
        }
        skillUser.transform.position = origPos;
        return;
    }
    public abstract void ExecuteSkill(CharacterModel skillUser, CharacterModel target);

    async Task MoveToTarget(Transform self, Transform target){
        while(Vector2.Distance(self.position, target.position)>1.5f){
            self.position += (target.position-self.position).normalized*0.4f;
            await Task.Yield();
        }
    }

    //This will apply to the user
    void InflictSelfEffects(CharacterModel skillUser){
        foreach(EffectInflictData statusEffectData in SelfStatusEffects){
            skillUser.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }
    //This us usually used to apply status effects to the target.. mostly when it is hit by this skill
    protected void InflictEffectsToTarget(CharacterModel target){
        foreach(EffectInflictData statusEffectData in EnemyStatusEffects){
            target.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }
}

[Serializable]
//Data for inflicting a status effect when the skill hits
public class EffectInflictData{
    public StatusEffect StatusEffect;
    public int StacksToApply = 1;
}
