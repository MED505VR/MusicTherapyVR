using System;
using System.Collections;
using System.Collections.Generic;
using ClosedXML.Excel;
using EmojiWall;
using Normal.Realtime;
using UnityEngine;
using Sound;

namespace DataRecording
{
    public class DataRecorder : MonoBehaviour
    {
        private XLWorkbook _xlWorkbook;

        private int _angryEmojiCounter;
        private int _neutralEmojiCounter;
        private int _sadEmojiCounter;
        private int _balafonCounterkey1, _balafonCounterkey2, _balafonCounterkey3, _balafonCounterkey4, _balafonCounterkey5, _balafonCounterkey6, _balafonCounterkey7, _balafonCounterkey8, _balafonCounterkey9, _balafonCounterkey10, _balafonCounterkey11, _balafonCounterkey12;
        private int _drumCounter;
        
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
            
            if (XylophoneKey.instrumentHit !=null)
                XylophoneKey.instrumentHit.AddListener(OnXylophoneKeyActivated);

            _xlWorkbook = new XLWorkbook();

            string[] headers = { "Data", "Balafon", "Drum", "Angry Emoji", "Sad Emoji", "Neutral Emoji", "Head" };
            string[] columns = { "Interaction Amount", "Interaction Strength" };
            _xlWorkbook.Worksheets.Add("Data").Cell(1, 1).InsertData(headers, true);
            _xlWorkbook.Worksheet("Data").Cell(2, 1).InsertData(columns, false);
            _xlWorkbook.Worksheet("Data").Row(1).Style.Font.SetBold();
            
            string[] bHeaders = { "Data", "Big drum", "Balafon key1", "Balafon key2", "Balafon key3", "Balafon key4", "Balafon key5", "Balafon key6", "Balafon key7", "Balafon key8", "Balafon key9", "Balafon key10", "Balafon key11", "Balafon key12",};
            string[] bColumns = { "Interaction Amount", "Interaction Strength" };
            _xlWorkbook.Worksheets.Add("Balafon data").Cell(4, 1).InsertData(bHeaders, true);
            _xlWorkbook.Worksheet("Balafon data").Cell(5, 1).InsertData(bColumns, false);
            _xlWorkbook.Worksheet("Balafon data").Row(4).Style.Font.SetBold();

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

        private void OnXylophoneKeyActivated(string objectName)
        {
            if (objectName.Equals("drumCollider"))
            {
                _drumCounter++;
                _xlWorkbook.Worksheet("Balafon data").Cell("B5").SetValue(_drumCounter);
            }
            
            if (objectName.Equals("key1"))
            {
                _balafonCounterkey1++;
                _xlWorkbook.Worksheet("Balafon data").Cell("C5").SetValue(_balafonCounterkey1);
            }
            
            if (objectName.Equals("key2"))
            {
                _balafonCounterkey2++;
                _xlWorkbook.Worksheet("Balafon data").Cell("D5").SetValue(_balafonCounterkey2);
            }
            
            if (objectName.Equals("key3"))
            {
                _balafonCounterkey3++;
                _xlWorkbook.Worksheet("Balafon data").Cell("E5").SetValue(_balafonCounterkey3);
            }
            
            if (objectName.Equals("key4"))
            {
                _balafonCounterkey4++;
                _xlWorkbook.Worksheet("Balafon data").Cell("F5").SetValue(_balafonCounterkey4);
            }
            
            if (objectName.Equals("key5"))
            {
                _balafonCounterkey5++;
                _xlWorkbook.Worksheet("Balafon data").Cell("G5").SetValue(_balafonCounterkey5);
            }
            
            if (objectName.Equals("key6"))
            {
                _balafonCounterkey6++;
                _xlWorkbook.Worksheet("Balafon data").Cell("H5").SetValue(_balafonCounterkey6);
            }
            
            if (objectName.Equals("key7"))
            {
                _balafonCounterkey7++;
                _xlWorkbook.Worksheet("Balafon data").Cell("I5").SetValue(_balafonCounterkey7);
            }
            
            if (objectName.Equals("key8"))
            {
                _balafonCounterkey8++;
                _xlWorkbook.Worksheet("Balafon data").Cell("J5").SetValue(_balafonCounterkey8);
            }
            
            if (objectName.Equals("key9"))
            {
                _balafonCounterkey9++;
                _xlWorkbook.Worksheet("Balafon data").Cell("K5").SetValue(_balafonCounterkey9);
            }
            
            if (objectName.Equals("key10"))
            {
                _balafonCounterkey10++;
                _xlWorkbook.Worksheet("Balafon data").Cell("L5").SetValue(_balafonCounterkey10);
            }
            
            if (objectName.Equals("key11"))
            {
                _balafonCounterkey11++;
                _xlWorkbook.Worksheet("Balafon data").Cell("M5").SetValue(_balafonCounterkey11);
            }
            
            if (objectName.Equals("key12"))
            {
                _balafonCounterkey12++;
                _xlWorkbook.Worksheet("Balafon data").Cell("N5").SetValue(_balafonCounterkey12);
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