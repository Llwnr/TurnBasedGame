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

    public async Task PlayAnimation(string animState, Vector2 animPos){
        _skillAnimator.Play(animState);
        _skillAnimator.transform.position = animPos;
        while(_skillAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f){
            await Task.Yield();
        }
    }
}
