using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : SingletonMono<Train>
{
    public TrackPlacement TrackPlacement;
    public TrainMovement TrainMovement;
    public Score Score;


    public void TrainDerailed()
    {
        //Train derailed;
        Destroy(this);
        GameOver();
    }

    private void GameOver()
    {
        TrackPlacement.enabled = false;
        TrainMovement.enabled = false;
    }
}
