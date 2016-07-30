using NUnit.Framework;
using Board;

public class KomadaiUtilTest
{
    [Test]
    public void FirstPieceTest()
    {
        Assert.That(KomadaiUtil.Calc(0, 1), Is.EqualTo(KomadaiUtil.Calc(4, 9)));
    }
}
