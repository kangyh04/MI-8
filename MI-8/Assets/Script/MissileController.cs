using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField]
    private DisturbanceDisposer disposer;
    [SerializeField]
    private Vector3 destPos = Vector3.zero;
    [SerializeField]
    private float speed = 0f;

    private void Awake()
    {
        Vector3 startPos = this.transform.position;
        var direction = (destPos - startPos).normalized;

        disposer.OnTappedImmediateAsObservable()
            .SelectMany(_ =>
            {
                this.transform.position = startPos;
                return Observable.EveryUpdate();
            })
            .TakeWhile(_ =>
            {
                var destDirection = (destPos - this.transform.position).normalized;
                var dot = Vector3.Dot(direction, destDirection);
                return dot == 1;
            })
            .RepeatUntilDestroy(this)
            .Subscribe(_ =>
            {
                this.transform.position += direction * speed;
            },
            () =>
            {
                this.transform.position = destPos;
            })
            .AddTo(this);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        this.transform.position = destPos;
    }
}
