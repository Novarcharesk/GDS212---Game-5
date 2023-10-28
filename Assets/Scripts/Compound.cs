using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Compound
{
    public CompoundType name;

    public bool IsAssembled()
    {
        GameObject[] connectors = GameObject.FindGameObjectsWithTag("Connector");
        switch (name)
        {
            case CompoundType.Water:
                List<bool> attached = new List<bool>();
                foreach (GameObject connector in connectors)
                {
                    AtomManager atomManager = connector.GetComponent<AtomManager>();
                    if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Oxygen))
                    {
                        attached.Add(true);
                    }
                    else
                    {
                        attached.Add(false);
                    }
                }
                if (attached.Contains(false) || attached.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case CompoundType.Methane:
                List<bool> attached2 = new List<bool>();
                foreach (GameObject connector in connectors)
                {
                    AtomManager atomManager = connector.GetComponent<AtomManager>();
                    if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Carbon))
                    {
                        attached2.Add(true);
                    }
                    else
                    {
                        attached2.Add(false);
                    }
                }
                if (attached2.Contains(false) || attached2.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case CompoundType.Ammonia:
                List<bool> attached3 = new List<bool>();
                foreach (GameObject connector in connectors)
                {
                    AtomManager atomManager = connector.GetComponent<AtomManager>();
                    if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Nitrogen))
                    {
                        attached3.Add(true);
                    }
                    else
                    {
                        attached3.Add(false);
                    }
                }
                if (attached3.Contains(false) || attached3.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case CompoundType.Methanol:
                List<bool> attached4 = new List<bool>();
                foreach (GameObject connector in connectors)
                {
                    AtomManager atomManager = connector.GetComponent<AtomManager>();
                    if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Carbon))
                    {
                        attached4.Add(true);
                    }
                    else if (atomManager.connectedAtomTypes.Contains(AtomType.Carbon) && atomManager.connectedAtomTypes.Contains(AtomType.Oxygen))
                    {
                        attached4.Add(true);
                    }
                    else if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Oxygen))
                    {
                        attached4.Add(true);
                    }
                    else
                    {
                        attached4.Add(false);
                    }
                }
                if (attached4.Contains(false) || attached4.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case CompoundType.AmmoniumHydroxide:
                List<bool> attached5 = new List<bool>();
                foreach (GameObject connector in connectors)
                {
                    AtomManager atomManager = connector.GetComponent<AtomManager>();
                    if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Nitrogen))
                    {
                        attached5.Add(true);
                    }
                    else if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Oxygen))
                    {
                        attached5.Add(true);
                    }
                    else
                    {
                        attached5.Add(false);
                    }
                }
                if (attached5.Contains(false) || attached5.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case CompoundType.AcetateAcid:
                List<bool> attached6 = new List<bool>();
                foreach (GameObject connector in connectors)
                {
                    AtomManager atomManager = connector.GetComponent<AtomManager>();
                    if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Carbon))
                    {
                        attached6.Add(true);
                    }
                    else if (atomManager.connectedAtomTypes.Contains(AtomType.Carbon) && atomManager.connectedAtomTypes.Contains(AtomType.Oxygen))
                    {
                        attached6.Add(true);
                    }
                    else if (atomManager.connectedAtomTypes.Contains(AtomType.Hydrogen) && atomManager.connectedAtomTypes.Contains(AtomType.Oxygen))
                    {
                        attached6.Add(true);
                    }
                    else if (atomManager.connectedAtomTypes.Contains(AtomType.Carbon) && atomManager.connectedAtomTypes.Contains(AtomType.Carbon))
                    {
                        attached6.Add(true);
                    }
                    else
                    {
                        attached6.Add(false);
                    }
                }
                if (attached6.Contains(false) || attached6.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            default:
                return false;
        }
    }
    public enum AtomType
    {
        Hydrogen,
        Carbon,
        Nitrogen,
        Oxygen,
        _Connector
    }

    public enum CompoundType
    {
        Water,
        Methane,
        Ammonia,
        Methanol,
        AmmoniumHydroxide,
        AcetateAcid
    }

}
