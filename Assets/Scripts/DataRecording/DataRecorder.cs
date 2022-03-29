using System;
using System.Collections;
using System.Collections.Generic;
using ClosedXML.Excel;
using EmojiWall;
using Normal.Realtime;
using UnityEngine;

namespace DataRecording
{
    public class DataRecorder : MonoBehaviour
    {
        private XLWorkbook _xlWorkbook;

        private int _angryEmojiCounter;
        private int _neutralEmojiCounter;
        private int _sadEmojiCounter;

        public List<RealtimeAvatar> CurrentConnectedPlayers { get; private set; }

        private void Awake()
        {
            CurrentConnectedPlayers = new List<RealtimeAvatar>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(CheckForConnectedPlayers), 5f, 10f);
            if (EmojiSynchronizedSound.emojiActivated != null)
                EmojiSynchronizedSound.emojiActivated.AddListener(OnEmojiActivated);

            _xlWorkbook = new XLWorkbook();

            string[] headers = { "Data", "Balafon", "Drum", "Angry Emoji", "Sad Emoji", "Neutral Emoji", "Head" };
            string[] columns = { "Interaction Amount", "Interaction Strength" };
            _xlWorkbook.Worksheets.Add("Data").Cell(1, 1).InsertData(headers, true);
            _xlWorkbook.Worksheet("Data").Cell(2, 1).InsertData(columns, false);
            _xlWorkbook.Worksheet("Data").Row(1).Style.Font.SetBold();

            StartCoroutine(SaveWorkbookRepeating(_xlWorkbook, 15, 30));
        }

        private IEnumerator SaveWorkbookRepeating(XLWorkbook workbook, float startTime, float repeatTime)
        {
            var startTimeElapsed = false;

            while (true)
                if (!startTimeElapsed)
                {
                    startTimeElapsed = true;
                    yield return new WaitForSeconds(startTime);
                }
                else
                {
                    SaveWorkbookToFile(workbook);
                    yield return new WaitForSeconds(repeatTime);
                }
        }
        
        private void SaveWorkbookToFile(XLWorkbook workbook)
        {
            workbook.SaveAs("./Report/" + DateTime.Today.ToString("yy-MM-dd") + ".xlsx");
            Debug.Log("Workbook Saved");
        }

        private void OnEmojiActivated(string emojiName)
        {
            if (emojiName.Equals("AngryEmoji"))
            {
                _angryEmojiCounter++;
                _xlWorkbook.Worksheet("Data").Cell("D2").SetValue(_angryEmojiCounter);
            }

            if (emojiName.Equals("SadEmoji"))
            {
                _sadEmojiCounter++;
                _xlWorkbook.Worksheet("Data").Cell("E2").SetValue(_sadEmojiCounter);
            }

            if (emojiName.Equals("NeutralEmoji"))
            {
                _neutralEmojiCounter++;
                _xlWorkbook.Worksheet("Data").Cell("F2").SetValue(_neutralEmojiCounter);
            }
        }

        private void CheckForConnectedPlayers()
        {
            Debug.Log("Checking for connected players...");

            var players = FindObjectsOfType<RealtimeAvatar>();

            if (players.Equals(CurrentConnectedPlayers)) return;

            CurrentConnectedPlayers.Clear();
            CurrentConnectedPlayers.AddRange(players);
        }
    }
}