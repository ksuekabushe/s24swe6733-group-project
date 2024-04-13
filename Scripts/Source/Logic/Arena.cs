using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Tetris3D
{
    public class Arena
    {
        public ArenaFloor[] floors;
        public ArenaSubPieceProperties[,,] references = new ArenaSubPieceProperties[20, 10, 10];

        public SubPiece[] getAllSubPieces()
        {
            List<SubPiece> subPieces = new List<SubPiece>() { };

            for (int i = 0; i < floors.Length; i++)
            {
                for (int j = 0; j < floors[i].rows.Length; j++)
                {
                    for (int k = 0; k < floors[i].rows[j].pieces.Length; k++)
                    {
                        subPieces.Add(floors[i].rows[j].pieces[k] as SubPiece);
                    }
                }
            }
            return subPieces.ToArray();
        }
        public void updateReferences()
        {
            if (floors.Length == 0 ||
             floors[0].rows.Length == 0 ||
             floors[0].rows[0].pieces.Length == 0)
            {
                return;
            }
            for (int i = 0; i < floors.Length; i++)
            {
                for (int j = 0; j < floors[i].rows.Length; j++)
                {
                    for (int k = 0; k < floors[i].rows[j].pieces.Length; k++)
                    {
                        references[i, j, k] = new ArenaSubPieceProperties()
                        {
                            piece = floors[i].rows[j].pieces[k] as SubPiece
                        };
                    }
                }
            }
        }
    }
}
