using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Looxid.Link;
using UnityEngine.PlayerLoop;
using System;
using System.IO;
using System.Text;
using Photon.Pun;

public class EEG : MonoBehaviourPunCallbacks, IPunObservable
{

    // Start is called before the first frame update
    public bool record = false;
    public float timeTotal = 61;
    public float timeRemaining=0;
    private string outputPath;
    public string fileName;

    void Start()
    {
        //outputPath = @"./Assets/RecordedEEGData/data1.csv";
        outputPath = @"./Assets/RecordedEEGData/" + fileName;

        LooxidLinkManager.Instance.Initialize();
        OnEnable();         

    }

    // Update is called once per frame
    void Update()
    {
        //print("Accessing update method");
        // Keystroke to start and stop recording if the 'f' key
        if (Input.GetKeyDown("space"))
        {
            //print("Switch!!!!!!!!!");
            record = !record;
            timeRemaining = timeTotal;
           
            
        }
         if (record == true)
        {
            TimeCounter();
        }
    }

    private void TimeCounter()
    {


        // Counting the time
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
          
        }
        else
        {
            record = false;
            Debug.Log("Time has run out!");
        }
    }

    /// <summary>
    /// </summary>
    void OnEnable()
    {
        LooxidLinkData.OnReceiveEEGRawSignals += OnReceiveEEGRawSignals;
    }
    void OnDisable()
    {
        LooxidLinkData.OnReceiveEEGRawSignals -= OnReceiveEEGRawSignals;
    }


    void OnReceiveEEGRawSignals(EEGRawSignal rawSignalData)
    {

        if (record == true)
        {
            if (!File.Exists(outputPath))
            {
                using (new FileStream(outputPath, FileMode.CreateNew)) { }
                string output = "timestamp,AF3,AF4,Fp1,Fp2,AF7,AF8,reference,ground\n";
                //File.AppendAllText(outputPath, output); 
                
                File.WriteAllText(outputPath, output);
            }
            Debug.Log("New data received from LooxID!");

            // Loop over row (the number of timepoints)
            for (int i = 0; i < rawSignalData.rawSignal.Count; i++)
            {
                               
                // To get the a better synchronized timestamp use the looxID function here
                string output = LooxidLinkUtility.GetTimeSynchronizedUTCTimestamp().ToString();

                // Loop over the column for the number of channels
                // To get the data for each channel, ground, and reference
                for (int j = 0; j < rawSignalData.rawSignal[i].ch_data.Length; j++)
                {
                    output = output + "," + rawSignalData.rawSignal[i].ch_data[j];
                }
                
                File.AppendAllText(outputPath, output + "\n");

             

                // Backup first

                //if (!File.Exists(@"C:\Ihshan\EEGData\EEG1.csv"))
                //{
                //    string path = @"C:\Ihshan\EEGData\EEG1.csv";
                //    using (new FileStream(path, FileMode.CreateNew)) { }
                //    string output = "timestamp,AF3,AF4,Fp1,Fp2,AF7,AF8,reference,ground\n";
                //    File.AppendAllText(path, output);
                //}
                //Debug.Log("New data received!");


                //// Loop over row (the number of timepoints)
                //for (int i = 0; i < rawSignalData.rawSignal.Count; i++)
                //{

                //    // string output = rawSignalData.rawSignal[i].timestamp.ToString(); Delete this later

                //    // To get the a better synchronized timestamp use the looxID function here
                //    string output = LooxidLinkUtility.GetTimeSynchronizedUTCTimestamp().ToString();

                //    // Loop over the column for the number of channels

                //    // To get the data for each channel, ground, and reference
                //    for (int j = 0; j < rawSignalData.rawSignal[i].ch_data.Length; j++)
                //    {
                //        output = output + "," + rawSignalData.rawSignal[i].ch_data[j];
                //    }

                // Add the data into CSV file

                //string path = @"C:\Ihshan\EEGData\EEG1.csv";
                //output = output + "\n";
                //File.AppendAllText(path, output);

            }
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            Debug.Log("Is Writing");
            stream.SendNext(record);
        }
        else
        {
            Debug.Log("Is Reading");
            record = (bool)stream.ReceiveNext();
            timeRemaining = timeTotal;
        }
    }
}
