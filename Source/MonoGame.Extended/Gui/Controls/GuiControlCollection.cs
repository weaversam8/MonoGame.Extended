using System.Collections;
using System.Collections.Generic;

namespace MonoGame.Extended.Gui.Controls
{
    public class GuiControlCollection : IList<GuiControl>
    {
        public GuiControlCollection(GuiContainerControl parent)
        {
            _parent = parent;
            _list = new List<GuiControl>();
        }

        private readonly GuiContainerControl _parent;
        private readonly List<GuiControl> _list;

        public IEnumerator<GuiControl> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) _list).GetEnumerator();
        }

        public void Add(GuiControl item)
        {
            item.Parent = _parent;
            _list.Add(item);
        }

        public void Clear()
        {
            foreach (var control in _list)
                control.Parent = null;

            _list.Clear();
        }

        public bool Contains(GuiControl item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(GuiControl[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public bool Remove(GuiControl item)
        {
            item.Parent = null;
            return _list.Remove(item);
        }

        public int Count => _list.Count;

        public bool IsReadOnly => false;

        public int IndexOf(GuiControl item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, GuiControl item)
        {
            item.Parent = _parent;
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list[index].Parent = null;
            _list.RemoveAt(index);
        }

        public GuiControl this[int index]
        {
            get { return _list[index]; }
            set
            {
                value.Parent = _parent;
                _list[index] = value;
            }
        }
    }
}