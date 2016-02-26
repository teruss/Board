using System;
using System.Collections.Generic;

namespace Board
{
    public class MoveDictionary
    {
        Dictionary<PieceType, Move> dic = new Dictionary<PieceType, Move>();
        public MoveDictionary()
        {
            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                dic.Add(type, Move.CreateInstance(type));
            }
        }

        public Move Get(PieceType type)
        {
            return dic[type];
        }
    }
}
