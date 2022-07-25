using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketballController : MonoBehaviour
{
    public float MoveSpeed = 10;
    public Transform Ball;
    public Transform Hand;
    public Transform PosOverHead;
    public Transform PosDrible;
    public Transform Target;

    private bool InBallHands = true;
    private bool IsBallFlying = false;

    private float T = 0;
    // Update is called once per frame
    void Update()
    { 
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Lerp(transform.rotation, Camera.main.transform.rotation, Time.deltaTime);
        transform.LookAt(transform.position + direction );

       
        //Ball.position = Pos.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));

        if (InBallHands)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Ball.position = PosOverHead.position;
                Hand.localEulerAngles = Vector3.right * 180;

                transform.LookAt(Target.parent.position);
                
            }
            else
            {
                Ball.position = PosDrible.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time * 5));
                Hand.localEulerAngles = Vector3.right * 0;
            } 
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Hand.localEulerAngles = Vector3.right * 0;
                InBallHands = false;
                IsBallFlying = true;
                T = 0;
            }
           
        }
       
        if (IsBallFlying)
        {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t01 = T / duration;

            Vector3 A = PosOverHead.position;
            Vector3 B = Target.position;
            Vector3 pos = Vector3.Lerp(A, B, t01);

            Vector3 arc = Vector3.up * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            if (t01 >= 1)
            {
                IsBallFlying = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsBallFlying && !InBallHands)
        {
            InBallHands = true;
            Ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
