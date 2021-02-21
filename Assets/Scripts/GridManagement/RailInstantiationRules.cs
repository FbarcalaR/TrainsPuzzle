using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RailInstantiationRules : MonoBehaviour {

    public AvailableRails[] availableRails;
    public Action<string, int> valueChanged;

    private Dictionary<string, int> availableRailsDictionary;

    void Awake()
    {
        availableRailsDictionary = availableRails.ToDictionary(k => k.railPrefab.tag, v => v.availableRails);
    }

    public int GetAvailableRails(string tag) {
        bool found = availableRailsDictionary.TryGetValue(tag, out int value);
        return found ? value : 0;
    }

    public void RailInstantiated(string tag) {
        availableRailsDictionary[tag] -= 1;
        valueChanged?.Invoke(tag, availableRailsDictionary[tag]);
    }

    public void RailDeleted(string tag) {
        availableRailsDictionary[tag] += 1;
        valueChanged?.Invoke(tag, availableRailsDictionary[tag]);
    }

}

[Serializable]
public struct AvailableRails {
    public Transform railPrefab;
    public int availableRails;
}
