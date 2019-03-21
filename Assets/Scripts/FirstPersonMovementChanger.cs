﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using DG.Tweening;


public class FirstPersonMovementChanger : MonoBehaviour
{
    public bool Debugging;
    public MovementType initialMovementType = MovementType.Normal;

    [Header("Movement Speed")]
    public float WalkSpeedNoiseInterval = 1f;
    public float WalkSpeedNoiseStandardDeviation = 1f;
    public float WalkSpeedNoiseMaxDeviation = 5f;
    [Header("Position Noise")]
    public float PositionNoiseInterval = 1f;
    public float PositionNoiseStandardDeviation = 1f;
    public float PositionNoiseMaxDeviation = 5f;

    private FirstPersonController controller;
    private NormalDist normal;

    private float nextWalkSpeedNoise;
    private float nextPositionNoise;
    private float walkSpeedMean;

    private bool walkSpeedNoise = false;
    private bool PositionNoise = false;


    // Start is called before the first frame update
    void Start()
    {
        normal = GetComponent<NormalDist>();
        controller = GetComponent<FirstPersonController>();
        nextWalkSpeedNoise = Time.time + WalkSpeedNoiseInterval;
        nextPositionNoise = Time.time + PositionNoiseInterval;
        walkSpeedMean = controller.m_WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Debugging)
        {
            if (Input.GetKey(KeyCode.P))
            {
                ToogleSpeedNoise();
            }
            if (Input.GetKey(KeyCode.M))
            {
                ToggleMoveNoise();
            }
        }

        if (walkSpeedNoise && Time.time > nextWalkSpeedNoise)
        {
            ChangeWalkSpeed();

        }


        if (PositionNoise && Time.time > nextPositionNoise)
        {
            ShiftPosition();

        }
    }

    public void ToggleMoveNoise()
    {
        PositionNoise = !PositionNoise;
        if (Debugging)
        {
            Debug.Log(this.GetType() + ": Toggling Movement Noise");
        }
    }

    public void ToogleSpeedNoise()
    {
        walkSpeedNoise = !walkSpeedNoise;
        if (walkSpeedNoise)
        {
            controller.m_WalkSpeed = walkSpeedMean;
        }

        if (Debugging)
        {
            Debug.Log(this.GetType() + ": Toggling Speed Noise");
        }


    }

    public void ChangeMovementControls(MovementType type)
    {
        switch (type)
        {
            case MovementType.Normal:
                controller.m_MouseLook.XSensitivity = controller.m_MouseLook.YSensitivity = 2f;

                break;
            case MovementType.ForwardOnly:
                controller.m_MouseLook.XSensitivity = controller.m_MouseLook.YSensitivity = 2f;

                break;
            case MovementType.TurnLeftAndRight:
                controller.m_MouseLook.XSensitivity = controller.m_MouseLook.YSensitivity = 0f;
                break;
            default:
                break;
        }
    }

    private void ChangeWalkSpeed()
    {
        float target = normal.StandardNormalDistribution(walkSpeedMean, WalkSpeedNoiseStandardDeviation, walkSpeedMean - WalkSpeedNoiseMaxDeviation, walkSpeedMean + WalkSpeedNoiseMaxDeviation);
        DOTween.To(() => controller.m_WalkSpeed, x => controller.m_WalkSpeed = x, target, WalkSpeedNoiseInterval / 2);
        nextWalkSpeedNoise = Time.time + WalkSpeedNoiseInterval;

        if (Debugging)
        {
            Debug.Log(this.GetType() + ": time is " + Time.time);
            Debug.Log(this.GetType() + ": Changing walk speed to " + target);
            Debug.Log(this.GetType() + ": Next change at " + nextWalkSpeedNoise);
        }
    }

    private void ShiftPosition()
    {
        float shiftX = normal.StandardNormalDistribution(0, PositionNoiseStandardDeviation, -PositionNoiseMaxDeviation, PositionNoiseMaxDeviation);
        float shiftY = normal.StandardNormalDistribution(0, PositionNoiseStandardDeviation, -PositionNoiseMaxDeviation, PositionNoiseMaxDeviation);

        transform.position += new Vector3(shiftX, 0, shiftY);

        nextPositionNoise = Time.time + PositionNoiseInterval;
        if (Debugging)
        {
            Debug.Log(this.GetType() + ": time is " + Time.time);
            Debug.Log(this.GetType() + ": Shifting position by: "+shiftX+", "+shiftY);
            Debug.Log(this.GetType() + ": Next change at " + nextPositionNoise);
        }
    }
}



public enum MovementType
{
    Normal,ForwardOnly,TurnLeftAndRight
}