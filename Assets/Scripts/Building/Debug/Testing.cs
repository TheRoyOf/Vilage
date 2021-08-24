using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building.Debug
{
    public class Testing : MonoBehaviour
    {
        [SerializeField]
        private GameObject testSpawnPrefab = null;

        [SerializeField]
        private Material greenMaterial = null;

        private bool ghostActive = false;

        private GameObject ghostObject = null;

        private void Update()
        {
            if(ghostActive)
            {
                RaycastHit hit;
                Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500f, 11 << 12);
                ghostObject.transform.position = hit.point;
            }
        }

        public void SpawnAtPoint(Vector3 point)
        {
            Instantiate(testSpawnPrefab, point, Quaternion.identity);
        }

        public void SpawnAtPoint()
        {
            if (!ghostActive)
                return;

            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500f);
            
            if (hit.collider.tag.Equals("Landscape") || hit.collider.tag.Equals("Ghost"))
            {
                Instantiate(testSpawnPrefab, ghostObject.transform.localPosition, Quaternion.identity);
                HideGhost();
            }
        }

        public void ShowGhost()
        {
            if (ghostActive)
                return;

            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500f);
            ghostObject = Instantiate(testSpawnPrefab, hit.point, Quaternion.identity);
            ghostObject.GetComponent<MeshRenderer>().material = greenMaterial;
            ghostObject.tag = "Ghost";

            ghostActive = true;

        }

        public void HideGhost()
        {
            ghostActive = false;

            Destroy(ghostObject);
        }
    }
}