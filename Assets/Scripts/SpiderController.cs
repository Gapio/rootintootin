using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    public int[] referenceLeg;
    public int[] referenceLeg2;
    public LegController[] legs;
    public AnimationCurve sensCurve;
    public float desiredSurfDist = -1f;
    public bool grounded;
    private Rigidbody rb;
    public float speed;
    public float angspeed;
    private float movx;
    public float movy;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public static SpiderController instance;

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        movx = Input.GetAxis("Horizontal");
        movy = Input.GetAxis("Vertical");


        if (movy != 0)
        {
            Vector3 movement = transform.right * movy * speed * Time.deltaTime;
            rb.velocity = movement;
        }

        transform.Rotate(0, movx * angspeed * Time.deltaTime, 0);

        calcOrientation();
    }

    public void calcOrientation()
    {
        Vector3 up = Vector3.zero;
        Vector3 point, a, b, c;
        float avgSurfDist = 0f;
        grounded = false;

        for (int i = 0; i < legs.Length; i++)
        {
            point = legs[i].newPos;
            a = ((legs[referenceLeg[i]].newPos) - point).normalized;
            b = ((legs[referenceLeg2[i]].newPos) - point).normalized;
            avgSurfDist += transform.InverseTransformPoint(point).y;
            c = Vector3.Cross(a, b) * -1;

            Debug.DrawRay(point, a, Color.red, 0);
            Debug.DrawRay(point, b, Color.blue, 0);
            Debug.DrawRay(point, c, Color.green, 0);

            up += c * sensCurve.Evaluate(c.magnitude) + (legs[i].stepNormal == Vector3.zero ? transform.forward : legs[i].stepNormal);
            grounded |= legs[i].legGrounded;
        }

        up /= legs.Length;
        avgSurfDist /= legs.Length;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.ProjectOnPlane(transform.forward, up), up), 20f * Time.deltaTime);

        if (grounded)
        {
            transform.Translate(0, -(-avgSurfDist + desiredSurfDist), 0, Space.Self);
        }
        /*
        else
        {
            transform.Translate(0, -10 * Time.deltaTime, 0, Space.Self);
        }
        */
    }
}
