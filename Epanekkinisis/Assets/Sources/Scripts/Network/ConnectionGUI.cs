using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Entities;
using Sfs2X.Requests;
using Sfs2X.Logging;
using Sfs2X.Entities.Data;
using Sfs2X.Entities.Variables;

public class ConnectionGUI : MonoBehaviour {
	
	
	public string serverName = "127.0.0.1";
	public int serverPort = 9933;
	public string zone;
	public string Room;

	public LogLevel logLevel = LogLevel.DEBUG;

	// Internal / private variables
	private SmartFox smartFox;
	bool Joined;
	Rect LoginWindow = new Rect(Screen.width/2 - 250, Screen.height/2 - 200, 500, 400);
	Rect RegisterWindow = new Rect(Screen.width/2 - 250, Screen.height/2 - 250, 500, 500);
	public string Username;
	public string Password;
	public string Mail;
	public GUISkin customSkin;
	string msg = "Please enter credentials";
	string Scene;
	public bool Sent;
	
	void Start() {
		
		// In a webplayer (or editor in webplayer mode) we need to setup security policy negotiation with the server first
		if (Application.isWebPlayer) {
			if (!Security.PrefetchSocketPolicy(serverName, serverPort, 500)) {
				Debug.LogError("Security Exception. Policy file load failed!");
			}
		}	
		
		// Lets connect
		smartFox = new SmartFox(true);
		
		// Register callback delegate
		smartFox.AddEventListener(SFSEvent.CONNECTION, OnConnection);
		smartFox.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
		smartFox.AddEventListener(SFSEvent.LOGIN, OnLogin);
		smartFox.AddEventListener(SFSEvent.LOGIN_ERROR, OnLoginError);
		smartFox.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
		smartFox.AddEventListener(SFSEvent.LOGOUT, OnLogout);
		smartFox.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
		
		smartFox.AddLogListener(logLevel, OnDebugMessage);
		
		smartFox.Connect(serverName, serverPort);

	}
	
	
	
	
	void FixedUpdate() {
		if (smartFox != null) {
			smartFox.ProcessEvents();
		}
	}


	
	
	void OnGUI (){
		
		GUI.skin = customSkin;
		
		if(Joined){
			LoginWindow = GUI.Window (7, LoginWindow, LoginWindowFunction, "L o g i n");
		}
		else{
			RegisterWindow = GUI.Window (8, RegisterWindow, RegisterWindowFunction, "R e g i s t e r");		
		}
		
	}
	
	
	
	//-------------------------------------------------------------------------------------------
	
	
	
	//LOGIN WINDOW
	
	
	void LoginWindowFunction (int windowID){
		
		GUI.Label (new Rect(LoginWindow.width/2 - 100, 120, 200, 30), "Username:");
		Username = GUI.TextField(new Rect(LoginWindow.width/2 - 100, 155, 200, 30), Username, 30);
		
		GUI.Label (new Rect(LoginWindow.width/2 - 100, 190, 200, 30), "Password:");
		Password = GUI.TextField(new Rect(LoginWindow.width/2 - 100, 225, 200, 30), Password, 30);


		if(!Sent && (GUI.Button ( new Rect(LoginWindow.width/2 - 100, 265, 200, 50), "Login") || Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password)){
				
				ISFSObject Credentials = new SFSObject();
				Credentials.PutUtfString("Username", Username);
				Credentials.PutUtfString("Password", Password);
				smartFox.Send(new ExtensionRequest("Login", Credentials));
				
				Sent = true;
				
			}


		if(GUI.Button ( new Rect(350, 120, 100, 35), "Register")){
			
			Joined = false;
			Info.IsReg = true;
			
		}
		
		GUI.TextArea (new Rect(LoginWindow.width/2 - 380/2, 315, 380, 25), msg, 500);
		
	}
	
	
	//LOGIN WINDOW
	
	
	
	
	
	//REGISTER WINDOW
	
	
	void RegisterWindowFunction (int windowID){
		
		GUI.Label (new Rect(LoginWindow.width/2 - 100, 120, 200, 30), "Username:");
		Username = GUI.TextField(new Rect(LoginWindow.width/2 - 100, 155, 200, 30), Username, 30);
		
		GUI.Label (new Rect(LoginWindow.width/2 - 100, 190, 200, 30), "Password:");
		Password = GUI.TextField(new Rect(LoginWindow.width/2 - 100, 225, 200, 30), Password, 30);
		
		GUI.Label (new Rect(LoginWindow.width/2 - 100, 260, 200, 30), "E-Mail:");
		Mail = GUI.TextField(new Rect(LoginWindow.width/2 - 100, 295, 200, 30), Mail, 30);
		

		if(!Sent && (GUI.Button ( new Rect(LoginWindow.width/2 - 100, 335, 200, 50), "Register") || Event.current.keyCode == KeyCode.Return || Event.current.keyCode == KeyCode.KeypadEnter) && !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && Mail.Contains("@") && Mail.Contains(".")){
				
				ISFSObject Credentials = new SFSObject();
				Credentials.PutUtfString("Username", Username.Trim());
				Credentials.PutUtfString("Password", Password.Trim());
				Credentials.PutUtfString("E-Mail", Mail.Trim());
				smartFox.Send(new ExtensionRequest("Register", Credentials));
				
				Sent = true;
				
			}

		
		if(GUI.Button ( new Rect(350, 120, 100, 35), "Login")){
			
			Joined = true;
			Info.IsReg = false;
			
		}
		
		GUI.TextArea (new Rect(LoginWindow.width/2 - 380/2, 400, 380, 25), msg, 500);
		
	}
	
	
	//REGISTER WINDOW
	
	
	
	//--------------------------------------------------------------------------------------
	
	
	
	//LISTENERS
	
	
	public void OnConnection(BaseEvent evt) {
		
		bool success = (bool)evt.Params["success"];
		string error = (string)evt.Params["errorMessage"];
		
		Debug.Log("On Connection callback got: " + success + " (error : <" + error + ">)");
		
		if (success) {
			SmartFoxConnection.Connection = smartFox;
		}
		
		smartFox.Send(new LoginRequest("", "", zone));
	}
	
	
	public void OnConnectionLost(BaseEvent evt) {
		// Reset all internal states so we kick back to login screen
		Debug.Log("OnConnectionLost");
	}
	
	public void OnLogin(BaseEvent evt) {
		Debug.Log("Logged in successfully");
		
		// We either create the Game Room or join it if it exists already
		if (smartFox.RoomManager.ContainsRoom(Room)) {
			smartFox.Send(new JoinRoomRequest(Room));
			
		} else {
			RoomSettings settings = new RoomSettings(Room);
			settings.MaxUsers = 100;
			
			smartFox.Send(new CreateRoomRequest(settings, true));
		}
	}
	
	public void OnLoginError(BaseEvent evt) {
		Debug.Log("Login error: "+(string)evt.Params["errorMessage"]);
	}
	
	public void OnRoomJoin(BaseEvent evt) {
		Debug.Log("Joined room successfully");
		Joined = true;
		// Room was joined - lets load the game and remove all the listeners from this component
	}
	
	void OnLogout(BaseEvent evt) {
		Debug.Log("OnLogout");
	}
	
	public void OnDebugMessage(BaseEvent evt) {
		string message = (string)evt.Params["message"];
		Debug.Log("[SFS DEBUG] " + message);
	}
	
	
	
	//LISTENERS
	
	
	
	//------------------------------------------------------------------------------------
	
	
	
	//MYSQL
	
	
	void OnExtensionResponse(BaseEvent evt){
		
		
		//LOGIN
		
		
		if((string)evt.Params["cmd"] == "Login") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			
			if(parameters.ContainsKey("Result")){
				
				msg = "Wrong credentials or not registred.";
				Sent = false;

			}
			else if(parameters.ContainsKey("PlayerID")){
				
				Info.PlayerID = parameters.GetInt("PlayerID");
				
				Info.haschar = parameters.GetBool("HasChar");
				
				Scene = parameters.GetUtfString("Location");
				
				smartFox.RemoveAllEventListeners();
				
				Application.LoadLevel(Scene);
				
			}			
			else if(parameters.ContainsKey("Deleted")) {
				
				msg = "Account Deleted";
				Sent = false;

			}
			
		}
		
		
		//LOGIN
		
		
		
		
		//REGISTER
		
		
		if((string)evt.Params["cmd"] == "Register") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			if(parameters.ContainsKey("Result")){
				
				msg = "Username or E-Mail already registered.";
				Sent = false;

			}
			else if(parameters.ContainsKey("PlayerID")){
				
				Info.PlayerID = parameters.GetInt("PlayerID");
				
				smartFox.RemoveAllEventListeners();
				Application.LoadLevel("Southbridge");
				
			}
			
		}		
		
		
		//REGISTER	
		
		
		
		
		//TEST
		
		
		if((string)evt.Params["cmd"] == "lol") {
			
			ISFSObject parameters = (SFSObject)evt.Params["params"];
			
			if(parameters.ContainsKey("lol")){	
				
				Application.LoadLevel("Treefolk");	
				
			}
			
		}
		
		//TEST
		
		
	}
	
	
	//MYSQL
	
	
}