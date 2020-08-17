using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace OutrealXR {

	[System.Serializable]
	public class UserEvent : UnityEvent<Simulator.User>{}

	// Don't change anything in this class. Work only in SimulatorHandler.cs
	public class Simulator : MonoBehaviour
	{
		[System.Serializable]
		public class User {
			public string UserName;
			public int UID, Level;
			public bool IsLocal;
			public GameObject controller;// It is Dummy

			public User(string userName, int uid, bool IsLocal) {
				UserName = userName;
				UID = uid;
				this.IsLocal = IsLocal;
			}

			override public string ToString() {
            	return UserName;
        	}
		}

		public ISimulatorHandler handler;
		public UserEvent OnUserAdded, OnUserRemoved;

		public async void InitUsers() {
			OnUserAdded.Invoke(new User("LocalTestUser", 1, true));
			await Task.Delay(Random.Range(100, 1000));
			for(int i = 2; i < 51; i++) OnUserAdded.Invoke(new User(string.Format("TestUser{0}", i), i, false));
		}

		public async void RequestToKickUser(User user) {
			await Task.Delay(Random.Range(100, 1000));
			OnUserRemoved.Invoke(user);
		}

		public static Simulator manager;
		public static Simulator instance
		{
			get
			{
				if (manager == null) {
					if(FindObjectOfType<Simulator>()) manager = FindObjectOfType<Simulator>();
					else manager = new GameObject("Simulator").AddComponent(typeof(Simulator)) as Simulator;
				}
				return manager;
			}
			set
			{
				manager = value;
			}
		}
	}
}