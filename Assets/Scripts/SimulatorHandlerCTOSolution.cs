using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutrealXR {
    public class SimulatorHandlerCTOSolution : MonoBehaviour, ISimulatorHandler
    {

        Simulator.User Me;
        Dictionary<int, Simulator.User> UserIDs = new Dictionary<int, Simulator.User>();
        Dictionary<string, Simulator.User> UserUserNames = new Dictionary<string, Simulator.User>();

        public Simulator.User GetUser()
        {
            return Me;
        }

        public Simulator.User GetUser(int id)
        {
            if(UserIDs.ContainsKey(id)) return UserIDs[id];
            return null;
        }

        public Simulator.User GetUser(string userName)
        {
            if(UserUserNames.ContainsKey(userName)) return UserUserNames[userName];
            return null;
        }

        public int GetUserCount()
        {
            return UserIDs.Count + 1;
        }

        public bool KickUser(int id)
        {
            if(Me.Level <= 1 || !UserIDs.ContainsKey(id)) return false;
            Simulator.instance.RequestToKickUser(UserIDs[id]);
            return true;
        }

        public bool KickUser(string userName)
        {
            if(Me.Level <= 1 || !UserUserNames.ContainsKey(userName)) return false;
            Simulator.instance.RequestToKickUser(UserUserNames[userName]);
            return true;
        }

        public bool RemoveUser() {
            Destroy(Me.controller);
            foreach(Simulator.User user in UserIDs.Values) Destroy(user.controller);
            UserIDs.Clear();
            UserUserNames.Clear();
            return true;
        }

        public bool RemoveUser(Simulator.User user)
        {
            if(!UserUserNames.ContainsKey(user.UserName) || !UserIDs.ContainsKey(user.UID)) return false;
            Destroy(UserUserNames[user.UserName].controller);
            Destroy(UserIDs[user.UID].controller);
            UserUserNames.Remove(user.UserName);
            UserIDs.Remove(user.UID);
            return true;
        }

        public bool SpawnUser(Simulator.User user)
        {
            if(UserUserNames.ContainsKey(user.UserName) || UserIDs.ContainsKey(user.UID)) return false;
            user.controller = new GameObject("Controller for " + user.UserName);
            user.controller.tag = "Player";
            if(user.IsLocal) {
                Me = user;
                return true;
            }
            UserUserNames.Add(user.UserName, user);
            UserIDs.Add(user.UID, user);
            return true;
        }

        public bool UpdateUser(Simulator.User user)
        {
            if(UserUserNames.ContainsKey(user.UserName) || UserIDs.ContainsKey(user.UID)) return false;
            UserUserNames[user.UserName] = user;
            UserIDs[user.UID] = user;
            return true;
        }
    }
}