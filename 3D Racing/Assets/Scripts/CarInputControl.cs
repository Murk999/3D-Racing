using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour //управление автомобилем
{
    [SerializeField] private Car car; //ссылка на код с ифнормацией о модели автомобиля
    [SerializeField] private AnimationCurve breakCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField][Range(0.0f, 1.0f)] private float autoBrealStrenth = 0.5f;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handnreakAxix;


    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrottle();
        UpdateSteer();

        car.ThrottleControl = Input.GetAxis("Vertical"); // уровень газа
        car.SteerControl = Input.GetAxis("Horizontal");
        UpdateAutoBreak();
    }

    private void UpdateThrottle()
    {

        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = verticalAxis;
            car.BrakeControl = 0;
        }
        else
        {
            car.ThrottleControl = 0;
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }
    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
    }

    private void UpdateAutoBreak()
    {
        if (verticalAxis == 0)
        {
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed) * autoBrealStrenth;
        }
    }
    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handnreakAxix = Input.GetAxis("Jump");
    }
}
