using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : SingletonMono<Train>
{
    public TrackPlacement TrackPlacement;
    public TrainMovement TrainMovement;


    public void TrainDerailed()
    {
        //Train derailed;
        transform.rotation = new Quaternion(0, 0, 90, 0);
        GameOver();
    }

    private void GameOver()
    {
        TrackPlacement.enabled = false;
        TrainMovement.enabled = false;
    }
}
