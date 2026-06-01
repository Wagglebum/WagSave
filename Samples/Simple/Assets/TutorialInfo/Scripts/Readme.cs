// -------------------------------------------------------------------------------------------------
// WaggleBum - WagSave
//  Copyright (c) 2026 WaggleBum, Inc. All Rights Reserved.
//
// File: Readme.cs
// -------------------------------------------------------------------------------------------------
using System;
using UnityEngine;

public class Readme : ScriptableObject
{
    public Texture2D icon;
    public string title;
    public Section[] sections;
    public bool loadedLayout;

    [Serializable]
    public class Section
    {
        public string heading, text, linkText, url;
    }
}
