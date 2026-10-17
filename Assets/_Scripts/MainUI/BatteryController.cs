using UnityEngine;

public class BatteryController : MonoBehaviour
{
    // battery components
    public GameObject chargeIcon;
    public GameObject leftSphere;
    public GameObject rightSphere;
    public Transform cylinder;

    // used to track current status of battery (to be implemented to actual battery features (from 1-100)
    [Range(0f, 1f)]
    public float battery = 1f;

    float maxHeight;
    float startPosition;

    // TESTING
    public float drainSpeed = 0.1f;

    public float targetBattery = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maxHeight = cylinder.localScale.y;
        startPosition = cylinder.localPosition.x;

        chargeIcon.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        // simulate draining battery
        targetBattery -= Time.deltaTime * drainSpeed;

        if (targetBattery <= 0f)
            targetBattery = 1f;

        //targetBattery = Mathf.Clamp01(targetBattery);

        // smooth animation
        battery = Mathf.MoveTowards(battery, targetBattery, Time.deltaTime);

        if (battery < 0f) 
        {
            battery = 1.3f;
        }

        UpdateBattery();

    }

    void UpdateBattery()
    {
        bool startCylinder = false;
        // Low battery
        if (battery <= 0.15f)
        {
            cylinder.gameObject.SetActive(false);
            leftSphere.SetActive(false);
            rightSphere.SetActive(false);

            chargeIcon.SetActive(true);

            chargeIcon.SetActive(Mathf.FloorToInt(Time.time * 3) % 2 == 0);

            return;
        }

        chargeIcon.SetActive(false);

        cylinder.gameObject.SetActive(true);
        leftSphere.SetActive(true);

        // full battery
        if (battery >= 0.93f)
        {
            rightSphere.SetActive(true);
            
        }
        else
        {
            rightSphere.SetActive(false);
            startCylinder = true;
        }
        
        if (startCylinder)
        {
            float newLength = maxHeight * battery;

            Vector3 scale = cylinder.localScale;
            scale.y = newLength;   // shrink along y
            cylinder.localScale = scale;

            // shift only enough to keep the left side anchored
            float offset = (maxHeight - newLength);
            cylinder.localPosition = new Vector3(startPosition - offset, cylinder.localPosition.y, cylinder.localPosition.z);
        }
        
    }
}
