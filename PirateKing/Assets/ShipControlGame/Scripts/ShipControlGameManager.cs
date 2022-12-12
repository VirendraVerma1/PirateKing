using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShipControl
{
    public class ShipControlGameManager : MonoBehaviour
    {
        public bool isGameOver = false;
        public GameObject YellowPoint;
        public GameObject GreenPoint;
        private GameObject selectedPoint;

        private GameObject selectedObject;
        private bool startmakingWaypoints = false;
        private ShipBotController _shipBotController;
        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
                if (Physics.Raycast(ray, out hit)) 
                {
                    print(hit.collider.gameObject.name);
                    if (hit.collider.tag == "Boat")
                    {
                        print("boat");
                        selectedObject = hit.collider.gameObject;
                        startmakingWaypoints = true;
                        _shipBotController = selectedObject.GetComponent<ShipBotController>();
                        _shipBotController.ClearWayPoint();
                        if (_shipBotController.myTag == "Yellow")
                            selectedPoint = YellowPoint;
                        else if (_shipBotController.myTag == "Green")
                            selectedPoint = GreenPoint;
                    }
                    else if (hit.collider.tag == "Yellow"||hit.collider.tag == "Green"&&selectedObject!=null)
                    {
                        //_shipBotController.myWaypoints.Add(hit.collider.gameObject);
                        _shipBotController.isLoopCompleted = true;
                        selectedObject = null;
                        startmakingWaypoints = false;
                    }
                    else if (startmakingWaypoints )
                    {
                        GameObject g = Instantiate(selectedPoint, hit.point, Quaternion.identity);
                        _shipBotController.myWaypoints.Add(g);
                        _shipBotController.SetPoint();
                    }
                    else
                    {
                        startmakingWaypoints = false;
                    }
                }
            }
        }
    }
}