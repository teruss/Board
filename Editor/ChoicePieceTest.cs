using NUnit.Framework;
using Board;
using System.Linq;

public class ChoicePieceTest
{
    [Test]
    public void ExecuteTest()
    {
        var world = new World();
        Assert.That(world.ChoiceDialog, Is.Null);
        world.AddPiece(PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.Black));
        var bishop = PieceModelUtil.CreatePieceModel(Location.Create(8, 8), PieceType.Bishop, Player.Black);
        bishop.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(10));

        var cell = world.TraversableCells.Single(c => c.Location == Location.Create(3, 3));
        world.Select(cell, () => { });
        var choicePiece = world.ChoiceDialog.Promoted;

        bool b = false;
        choicePiece.Executed += (obj, args) => b = true;
        Assert.That(b, Is.False);
        choicePiece.Execute();
        Assert.That(b, Is.True);
        Assert.That(world.ChoiceDialog, Is.Null);
    }
}
