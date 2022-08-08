using UnityEngine;
using System.Collections;

public class SimpleGun : MonoBehaviour {
	
	// Update is called once per frame
    void Update()
    {
        // Activate on Left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            // Returns a ray going from camera through a screen point
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Casts a ray against colliders in the scene
            if (Physics.Raycast(ray, out hit, 500.0f))
            {
                // Get Forcefield script component
                Forcefield ffHit = hit.transform.gameObject.GetComponent<Forcefield>();

                // Generate random hit power value and call Force Field script if successful
                if (ffHit != null)
                {
                    float hitPower = Random.Range(-7.0f, 1.0f);
                    ffHit.OnHit(hit.point, hitPower);
                }
            }
        }
    }
}
