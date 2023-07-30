using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class Train : SingletonMono<Train>
{
    public TrackPlacement TrackPlacement;
    public TrainMovement TrainMovement;
    public TrainAnimation TrainAnimation;
    public Score Score;
    [SerializeField] Shaker m_CamShake;
    [SerializeField] ShakePreset m_HitShakePreset;
    [SerializeField] ShakePreset m_FallShakePreset;

    public void TrainCrashed()
    {
        // Play train crash
        TrainAnimation.PlayAnimation(TrainAnimation.OFFRAIL_ANIM);
        m_CamShake.Shake(m_HitShakePreset);
        GameOver();
    }

    public void TrainDerailed()
    {
        //Train derailed;
        TrainAnimation.PlayAnimation(TrainAnimation.OFFRAIL_ANIM);
        m_CamShake.Shake(m_HitShakePreset);

        GameOver();
    }

    private void GameOver()
    {
        TrackPlacement.enabled = false;
        TrainMovement.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
    }
}
