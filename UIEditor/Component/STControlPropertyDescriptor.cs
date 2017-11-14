using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text;

namespace UIEditor.Component
{
    public class STControlPropertyDescriptor : PropertyDescriptor
    {
        private PropertyDescriptor _descriptor;
        public STControlPropertyDescriptor(PropertyDescriptor descriptor)
            : base(descriptor)
        {
            this._descriptor = descriptor;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override void SetValue(object component, object value)
        {
            this._descriptor.SetValue(component, value);
        }

        public override void ResetValue(object component)
        {
            this._descriptor.ResetValue(component);
        }

        public override object GetValue(object component)
        {
            return this._descriptor.GetValue(component);
        }

        public override bool CanResetValue(object component)
        {
            return this._descriptor.CanResetValue(component);
        }

        public override Type PropertyType
        {
            get { return this._descriptor.PropertyType; }
        }

        public override Type ComponentType
        {
            get { return this._descriptor.ComponentType; }
        }

        #region PropertyDescriptor 重写只读属性
        private string _Category = string.Empty;
        public override string Category
        {
            get { return _Category; }
        }
        public void SetCategory(string pCategory)
        {
            _Category = pCategory;
        }

        private string _DisplayName = string.Empty;
        public override string DisplayName
        {
            get { return _DisplayName; }
        }
        public void SetDisplayName(string pDispalyName)
        {
            _DisplayName = pDispalyName;
        }

        private string _Description = string.Empty;
        public override string Description
        {
            get { return _Description; }
        }
        public void SetDescription(string description)
        {
            _Description = description;
        }

        private bool _isReadOnly = false;
        public override bool IsReadOnly
        {
            get { return _isReadOnly; }
        }
        public void SetIsReadOnly(bool isReadOnly)
        {
            _isReadOnly = isReadOnly;
        }

        //private bool _isBrowsable = true;
        //public override bool IsBrowsable
        //{
        //    get { return _isBrowsable; }
        //}
        //public void SetIsBrowsable(bool isBrowsable)
        //{
        //    _isBrowsable = isBrowsable;
        //}

        //private object _editor;
        //public override object GetEditor(Type editorBaseType)
        //{
        //    return _editor;
        //}
        //public void SetEditor(Type editor)
        //{
        //    this._editor = editor;
        //}
        #endregion

        //public override void AddValueChanged(object component, EventHandler handler)
        //{
        //    Console.WriteLine("component:" + component + "handler:" + handler);
        //}

    }
}
