using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ClosedXML.Excel;
using Normal.Realtime;
using Sound;
using UnityEngine;

namespace DataRecording
{
    public class DataRecorder : MonoBehaviour
    {
        private int _angryEmojiCounter;
        private int[] _balafonCounters;
        private List<List<float>> _hitStrenght;
        private int _drumCounter;
        private int _neutralEmojiCounter;
        private int _sadEmojiCounter;
        private XLWorkbook _xlWorkbook;

        public List<RealtimeAvatar> CurrentConnectedPlayers { get; private set; }

        private void Awake()
        {
            _balafonCounters = new int[12];
            CurrentConnectedPlayers = new List<RealtimeAvatar>();

            _hitStrenght = new List<List<float>>();

            for (int i = 0; i < 13; i++)
            {
                _hitStrenght.Add(new List<float>());
            }
        }

        private void Start()
        {
            InvokeRepeating(nameof(CheckForConnectedPlayers), 5f, 10f);
            SynchronizedSound.SynchronizedSoundIsFired += OnSynchronizedSoundIsFired;
            SynchronizedSound.SoundInteractionStrengthIsUsed += OnInteractionStrengthIsUsed;

            _xlWorkbook = new XLWorkbook();

            string[] headers = { "Data", "Balafon", "Drum", "Angry Emoji", "Sad Emoji", "Neutral Emoji", "Head" };
            string[] columns = { "Interaction Amount", "Interaction Strength" };
            _xlWorkbook.Worksheets.Add("Data").Cell(1, 1).InsertData(headers, true);
            _xlWorkbook.Worksheet("Data").Cell(2, 1).InsertData(columns, false);
            _xlWorkbook.Worksheet("Data").Row(1).Style.Font.SetBold();

            string[] bHeaders =
            {
                "Data", "Big drum", "Balafon key1", "Balafon key2", "Balafon key3", "Balafon key4", "Balafon key5",
                "Balafon key6", "Balafon key7", "Balafon key8", "Balafon key9", "Balafon key10", "Balafon key11",
                "Balafon key12"
            };
            
            string[] bColumns = { "Interaction Amount", "Interaction Strength" };
            _xlWorkbook.Worksheets.Add("Balafon data").Cell(1, 1).InsertData(bHeaders, true);
            _xlWorkbook.Worksheet("Balafon data").Cell(2, 1).InsertData(bColumns, false);
            _xlWorkbook.Worksheet("Balafon data").Row(1).Style.Font.SetBold();

            StartCoroutine(SaveWorkbookRepeating(_xlWorkbook, 15, 30));
        }

        private void OnSynchronizedSoundIsFired(string objectName)
        {
            switch (objectName)
            {
                case "AngryEmoji":
                    _angryEmojiCounter++;
                    _xlWorkbook.Worksheet("Data").Cell("D2").SetValue(_angryEmojiCounter);
                    break;

                case "SadEmoji":
                    _sadEmojiCounter++;
                    _xlWorkbook.Worksheet("Data").Cell("E2").SetValue(_sadEmojiCounter);
                    break;

                case "NeutralEmoji":
                    _neutralEmojiCounter++;
                    _xlWorkbook.Worksheet("Data").Cell("F2").SetValue(_neutralEmojiCounter);
                    break;

                case "drumCollider":
                    _drumCounter++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("B2").SetValue(_drumCounter);
                    break;

                case "key1":
                    _balafonCounters[0]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("C2").SetValue(_balafonCounters[0]);
                    break;

                case "key2":
                    _balafonCounters[1]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("D2").SetValue(_balafonCounters[1]);
                    break;

                case "key3":
                    _balafonCounters[2]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("E2").SetValue(_balafonCounters[2]);
                    break;

                case "key4":
                    _balafonCounters[3]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("F2").SetValue(_balafonCounters[3]);
                    break;

                case "key5":
                    _balafonCounters[4]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("G2").SetValue(_balafonCounters[4]);
                    break;

                case "key6":
                    _balafonCounters[5]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("H2").SetValue(_balafonCounters[5]);
                    break;

                case "key7":
                    _balafonCounters[6]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("I2").SetValue(_balafonCounters[6]);
                    break;

                case "key8":
                    _balafonCounters[7]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("J2").SetValue(_balafonCounters[7]);
                    break;

                case "key9":
                    _balafonCounters[8]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("K2").SetValue(_balafonCounters[8]);
                    break;

                case "key10":
                    _balafonCounters[9]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("L2").SetValue(_balafonCounters[9]);
                    break;

                case "key11":
                    _balafonCounters[10]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("M2").SetValue(_balafonCounters[10]);
                    break;

                case "key12":
                    _balafonCounters[11]++;
                    _xlWorkbook.Worksheet("Balafon data").Cell("N2").SetValue(_balafonCounters[11]);
                    break;

                default:
                    Debug.Log(objectName + ": Is not a recognized object.");
                    break;
            }
        }
        
        private void OnInteractionStrengthIsUsed(string objectName, float value)
        {
            switch (objectName)
            {
                case "drumCollider":
                    _hitStrenght[0].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("B3").SetValue(_hitStrenght[0].Average());
                    break;

                case "key1":
                    _hitStrenght[1].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("C3").SetValue(_hitStrenght[1].Average());
                    break;

                case "key2":
                    _hitStrenght[2].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("D3").SetValue(_hitStrenght[2].Average());
                    break;

                case "key3":
                    _hitStrenght[3].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("E3").SetValue(_hitStrenght[3].Average());
                    break;

                case "key4":
                    _hitStrenght[4].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("F3").SetValue(_hitStrenght[4].Average());
                    break;

                case "key5":
                    _hitStrenght[5].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("G3").SetValue(_hitStrenght[5].Average());
                    break;

                case "key6":
                    _hitStrenght[6].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("H3").SetValue(_hitStrenght[6].Average());
                    break;

                case "key7":
                    _hitStrenght[7].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("I3").SetValue(_hitStrenght[7].Average());
                    break;

                case "key8":
                    _hitStrenght[8].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("J3").SetValue(_hitStrenght[8].Average());
                    break;

                case "key9":
                    _hitStrenght[9].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("K3").SetValue(_hitStrenght[9].Average());
                    break;

                case "key10":
                    _hitStrenght[10].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("L3").SetValue(_hitStrenght[10].Average());
                    break;

                case "key11":
                    _hitStrenght[11].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("M3").SetValue(_hitStrenght[11].Average());
                    break;

                case "key12":
                    _hitStrenght[12].Add(value);
                    _xlWorkbook.Worksheet("Balafon data").Cell("N3").SetValue(_hitStrenght[12].Average());
                    break;

                default:
                    Debug.Log(objectName + ": Is not a recognized object.");
                    break;
            }
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