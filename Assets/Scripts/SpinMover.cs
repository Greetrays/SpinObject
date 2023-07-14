using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Axis _axis;

    public enum Axis
    {
        X,
        Y,
        Z
    }

    private void OnValidate()
    {
        if (_speed <= 0)
        {
            _speed = 0;
        }
    }

    private void Update()
    {
        switch (_axis)
        {
            case Axis.X:
                transform.Rotate(_speed * Time.deltaTime, 0, 0);
                break;

            case Axis.Y:
                transform.Rotate(0, _speed * Time.deltaTime, 0);
                break;

            case Axis.Z:
                transform.Rotate(0, 0, _speed * Time.deltaTime);
                break;
        }
    }

    public void SetAxisX()
    {
        _axis = Axis.X;
    }

    public void SetAxisY()
    {
        _axis = Axis.Y;
    }

    public void SetAxisZ()
    {
        _axis = Axis.Z;
    }
}
