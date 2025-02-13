
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour //информационная модель автомобиля
{
    [SerializeField] private float maxSteerAngle; //ссыока на макс поворот
    [SerializeField] private float maxBrakeTorque; //ссылка на макс тормоз

    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float maxMotorTorque; //ссылка на макс мотор
    [SerializeField] private int maxSpeed; //значение для максиальной скорости
    
    public float LinearVelocity => chassis.LinearVelocity; //ссылается на шасси
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    private CarChassis chassis; //ссылка на калеса
    //DEBUG
    [SerializeField] private float linearVelocity;
    public float ThrottleControl; //педаль газа
    public float SteerControl; //поворот
    public float BrakeControl; //тормоз

    private void Start()
    {
        chassis = GetComponent<CarChassis>(); //берем компонент
    }

    private void Update() //упарвление физикой автомобиля
    {
        linearVelocity = LinearVelocity;

        float engineTorque = engineTorqueCurve.Evaluate(LinearVelocity / maxSpeed) * maxMotorTorque;

        if (LinearVelocity >= maxSpeed)
            engineTorque = 0;

        chassis.MotorTorque = engineTorque * ThrottleControl;
        chassis.SteerAngle = maxSteerAngle * SteerControl;
        chassis.BrakeTorque = maxBrakeTorque * BrakeControl;
    } 
}