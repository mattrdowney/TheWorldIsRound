﻿using UnityEngine;

public class SunRotation : MonoBehaviour // Everything is relative, and rotating the Earth would cause motion sickness.
{
    // Start is called before the first frame update
    void Start()
    {
        sun = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, +360/60*Time.deltaTime, 0)); // one rotation per minute, positive sign matters because sun rises in true East and sets in true West (implementation note: Planetaria is inverted)
    }

    [SerializeField] [HideInInspector] private Transform sun;
}

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.