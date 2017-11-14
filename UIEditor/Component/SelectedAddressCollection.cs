using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UIEditor.Component
{

    /// <summary>
    /// 自定义BaseCollection对象集合，实现IList接口
    /// 如Object Collection Editor窗体的Add/Delete钮不可用是因为没有实现IList接口
    /// </summary> 
    class KNXSelectedAddressCollection : BaseCollection, IList
    {
        private ArrayList _innerList;

        public KNXSelectedAddressCollection()
        {
            _innerList = new ArrayList();
        }

        protected override ArrayList List
        {
            get
            {
                return (ArrayList)_innerList;
            }
        }

        #region IList Members

        public int Add(object value)
        {
            return this.List.Add(value);
        }

        public void Clear()
        {
            this.List.Clear();
        }

        public bool Contains(object value)
        {
            return this.List.Contains(value);
        }

        public int IndexOf(object value)
        {
            return this.List.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            this.List.Insert(index, value);
        }

        public bool IsFixedSize
        {
            get { return this.List.IsFixedSize; }
        }

        public void Remove(object value)
        {
            this.List.Remove(value);
        }

        public void RemoveAt(int index)
        {
            this.List.RemoveAt(index);
        }

        public object this[int index]
        {
            get
            {
                return List[index];
            }
            set
            {
                List[index] = value;
            }
        }

        #endregion
    }
}
