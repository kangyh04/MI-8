using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [SerializeField]
    private DisturbanceDisposer disposer;
    [SerializeField]
    private Vector3 destPos = Vector3.zero;

    private void Awake()
    {
        var startPos = this.transform.position;

        disposer.OnTappedImmediateAsObservable()
            .SelectMany(_ => Observable.EveryUpdate())
            .Scan(new float(), (accumulateTime, _) => accumulateTime += Time.deltaTime)
            .TakeWhile(timer => timer <= disposer.FireTime)
            .Subscribe(timer =>
            {
                this.transform.position = Vector3.Lerp(startPos, destPos, timer / disposer.FireTime);
            },
            () =>
            {
                this.transform.position = destPos;
            })
            .AddTo(this);

    }
}
