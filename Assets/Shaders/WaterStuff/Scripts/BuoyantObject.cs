using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyantObject : MonoBehaviour
{
    private readonly Color _red = new(0.92f, 0.25f, 0.2f);
    private readonly Color _green = new(0.2f, 0.92f, 0.51f);
    private readonly Color _blue = new(0.2f, 0.67f, 0.92f);
    private readonly Color _orange = new(0.97f, 0.79f, 0.26f);

    [Header("Water"), SerializeField]
    private float _waterHeight = 0.0f;

    [Header("Waves"), SerializeField]
    private float _steepness;
    [SerializeField]
    private float _wavelength;
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float[] _directions = new float[4];

    [Header("Buoyancy")]
    [SerializeField, Range(1, 5)]
    private float _strength = 1f;
    [SerializeField, Range(0.2f, 5)]
    private float _objectDepth = 1f;

    [SerializeField]
    private float _velocityDrag = 0.99f;
    [SerializeField]
    private float _angularDrag = 0.5f;

    [Header("Effectors"), SerializeField]
    private Transform[] _effectors;

    private Rigidbody _rb;
    private Vector3[] _effectorProjections;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _effectorProjections = new Vector3[_effectors.Length];

        for (int i = 0; i < _effectors.Length; i++)
            _effectorProjections[i] = _effectors[i].position;
    }

    private void OnDisable()
    {
        _rb.useGravity = true;
    }

    private void FixedUpdate()
    {
        int effectorAmount = _effectors.Length;

        for (int i = 0; i < effectorAmount; i++)
        {
            Vector3 effectorPosition = _effectors[i].position;

            _effectorProjections[i] = effectorPosition;
            _effectorProjections[i].y = _waterHeight + GerstnerWaveDisplacement.GetWaveDisplacement(effectorPosition, _steepness, _wavelength, _speed, _directions).y;

            //Gravity
            _rb.AddForceAtPosition(Physics.gravity / effectorAmount, effectorPosition, ForceMode.Acceleration);

            float waveHeight = _effectorProjections[i].y;
            float effectorHeight = effectorPosition.y;

            if (!(effectorHeight < waveHeight))
                continue;

            float submersion = Mathf.Clamp01(waveHeight - effectorHeight) / _objectDepth;
            float buoyancy = Mathf.Abs(Physics.gravity.y) * submersion * _strength;

            //Buoyancy
            _rb.AddForceAtPosition(Vector3.up * buoyancy, effectorPosition, ForceMode.Acceleration);
            //Drag
            _rb.AddForce(-_rb.velocity * (_velocityDrag * Time.fixedDeltaTime), ForceMode.VelocityChange);
            //Torque
            _rb.AddTorque(-_rb.angularVelocity * (_angularDrag * Time.fixedDeltaTime), ForceMode.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        if (_effectors == null)
            return;

        for (int i = 0; i < _effectors.Length; i++)
        {
            if (!Application.isPlaying && _effectors[i] != null)
            {
                Gizmos.color = _green;
                Gizmos.DrawSphere(_effectors[i].position, 0.06f);
            }
            else
            {
                if (_effectors[i] == null)
                    return;

                Gizmos.color = _effectors[i].position.y < _effectorProjections[i].y ? _red : _green; // submerged
                Gizmos.DrawSphere(_effectors[i].position, 0.06f);

                Gizmos.color = _orange;
                Gizmos.DrawSphere(_effectorProjections[i], 0.06f);

                Gizmos.color = _blue;
                Gizmos.DrawLine(_effectors[i].position, _effectorProjections[i]);
            }
        }
    }
}
