using System;
using System.Collections;
using System.Collections.Generic;

namespace File_explorer.ViewModels
{
    internal class DirectoryHistory : IDirectoryHistory
    {
        private DirectoryNode _head;
        private void RaiseHistoryChanged() => HistoryChanged?.Invoke(this, EventArgs.Empty);

        public event EventHandler HistoryChanged;


        public bool CanMoveBack => Current.PriviousNode != null;
        public bool CanMoveForward => Current.NextNode != null;



        public DirectoryNode Current { get; private set; }


        public DirectoryHistory(string directoryPath)
        {
            _head = new DirectoryNode(directoryPath);
            Current = _head;
        }


        public void Add(string filePath)
        {
            var node = new DirectoryNode(filePath);

            Current.NextNode = node;
            node.PriviousNode = Current;

            Current = node;

            RaiseHistoryChanged();
        }

        

        public void MoveBack()
        {
            var prev = Current.PriviousNode;
            Current = prev;

            RaiseHistoryChanged();
        }



        public void MoveForward()
        {
            var next = Current.NextNode;
            Current = next;
            
            RaiseHistoryChanged();
        }



        public IEnumerator<DirectoryNode> GetEnumerator()
        {
            yield return Current;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }
}