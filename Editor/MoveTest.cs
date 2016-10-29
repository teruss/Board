﻿using NUnit.Framework;
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
    public void NotPinnedLance()
    {
        var world = new World(true);
        var blackKing = PieceModelUtil.CreatePieceModel(Location.Create(9, 1), PieceType.King, Player.Black);
        world.AddPiece(blackKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 2), PieceType.Lance, Player.Black);
        world.AddPiece(yourRook);
        var yourPawn = PieceModelUtil.CreatePieceModel(Location.Create(9, 3), PieceType.Pawn, Player.Black);
        world.AddPiece(yourPawn);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(3, 3), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myPawn = PieceModelUtil.CreatePieceModel(Location.Create(9, 7), PieceType.Pawn, Player.White);
        world.AddPiece(myPawn);
        var whiteLance = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Lance, Player.White);
        world.AddPiece(whiteLance);
        var whiteKing = PieceModelUtil.CreatePieceModel(Location.Create(8, 8), PieceType.King, Player.White);
        world.AddPiece(whiteKing);

        whiteLance.DropOrCreateMovable(world);
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
    public void PinnedByRookIfCaptured()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);
        var myKnight2 = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.Knight, Player.White);
        myKnight2.captured = true;
        world.AddPiece(myKnight2);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedByRookIfCapturedHorizon()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(8, 9), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);
        var myKnight2 = PieceModelUtil.CreatePieceModel(Location.Create(7, 9), PieceType.Knight, Player.White);
        myKnight2.captured = true;
        world.AddPiece(myKnight2);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedByDragon()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(8, 8), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackKing = PieceModelUtil.CreatePieceModel(Location.Create(1, 2), PieceType.King, Player.Black);
        world.AddPiece(blackKing);
        var blackSilver = PieceModelUtil.CreatePieceModel(Location.Create(3, 2), PieceType.SilverGeneral, Player.Black);
        world.AddPiece(blackSilver);
        var whiteDragon = PieceModelUtil.CreatePieceModel(Location.Create(4, 2), PieceType.Rook, Player.White);
        whiteDragon.promoted = true;
        world.AddPiece(whiteDragon);
        var myLance = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Lance, Player.White);
        world.AddPiece(myLance);
        var blackLance = PieceModelUtil.CreatePieceModel(Location.Create(1, 1), PieceType.Lance, Player.Black);
        world.AddPiece(blackLance);
        var blackDragon = PieceModelUtil.CreatePieceModel(Location.Create(2, 9), PieceType.Rook, Player.Black);
        blackDragon.promoted = true;
        world.AddPiece(blackDragon);
        var blackBishop = PieceModelUtil.CreatePieceModel(Location.Create(6, 9), PieceType.Bishop, Player.Black);
        world.AddPiece(blackBishop);
        var blackLance2 = PieceModelUtil.CreatePieceModel(Location.Create(9, 3), PieceType.Lance, Player.Black);
        world.AddPiece(blackLance2);

        blackSilver.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void RookCannotPinnedByRook()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Rook, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(15));
    }

    [Test]
    public void PinnedByRookFromRightCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(7, 9), PieceType.SilverGeneral, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedGoldGeneralByRookFromRightCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(6, 9), PieceType.GoldGeneral, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(1));
    }

    [Test]
    public void NotPinnedByFriendlyRook()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var whiteRook = PieceModelUtil.CreatePieceModel(Location.Create(9, 9), PieceType.Rook, Player.White);
        world.AddPiece(whiteRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(6, 9), PieceType.GoldGeneral, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(4));
    }

    [Test]
    public void PinnedGoldGeneralByLanceFromUpCannotMoveHorizontally()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackLance = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Lance, Player.Black);
        world.AddPiece(blackLance);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.GoldGeneral, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void PinnedSilverGeneralByLanceFromUpCannotMoveHorizontally()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackLance = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Lance, Player.Black);
        world.AddPiece(blackLance);
        var whiteSilver = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.SilverGeneral, Player.White);
        world.AddPiece(whiteSilver);

        whiteSilver.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(1));
    }

    [Test]
    public void PinnedPawnGeneralByLanceFromUpCanMoveVertically()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackLance = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Lance, Player.Black);
        world.AddPiece(blackLance);
        var whitePawn = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.Pawn, Player.White);
        world.AddPiece(whitePawn);

        whitePawn.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(1));
    }

    [Test]
    public void PinnedLanceByHorseFromUpRightCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackHorse = PieceModelUtil.CreatePieceModel(Location.Create(1, 5), PieceType.Bishop, Player.Black);
        blackHorse.promoted = true;
        world.AddPiece(blackHorse);
        var whiteLance = PieceModelUtil.CreatePieceModel(Location.Create(3, 7), PieceType.Lance, Player.White);
        world.AddPiece(whiteLance);

        whiteLance.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
    }

    [Test]
    public void PinnedHorseByLanceCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackLance = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Lance, Player.Black);
        world.AddPiece(blackLance);
        var whiteHorse = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.Bishop, Player.White);
        whiteHorse.promoted = true;
        world.AddPiece(whiteHorse);

        whiteHorse.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
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
    public void NotPinnedByRookInTheFrontCanMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Rook, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(9));
    }

    [Test]
    public void NotPinnedBishopByRookInTheFrontCanMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myBishop = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Bishop, Player.White);
        world.AddPiece(myBishop);

        myBishop.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(8));
    }

    [Test]
    public void NotPinnedKnight()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.Black);
        world.AddPiece(blackKing);
        var myRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 8), PieceType.Rook, Player.White);
        world.AddPiece(myRook);
        var mySilver = PieceModelUtil.CreatePieceModel(Location.Create(5, 6), PieceType.SilverGeneral, Player.White);
        world.AddPiece(mySilver);
        var myLance = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Lance, Player.White);
        world.AddPiece(myLance);
        var blackKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 5), PieceType.Knight, Player.Black);
        world.AddPiece(blackKnight);

        blackKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void NotPinnedDragonByRookInTheFrontCanMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 7), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myDragon = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Rook, Player.White);
        myDragon.promoted = true;
        world.AddPiece(myDragon);

        myDragon.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(11));
    }

    [Test]
    public void PinnedDragonByBishopFromUpRightCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackBishop = PieceModelUtil.CreatePieceModel(Location.Create(1, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(blackBishop);
        var myDragon = PieceModelUtil.CreatePieceModel(Location.Create(3, 7), PieceType.Rook, Player.White);
        myDragon.promoted = true;
        world.AddPiece(myDragon);

        myDragon.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void PinnedDragonByBishopFromUpLeftCannotMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var blackBishop = PieceModelUtil.CreatePieceModel(Location.Create(9, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(blackBishop);
        var myDragon = PieceModelUtil.CreatePieceModel(Location.Create(7, 7), PieceType.Rook, Player.White);
        myDragon.promoted = true;
        world.AddPiece(myDragon);

        myDragon.DropOrCreateMovable(world);
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
    public void NotPinnedByRookInTheRight()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Rook, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(8, 9), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void NotPinnedByLanceIfColumnIsDifferent()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(1, 1), PieceType.Lance, Player.Black);
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
    public void NotPinnedByLanceIfBehind()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 2), PieceType.Lance, Player.Black);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void NotPinnedByWhiteLanceIfBehind()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 3), PieceType.King, Player.Black);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Lance, Player.White);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.Knight, Player.Black);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void NotPinnedByWhiteLanceIfThereIsAPieceBetweenKing()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.Black);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Lance, Player.White);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 3), PieceType.Knight, Player.Black);
        world.AddPiece(myKnight);
        var myKnight2 = PieceModelUtil.CreatePieceModel(Location.Create(5, 5), PieceType.Knight, Player.Black);
        world.AddPiece(myKnight2);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }

    [Test]
    public void PinnedByWhiteLanceIfFront()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 1), PieceType.King, Player.Black);
        world.AddPiece(myKing);
        var yourRook = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.Lance, Player.White);
        world.AddPiece(yourRook);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(5, 3), PieceType.Knight, Player.Black);
        world.AddPiece(myKnight);

        myKnight.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(0));
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
    public void PinnedByBishopIfCaptured()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(5, 9), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(1, 5), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var myKnight = PieceModelUtil.CreatePieceModel(Location.Create(3, 7), PieceType.Knight, Player.White);
        world.AddPiece(myKnight);
        var myKnight2 = PieceModelUtil.CreatePieceModel(Location.Create(4, 8), PieceType.Knight, Player.White);
        world.AddPiece(myKnight2);
        myKnight2.captured = true;

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
    public void NotPinnedByBishopBackSlashCanMove()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(2, 8), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(7, 3), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var whiteLance = PieceModelUtil.CreatePieceModel(Location.Create(1, 9), PieceType.Lance, Player.White);
        world.AddPiece(whiteLance);

        whiteLance.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(8));
    }

    [Test]
    public void NotPinnedByBishopBetweenThereAreSomePieces()
    {
        var world = new World(true);
        var myKing = PieceModelUtil.CreatePieceModel(Location.Create(2, 8), PieceType.King, Player.White);
        world.AddPiece(myKing);
        var yourBishop = PieceModelUtil.CreatePieceModel(Location.Create(7, 3), PieceType.Bishop, Player.Black);
        world.AddPiece(yourBishop);
        var whitePawn = PieceModelUtil.CreatePieceModel(Location.Create(3, 7), PieceType.Pawn, Player.White);
        world.AddPiece(whitePawn);
        var whiteSilver = PieceModelUtil.CreatePieceModel(Location.Create(4, 6), PieceType.SilverGeneral, Player.White);
        world.AddPiece(whiteSilver);

        whitePawn.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(1));
        world.TraversableCells.Clear();
        whiteSilver.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(4));
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

    [Test]
    public void PinnedByHorse()
    {
        var world = new World(true);
        var blackKing = PieceModelUtil.CreatePieceModel(Location.Create(1, 5), PieceType.King, Player.Black);
        world.AddPiece(blackKing);
        var blackBishop = PieceModelUtil.CreatePieceModel(Location.Create(2, 4), PieceType.Bishop, Player.Black);
        world.AddPiece(blackBishop);
        var blackPawn = PieceModelUtil.CreatePieceModel(Location.Create(3, 3), PieceType.Pawn, Player.Black);
        world.AddPiece(blackPawn);
        blackPawn.captured = true;
        var whiteLance = PieceModelUtil.CreatePieceModel(Location.Create(4, 9), PieceType.Lance, Player.White);
        world.AddPiece(whiteLance);
        var whiteHorse = PieceModelUtil.CreatePieceModel(Location.Create(4, 2), PieceType.Bishop, Player.White);
        whiteHorse.promoted = true;
        world.AddPiece(whiteHorse);
        var whiteSilver = PieceModelUtil.CreatePieceModel(Location.Create(8, 6), PieceType.SilverGeneral, Player.White);
        world.AddPiece(whiteSilver);

        blackBishop.DropOrCreateMovable(world);
        Assert.That(world.TraversableCells.Count, Is.EqualTo(2));
    }
}
