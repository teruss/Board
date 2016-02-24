﻿using System.Collections.Generic;

namespace Board
{
    public interface IGameController
    {
        Komadai GetKomadai(bool opposed);
        IList<IPiece> Pieces();
        void DestroyMovableCells();
        void AddMovableCell(IMovableCell cell);
    }
}
