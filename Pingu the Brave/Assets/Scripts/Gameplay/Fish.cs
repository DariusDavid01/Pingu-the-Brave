using UnityEngine;

public class Fish : MonoBehaviour
{
    private Animator anim;
    public float moveSpeed = 17f;
    CoinMove coinMoveScript;
    //public Rigidbody rb;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();
        coinMoveScript = gameObject.GetComponent<CoinMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin Detector" || other.tag == "Coin Detector" || other.tag == "Player Bubble" || other.gameObject.tag == "Player Bubble" ||
            other.tag == "Player" || other.gameObject.tag == "Player")
            PickupFish();
    }

    private void PickupFish()
    {
        Debug.Log("pickup activd");
        anim.SetTrigger("Pickup");
        GameStats.Instance.CollectFish();
    }

    public void OnShowChunk()
    {
        anim?.SetTrigger("Idle");
    }
}
