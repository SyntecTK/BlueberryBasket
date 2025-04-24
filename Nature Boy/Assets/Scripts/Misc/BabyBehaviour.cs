using UnityEngine;

public class BabyBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerController player;
    private bool isFollowing = false;
    private int followIndex;
    private Vector3 offset;

    private void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
    }

    private void Update()
    {
        if(isFollowing)
        {
            Vector3 targetPos = player.transform.position + player.transform.up * (0.3f * followIndex);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * 2f);
        }
    }

    public void StartFollowing()
    {
        isFollowing = true;
        followIndex = GameManager.Instance.RegisterBaby();
        Debug.Log("FollowIndex: "+followIndex);
    }
}
