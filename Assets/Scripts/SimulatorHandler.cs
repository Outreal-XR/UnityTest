using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutrealXR {
    public class SimulatorHandler : MonoBehaviour, ISimulatorHandler
    {

        // HINT #1: Feel free to use an array or list or dictionay or any other collections to store users

        public Simulator.User GetUser()
        {
            throw new System.NotImplementedException();
        }

        public Simulator.User GetUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public Simulator.User GetUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public int GetUserCount()
        {
            throw new System.NotImplementedException();
        }

        public bool KickUser(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool KickUser(string userName)
        {
            throw new System.NotImplementedException();
        }

        public bool RemoveUser(Simulator.User user)
        {
            throw new System.NotImplementedException();
        }

        public bool SpawnUser(Simulator.User user)
        {
            throw new System.NotImplementedException();
        }
    }
}