using System;
using System.Linq;
using Godot;
using LostGen;
public class StoreDelegate : Node
{
    private Store _store = new Store();
    public Store Store { get { return _store; } }
}
