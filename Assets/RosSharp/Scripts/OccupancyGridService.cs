﻿/*
© Federal Univerity of Minas Gerais (Brazil), 2017
Author: Italo Lelis (hello@italolelis.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

<http://www.apache.org/licenses/LICENSE-2.0>.

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/


using UnityEngine;


namespace RosSharp.RosBridgeClient
{

    [RequireComponent(typeof(RosConnector))]
    public class OccupancyGridService : MonoBehaviour
    {
        private OccupancyGridManager occupancyGridManager;
        private RosSocket rosSocket;

        public void Start()
        {
            rosSocket = transform.GetComponent<RosConnector>().RosSocket;

            occupancyGridManager = this.GetComponent<OccupancyGridManager>();
            rosSocket.CallService("/static_map", typeof(NavigationGetMap), serviceReceiver);

        }

        public void callService(string serviceName)
        {
        }

        public void serviceReceiver(object message)
        {
            NavigationGetMap getmap = (NavigationGetMap) message;
            occupancyGridManager.updateGrid(getmap.map);
        }

        private void updatePoseStamped(Message message)
        {
            NavigationOccupancyGrid occupancyGrid = (NavigationOccupancyGrid)message;
            occupancyGridManager.updateGrid(occupancyGrid);
        }
    }
}