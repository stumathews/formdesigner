using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Design;
using ToolboxLibrary;
using System.Xml;
using System.ComponentModel.Design.Serialization;
using System.Windows.Forms.Design;
using System.Collections;
using System.Windows.Forms.Layout;


namespace ApptitudeFormDesigner
{
    
    public partial class Form1 : Form
    {
        myForm formIndesign; 
        DesignSurface designerSurface = new MyDesignSurface();
        TableLayoutPanel layout = new TableLayoutPanel();
        ITypeDescriptorFilterService typeDescriptorFilterService;
        IContainer container;
        BasicHostLoader loader;
        ISelectionService select;
        IMenuCommandService menuService;
        PropertyGrid propetyGrid = new PropertyGrid();
        bool showAllProperties = false;
     

       
        ScintillaNet.Scintilla editor = new ScintillaNet.Scintilla();   
        
 
        IDesignerHost dhost;

        public Form1()
        {
            InitializeComponent();           
       
        }
              
        private void formToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewForm("");
            status.Text = "Double click or drag on a control in/from the toolbox to add it to your form";   
        }
        private void CreateNewForm(string fileName)
        {
            // Create a new Form Designer
            bool makeOwnForm = false;
            replaceExistingDesignObjects();
            if (fileName.Length > 0)
            {
                loader = new BasicHostLoader(fileName);
            }
            else
            {
                loader = new BasicHostLoader(typeof(myForm));
                makeOwnForm = true;
            }
            
            designerSurface.BeginLoad(loader);
            
            
            Control formDesigner = designerSurface.View as Control;
            
            

            formDesigner.KeyDown += new KeyEventHandler(formDesigner_KeyDown);
            
            formDesigner.Visible = true;
            formDesigner.Dock = DockStyle.Fill;
            formDesigner.Parent = designTab;


            
            IDesignerHost designerHost = (IDesignerHost)designerSurface.GetService(typeof(IDesignerHost));
            container = (IContainer)designerHost.GetService(typeof(IContainer));
            IServiceContainer servContainer = (IServiceContainer)designerHost.GetService(typeof(IServiceContainer));
            dhost = designerHost;
            //dhost = new DesignerHost(servContainer);     
            servContainer.AddService(typeof(IToolboxService),toolbox1);
            
            toolbox1.DesignerHost = (IDesignerHost)designerSurface.GetService(typeof(IDesignerHost)); 
            
            //MessageBox.Show(dhost.RootComponent.GetType().ToString());
            ITypeDescriptorFilterService typeDescriptorFilterService = (ITypeDescriptorFilterService)dhost.GetService(typeof(ITypeDescriptorFilterService));
            dhost.RemoveService(typeof(INameCreationService));
            dhost.AddService(typeof(IMenuCommandService), new MyMenuCommandService(dhost));
            dhost.AddService(typeof(INameCreationService),new MyNameCreationService());
            dhost.RemoveService(typeof(ITypeDescriptorFilterService));
            dhost.AddService(typeof(ITypeDescriptorFilterService), new MyTypeDescriptorFilterService(dhost));
            select = (ISelectionService)dhost.GetService(typeof(ISelectionService));
            select.SelectionChanged += new EventHandler(select_SelectionChanged);
            
            IComponentChangeService controlChanged = (IComponentChangeService)dhost.GetService(typeof(IComponentChangeService));
            
            menuService = servContainer.GetService(typeof(IMenuCommandService)) as IMenuCommandService;
            IExtenderProviderService extenderProvider = (IExtenderProviderService)dhost.GetService(typeof(IExtenderProviderService));
            extenderProvider.AddExtenderProvider(new HelpLabel());
            if (makeOwnForm)
            {
                formIndesign = (myForm)dhost.RootComponent;
                formIndesign.ControlBox = false;            

                formIndesign.Text = "Form";
                formIndesign.BackColor = Color.White;
               // formDesigner.Size = new Size(597, 296);
                //formIndesign.Size = new Size(597, 296);
                
         
                
                //Size(597, 296);
               


                //formIndesign.Icon = this.Icon;
                

                formIndesign.FormBorderStyle = FormBorderStyle.SizableToolWindow;
            }
           

        }

     

        void formDesigner_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                foreach (Component comp in select.GetSelectedComponents())
                {
                    // Don't destroy the form at any point, event if its currently selected
                    if ((dhost.RootComponent as Control).Controls.Count > 0)
                    {
                        dhost.DestroyComponent(comp);
                        loader.Flush();
                    }
                }
            }
        }
    

      

        

        void select_SelectionChanged(object sender, EventArgs e)
        {
            
            ISelectionService select = (ISelectionService)sender as ISelectionService;
           
            // This shows off all the properties of the winforms control.
            //propertyGrid1.SelectedObject = select.GetSelectedComponents();
            ICollection<object> selectedControls = (ICollection<object>)select.GetSelectedComponents();
            
            List<object> list = selectedControls.ToList<object>();
            if (list.Count > 0)
            {

                Control selectedControl = list[0] as Control;
                //PropertyGrid propetyGrid = new PropertyGrid();
                propetyGrid.Parent = splitContainer2.Panel2;
                propetyGrid.Dock = DockStyle.Fill;
                
                // filter out everything except

                // You first create the attribute
                // you want to filter with :
             
                    Attribute myfilterattribute = new CategoryAttribute("Appearance");
                    Attribute myfilterattribute1 = new CategoryAttribute("Describe");
                    // And you pass it to the PropertyGrid,
                    // via its BrowsableAttributes property :
                    AttributeCollection attributeCollection = new AttributeCollection(new Attribute[] { myfilterattribute1 });



                    propetyGrid.BrowsableAttributes = attributeCollection;


                    //unformlyLayoutControl(selectedControl,select);
                
                propetyGrid.SelectedObject = select.PrimarySelection;                 
            }
           

            
           

            //foreach (Control item in select.GetSelectedComponents())
            //{
            //    MessageBox.Show(item.GetType().ToString());
            //}
            
            //
        }

        private void unformlyLayoutControl(Control selectedControl, ISelectionService select)
        {
            // look at a last control added to the same parent as the selected control.
            // add this control after the last control.
           

            //propertyGrid = new PropertyGrid();
            //propertyGrid.SelectedObject = selectedControl;
            //Form f = new Form();
            //propertyGrid.Parent = f;
            //propertyGrid.Dock = DockStyle.Fill;
            ////f.Show();

            
        }


       

        /// <summary>
        /// Recreates the DesignerSurface and TableLayout
        /// </summary>
        private void replaceExistingDesignObjects()
        {
            if (designerSurface.IsLoaded)
            {
                designerSurface.Dispose();
                designerSurface = new DesignSurface();
                layout = new TableLayoutPanel();
            }
        }
        // Load a new form on start of the main form
        private void Form1_Load(object sender, EventArgs e)
        {
            // set default size:
            //Rectangle rect = Screen.PrimaryScreen.Bounds;
            //rect.Height /= 2; // half the screen size
            //rect.Width /= 2; // half the screen size
            //this.Width = rect.Width;
            //this.Height = rect.Height;
            //this.Refresh();
            //this.Bounds = rect; // set the with of the form by default       
            
            formToolStripMenuItem_Click(this, null);
        }

        private void toolbox1_Load(object sender, EventArgs e)
        {
            
        }

     

        private void listComponentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            ListBox lb = new ListBox();
            foreach (Component comp in container.Components)
            {
                lb.Items.Add(comp.ToString());
            }
            lb.Parent = f;
            lb.Dock = DockStyle.Fill;
            f.Show();
        }

       

     

        private void saveFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loader.Save(true);
          
        }

        private void loadFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            filename = ofd.FileName;
            if (filename.Length > 0)
            {
                
                CreateNewForm(filename);
            }
        }

        private void codeTab_Enter(object sender, EventArgs e)
        {
            loader.Flush();

            editor.Folding.IsEnabled = true;
      
            editor.ResetText();

            editor.Text = loader.GetCode();
            editor.ConfigurationManager.Language = "xml";

            editor.Parent = codeTab;
            editor.Dock = DockStyle.Fill;
        }
        
        private void layoutFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point test = new Point(0, 0);
            Point NextLayout = new Point(0, 0);
            Form form = (Form)dhost.RootComponent;
            for (int i = 0; i < form.Controls.Count; i++)
            {
                form.Controls[i].Location = test;
                //test = new Point(0, (form.Controls[i].Height*2));
                NextLayout = form.Controls[i].Location;
                NextLayout.Y *= 5;
                test = NextLayout;
            }
        }

        private void showFormHeirachyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeView treeView = new TreeView();
            loader.Flush();
            string xml =  loader.GetCode();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);
            DisplayXmlFile(xmldoc, treeView);
            Form f = new Form();
            treeView.Parent = f;
            treeView.Dock = DockStyle.Fill;
            f.Show();

           

        }
        // Display a XML file in a TreeView
        // Note: requires Imports System.Xml
        // Example: DisplayXmlFile("employees.xml", TreeView1)

        public void DisplayXmlFile(XmlDocument xmldoc, TreeView tvw)
        {
                        
            // Add it to the TreeView Nodes collection
            DisplayXmlNode(xmldoc, tvw.Nodes);
            // Expand the root node.
            tvw.Nodes[0].Expand();

        }

    public void DisplayXmlNode(XmlNode xmlnode, TreeNodeCollection nodes)
        {
	    // Add a TreeView node for this XmlNode.
	    // (Using the node's Name is OK for most XmlNode types.)
	    TreeNode tvNode = nodes.Add(xmlnode.Name);

	    switch (xmlnode.NodeType) {
		    case XmlNodeType.Element:
			    // This is an element: Check whether there are attributes.
			    if (xmlnode.Attributes.Count > 0) {
				    // Create an ATTRIBUTES node.
				    TreeNode attrNode = tvNode.Nodes.Add("(ATTRIBUTES)");
				    // Add all the attributes as children of the new node.
				    XmlAttribute xmlAttr = default(XmlAttribute);
				    foreach ( XmlAttribute Attr in xmlnode.Attributes) {
					    // Each node shows name and value.
					    attrNode.Nodes.Add(Attr.Name + " = '" + Attr.Value + "'");
				    }
			    }
			    break;
		    case XmlNodeType.Text:
		    case XmlNodeType.CDATA:
			    // For these node types we display the value
			    tvNode.Text = xmlnode.Value;
			    break;
		    case XmlNodeType.Comment:
			    tvNode.Text = "<!--" + xmlnode.Value + "-->";
			    break;
		    case XmlNodeType.ProcessingInstruction:
		    case XmlNodeType.XmlDeclaration:
			    tvNode.Text = "<?" + xmlnode.Name + " " + xmlnode.Value + "?>";
			    break;
		    default:
			    break;
		    // ignore other node types.
	    }

	    // Call this routine recursively for each child node.
	    XmlNode xmlChild = xmlnode.FirstChild;
	    while (!(xmlChild == null)) {
		    DisplayXmlNode(xmlChild, tvNode.Nodes);
		    xmlChild = xmlChild.NextSibling;
	    }
    }   
    
      





    }
    class MyDesignerOptionService : DesignerOptionService
    {
        protected override void PopulateOptionCollection(DesignerOptionCollection options)
        {
            if (options.Parent == null)
            {
                DesignerOptionCollection doc =
                    this.CreateOptionCollection(options, "WindowsFormsDesigner", null);
                DesignerOptions doptions = new DesignerOptions();
                doptions.UseSmartTags = true;
                doptions.ShowGrid = false;
                doptions.SnapToGrid = true;
                this.CreateOptionCollection(doc, "General", doptions);
            }
        }
    }

    public class MyDesignSurface : DesignSurface
    {
        
        public MyDesignSurface()
        {
            
            MyDesignerOptionService optionService = new MyDesignerOptionService();
            this.ServiceContainer.AddService(typeof(DesignerOptionService), optionService);
        }
       
       
       
    }

   
    // The form 
    public class myForm : Form
    {
        private TableLayoutPanel layout = new TableLayoutPanel();

        protected override void OnLoad(EventArgs e)
        {
            //base.OnLoad(e);
            // Set the default width of the this control( form being designed)
            Width = 600;
            
        }
     
        public myForm()
        {
            layout.ColumnCount = 2;

        }
        protected override void OnControlAdded(ControlEventArgs e)
        {

           
           
        }

       
        

        

       
       
    }
   
}
