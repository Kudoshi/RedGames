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
    [SerializeField] private Shaker m_CamShake;
    [SerializeField] private ShakePreset m_HitShakePreset;

    public void TrainCrashed()
    {
        // Play train crash
        //Play audio of crash obstacle
        TrainAnimation.PlayAnimation(TrainAnimation.OFFRAIL_ANIM);
        m_CamShake.Shake(m_HitShakePreset);
        StartCoroutine(GameOver());
    }

    public void TrainDerailed()
    {
        //Train derailed;
        //Play Train derailed audio
        UXManager.Instance.SoundManager.PlayOneShot("OutofRail");
        TrainAnimation.PlayAnimation(TrainAnimation.OFFRAIL_ANIM);
        m_CamShake.Shake(m_HitShakePreset);

        StartCoroutine(GameOver());

    }

    private IEnumerator GameOver()
    {
        TrackPlacement.enabled = false;
        TrainMovement.enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        UXManager.Instance.GameUI.gameObject.SetActive(false);

        yield return new WaitForSeconds(3);

        UXManager.Instance.GameOverScreen.DisplayEndGame(Score.m_CollectableScore, Score.m_CollectedItems);
    }
}
