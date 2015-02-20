using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    public Transform referenceTransform;
    protected void Update()
    {
        transform.localScale = referenceTransform.localScale;
    }
}

