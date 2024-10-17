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

        score.text = $"óġ �� : {currentScore.ToString()}";

    }


}
