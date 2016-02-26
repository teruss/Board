using System;
using System.Collections.Generic;

namespace Board
{
    public class MoveDictionary
    {
        Dictionary<PieceType, Move> dic = new Dictionary<PieceType, Move>();
        Dictionary<PieceType, Move> dicPromoted = new Dictionary<PieceType, Move>();
        public MoveDictionary()
        {
            foreach (PieceType type in Enum.GetValues(typeof(PieceType)))
            {
                dic.Add(type, Move.CreateInstance(type));
                dicPromoted.Add(type, Move.CreateInstancePromoted(type));
            }
        }

        public Move Get(PieceType type)
        {
            return dic[type];
        }
        public Move GetPromoted(PieceType type)
        {
            return dicPromoted[type];
        }
    }
}
