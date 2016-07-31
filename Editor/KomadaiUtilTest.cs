using NUnit.Framework;
using Board;

public class KomadaiUtilTest
{
    [Test]
    public void FirstPieceTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 1), Is.EqualTo(KomadaiUtil.Calc(4, 9)));
    }

    [Test]
    public void TwoPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 2), Is.EqualTo((KomadaiUtil.Calc(3, 9) + KomadaiUtil.Calc(4, 9)) / 2));
    }

    [Test]
    public void ThreePiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 3), Is.EqualTo((KomadaiUtil.Calc(5, 16) + KomadaiUtil.Calc(6, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(1, 3), Is.EqualTo(KomadaiUtil.Calc(9, 16)));
    }

    [Test]
    public void FourPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 4), Is.EqualTo(KomadaiUtil.Calc(5, 16)));
        Assert.That(KomadaiUtil.Calc(2, 4), Is.EqualTo(KomadaiUtil.Calc(9, 16)));
    }

    [Test]
    public void FivePiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 5), Is.EqualTo(KomadaiUtil.Calc(5, 16)));
        Assert.That(KomadaiUtil.Calc(2, 5), Is.EqualTo((KomadaiUtil.Calc(8, 16) + KomadaiUtil.Calc(9, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(4, 5), Is.EqualTo((KomadaiUtil.Calc(10, 16) + KomadaiUtil.Calc(11, 16)) / 2));
    }

    [Test]
    public void SixPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 6), Is.EqualTo((KomadaiUtil.Calc(4, 16) + KomadaiUtil.Calc(5, 16)) / 2));
    }

    [Test]
    public void SevenPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 7), Is.EqualTo(KomadaiUtil.Calc(1, 9)));
        Assert.That(KomadaiUtil.Calc(1, 7), Is.EqualTo(KomadaiUtil.Calc(3, 9)));
    }

    [Test]
    public void EightPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 8), Is.EqualTo((KomadaiUtil.Calc(0, 9) + KomadaiUtil.Calc(1, 9)) / 2));
        Assert.That(KomadaiUtil.Calc(1, 8), Is.EqualTo((KomadaiUtil.Calc(1, 9) + KomadaiUtil.Calc(2, 9)) / 2));
        Assert.That(KomadaiUtil.Calc(2, 8), Is.EqualTo(KomadaiUtil.Calc(3, 9)));
    }

    [Test]
    public void TenPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 10), Is.EqualTo((KomadaiUtil.Calc(1, 16) + KomadaiUtil.Calc(5, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(1, 10), Is.EqualTo((KomadaiUtil.Calc(2, 16) + KomadaiUtil.Calc(6, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(2, 10), Is.EqualTo((KomadaiUtil.Calc(4, 16) + KomadaiUtil.Calc(8, 16)) / 2));
    }

    [Test]
    public void ElevenPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 11), Is.EqualTo((KomadaiUtil.Calc(0, 10) + new Vector2(0.5f, 0))));
    }

    [Test]
    public void TwelvePiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 12), Is.EqualTo((KomadaiUtil.Calc(0, 11) + new Vector2(0.5f, 0))));
        Assert.That(KomadaiUtil.Calc(3, 12), Is.EqualTo((KomadaiUtil.Calc(2, 11) - new Vector2(0.5f, 0))));
    }

    [Test]
    public void ThirteenPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 13), Is.EqualTo((KomadaiUtil.Calc(1, 16) + KomadaiUtil.Calc(2, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(1, 13), Is.EqualTo(KomadaiUtil.Calc(4, 16)));
    }

    [Test]
    public void FourteenPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 14), Is.EqualTo(KomadaiUtil.Calc(1, 16)));
        Assert.That(KomadaiUtil.Calc(2, 14), Is.EqualTo(KomadaiUtil.Calc(4, 16)));
    }

    [Test]
    public void FifteenPiecesTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 15), Is.EqualTo((KomadaiUtil.Calc(0, 16) + KomadaiUtil.Calc(1, 16)) / 2));
        Assert.That(KomadaiUtil.Calc(3, 15), Is.EqualTo(KomadaiUtil.Calc(4, 16)));
    }
}
