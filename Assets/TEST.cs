using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 델리게이트
// 무명함수
// 람다식(Lambda)
// 코루틴

public class TEST : MonoBehaviour
{
    public float speed = 5;
    // Start is called before the first frame update

    void BB() => print(""); // 람다식

    void BBB()
    {
        print("");
    }

    void Start()
    {
        Move((num, name) =>
        {
            transform.Rotate(Vector3.forward * 90);
        });
    }

    void Move(System.Action<int, string> callback)
    {
        StartCoroutine("IEMove", callback);
    }

    IEnumerator IEMove(System.Action<int, string> callback)
    {
        yield return new WaitForSeconds(1f);
        if (callback != null)
            callback(1, "");
    }
    
    delegate void MyCallback();
    MyCallback myCallback;
    void Start1()
    {
        myCallback = () =>
        {
            transform.Rotate(Vector3.forward * 90);
        };
        StartCoroutine("IETurn", myCallback);
    }
    IEnumerator IETurn(MyCallback callback)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            callback();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;

    }
}
