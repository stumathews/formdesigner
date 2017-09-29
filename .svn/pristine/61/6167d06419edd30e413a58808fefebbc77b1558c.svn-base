using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Windows.Forms;

namespace ApptitudeFormDesigner
{
    /// This service relays requests for filtering a component's exposed
    /// attributes, properties, and events to that component's designer.
    public class MyTypeDescriptorFilterService : ITypeDescriptorFilterService
    {
        public IDesignerHost host;

        public MyTypeDescriptorFilterService(IDesignerHost host)
        {
            this.host = host;
        }

        /// Get the designer for the given component and cast it as a designer filter.
        private IDesignerFilter GetDesignerFilter(IComponent component)
        {
            return host.GetDesigner(component) as IDesignerFilter;
        }

        #region Implementation of ITypeDescriptorFilterService
        /// Tell the given component's designer to filter properties.
        public bool FilterProperties(System.ComponentModel.IComponent component, System.Collections.IDictionary properties)
        {
            // This removes all properties but will break the design time stuff
            //removeAllProperties(properties);

            IDesignerFilter filter = GetDesignerFilter(component);
            if (filter != null)
            {
                filter.PreFilterProperties(properties);
                filter.PostFilterProperties(properties);
                return true;
            }
            return false;
        }

        private void removeAllProperties(System.Collections.IDictionary properties)
        {
            //string[] propertiesToHide = 
            //             {"FlatStyle", "FlatAppearance"};
            List<string> keys = new List<string>();
            foreach (string key in properties.Keys)
            {
                keys.Add(key);
            }
            keys.Remove("Size");
            keys.Remove("AutoSizeMode");
            keys.Remove("Dock");
            keys.Remove("SizeMode");
            keys.Remove("Text");
            string[] propertiesToHide = keys.ToArray();


            foreach (string propname in propertiesToHide)
            {
                PropertyDescriptor prop = (PropertyDescriptor)properties[propname];
                if (prop != null)
                {
                    AttributeCollection runtimeAttributes =
                                               prop.Attributes;
                    // make a copy of the original attributes 
                    // but make room for one extra attribute
                    Attribute[] attrs =
                       new Attribute[runtimeAttributes.Count + 1];
                    runtimeAttributes.CopyTo(attrs, 0);
                    attrs[runtimeAttributes.Count] =
                                    new BrowsableAttribute(false);
                    prop =
                     TypeDescriptor.CreateProperty(this.GetType(),
                                 propname, prop.PropertyType, attrs);
                    properties[propname] = prop;
                }
            }
        }

        /// Tell the given component's designer to filter attributes.
        public bool FilterAttributes(System.ComponentModel.IComponent component, System.Collections.IDictionary attributes)
        {
            IDesignerFilter filter = GetDesignerFilter(component);
            if (filter != null)
            {
                filter.PreFilterAttributes(attributes);
                filter.PostFilterAttributes(attributes);
                return true;
            }
            return false;
        }

        /// Tell the given component's designer to filter events.
        public bool FilterEvents(System.ComponentModel.IComponent component, System.Collections.IDictionary events)
        {
            IDesignerFilter filter = GetDesignerFilter(component);
            if (filter != null)
            {
                filter.PreFilterEvents(events);
                filter.PostFilterEvents(events);
                return true;
            }
            return false;
        }
        #endregion
    }
}
