using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Utility;

public sealed class Rope : MonoBehaviour
{
    [SerializeField] private RopeConnectors[] connectorPrefabs;
    [SerializeField] [CanBeNull] private Attachable connectedItem;
    private List<RopeConnectors> spawnedConnectors = new List<RopeConnectors>();
    private float distance = 0f;
    private Func<float> onSpriteSpawned;
    private void Awake()
    {
        CheckReferences();
    }

    private void Start()
    {
        CalculateDistance();
        if (!CheckIfSpawnChain()) return;
        // MakeRope();
    }

    private void MakeRope()
    {
        SpawnConnectors();
        ConfigureAllRopeConnectors();
    }

    private void CalculateDistance()
    {
        if (connectedItem == null) return;
        distance = (gameObject.transform.position - connectedItem.GetAttachmentPoint().position).magnitude;
    }

    /// <summary>
    /// Index <= -1 decides if a random index will be chosen + if index is inside range. Other wise return a random piece from the collection
    /// </summary>
    [CanBeNull]
    private RopeConnectors PickRandomRopeConnector(int index = -1)
    {
        RopeConnectors ropeConnector = null;
        if (connectorPrefabs.Length == 0) return null;
        if (index >= 0 && index < connectorPrefabs.Length)
        {
            ropeConnector = connectorPrefabs[index];
        }
        else
        {
            int randomIndex = UnityEngine.Random.Range(0, connectorPrefabs.Length);
            ropeConnector = connectorPrefabs[randomIndex];
        }
        return ropeConnector;
    }

    private void SpawnConnectors()
    {
        float totalLength = 0f;
        while (totalLength <= distance)
        {
            RopeConnectors ropeConnector = SpawnSingleRopeConnector();
            totalLength += ropeConnector.SpriteLength;
            spawnedConnectors.Add(ropeConnector);
        }
    }

    private void ConfigureAllRopeConnectors()
    {
        for (int i = 0; i < spawnedConnectors.Count; i++)
        {
            int forwardIndex = i + 1;
            RopeConnectors currentConnector = connectorPrefabs[i];
            if (forwardIndex < spawnedConnectors.Count - 1)
            {
                currentConnector.Joint.connectedBody = spawnedConnectors[forwardIndex].JointRigidBody;
            }
            else
            {
                if (connectedItem != null)
                {
                    connectedItem.PositionAccordingToAttachPoint();
                }
            }
        }
    }

    private RopeConnectors SpawnSingleRopeConnector()
    {
        Vector3 position = Vector3.zero;
        return Instantiate(PickRandomRopeConnector(), position, Quaternion.identity, transform).GetComponent<RopeConnectors>();
    }

    private bool CheckIfSpawnChain()
    {
        return connectorPrefabs != null;
    }
    
    private void CheckReferences()
    {
        if (connectorPrefabs == null || connectorPrefabs.Length == 0) Logging.LogEmptyCollectionError(nameof(connectorPrefabs), ErrorSeverity.Warning, gameObject);
        ReferenceValidator.CheckComponentForNull(ref connectedItem, gameObject, nameof(connectedItem) ,ErrorSeverity.Warning);
    }
}