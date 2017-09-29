using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace ApptitudeFormDesigner
{
    class MyButton : Button, IComponent, ISite
    {
        #region ISite Members

        public new IComponent Component
        {
            get { throw new NotImplementedException(); }
        }

        public new bool DesignMode
        {
            get { throw new NotImplementedException(); }
        }

        #endregion

        #region IServiceProvider Members

        public new object GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IComponent Members

        event EventHandler IComponent.Disposed
        {
            add { throw new NotImplementedException(); }
            remove { throw new NotImplementedException(); }
        }

        ISite IComponent.Site
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
