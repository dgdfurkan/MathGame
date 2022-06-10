using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UserValuesTwo : IComparable<UserValuesTwo>
{
    public string name;
    public int corrects;
    public int falses;

    public UserValuesTwo(string newName, int newCorrects, int newFalses)
    {
        name = newName;
        corrects = newCorrects;
        falses = newFalses;
    }

    public int CompareTo(UserValuesTwo other)
    {

        if (other == null)
        {
            return 1;
        }

        return corrects - other.corrects;

    }
}
