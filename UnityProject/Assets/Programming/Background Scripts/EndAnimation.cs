using System;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{

    public void PlayWinAnimation()
    {
        gameObject.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
        rigidbody.AddForce(Vector3.up * 700f);
        GameObject.Destroy(GameObject.Find("Kill Zone"));
    }
}
