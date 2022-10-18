using UnityEngine;

public class GunRotation : MonoBehaviour
{
    public float offset;

    void Update()
    {
        if (Time.timeScale == 1f)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            Vector3 localScale = Vector3.one;

            if (rotZ > 90 || rotZ < -90)
            {
                localScale.y = -1f;
            }
            else
            {
                localScale.y = 1f;
            }

            transform.localScale = localScale;
        }


    }








}
