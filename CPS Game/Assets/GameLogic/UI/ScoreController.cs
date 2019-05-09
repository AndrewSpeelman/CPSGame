using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int attackerScore;
    private int defenderScore;
    public Text attackerScoreText;
    public Text defenderScoreText;
    // Start is called before the first frame update
    void Start()
    {
        attackerScore = 0;
        defenderScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        attackerScoreText.text = "" + attackerScore;
        defenderScoreText.text = "" + defenderScore;
    }


}
