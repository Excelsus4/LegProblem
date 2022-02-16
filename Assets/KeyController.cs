using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    public UnityEngine.UI.Text text;

    public Transform[] LengthControl2;
    public Transform[] ShortControl;
    public Transform[] LongControl;

    public float length;
    [Range(67.114620209f, 68.75f)]
    public float shortAngle;
    public float longAngle;

    public float seal;

    private void Start()
    {
        seal = 0.2f;
        ChangeAngle(67.114620209f);
    }
    // Update is called once per frame
    void Update()
    {

    }

    public float ComputeAngle(float alpha)
    {
        return Mathf.Acos(18 * Mathf.Cos(Mathf.Deg2Rad * alpha) / 7) * Mathf.Rad2Deg;
    }

    public float RaycastLength(Transform origin)
    {
        RaycastHit hit;
        if(Physics.Raycast(origin.position, origin.TransformDirection(Vector3.right), out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.right) * 100f, Color.yellow);
            return hit.distance;
        }
        return 1f;
    }

    public void ChangeAngle(float angle)
    {
        shortAngle = angle;
        longAngle = ComputeAngle(shortAngle);

        LongControl[0].SetPositionAndRotation(LongControl[0].position, Quaternion.Euler(0, -longAngle, 0));
        ShortControl[0].SetPositionAndRotation(ShortControl[0].position, Quaternion.Euler(0, -shortAngle - 90, 0));
        LongControl[1].SetPositionAndRotation(LongControl[1].position, Quaternion.Euler(0, -longAngle - 180, 0));
        ShortControl[1].SetPositionAndRotation(ShortControl[1].position, Quaternion.Euler(0, -shortAngle - 270, 0));

        length = RaycastLength(LengthControl2[0].transform) + seal;

        foreach (Transform t in LengthControl2)
        {
            t.localScale = new Vector3(length, .2f, .2f);
        }

        MakeText();
    }

    public void MakeText()
    {
        text.text = @"현재 테이블 길이: 1800mm × 700mm
다리 길이: "+length*10+@"mm
알파각: "+shortAngle+@"도
세타각: "+longAngle+@"도";
    }
}
