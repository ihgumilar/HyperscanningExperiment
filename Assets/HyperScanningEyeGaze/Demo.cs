using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Looxid.Link;
using UnityEngine.PlayerLoop;
using System;
using System.IO;
using System.Text;

public class Demo : MonoBehaviour
{
    bool status;

    void Start()
    {
        LooxidLinkManager.Instance.Initialize();
        OnEnable();

        string path = @"C:\Users\dcopp\Desktop\DataStorageTest\test.txt";
        using (new FileStream(path, FileMode.CreateNew)) { }

    }


    void OnEnable()
    {
        LooxidLinkData.OnReceiveEEGSensorStatus += OnReceiveEEGSensorStatus;
        LooxidLinkData.OnReceiveEEGFeatureIndexes += OnReceiveEEGFeatureIndexes;
        LooxidLinkData.OnReceiveEEGRawSignals += OnReceiveEEGRawSignals;
    }
    void OnDisable()
    {
        LooxidLinkData.OnReceiveEEGSensorStatus -= OnReceiveEEGSensorStatus;
        LooxidLinkData.OnReceiveEEGFeatureIndexes -= OnReceiveEEGFeatureIndexes;
        LooxidLinkData.OnReceiveEEGRawSignals -= OnReceiveEEGRawSignals;
    }
    void OnReceiveEEGSensorStatus(EEGSensor sensorStatus)
    {
        bool AF3_isSensorOn = sensorStatus.IsSensorOn(EEGSensorID.AF3);
        // Debug.Log("AF3 Sensor is on: " +AF3_isSensorOn);
    }

    void OnReceiveEEGFeatureIndexes(EEGFeatureIndex featureIndex)
    {
        // Debug.Log("AF3_Alpha: " + featureIndex.Alpha(EEGSensorID.AF3));
        // Debug.Log("AF3_Delta: " + featureIndex.Delta(EEGSensorID.AF3));
        // Debug.Log("AF3_Theta: " + featureIndex.Theta(EEGSensorID.AF3));
        // Debug.Log("AF3_Beta: " + featureIndex.Beta(EEGSensorID.AF3));
        //Debug.Log("AF3_Gamma: " + featureIndex.Gamma(EEGSensorID.AF3));

        // I know this code is ugly as fuck
        double SensAF3 = (featureIndex.Alpha(EEGSensorID.AF3) +
            featureIndex.Delta(EEGSensorID.AF3) +
            featureIndex.Theta(EEGSensorID.AF3) +
            featureIndex.Beta(EEGSensorID.AF3) +
            featureIndex.Gamma(EEGSensorID.AF3)) / 5.0;

        double SensAF4 = (featureIndex.Alpha(EEGSensorID.AF4) +
            featureIndex.Delta(EEGSensorID.AF4) +
            featureIndex.Theta(EEGSensorID.AF4) +
            featureIndex.Beta(EEGSensorID.AF4) +
            featureIndex.Gamma(EEGSensorID.AF4)) / 5.0;

        double SensFp1 = (featureIndex.Alpha(EEGSensorID.Fp1) +
            featureIndex.Delta(EEGSensorID.Fp1) +
            featureIndex.Theta(EEGSensorID.Fp1) +
            featureIndex.Beta(EEGSensorID.Fp1) +
            featureIndex.Gamma(EEGSensorID.Fp1)) / 5.0;

        double SensFp2 = (featureIndex.Alpha(EEGSensorID.Fp2) +
            featureIndex.Delta(EEGSensorID.Fp2) +
            featureIndex.Theta(EEGSensorID.Fp2) +
            featureIndex.Beta(EEGSensorID.Fp2) +
            featureIndex.Gamma(EEGSensorID.Fp2)) / 5.0;

        double SensAF7 = (featureIndex.Alpha(EEGSensorID.AF7) +
            featureIndex.Delta(EEGSensorID.AF7) +
            featureIndex.Theta(EEGSensorID.AF7) +
            featureIndex.Beta(EEGSensorID.AF7) +
            featureIndex.Gamma(EEGSensorID.AF7)) / 5.0;

        double SensAF8 = (featureIndex.Alpha(EEGSensorID.AF8) +
            featureIndex.Delta(EEGSensorID.AF8) +
            featureIndex.Theta(EEGSensorID.AF8) +
            featureIndex.Beta(EEGSensorID.AF8) +
            featureIndex.Gamma(EEGSensorID.AF8)) / 5.0;

        string AF3String = SensAF3.ToString();
        string AF4String = SensAF4.ToString();
        string Fp1String = SensFp1.ToString();
        string Fp2String = SensFp2.ToString();
        string AF7String = SensAF7.ToString();
        string AF8String = SensAF8.ToString();

        string output = AF3String + "," + AF4String + "," + Fp1String + "," + Fp2String + "," + AF7String + "," + AF8String + "\n";

        string path = @"C:\Users\dcopp\Desktop\DataStorageTest\test.txt";
        File.AppendAllText(path, output);

    }

    void Update()
    {
        // Returns EEG feature index data from last 3 seconds
        List<EEGFeatureIndex> featureIndexList = LooxidLinkData.Instance.GetEEGFeatureIndexData(3.0f);
        // Debug.Log("List Size: " + featureIndexList.Count);
    }
    void OnReceiveEEGRawSignals(EEGRawSignal rawSignalData)
    {
        //Debug.Log("New data received!");
        //Debug.Log("EEG Raw Signal Data: " + rawSignalData.rawSignal.Count + "samples");

        //for (int i = 0; i < rawSignalData.rawSignal.Count ; i++)
        // {
        //    for (int j = 0; j < rawSignalData.rawSignal[i].ch_data.Length; j++)
        //   {
        //       Debug.Log("Signal " + i  + "; Electrode reading; " + rawSignalData.rawSignal[i].ch_data[j]);
        //   }
        // }

        string path = @"C:\Users\dcopp\Desktop\DataStorageTest\test.csv";
        //File.AppendAllText(path, rawSignalData.rawSignal);
    }





}
