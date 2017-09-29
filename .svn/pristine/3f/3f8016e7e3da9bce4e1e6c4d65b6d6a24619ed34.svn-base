using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.Reflection;



namespace AppControls
{
    [Designer(typeof(AppTextBoxDesigner))]
    [ToolboxBitmap(typeof(resfinder), "AppControls.app-dna.ico")]
    public partial class AppTextBox : UserControl
    {
        private string test;
        public AppTextBox()
        {
            InitializeComponent();
            string[] sa = this.GetType().Assembly.GetManifestResourceNames();
            foreach (string s in sa)
                System.Diagnostics.Trace.WriteLine(s);
        }

        [Category("Describe")]
        [Description("Test Category Description")]
        public string FieldName
        {
            get { return label1.Text; }
            set { this.label1.Text = value; }
        }
        [Category("Describe")]
        [Description("Enter the description that describes this field")]
        public string Description
        {
            get { return label2.Text; }
            set { this.label2.Text = value; }
        }
    }

    // Create an AppTextBox Designer:
    public class AppTextBoxDesigner : ControlDesigner
    {
    
        public override DesignerActionListCollection ActionLists
        {
            
            get
            {
                // Create action list collection
                DesignerActionListCollection actionLists = new DesignerActionListCollection();

                // Add custom action list
                actionLists.Add(new AppTextBoxDesignerActionList(this.Component));

                // Return to the designer action service
                return actionLists;
            }
        }

        // reference to the type we are designing
        private AppTextBox appTextBox
        {
            get { return (AppTextBox)this.Component; }
        }
    }

    //AppTextBoxDesignerActionsLIst

    public class AppTextBoxDesignerActionList : DesignerActionList
    {
        public AppTextBoxDesignerActionList(IComponent component)
            : base(component)
        {
            // Automatically display smart tag panel when
            // design-time component is dropped onto the
            // Windows Forms Designer


            this.AutoShow = true;
        }

        // main function to get actions to add to the smart tag

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            // Create list to store designer action items
            DesignerActionItemCollection actionItems = new DesignerActionItemCollection();

            // Add Appearance category header text
            actionItems.Add(new DesignerActionHeaderItem("Describe the field"));

            // Add Appearance category descriptive label
            actionItems.Add(
              new DesignerActionTextItem(
                "Properties that describe the AppTextBox's fields",
                "Describe"));
            actionItems.Add(
              new DesignerActionPropertyItem(
                "FieldName",
                "FieldName",
                GetCategory(this.AppTextBox, "FieldName"),
                GetDescription(this.AppTextBox, "FieldName")));

            actionItems.Add(
              new DesignerActionPropertyItem(
                "Description",
                "Description",
                GetCategory(this.AppTextBox, "Description"),
                GetDescription(this.AppTextBox, "Description")));

            return actionItems;
        }
        public string FieldName
        {
            get { return this.AppTextBox.FieldName; }
            set { SetProperty("FieldName", value); }
        }
        public string Description
        {
            get { return this.AppTextBox.Description; }
            set { SetProperty("Description", value); }
        }
       
        // Helper property to acquire a ClockControl reference
        private AppTextBox AppTextBox
        {
            get { return (AppTextBox)this.Component; }
        }

        // Helper method to acquire a ClockControlDesigner reference
        private AppTextBoxDesigner Designer
        {
            get
            {
                IDesignerHost designerHost = (IDesignerHost)this.AppTextBox.Site.Container;
                return (AppTextBoxDesigner)designerHost.GetDesigner(this.AppTextBox);
            }
        }

        // Helper method to safely set a component’s property
        private void SetProperty(string propertyName, object value)
        {
            // Get property
            PropertyDescriptor property = TypeDescriptor.GetProperties(this.AppTextBox)[propertyName];
            // Set property value
            property.SetValue(this.AppTextBox, value);
        }

        // Helper method to return the Category string from a
        // CategoryAttribute assigned to a property exposed by 
        //the specified object
        private string GetCategory(object source, string propertyName)
        {
            PropertyInfo property = source.GetType().GetProperty(propertyName);
            CategoryAttribute attribute = (CategoryAttribute)property.GetCustomAttributes(typeof(CategoryAttribute), false)[0];
            if (attribute == null) return null;
            return attribute.Category;
        }

        // Helper method to return the Description string from a
        // DescriptionAttribute assigned to a property exposed by 
        //the specified object
        private string GetDescription(object source, string propertyName)
        {
            PropertyInfo property = source.GetType().GetProperty(propertyName);
            DescriptionAttribute attribute = (DescriptionAttribute)property.GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
            if (attribute == null) return null;
            return attribute.Description;
        }
    }
}
internal class resfinder { }