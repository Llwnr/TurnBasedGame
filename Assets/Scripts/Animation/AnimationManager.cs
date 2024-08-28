using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager instance;
    private void Awake() {
        if(instance == null) {
            instance = this;
        }else{
            Debug.LogError("More than one animation manager");
        }
    }

    [SerializeField]private Animator _skillAnimator;

    public async Task PlayAnimation(string animState, Vector2 animPos, float animHitTime){
        _skillAnimator.transform.position = animPos;
        _skillAnimator.Play(animState, -1, 0f);
        
        while(_skillAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < animHitTime){
            await Task.Delay(1);
        }
    }
}
