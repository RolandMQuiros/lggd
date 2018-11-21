using System;
using System.Collections.Generic;

using Godot;
using LostGen;
using LostGen.Action;

public class DebugDispatch : Node {
    private Store _store;
    
    public override void _Ready() {
        _store = ((StoreDelegate)GetOwner()).Store;
    }

    public void DispatchAction() {
        _store.Dispatch(
            new AddPawns {
                ToAdd = new List<Pawn> {
                    new Pawn {
                        Position = LostGen.Point.One,
                        Footprint = new HashSet<LostGen.Point> { LostGen.Point.Zero }
                    }
                }
            }
        );
    }
}
