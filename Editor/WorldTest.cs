using NUnit.Framework;
using Board;
using System.Linq;

public class WorldTest
{
    [Test]
    public void FirstPlayerIsNotSet()
    {
        var world = new World();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
    }

    [Test]
    public void CurrentTurnIsWhiteAfterPromoteMove()
    {
        var world = new World();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
        var bishop = world.CreatePieceModel(Location.Create(8, 8), PieceType.Bishop, Player.Black);
        bishop.DropOrCreateMovable(world);
        var cell = world.TraversableCells.Single(c => c.Location == Location.Create(3, 3));
        Assert.That(cell.Piece.Player, Is.EqualTo(Player.Black));
        world.Select(cell, () => { });
        world.ChoiseDialog.Promoted.Execute();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.White));
    }

    [Test]
    public void UndoTest()
    {
        var world = new World();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
        var bishop = world.CreatePieceModel(Location.Create(8, 8), PieceType.Bishop, Player.Black);
        bishop.DropOrCreateMovable(world);
        var cell = world.TraversableCells.Single(c => c.Location == Location.Create(3, 3));
        Assert.That(cell.Piece.Player, Is.EqualTo(Player.Black));
        world.Select(cell, () => { });
        world.ChoiseDialog.Promoted.Execute();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.White));

        world.MoveController.Undo();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
    }

    [Test]
    public void RedoTest()
    {
        var world = new World();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
        var king = world.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.Black);
        king.DropOrCreateMovable(world);
        var cell = world.TraversableCells.Single(c => c.Location == Location.Create(5, 8));
        Assert.That(cell.Piece.Player, Is.EqualTo(Player.Black));
        world.Select(cell, () => { });
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.White));

        world.MoveController.Undo();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));

        world.MoveController.Redo();
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.White));
    }

    [Test]
    public void FinishOnKingKilledTest()
    {
        var world = new World();
        bool b = false;
        world.KingKilled += (sender, e) =>
        {
            b = true;
        };
        var king = world.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.Black);
        world.Capture(king);
        Assert.That(b, Is.True);
    }
}
