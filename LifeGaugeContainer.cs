using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LifeGaugeContainer : MonoBehaviour
{

    private static LifeGaugeContainer _instance;


    [SerializeField] private Camera mainCamera;
    [SerializeField] private LifeGauge lifeGaugePrefab;

    private RectTransform rectTransform;
    private readonly Dictionary<MobStatus, LifeGauge> _statusLifeBarMap = new Dictionary<MobStatus, LifeGauge>();

    public static LifeGaugeContainer Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null) throw new Exception("LifeBarContainer instance already exists.");
        _instance = this;
        rectTransform = GetComponent<RectTransform>();
    }

    public void Add(MobStatus _status)
    {
        var LifeGauge = Instantiate(lifeGaugePrefab, transform);
        LifeGauge.Initialize(rectTransform, mainCamera, _status);
        _statusLifeBarMap.Add(_status, LifeGauge);
    }

    public void Remove(MobStatus status)
    {
        Destroy(_statusLifeBarMap[status].gameObject);
        _statusLifeBarMap.Remove(status);
    }
}