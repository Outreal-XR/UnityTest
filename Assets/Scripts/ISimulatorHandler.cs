using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutrealXR {

    // Don't change anything in this class. Work only in SimulatorHandler.cs
    public interface ISimulatorHandler
    {
        bool SpawnUser(Simulator.User user);
        bool RemoveUser(Simulator.User user);

        Simulator.User GetUser();
        Simulator.User GetUser(int id);
        Simulator.User GetUser(string userName);

        bool KickUser(int id);
        bool KickUser(string userName);

        int GetUserCount();
    }
}