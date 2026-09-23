using System;
using System.Collections;
using UnityEngine;

using Random = UnityEngine.Random;

public class SlashVFXController : MonoBehaviour
{
    [SerializeField]
    private Transform m_transform;

    [SerializeField, Header("ParticleEffect")]
    private ParticleSystem _slashVFX;

    private const float DefaultRotation = 0f;
    private const float FullRotation = 360f;

    private void Awake()
    {
        GameManager.OnSystemStartProcessCompleted += OnSystemsReady;
    }

    private void OnSystemsReady()
    {
        AttackDefenseSystem
            .OnAttack
            .AddNewListener(Randomize, true);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Randomize()
    {
        _slashVFX.Play();
        transform.localRotation = Quaternion.Euler(0, 0, Random.Range(DefaultRotation, FullRotation));
    }
}