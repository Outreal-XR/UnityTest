using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using OutrealXR;

namespace Tests
{
    // Don't change anything in this class. Work only in SimulatorHandler.cs
    public class HandlerTester
    {
        ISimulatorHandler handler;
        Simulator simulator;

        [UnityTest]
        public IEnumerator TestSolution()
        {
            simulator = Simulator.instance;

            Assert.NotNull(Simulator.instance);
            simulator = Simulator.instance;
            Assert.AreEqual(1, GameObject.FindObjectsOfType<Simulator>().Length);

            // Create and configure the handler here
            handler = new GameObject("SimulatorHandler", typeof(SimulatorHandler)).GetComponent<ISimulatorHandler>();

            simulator.OnUserAdded = new UserEvent();
            simulator.OnUserRemoved = new UserEvent();

            simulator.OnUserAdded.AddListener((user) => handler.SpawnUser(user));
            simulator.OnUserRemoved.AddListener((user) => handler.RemoveUser(user));
            
            yield return new WaitForEndOfFrame();
            // Your handler will be tested below. You can use this to design your code
            Simulator.instance.InitUsers();
            Assert.AreEqual(1, handler.GetUserCount());
            yield return new WaitWhile(() => handler.GetUserCount() != 50);
            Assert.AreEqual("TestUser29", handler.GetUser(29).UserName);
            Assert.NotNull(handler.GetUser(11));

            int[] useridsToKickAdd = {3, 5, 7, 11, 15};
            int[] invalidIds = {52, 54, 57};
            int expectedUserCount = 50;

            foreach(int uid in useridsToKickAdd) {
                Simulator.instance.RequestToKickUser(handler.GetUser(uid));
                expectedUserCount--;
                yield return new WaitForSeconds(1.25f);
                Assert.Null(handler.GetUser(uid));
                Assert.AreEqual(expectedUserCount, handler.GetUserCount());
            }

            foreach(int uid in useridsToKickAdd) Assert.Null(handler.GetUser(uid));

            foreach(int uid in invalidIds) Assert.Null(handler.GetUser(uid));

            foreach(int uid in useridsToKickAdd) {
                Simulator.instance.OnUserAdded.Invoke(new Simulator.User(string.Format("TestUser{0}", uid), uid, false));
                expectedUserCount++;
                yield return new WaitForSeconds(1.25f);
                Assert.NotNull(handler.GetUser(uid));
                Assert.AreEqual(expectedUserCount, handler.GetUserCount());
            }

            foreach(int uid in useridsToKickAdd) {
                Simulator.User newUser = new Simulator.User(string.Format("TestUser{0}", uid), uid, false);
                Simulator.instance.OnUserAdded.Invoke(newUser);
                yield return new WaitForSeconds(1.25f);
                Assert.NotNull(handler.GetUser(uid));
                Assert.AreEqual(expectedUserCount, handler.GetUserCount());
            }
            
            GameObject[] controllers = GameObject.FindGameObjectsWithTag("Player");
            List<string> trackedControllers = new List<string>();
            foreach(GameObject controller in controllers) {
                if(trackedControllers.Contains(controller.gameObject.name)) Debug.LogErrorFormat("[HandlerTester] Found duplicate of {0}", controller.gameObject.name);
                trackedControllers.Add(controller.gameObject.name);
            }
            Assert.AreEqual(handler.GetUserCount(), controllers.Length);
            Debug.Log("[HandlerTester] Congratulations. Your passed technical test. Please, share you solution for code review. After that we will get back you to shortly");
        }
    }
}