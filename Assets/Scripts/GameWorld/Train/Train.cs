using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : SingletonMono<Train>
{
    public TrackPlacement TrackPlacement;
    public TrainMovement TrainMovement;
    public TrainAnimation TrainAnimation;
    public Score Score;

    public void TrainCrashed()
    {
        // Play train crash
        TrainAnimation.PlayAnimation(TrainAnimation.OFFRAIL_ANIM);
        GameOver();
    }

    public void TrainDerailed()
    {
        //Train derailed;
        TrainAnimation.PlayAnimation(TrainAnimation.OFFRAIL_ANIM);
        GameOver();
    }

    private void GameOver()
    {
        TrackPlacement.enabled = false;
        TrainMovement.enabled = false;
    }
}
