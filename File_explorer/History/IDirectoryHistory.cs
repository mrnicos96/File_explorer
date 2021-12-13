using System;
using System.Collections.Generic;

namespace File_explorer.ViewModels
{
    internal interface IDirectoryHistory : IEnumerable<DirectoryNode>
    {
        bool CanMoveBack { get; }
        bool CanMoveForward { get; }

        DirectoryNode Current { get; }

        event EventHandler HistoryChanged;

        void MoveBack();
        void MoveForward();
        void Add(string filePath);
    }

    public class DirectoryNode
    {
        public DirectoryNode PriviousNode { get; set; }
        public DirectoryNode NextNode { get; set; }

        public string DirectoryPath { get; }

        public DirectoryNode(string directoryPath)
        {
            DirectoryPath = directoryPath;
        }        
    }
}
