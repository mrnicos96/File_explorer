using System;
using System.Collections;
using System.Collections.Generic;

namespace File_explorer.ViewModels
{
    internal class DirectoryHistory : IDirectoryHistory
    {
        #region Private Properties

        private void RaiseHistoryChanged() => HistoryChanged?.Invoke(this, EventArgs.Empty);
        private DirectoryNode _head;

        #endregion

        #region Public Properties

        public bool CanMoveBack => Current.PriviousNode != null;
        public bool CanMoveForward => Current.NextNode != null;

        public DirectoryNode Current { get; private set; }

        #endregion

        #region Event

        public event EventHandler HistoryChanged;

        #endregion

        #region Constructor

        public DirectoryHistory(string directoryPath)
        {
            _head = new DirectoryNode(directoryPath);
            Current = _head;
        }

        #endregion

        #region Public Metods

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

        #endregion

        #region Enumerator

        public IEnumerator<DirectoryNode> GetEnumerator()
        {
            yield return Current;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}