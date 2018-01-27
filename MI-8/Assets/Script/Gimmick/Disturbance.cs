using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

public class Disturbance : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> changeTarget = new List<GameObject>();
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
        get
        {
            return this.gameObject.activeInHierarchy;
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
            .Where(disposer => disposer.DisposerType == DisposerType.Destoryer)
            .Select(disposer => disposer.OnTappedAsObservable())
            .ToObservable()
            .Scan((pre, cur) =>
            {
                return pre.SelectMany(cur);
            })
            .Switch()
            .Subscribe(_ =>
            {
                Inactivate();
                onInactivated.OnNext(this);
            })
            .AddTo(this);

        disposers
            .Where(disposer => disposer.DisposerType == DisposerType.Modifier)
            .Select(disposer => disposer.OnTappedAsObservable())
            .ToObservable()
            .Scan((pre, cur) =>
            {
                return pre.SelectMany(cur);
            })
            .Switch()
            .Subscribe(_ =>
            {
                changeTarget.ForEach(obj =>
                {
                    obj.SetActive(!obj.activeInHierarchy);
                });
            })
            .AddTo(this);
    }
}
