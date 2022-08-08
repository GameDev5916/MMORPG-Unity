using UnityEngine;
using System.Collections;

public class CameraGun : MonoBehaviour
{
    public GameObject BallPrefab;
    public GameObject ParticlePrefab;

    public AudioSource[] ShieldHitSFX;

    float curTime = 0;
    public float FireRate = 0.1f;

    private float hitPower = 0.0f;

    // Display help message
    void OnGUI()
    {
        GUI.Label(new Rect(5, 5, Screen.width, Screen.height), "Left Mouse Button - Throw\nRight Mouse Button - Raycast hit\nQ, E Keys- Hit Power: " + hitPower.ToString());
    }

    void OnMouse()
    {        
        if (Input.GetMouseButtonDown(0))
        {  
            if (BallPrefab != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                GameObject prefab = (GameObject)GameObject.Instantiate(BallPrefab, transform.position + transform.forward , Quaternion.identity);
                prefab.GetComponent<Rigidbody>().AddForce(ray.direction * 55f, ForceMode.VelocityChange);
                prefab.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), Random.Range(-20f, 20f)));
            }
        }

        if (Input.GetMouseButton(1) && curTime >= FireRate)
        {
            curTime = 0.0f;
            
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 500.0f))
            {
                Forcefield ffHit = hit.transform.gameObject.GetComponent<Forcefield>();
                if (ffHit != null)
                {
                    ffHit.OnHit(hit.point, hitPower);
                    
                    if(ParticlePrefab != null)
                        GameObject.Instantiate(ParticlePrefab, hit.point, Quaternion.identity);

                    if (ShieldHitSFX.Length > 0)
                    {
                        int index = Random.Range(0, ShieldHitSFX.Length);

                        for (int i = 0; i < 10; i++)
                        {
                            if (!ShieldHitSFX[index].isPlaying)
                                break;
                            else
                                index = Random.Range(0, ShieldHitSFX.Length);
                        }

                        ShieldHitSFX[index].transform.position = hit.point;
                        ShieldHitSFX[index].Play();
                    }
                }
            }
        }
    }

	// Update is called once per frame
	void Update ()
    {
        curTime += Time.deltaTime;
        OnMouse();

        if (Input.GetKey(KeyCode.E))        
            hitPower += 0.1f;        
        else if (Input.GetKey(KeyCode.Q))        
            hitPower -= 0.1f;
	}
}
