using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public enum DroneMoving
{
    Flying,
    DropDown,
}

public class DroneController : MonoBehaviour
{
    [SerializeField]
    private DisturbanceDisposer disposer;
    [SerializeField]
    private Vector3 LeftSideDestPos = Vector3.zero;
    [SerializeField]
    private Vector3 RightSideDestPos = Vector3.zero;
    [SerializeField]
    private float dropSpeed = 0f;

    private void Awake()
    {
        Observable.EveryUpdate()
            .Scan(new float(), (accumulateTime, _) => accumulateTime += Time.deltaTime)
            .Select(timer => Tuple.Create(DroneMoving.Flying, Mathf.Sin(timer)))
            .CombineLatest(
            disposer.OnTappedImmediateAsObservable().Select(_ => Tuple.Create(DroneMoving.DropDown, 0f))
                .StartWith(Tuple.Create(DroneMoving.Flying, 0f)),
            (fromUpdate, fromTap) =>
            {
                return Tuple.Create(fromTap.Item1, fromUpdate.Item2);
            })
            .Subscribe(item =>
            {
                var rad = item.Item2;
                var calcedPos = Vector3.Lerp(LeftSideDestPos, RightSideDestPos, rad * 0.5f + 0.5f);
                calcedPos.y = this.transform.position.y;
                this.transform.position = calcedPos;
                if (item.Item1 == DroneMoving.DropDown)
                {
                    Debug.Log(item.Item1);
                    this.transform.position += Vector3.down * dropSpeed;
                }
            })
            .AddTo(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
