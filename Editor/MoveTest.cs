using NUnit.Framework;
using Board;

public class MoveTest
{
    [Test]
    public void FirstMoveTest()
    {
        MoveDictionary dic = new MoveDictionary();
        var move = dic.Get(PieceType.King);
        Assert.IsInstanceOf<King>(move);
    }

    [Test]
    public void PawnTurnsToGold()
    {
        MoveDictionary dic = new MoveDictionary();
        var move = dic.GetPromoted(PieceType.Pawn);
        Assert.IsInstanceOf<GoldGeneral>(move);
    }

    [Test]
    public void AtFirstPrevPieceIsNull()
    {
        var c = new MoveController();
        Assert.That(c.PrevPiece, Is.Null);
    }

    [Test]
    public void SecondTouchIsIgnored()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.Black);
        world.AddPiece(yourKing);

        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Gray));
        myKing.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(5));

        var cell = world.TraversableCells[0];
        world.Select(cell, () => { });

        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
        Assert.That(world.CurrentPlayer, Is.EqualTo(Player.Black));

        myKing.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedPawnCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myPawn = PieceModelUtil.CreatePieceModel(Location.Create(7, 9), PieceType.Pawn, Player.White);
        world.AddPiece(myPawn);
        
        myPawn.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void NotPinnedPawnCanMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myPawn = PieceModelUtil.CreatePieceModel(Location.Create(7, 9), PieceType.Pawn, Player.White);
        world.AddPiece(myPawn);
        var myPawn2 = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Pawn, Player.White);
        world.AddPiece(myPawn2);

        myPawn2.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(1));
    }

    [Test]
    public void PinnedOppositePawnCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myPawn = PieceModelUtil.CreatePieceModel(Location.Create(7, 9), PieceType.Pawn, Player.White);
        world.AddPiece(myPawn);
        var myPawn2 = PieceModelUtil.CreatePieceModel(Location.Create(3, 9), PieceType.Pawn, Player.White);
        world.AddPiece(myPawn2);

        myPawn2.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
        myPawn.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(1));
    }

    [Test]
    public void PinnedPromotedPawnCannotMoveVertical()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myPawn = PieceModelUtil.CreatePieceModel(Location.Create(7, 9), PieceType.Pawn, Player.White);
        myPawn.promoted = true;
        world.AddPiece(myPawn);
        var myPawn2 = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Pawn, Player.White);
        myPawn2.promoted = true;
        world.AddPiece(myPawn2);

        myPawn.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
        world.TraversableCells.Clear();
        myPawn2.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(3));
    }

    [Test]
    public void PinnedByColumnCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedByRookInTheBackCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void NotPinnedByRookInTheBackCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);
        var myKnight2 = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.Knight, Player.White);
        world.AddPiece(myKnight2);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
        world.TraversableCells.Clear();
        myKnight2.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void NotPinnedByRookInTheFrontCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);
        var myKnight2 = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.Knight, Player.White);
        world.AddPiece(myKnight2);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
        world.TraversableCells.Clear();
        myKnight2.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void NotPinnedByRookInTheRightCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(2, 9), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);
        var myKnight2 = PieceModelUtil.CreatePieceModel(Location.Create(3, 9), PieceType.Knight, Player.White);
        world.AddPiece(myKnight2);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
        world.TraversableCells.Clear();
        myKnight2.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void PinnedByLanceCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourLance = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Lance, Player.Black);
        world.AddPiece(yourLance);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void NotPinnedByPromotedLance()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourLance = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Lance, Player.Black);
        yourLance.promoted = true;
        world.AddPiece(yourLance);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void PinnedByBishopCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(1, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(3, 7), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void NotPinnedByBishopCanMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(1, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(2, 9), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void NotPinnedByBishopCanMoveFromLeft()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(9, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(2, 9), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void PinnedByBishopFromLeftForwardCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(9, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(7, 7), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedByBishopFromLeftBackwardCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(9, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(7, 3), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedByBishopFromRightBackwardCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(1, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(3, 3), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

}
