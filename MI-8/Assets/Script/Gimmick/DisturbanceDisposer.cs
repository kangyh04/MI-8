using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum DisposerType
{
    Correct,
    Fake,
}

public class DisturbanceDisposer : MonoBehaviour
{
    [SerializeField]
    private float FireInterval = 0f;
    public float FireTime { get { return FireInterval; } }
    private readonly ISubject<DisturbanceDisposer> onTapped = new Subject<DisturbanceDisposer>();
    public IObservable<DisturbanceDisposer> OnTappedAsObservable()
    {
        return onTapped.Share();
    }

    private readonly ISubject<DisturbanceDisposer> onTappedImmediate = new Subject<DisturbanceDisposer>();
    public IObservable<DisturbanceDisposer> OnTappedImmediateAsObservable()
    {
        return onTappedImmediate.Share();
    }

    [SerializeField]
    private DisposerType disposerType;
    public DisposerType DisposerType { get { return disposerType; } }

    private void Awake()
    {
        ObservableInput.Instance.OnTappedAsObservable()
            .Where(tappedObj => tappedObj == this.gameObject)
            .Do(_ => onTappedImmediate.OnNext(this))
            .Delay(TimeSpan.FromSeconds(FireInterval))
            .Subscribe(_ => onTapped.OnNext(this))
            .AddTo(this);
    }
}
