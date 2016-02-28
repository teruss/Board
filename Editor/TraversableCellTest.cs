using NUnit.Framework;
using Board;
using System.Linq;

public class TraversableCellTest
{
    [Test]
    public void FirstPlayerIsNotSet()
    {
        var world = new World();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
        var bishop = world.CreatePieceModel(Location.Create(8, 8), PieceType.Bishop, Player.Black);
        bishop.DropOrCreateMovable(world);
        var cell = world.TraversableCells.Single(c => c.Location == Location.Create(3, 3));
        Assert.That(cell.Piece.Player, Is.EqualTo(Player.Black));
        Assert.That(world.ChoiseDialog, Is.Null);
        world.Select(cell, () => { });
        Assert.That(world.ChoiseDialog, Is.Not.Null);
        world.ChoiseDialog.Promoted.Execute();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.White));
    }
}
