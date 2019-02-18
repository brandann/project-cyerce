using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{

    public Transform player1Transform;
    protected const string PLAYER1_TAG = "Player/player1";

    protected Vector3 targetPosition;

    public float FollowLerp;

    // How long the object should shake for.
    public float mShake;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float mShakeAmount;
    public float mDecreaseFactor = .1f;

    protected Vector3 ShakePosition;

    protected Transform Player1Transform
    {
        get
        {
            if (null == player1Transform)
            {
                var go = GameObject.FindWithTag(PLAYER1_TAG);
                if (null != go)
                    player1Transform = go.transform;
            }
            return player1Transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayers();
    }

    private void LateUpdate()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, ShakePosition + targetPosition, FollowLerp);
    }

    private void FollowPlayers()
    {
        if (null != Player1Transform)
            targetPosition = Player1Transform.position;
        else
            targetPosition = new Vector3(0, 0, 0);

        targetPosition.z = -10;
    }

    public void Shake(float shake, float amount)
    {
        mShake = shake;
        mShakeAmount = amount;
        StartCoroutine(ShakeCamera());
    }

    protected IEnumerator ShakeCamera()
    {
        while (mShake > 0)
        {
            ShakePosition = Random.insideUnitSphere.normalized * mShakeAmount * Time.timeScale;
            mShake -= Time.deltaTime * mDecreaseFactor;
            if (Time.deltaTime <= 0 || mShake <= 0)
                mShake = 0;
            yield return new WaitForSeconds(0);
        }
        ShakePosition = new Vector3(0, 0, 0);
        yield return null;
    }
}
