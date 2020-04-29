using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyVertex
{
    public int verticeIndex;
    public Vector3 initialVertexPosition;
    public Vector3 currentVertexPosition;
    public Vector3 currentVelocity;

    public JellyVertex(int index, Vector3 initial, Vector3 currentVertex, Vector3 currentVel)
    {
        verticeIndex = index;
        initialVertexPosition = initial;
        currentVertexPosition = currentVertex;
        currentVelocity = currentVel;
    }

    public Vector3 CurrentDisplacement()
    {

        return currentVertexPosition - initialVertexPosition;
    }

    public void UpdateVelocity(float bounceSpeed)
    {
        currentVelocity = currentVelocity - CurrentDisplacement() * bounceSpeed * Time.deltaTime;

    }
    public void Settle(float stiffness)
    {

        currentVelocity *= 1f - stiffness * Time.deltaTime;
    }
    public void ApplyPressure(Transform trans, Vector3 pos, float pressure)
    {
        Vector3 distanceVerticePoint = currentVertexPosition - trans.InverseTransformPoint(pos);
        float adaptedPressure = pressure / (1f + distanceVerticePoint.sqrMagnitude);
        float velocity = adaptedPressure * Time.deltaTime;
        currentVelocity += distanceVerticePoint.normalized * velocity;
    }
}
