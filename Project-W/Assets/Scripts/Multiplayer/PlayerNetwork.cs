using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetwork : NetworkBehaviour
{
    private readonly NetworkVariable<PlayerNetworkData> _netState = new(writePerm: NetworkVariableWritePermission.Owner);

    Vector3 _vel;
    float interpolateTime = 0.1f;


    void Update()
    {
        if (base.IsOwner)
            _netState.Value = new PlayerNetworkData()
            {
                position = transform.position
            };
        else
            transform.position = Vector3.SmoothDamp(transform.position, _netState.Value.position, ref _vel, interpolateTime);
    }

    struct PlayerNetworkData : INetworkSerializable
    {
        public Vector3 position;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref position);
        }
    }

    public override void OnNetworkSpawn()
    {
        transform.position = new Vector3(0, 85, 0);
    }
}
