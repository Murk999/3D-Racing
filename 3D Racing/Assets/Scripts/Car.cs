
using UnityEngine;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour //�������������� ������ ����������
{
    [SerializeField] private float maxSteerAngle; //������ �� ���� �������
    [SerializeField] private float maxBrakeTorque; //������ �� ���� ������

    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float maxMotorTorque; //������ �� ���� �����
    [SerializeField] private int maxSpeed; //�������� ��� ����������� ��������
    
    public float LinearVelocity => chassis.LinearVelocity; //��������� �� �����
    public float WheelSpeed => chassis.GetWheelSpeed();
    public float MaxSpeed => maxSpeed;

    private CarChassis chassis; //������ �� ������
    //DEBUG
    [SerializeField] private float linearVelocity;
    public float ThrottleControl; //������ ����
    public float SteerControl; //�������
    public float BrakeControl; //������

    private void Start()
    {
        chassis = GetComponent<CarChassis>(); //����� ���������
    }

    private void Update() //���������� ������� ����������
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