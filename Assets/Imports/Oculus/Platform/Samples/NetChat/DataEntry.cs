namespace Oculus.Platform.Samples.NetChat
{
    using UnityEngine;
    using UnityEngine.UI;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Platform;
    using Models;

    internal enum states
    {
        NOT_INIT = 0,
        IDLE,
        REQUEST_FIND,
        FINDING_ROOM,
        REQUEST_CREATE,
        REQUEST_JOIN,
        REQUEST_LEAVE,
        IN_EMPTY_ROOM,
        IN_FULL_ROOM
    }

    // Pools are defined on the Oculus developer portal
    //
    // For this test we have a pool created with the pool key set as 'filter_pool'
    // Mode is set to 'Room'
    // Skill Pool is set to 'None'
    // We are not considering Round Trip Time
    // The following Data Settings are set:
    //  key: map_name, Type: STRING, String options: Small_Map, Big_Map, Really_Big_Map
    //  key: game_type, Type: STRING, String Options: deathmatch, CTF
    //
    // We also have the following two queries defined:
    //  Query Key: map
    //  Template: Set (String)
    //  Key: map_name
    //  Wildcards: map_param_1, map_param_2
    //
    //  Query Key: game_type
    //  Template: Set (String)
    //  Key: game_type_name
    //  Wildcards: game_type_param
    //
    // For this test we have a pool created with the pool key set as 'bout_pool'
    // Mode is set to 'Bout'
    // Skill Pool is set to 'None'
    // We are not considering Round Trip Time
    // No Data Settings are set:
    //

    public static class Constants
    {
        public const int BUFFER_SIZE = 512;
        public const string BOUT_POOL = "bout_pool";
        public const string FILTER_POOL = "filter_pool";
    }

    public class chatPacket
    {
        public int packetID { get; set; }
        public string textString { get; set; }

        public byte[] Serialize()
        {
            using (var m = new MemoryStream())
            {
                using (var writer = new BinaryWriter(m))
                {
                    // Limit our string to BUFFER_SIZE
                    if (textString.Length > Constants.BUFFER_SIZE)
                        textString = textString.Substring(0, Constants.BUFFER_SIZE - 1);
                    writer.Write(packetID);
                    writer.Write(textString.ToCharArray());
                    writer.Write('\0');
                }

                return m.ToArray();
            }
        }

        public static chatPacket Deserialize(byte[] data)
        {
            var result = new chatPacket();
            using (var m = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(m))
                {
                    result.packetID = reader.ReadInt32();
                    result.textString = System.Text.Encoding.Default.GetString(reader.ReadBytes(Constants.BUFFER_SIZE));
                }
            }

            return result;
        }
    }

    public class DataEntry : MonoBehaviour
    {
        public Text dataOutput;

        private states currentState;
        private User localUser;
        private User remoteUser;
        private Room currentRoom;
        private int lastPacketID;
        private bool ratedMatchStarted;

        // Use this for initialization
        private void Start()
        {
            currentState = states.NOT_INIT;
            localUser = null;
            remoteUser = null;
            currentRoom = null;
            lastPacketID = 0;
            ratedMatchStarted = false;

            Core.Initialize();

            // Setup our room update handler
            Rooms.SetUpdateNotificationCallback(updateRoom);
            // Setup our match found handler
            Matchmaking.SetMatchFoundNotificationCallback(foundMatch);

            checkEntitlement();
        }

        // Update is called once per frame
        private void Update()
        {
            var currentText = GetComponent<InputField>().text;

            if (Input.GetKey(KeyCode.Return))
            {
                if (currentText != "") SubmitCommand(currentText);

                GetComponent<InputField>().text = "";
            }

            processNetPackets();
            // Handle all messages being returned
            Request.RunCallbacks();
        }

        private void SubmitCommand(string command)
        {
            var commandParams = command.Split('!');

            if (commandParams.Length > 0)
                switch (commandParams[0])
                {
                    case "c":
                        requestCreateRoom();
                        break;
                    case "d":
                        requestCreateFilterRoom();
                        break;
                    case "f":
                        requestFindMatch();
                        break;
                    case "g":
                        requestFindRoom();
                        break;
                    case "i":
                        requestFindFilteredRoom();
                        break;
                    case "s":
                        if (commandParams.Length > 1) sendChat(commandParams[1]);
                        break;
                    case "l":
                        requestLeaveRoom();
                        break;
                    case "1":
                        requestStartRatedMatch();
                        break;
                    case "2":
                        requestReportResults();
                        break;
                    default:
                        printOutputLine("Invalid Command");
                        break;
                }
        }

        private void printOutputLine(string newLine)
        {
            dataOutput.text = "> " + newLine + Environment.NewLine + dataOutput.text;
        }

        private void checkEntitlement()
        {
            Entitlements.IsUserEntitledToApplication().OnComplete(getEntitlementCallback);
        }

        private void getEntitlementCallback(Message msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("You are entitled to use this app.");
                Users.GetLoggedInUser().OnComplete(init);
            }
            else
            {
                printOutputLine("You are NOT entitled to use this app.");
            }
        }

        private void init(Message<User> msg)
        {
            if (!msg.IsError)
            {
                var user = msg.Data;
                localUser = user;

                currentState = states.IDLE;
            }
            else
            {
                printOutputLine("Received get current user error");
                var error = msg.GetError();
                printOutputLine("Error: " + error.Message);

                // Retry getting the current user
                Users.GetLoggedInUser().OnComplete(init);
                currentState = states.NOT_INIT;
            }
        }

        private void requestCreateRoom()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;
                case states.IDLE:
                    printOutputLine("Trying to create a matchmaking room");
                    Matchmaking.CreateAndEnqueueRoom(Constants.FILTER_POOL, 8, true, null)
                        .OnComplete(createRoomResponse);
                    currentState = states.REQUEST_CREATE;
                    break;
                case states.REQUEST_FIND:
                    printOutputLine(
                        "You have already made a request to find a room.  Please wait for that request to complete.");
                    break;
                case states.FINDING_ROOM:
                    printOutputLine(
                        "You have already currently looking for a room.  Please wait for the match to be made.");
                    break;
                case states.REQUEST_JOIN:
                    printOutputLine("We are currently trying to join a room.  Please wait to see if we can join it.");
                    break;
                case states.REQUEST_LEAVE:
                    printOutputLine("We are currently trying to leave a room.  Please wait to see if we can leave it.");
                    break;
                case states.REQUEST_CREATE:
                    printOutputLine(
                        "You have already requested a matchmaking room to be created.  Please wait for the room to be made.");
                    break;
                case states.IN_EMPTY_ROOM:
                    printOutputLine("You have already in a matchmaking room.  Please wait for an opponent to join.");
                    break;
                case states.IN_FULL_ROOM:
                    printOutputLine("You have already in a match.");
                    break;
                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void createRoomResponse(Message<MatchmakingEnqueueResultAndRoom> msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("Received create matchmaking room success");
                var room = msg.Data.Room;
                currentRoom = room;

                printOutputLine("RoomID: " + room.ID.ToString());
                currentState = states.IN_EMPTY_ROOM;
            }
            else
            {
                printOutputLine("Received create matchmaking room Error");
                var error = msg.GetError();
                printOutputLine("Error: " + error.Message);
                printOutputLine(
                    "You can only create a matchmaking room for pools of mode Room.  Make sure you have an appropriate pool setup on the Developer portal.\n");
                currentState = states.IDLE;
            }
        }

        private void requestCreateFilterRoom()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.\n");
                    break;

                case states.IDLE:
                    printOutputLine("Trying to create a matchmaking room");

                    // We're going to create a room that has the following values set:
                    // game_type_name = "CTF"
                    // map_name = "Really_Big_Map"
                    //

                    var roomCustomQuery = new Matchmaking.CustomQuery();

                    roomCustomQuery.criteria = null;
                    roomCustomQuery.data = new Dictionary<string, object>();

                    roomCustomQuery.data.Add("game_type_name", "CTF");
                    roomCustomQuery.data.Add("map_name", "Really_Big_Map");

                    Matchmaking.CreateAndEnqueueRoom(Constants.FILTER_POOL, 8, true, roomCustomQuery)
                        .OnComplete(createRoomResponse);
                    currentState = states.REQUEST_CREATE;
                    break;

                case states.REQUEST_FIND:
                    printOutputLine(
                        "You have already made a request to find a room.  Please wait for that request to complete.\n");
                    break;
                case states.FINDING_ROOM:
                    printOutputLine(
                        "You have already currently looking for a room.  Please wait for the match to be made.\n");
                    break;
                case states.REQUEST_JOIN:
                    printOutputLine("We are currently trying to join a room.  Please wait to see if we can join it.\n");
                    break;
                case states.REQUEST_LEAVE:
                    printOutputLine(
                        "We are currently trying to leave a room.  Please wait to see if we can leave it.\n");
                    break;
                case states.REQUEST_CREATE:
                    printOutputLine(
                        "You have already requested a matchmaking room to be created.  Please wait for the room to be made.\n");
                    break;
                case states.IN_EMPTY_ROOM:
                    printOutputLine("You have already in a matchmaking room.  Please wait for an opponent to join.\n");
                    break;
                case states.IN_FULL_ROOM:
                    printOutputLine("You have already in a match.\n");
                    break;
                default:
                    printOutputLine("You have hit an unknown state.\n");
                    break;
            }
        }

        private void requestFindRoom()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;

                case states.IDLE:
                    printOutputLine("\nTrying to find a matchmaking room\n");

                    Matchmaking.Enqueue(Constants.FILTER_POOL, null).OnComplete(searchingStarted);
                    currentState = states.REQUEST_FIND;
                    break;

                case states.REQUEST_FIND:
                    printOutputLine(
                        "You have already made a request to find a room.  Please wait for that request to complete.");
                    break;

                case states.FINDING_ROOM:
                    printOutputLine(
                        "You have already currently looking for a room.  Please wait for the match to be made.");
                    break;

                case states.REQUEST_JOIN:
                    printOutputLine("We are currently trying to join a room.  Please wait to see if we can join it.");
                    break;

                case states.REQUEST_LEAVE:
                    printOutputLine("We are currently trying to leave a room.  Please wait to see if we can leave it.");
                    break;

                case states.REQUEST_CREATE:
                    printOutputLine(
                        "You have already requested a matchmaking room to be created.  Please wait for the room to be made.");
                    break;

                case states.IN_EMPTY_ROOM:
                    printOutputLine("You have already in a matchmaking room.  Please wait for an opponent to join.");
                    break;

                case states.IN_FULL_ROOM:
                    printOutputLine("You have already in a match.");
                    break;

                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void requestFindFilteredRoom()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;

                case states.IDLE:
                    printOutputLine("Trying to find a matchmaking room");

                    // Our search filter criterion
                    //
                    // We're filtering using two different queries setup on the developer portal
                    //
                    // map - query to filter by map.  The query allows you to filter with up to two different maps using keys called 'map_1' and 'map_2'
                    // game_type - query to filter by game type.  The query allows you to filter with up to two different game types using keys called 'type_1' and 'type_2'
                    //
                    // In the example below we are filtering for matches that are of type CTF and on either Big_Map or Really_Big_Map.
                    //

                    var roomCustomQuery = new Matchmaking.CustomQuery();
                    var queries = new Matchmaking.CustomQuery.Criterion[2];

                    queries[0].key = "map";
                    queries[0].importance = MatchmakingCriterionImportance.Required;
                    queries[0].parameters = new Dictionary<string, object>();
                    queries[0].parameters.Add("map_param_1", "Really_Big_Map");
                    queries[0].parameters.Add("map_param_2", "Big_Map");

                    queries[1].key = "game_type";
                    queries[1].importance = MatchmakingCriterionImportance.Required;
                    queries[1].parameters = new Dictionary<string, object>();
                    queries[1].parameters.Add("game_type_param", "CTF");

                    roomCustomQuery.criteria = queries;
                    roomCustomQuery.data = null;

                    Matchmaking.Enqueue(Constants.FILTER_POOL, roomCustomQuery);
                    currentState = states.REQUEST_FIND;
                    break;

                case states.REQUEST_FIND:
                    printOutputLine(
                        "You have already made a request to find a room.  Please wait for that request to complete.");
                    break;

                case states.FINDING_ROOM:
                    printOutputLine(
                        "You have already currently looking for a room.  Please wait for the match to be made.");
                    break;

                case states.REQUEST_JOIN:
                    printOutputLine("We are currently trying to join a room.  Please wait to see if we can join it.");
                    break;

                case states.REQUEST_LEAVE:
                    printOutputLine("We are currently trying to leave a room.  Please wait to see if we can leave it.");
                    break;

                case states.REQUEST_CREATE:
                    printOutputLine(
                        "You have already requested a matchmaking room to be created.  Please wait for the room to be made.");
                    break;

                case states.IN_EMPTY_ROOM:
                    printOutputLine("You have already in a matchmaking room.  Please wait for an opponent to join.");
                    break;

                case states.IN_FULL_ROOM:
                    printOutputLine("You have already in a match.");
                    break;

                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void foundMatch(Message<Room> msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("Received find match success. We are now going to request to join the room.");
                var room = msg.Data;

                Rooms.Join(room.ID, true).OnComplete(joinRoomResponse);
                currentState = states.REQUEST_JOIN;
            }
            else
            {
                printOutputLine("Received find match error");
                var error = msg.GetError();
                printOutputLine("Error: " + error.Message);
                currentState = states.IDLE;
            }
        }

        private void joinRoomResponse(Message<Room> msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("Received join room success.");
                currentRoom = msg.Data;

                currentState = states.IN_EMPTY_ROOM;

                // Try to pull out remote user's ID if they have already joined
                if (currentRoom.UsersOptional != null)
                    foreach (var element in currentRoom.UsersOptional)
                        if (element.ID != localUser.ID)
                        {
                            remoteUser = element;
                            currentState = states.IN_FULL_ROOM;
                        }
            }
            else
            {
                printOutputLine("Received join room error");
                printOutputLine("It's possible the room filled up before you could join it.");

                var error = msg.GetError();
                printOutputLine("Error: " + error.Message);
                currentState = states.IDLE;
            }
        }

        private void requestFindMatch()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;
                case states.IDLE:
                    printOutputLine("Trying to find a matchmaking room");
                    Matchmaking.Enqueue(Constants.BOUT_POOL, null).OnComplete(searchingStarted);
                    currentState = states.REQUEST_FIND;
                    break;
                case states.REQUEST_FIND:
                    printOutputLine(
                        "You have already made a request to find a room.  Please wait for that request to complete.");
                    break;
                case states.FINDING_ROOM:
                    printOutputLine(
                        "You have already currently looking for a room.  Please wait for the match to be made.");
                    break;
                case states.REQUEST_JOIN:
                    printOutputLine("We are currently trying to join a room.  Please wait to see if we can join it.");
                    break;
                case states.REQUEST_LEAVE:
                    printOutputLine("We are currently trying to leave a room.  Please wait to see if we can leave it.");
                    break;
                case states.REQUEST_CREATE:
                    printOutputLine(
                        "You have already requested a matchmaking room to be created.  Please wait for the room to be made.");
                    break;
                case states.IN_EMPTY_ROOM:
                    printOutputLine("You have already in a matchmaking room.  Please wait for an opponent to join.");
                    break;
                case states.IN_FULL_ROOM:
                    printOutputLine("You have already in a match.");
                    break;
                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void searchingStarted(Message msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("Searching for a match successfully started");
                currentState = states.REQUEST_FIND;
            }
            else
            {
                printOutputLine("Searching for a match error");

                var error = msg.GetError();
                printOutputLine("Error: " + error.Message);
            }
        }

        private void updateRoom(Message<Room> msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("Received room update notification");
                var room = msg.Data;

                if (currentState == states.IN_EMPTY_ROOM)
                {
                    // Check to see if this update is another user joining
                    if (room.UsersOptional != null)
                        foreach (var element in room.UsersOptional)
                            if (element.ID != localUser.ID)
                            {
                                remoteUser = element;
                                currentState = states.IN_FULL_ROOM;
                            }
                }
                else
                {
                    // Check to see if this update is another user leaving
                    if (room.UsersOptional != null && room.UsersOptional.Count == 1)
                    {
                        printOutputLine("User ID: " + remoteUser.ID.ToString() + "has left");
                        remoteUser = null;
                        currentState = states.IN_EMPTY_ROOM;
                    }
                }
            }
            else
            {
                printOutputLine("Received room update error");

                var error = msg.GetError();
                printOutputLine("Error: " + error.Message);
            }
        }

        private void sendChat(string chatMessage)
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;

                case states.IDLE:
                case states.REQUEST_FIND:
                case states.FINDING_ROOM:
                case states.REQUEST_JOIN:
                case states.REQUEST_CREATE:
                case states.REQUEST_LEAVE:
                case states.IN_EMPTY_ROOM:
                    printOutputLine("You need to be in a room with another player to send a message.");
                    break;

                case states.IN_FULL_ROOM:
                {
                    var newMessage = new chatPacket();

                    // Create a packet to send with the packet ID and string payload
                    lastPacketID++;
                    newMessage.packetID = lastPacketID;
                    newMessage.textString = chatMessage;

                    Net.SendPacket(remoteUser.ID, newMessage.Serialize(), SendPolicy.Reliable);
                }
                    break;

                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void processNetPackets()
        {
            var incomingPacket = Net.ReadPacket();

            while (incomingPacket != null)
            {
                var rawBits = new byte[incomingPacket.Size];
                incomingPacket.ReadBytes(rawBits);

                var newMessage = chatPacket.Deserialize(rawBits);

                printOutputLine("Chat Text: " + newMessage.textString.ToString());
                printOutputLine("Received Packet from UserID: " + incomingPacket.SenderID.ToString());
                printOutputLine("Received Packet ID: " + newMessage.packetID.ToString());

                // Look to see if there's another packet waiting
                incomingPacket = Net.ReadPacket();
            }
        }

        private void requestLeaveRoom()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;

                case states.IDLE:
                case states.REQUEST_FIND:
                case states.FINDING_ROOM:
                case states.REQUEST_JOIN:
                case states.REQUEST_CREATE:
                    printOutputLine("You are currently not in a room to leave.");
                    break;

                case states.REQUEST_LEAVE:
                    printOutputLine("We are currently trying to leave a room.  Please wait to see if we can leave it.");
                    break;

                case states.IN_EMPTY_ROOM:
                case states.IN_FULL_ROOM:
                    printOutputLine("Trying to leave room.");
                    Rooms.Leave(currentRoom.ID).OnComplete(leaveRoomResponse);
                    break;

                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void leaveRoomResponse(Message<Room> msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("We were able to leave the room");
                currentRoom = null;
                remoteUser = null;
                currentState = states.IDLE;
            }
            else
            {
                printOutputLine("Leave room error");

                var error = msg.GetError();
                printOutputLine("Error: " + error.Message);
            }
        }

        private void requestStartRatedMatch()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;

                case states.IDLE:
                case states.REQUEST_FIND:
                case states.FINDING_ROOM:
                case states.REQUEST_JOIN:
                case states.REQUEST_CREATE:
                case states.REQUEST_LEAVE:
                case states.IN_EMPTY_ROOM:
                    printOutputLine("You need to be in a room with another player to start a rated match.");
                    break;

                case states.IN_FULL_ROOM:
                    printOutputLine(
                        "Trying to start a rated match.  This call should be made once a rated match begins so we will be able to submit results after the game is done.");

                    Matchmaking.StartMatch(currentRoom.ID).OnComplete(startRatedMatchResponse);
                    break;

                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void startRatedMatchResponse(Message msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("Started a rated match");
                ratedMatchStarted = true;
            }
            else
            {
                var error = msg.GetError();
                printOutputLine("Received starting rated match failure: " + error.Message);
                printOutputLine(
                    "Your matchmaking pool needs to have a skill pool associated with it to play rated matches");
            }
        }

        private void requestReportResults()
        {
            switch (currentState)
            {
                case states.NOT_INIT:
                    printOutputLine("The app has not initialized properly and we don't know your userID.");
                    break;

                case states.IDLE:
                case states.REQUEST_FIND:
                case states.FINDING_ROOM:
                case states.REQUEST_JOIN:
                case states.REQUEST_CREATE:
                case states.REQUEST_LEAVE:
                    printOutputLine("You need to be in a room with another player to report results on a rated match.");
                    break;

                case states.IN_EMPTY_ROOM:
                case states.IN_FULL_ROOM:
                    if (ratedMatchStarted)
                    {
                        printOutputLine("Submitting rated match results.");

                        var results = new Dictionary<string, int>();
                        results.Add(localUser.ID.ToString(), 1);
                        results.Add(remoteUser.ID.ToString(), 2);

                        Matchmaking.ReportResultsInsecure(currentRoom.ID, results).OnComplete(reportResultsResponse);
                    }
                    else
                    {
                        printOutputLine("You can't report results unless you've already started a rated match");
                    }

                    break;

                default:
                    printOutputLine("You have hit an unknown state.");
                    break;
            }
        }

        private void reportResultsResponse(Message msg)
        {
            if (!msg.IsError)
            {
                printOutputLine("Rated match results reported. Now attempting to leave room.");
                ratedMatchStarted = false;
                requestLeaveRoom();
            }
            else
            {
                var error = msg.GetError();
                printOutputLine("Received reporting rated match failure: " + error.Message);
            }
        }
    }
}