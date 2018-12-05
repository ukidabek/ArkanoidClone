using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable] public class LeaderBoard
{
    [Serializable] public class Entry : IComparable
    {
        public string Name = string.Empty;
        public int Score = 0;

        public Entry(string name, int score)
        {
            Name = name;
            Score = score;
        }

        public int CompareTo(object obj)
        {
            return (obj as Entry).Score.CompareTo(Score);
        }
    }

    public List<Entry> Entries = new List<Entry>();

    public void MakeEntry(string name, int score)
    {
        Entry entry = Entries.FirstOrDefault(e => e.Name == name);
        if (entry != null)
        {
            if (entry.Score < score)
                entry.Score = score;
        }
        else
        {
            entry = new Entry(name, score);
            Entries.Add(entry);
        }

        Entries.Sort();
    }

    public void Clear()
    {
        Entries.Clear();
    }
}
