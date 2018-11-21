using System.Collections.Generic;
using System.Linq;

public class PawnCreator : Godot.Node {
    [Godot.Export]private Godot.PackedScene _pawnScene = null;
    private Dictionary<uint, Godot.Node> _activePawns = new Dictionary<uint, Godot.Node>();

    public override void _Ready() {
        var storeDelegate = ((StoreDelegate)GetOwner());
        // storeDelegate.Store
        //     .Subscribe(OnStateUpdate);
    }

    public void OnStateUpdate(LostGen.Board world) {
        var toRemove = _activePawns.Keys.Except(world.Pawns.Keys);
        var toAdd = world.Pawns.Keys.Except(_activePawns.Keys);

        foreach (var key in toRemove) {
            _activePawns[key].QueueFree();
        }

        foreach (var key in toAdd) {
            var pawn = (PawnCharacter)_pawnScene.Instance();
            pawn.PlayerID = key;
            AddChild(pawn);
            pawn.OnStateUpdate(world);
        }
    }
}
