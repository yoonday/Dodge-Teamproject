using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterUIController : DodgeUIController
{

    protected override void Start()
    {
        base.Start();
        healthSystem.OnDeath += PlayerGetScore;

    }

    private void PlayerGetScore()
    {
        GameManager.Instance.currentScore += 1;
        score.text = $"Ã³Ä¡ ¼ö : {GameManager.Instance.currentScore.ToString()}";

    }


}
