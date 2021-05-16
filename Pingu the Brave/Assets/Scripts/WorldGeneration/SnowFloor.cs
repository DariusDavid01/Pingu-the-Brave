using UnityEngine;

public class SnowFloor : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Material material;
    public float offsetSpeed = 0.25f;

    private void Update()
    {
        transform.position = Vector3.forward * player.transform.position.z;
        material.SetVector("Vector2_1418DAD4", new Vector2(0, -transform.position.z * offsetSpeed));
    }
}
