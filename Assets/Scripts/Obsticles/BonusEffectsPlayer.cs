using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BonusEffectsPlayer : OnBallTouchEffectsPlayer
{
    protected override void DestroySelf()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        base.DestroySelf();
    }
}
