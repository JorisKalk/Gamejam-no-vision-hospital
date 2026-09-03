using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public void TriggerSleepingAnimation()
    {
        anim.SetTrigger("ZZZ");
    }
}
