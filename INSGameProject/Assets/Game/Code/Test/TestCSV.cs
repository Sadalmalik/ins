using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[ExecuteInEditMode]
[ShowOdinSerializedPropertiesInInspector]
public class TestCSV : MonoBehaviour
{
    public TextAsset testCsv;
    public bool test;
    [TextArea(3, 15)]
    public string result;
    [Space]
    public List<string[]> table;
    
    void Update()
    {
        if (test) {test=false; Test();}
    }
    
    void Test()
    {
        table = CSVReader.FromStrint(testCsv.text);
        
        result = CSVReader.ToString(table);
    }
}
