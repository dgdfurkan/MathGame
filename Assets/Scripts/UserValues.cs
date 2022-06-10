using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserValues : IComparable<UserValues>
{
    public string name;
    public int corrects;
    public int falses;

    public UserValues(string newName, int newCorrects, int newFalses)
    {
        name = newName;
        corrects = newCorrects;
        falses = newFalses;
    }

    public int CompareTo(UserValues other)
    {

        if (other == null)
        {
            return 1;
        }

        return corrects - other.corrects;

    }
}
