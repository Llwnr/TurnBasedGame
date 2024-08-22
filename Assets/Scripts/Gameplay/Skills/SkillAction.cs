using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class SkillAction : ScriptableObject
{
    public List<EffectInflictData> SelfStatusEffects, EnemyStatusEffects; // Status effects to apply to self and enemies
    public bool IsAoe = false; // Whether the skill is an area of effect attack
    public Sprite Icon; // Icon for the skill
    [TextArea(3,10)]
    public string Description; // Description of the skill
    public float SkillDmgMultiplier; // Damage multiplier for the skill
    public AnimationClip SkillAnim; // Animation clip for the skill

    // Executes the skill action
    public async Task Execute(CharacterModel skillUser, Func<CharacterModel> targetFinder){
        // Don't execute if the skill user is dead
        if(!skillUser.gameObject.activeSelf) return;

        Vector2 origPos = skillUser.transform.position;
        CharacterModel target = targetFinder.Invoke();
        await Task.Delay(300);

        // Move to the target
        await MoveToTarget(skillUser.transform, target.transform);

        // Play the skill animation
        await PlaySkillAnimation(target.transform.position);

        // Execute the skill based on whether it's AoE or single target
        if(!IsAoe){
            ExecuteSkill(skillUser, target);
            InflictSelfEffects(skillUser);
        }else{
            // AoE skill execution
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

        // Return to the original position
        skillUser.transform.position = origPos;
    }

    // Abstract method to be implemented by specific skill actions
    public abstract void ExecuteSkill(CharacterModel skillUser, CharacterModel target);

    // Moves the skill user towards the target
    async Task MoveToTarget(Transform self, Transform target){
        while(Vector2.Distance(self.position, target.position)>1.5f){
            self.position += (target.position-self.position).normalized*0.4f;
            await Task.Yield();
        }
    }

    // Plays the skill animation
    async Task PlaySkillAnimation(Vector2 position){
        if(AnimationManager.instance == null) return;
        await AnimationManager.instance.PlayAnimation(SkillAnim.name, position);
    }

    // Inflicts status effects on the skill user
    void InflictSelfEffects(CharacterModel skillUser){
        foreach(EffectInflictData statusEffectData in SelfStatusEffects){
            skillUser.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }

    // Inflicts status effects on the target
    protected void InflictEffectsToTarget(CharacterModel target){
        foreach(EffectInflictData statusEffectData in EnemyStatusEffects){
            target.InflictStatusEffect(statusEffectData.StatusEffect, statusEffectData.StacksToApply);
        }
    }
}

// Data for inflicting a status effect
[Serializable]
public class EffectInflictData{
    public StatusEffect StatusEffect; // The status effect to inflict
    public int StacksToApply = 1; // The number of stacks to apply
}