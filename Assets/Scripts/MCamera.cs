using UnityEngine;

public class MCamera : MonoBehaviour {

    public Character FollowedPlayer;
    Vector3 offset;
    float offX;
    private void Start()
    {
        offset = transform.position - FollowedPlayer.transform.position;
        offX = offset.x;
    }

    private void Update()
    {
        if(FollowedPlayer == null)
        {
            FollowedPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        }
        if (FollowedPlayer.Direction == Directions.Right)
        {
            if(offset.x < Mathf.Abs(offX))
            {
                offset.x += 0.1f;
            }
        }
        else
        {
            if(offset.x > -Mathf.Abs(offX))
            {
                offset.x -= 0.1f;
            }
        }
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(FollowedPlayer.transform.position.x + offset.x, 0, transform.position.z);
        if (transform.position.x < -4.51f)
            transform.position = new Vector3(-4.51f, 0, transform.position.z);
        if (transform.position.x > 4.75f)
            transform.position = new Vector3(4.75f, 0, transform.position.z);
    }
}