using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPlaysGenerator
{

    private float minAngle;
    private float maxAngle;
    private float minForce;
    private float maxForce;
    private int numOfPlaysX;
    private int numOfPlaysY;
    private int numOfPlaysZ;

    private float diffAngle;
    private float currAngle;
    private float diffForce;
    private float currForce;

    public RandomPlaysGenerator(
        float minAngle, float minForce,
        float maxAngle, float maxForce, 
        int numOfPlaysX, int numOfPlaysY, 
        int numOfPlaysZ)
    {
        this.minAngle = minAngle;
        this.maxAngle = maxAngle;
        this.minForce = minForce;
        this.maxForce = maxForce;
        this.numOfPlaysX = numOfPlaysX;
        this.numOfPlaysY = numOfPlaysY;
        this.numOfPlaysZ = numOfPlaysZ;

        minAngle = ((minAngle) * 2 * Mathf.PI) / 360.0f;
        maxAngle = ((maxAngle) * 2 * Mathf.PI) / 360.0f;

        diffAngle = (minAngle - maxAngle) / (numOfPlaysX / numOfPlaysY);
        currAngle = minAngle;

        diffForce = (maxForce - minForce + 1) / numOfPlaysY;
        currForce = minForce;
    }

    public Vector3 GetNextPlay()
    {
        float x = currForce * Mathf.Cos(currAngle);
        float y = currForce * Mathf.Sin(currAngle);

        currForce = currForce + diffForce;
        if (currForce > maxForce)
        {
            currForce = minForce;
            currAngle -= diffAngle;
        }

        return new Vector3(x, y, 0.0f);
    }

}
