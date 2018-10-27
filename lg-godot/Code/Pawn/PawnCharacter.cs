using Godot;

public class PawnCharacter : Spatial {
    public uint PlayerID;
    public void OnStateUpdate(LostGen.Board state) {
        var self = state.Pawns[PlayerID];
        Translation = new Vector3(self.Position.X, self.Position.Y, self.Position.Z);
    }
}