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

        disposer.OnTappedImmediateAsObservable()
            .SelectMany(_ =>
            {
                this.transform.position = startPos;
                return Observable.EveryUpdate();
            })
            .TakeWhile(_ =>
            {
                var direction = 
            })

    }
}
