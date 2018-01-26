using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class Disturbance : MonoBehaviour
{
    [SerializeField]
    private List<DisturbanceDisposer> disposers = new List<DisturbanceDisposer>();
    public List<DisturbanceDisposer> Disposers
    {
        get
        {
            return disposers;
        }
    }

    private readonly ISubject<Disturbance> onInactivated = new Subject<Disturbance>();
    public IObservable<Disturbance> OnInactivatedAsObservable()
    {
        return onInactivated;
    }

    private bool Active
    {
        set
        {
            this.gameObject.SetActive(value);
        }
    }

    public void Activate()
    {
        Active = true;
    }

    public void Inactivate()
    {
        Active = false;
    }


    private void Awake()
    {
        disposers
            .Select(disposer => disposer.OnTappedAsObservable())
            .Aggregate((pre, cur) =>
            {
                return pre.SelectMany(cur);
            })
            .Subscribe(_ =>
            {
                Inactivate();
                onInactivated.OnNext(this);
            })
            .AddTo(this);
    }
}
