using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddlePowerup : MonoBehaviour
{
    [SerializeField]
    private float paddleYSize = 2f;

    [SerializeField]
    private float paddleXSize = 4f;

    [SerializeField]
    private float powerupTime = 3f;

    public float ReturnPaddleYSizeToChange()
    {
        return paddleYSize;
    }
    public float ReturnPaddleXSizeToChange()
    {
        return paddleXSize;
    }
    public float ReturnPowerupTime()
    {
        return powerupTime;
    }

}
