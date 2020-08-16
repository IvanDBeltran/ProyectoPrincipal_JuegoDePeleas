using UnityEngine;
using System.Collections;
using System;

public class BotonesException : Exception
{
    public BotonesException()
    {
    }

    public BotonesException(string message)
        : base(message)
    {
    }

    public BotonesException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
