using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace Intillisense
{
	public class FormMain : System.Windows.Forms.Form
	{
		#region Components
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private GListBox listBoxAutoComplete;
		private System.Windows.Forms.TreeView treeViewItems;
		private System.Windows.Forms.TextBox textBoxTooltip;
		private System.Windows.Forms.ImageList imageList1;
		#endregion
		
		#region Custom members
		private TreeNode findNodeResult = null;
		private string typed = "";
		private bool wordMatched = false;
		private Assembly assembly;
		private Hashtable namespaces;
		private TreeNode nameSpaceNode;
		private bool foundNode = false;
		private string currentPath;
		#endregion

		#region Constructor, main
		public FormMain()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();			
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormMain());
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormMain));
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.listBoxAutoComplete = new Intillisense.GListBox();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.treeViewItems = new System.Windows.Forms.TreeView();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.textBoxTooltip = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.AcceptsTab = true;
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(560, 377);
			this.richTextBox1.TabIndex = 0;
			this.richTextBox1.Text = "";
			this.richTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyDown);
			this.richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDown);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 355);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Size = new System.Drawing.Size(560, 22);
			this.statusBar1.TabIndex = 2;
			// 
			// listBoxAutoComplete
			// 
			this.listBoxAutoComplete.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listBoxAutoComplete.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.listBoxAutoComplete.ImageList = this.imageList1;
			this.listBoxAutoComplete.Location = new System.Drawing.Point(136, 288);
			this.listBoxAutoComplete.Name = "listBoxAutoComplete";
			this.listBoxAutoComplete.Size = new System.Drawing.Size(208, 54);
			this.listBoxAutoComplete.TabIndex = 3;
			this.listBoxAutoComplete.Visible = false;
			this.listBoxAutoComplete.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listBoxAutoComplete_KeyDown);
			this.listBoxAutoComplete.DoubleClick += new System.EventHandler(this.listBoxAutoComplete_DoubleClick);
			this.listBoxAutoComplete.SelectedIndexChanged += new System.EventHandler(this.listBoxAutoComplete_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Lime;
			// 
			// treeViewItems
			// 
			this.treeViewItems.FullRowSelect = true;
			this.treeViewItems.ImageIndex = -1;
			this.treeViewItems.Location = new System.Drawing.Point(24, 56);
			this.treeViewItems.Name = "treeViewItems";
			this.treeViewItems.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																					  new System.Windows.Forms.TreeNode("CodeProject", new System.Windows.Forms.TreeNode[] {
																																											   new System.Windows.Forms.TreeNode("Stuff1", new System.Windows.Forms.TreeNode[] {
																																																																   new System.Windows.Forms.TreeNode("BigParser")}),
																																											   new System.Windows.Forms.TreeNode("Stuff2", new System.Windows.Forms.TreeNode[] {
																																																																   new System.Windows.Forms.TreeNode("TreeChopper")}),
																																											   new System.Windows.Forms.TreeNode("EvenMoreStuff", new System.Windows.Forms.TreeNode[] {
																																																																		  new System.Windows.Forms.TreeNode("Widgets", new System.Windows.Forms.TreeNode[] {
																																																																																							   new System.Windows.Forms.TreeNode("FileCreater", new System.Windows.Forms.TreeNode[] {
																																																																																																														new System.Windows.Forms.TreeNode("AddFile"),
																																																																																																														new System.Windows.Forms.TreeNode("DeleteFile"),
																																																																																																														new System.Windows.Forms.TreeNode("RenameFile")}),
																																																																																							   new System.Windows.Forms.TreeNode("GraphicsEngine", new System.Windows.Forms.TreeNode[] {
																																																																																																														   new System.Windows.Forms.TreeNode("AddThing")})})})})});
			this.treeViewItems.PathSeparator = ".";
			this.treeViewItems.SelectedImageIndex = -1;
			this.treeViewItems.Size = new System.Drawing.Size(432, 216);
			this.treeViewItems.TabIndex = 4;
			this.treeViewItems.Visible = false;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Shortcut = System.Windows.Forms.Shortcut.CtrlO;
			this.menuItem2.Text = "&Load Assembly";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.Filter = "DLL files (*.dll) | *.dll";
			// 
			// textBoxTooltip
			// 
			this.textBoxTooltip.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(225)));
			this.textBoxTooltip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBoxTooltip.Location = new System.Drawing.Point(368, 288);
			this.textBoxTooltip.Multiline = true;
			this.textBoxTooltip.Name = "textBoxTooltip";
			this.textBoxTooltip.ReadOnly = true;
			this.textBoxTooltip.Size = new System.Drawing.Size(100, 20);
			this.textBoxTooltip.TabIndex = 5;
			this.textBoxTooltip.Text = "";
			this.textBoxTooltip.Visible = false;
			this.textBoxTooltip.Enter += new System.EventHandler(this.textBoxTooltip_Enter);
			// 
			// FormMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
			this.ClientSize = new System.Drawing.Size(560, 377);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.textBoxTooltip,
																		  this.treeViewItems,
																		  this.listBoxAutoComplete,
																		  this.statusBar1,
																		  this.richTextBox1});
			this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Menu = this.mainMenu1;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Intellisense Demo";
			this.ResumeLayout(false);

		}
		#endregion

		#region Component events
		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			if ( this.openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				readAssembly(this.openFileDialog1.FileName);
			}
		}

		private void richTextBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Keep track of the current character, used
			// for tracking whether to hide the list of members,
			// when the delete button is pressed
			int i = this.richTextBox1.SelectionStart;
			string currentChar = "";

			if ( i > 0 )
			{
				currentChar = this.richTextBox1.Text.Substring(i-1,1);
			}
			
			if ( e.KeyData == Keys.OemPeriod )
			{
				// The amazing dot key

				if ( !this.listBoxAutoComplete.Visible)
				{
					// Display the member listview if there are
					// items in it
					if ( populateListBox() )
					{
						//this.listBoxAutoComplete.SelectedIndex = 0;

						// Find the position of the caret
						Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
						point.Y += (int) Math.Ceiling(this.richTextBox1.Font.GetHeight()) + 2;
						point.X += 2; // for Courier, may need a better method

						this.statusBar1.Text = point.X + "," + point.Y;
						this.listBoxAutoComplete.Location = point;
						this.listBoxAutoComplete.BringToFront();
						this.listBoxAutoComplete.Show();
					}
				}
				else
				{
					this.listBoxAutoComplete.Hide();
					typed = "";
				}
			
			}
			else if ( e.KeyCode == Keys.Back )
			{
				// Delete key - hides the member list if the character
				// being deleted is a dot

				this.textBoxTooltip.Hide();
				if ( typed.Length > 0 )
				{
					typed = typed.Substring(0,typed.Length -1);
				}
				if ( currentChar == "." )
				{
					this.listBoxAutoComplete.Hide();
				}
				
			}
			else if ( e.KeyCode == Keys.Up )
			{
				// The up key moves up our member list, if
				// the list is visible

				this.textBoxTooltip.Hide();

				if ( this.listBoxAutoComplete.Visible)
				{
					this.wordMatched = true;
					if ( this.listBoxAutoComplete.SelectedIndex > 0 )
						this.listBoxAutoComplete.SelectedIndex--;

					e.Handled = true;
				}
			}
			else if ( e.KeyCode == Keys.Down )
			{
				// The up key moves down our member list, if
				// the list is visible

				this.textBoxTooltip.Hide();

				if ( this.listBoxAutoComplete.Visible)
				{
					this.wordMatched = true;
					if ( this.listBoxAutoComplete.SelectedIndex < this.listBoxAutoComplete.Items.Count -1 )
						this.listBoxAutoComplete.SelectedIndex++;

					e.Handled = true;
				}
			}
			else if ( e.KeyCode == Keys.D9 )
			{
				// Trap the open bracket key, displaying a cheap and
				// cheerful tooltip if the word just typed is in our tree
				// (the parameters are stored in the tag property of the node)

				string word = this.getLastWord();
				this.foundNode = false;
				this.nameSpaceNode = null;

				this.currentPath = "";
				searchTree(this.treeViewItems.Nodes,word,true);

				if ( this.nameSpaceNode != null )
				{
					if ( this.nameSpaceNode.Tag is string )
					{
						this.textBoxTooltip.Text = (string) this.nameSpaceNode.Tag;
					
						Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
						point.Y += (int) Math.Ceiling(this.richTextBox1.Font.GetHeight()) + 2;
						point.X -= 10;
						this.textBoxTooltip.Location = point;
						this.textBoxTooltip.Width = this.textBoxTooltip.Text.Length *6;

						this.textBoxTooltip.Size = new Size(this.textBoxTooltip.Text.Length *6,this.textBoxTooltip.Height);

						// Resize tooltip for long parameters
						// (doesn't wrap text nicely)
						if ( this.textBoxTooltip.Width > 300 )
						{
							this.textBoxTooltip.Width = 300;
							int height = 0;
							height = this.textBoxTooltip.Text.Length / 50;
							this.textBoxTooltip.Height =  height *15;
						}
						this.textBoxTooltip.Show();
					}
				}
			}
			else if ( e.KeyCode == Keys.D8 )
			{
				// Close bracket key, hide the tooltip textbox

				this.textBoxTooltip.Hide();
			}
			else if ( e.KeyValue < 48 || ( e.KeyValue >= 58 && e.KeyValue <= 64) || ( e.KeyValue >= 91 && e.KeyValue <= 96) || e.KeyValue > 122)
			{
				// Check for any non alphanumerical key, hiding
				// member list box if it's visible.

				if ( this.listBoxAutoComplete.Visible)
				{
					// Check for keys for autofilling (return,tab,space)
					// and autocomplete the richtextbox when they're pressed.
					if ( e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab || e.KeyCode == Keys.Space )
					{
						this.textBoxTooltip.Hide();

						// Autocomplete
						this.selectItem();

						this.typed = "";
						this.wordMatched = false;
						e.Handled = true;
					}

					// Hide the member list view
					this.listBoxAutoComplete.Hide();
				}	
			}
			else
			{
				// Letter or number typed, search for it in the listview
				if ( this.listBoxAutoComplete.Visible)
				{
					char val = (char) e.KeyValue;
					this.typed += val;

					this.wordMatched = false;

					// Loop through all the items in the listview, looking
					// for one that starts with the letters typed
					for (i=0;i < this.listBoxAutoComplete.Items.Count;i++)
					{
						if ( this.listBoxAutoComplete.Items[i].ToString().ToLower().StartsWith(this.typed.ToLower()) )
						{
							this.wordMatched = true;
							this.listBoxAutoComplete.SelectedIndex = i;
							break;
						}
					}
				}
				else
				{
					this.typed = "";
				}
			}
		}

		private void richTextBox1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Hide the listview and the tooltip
			this.textBoxTooltip.Hide();
			this.listBoxAutoComplete.Hide();
		}

		
		private void listBoxAutoComplete_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			// Ignore any keys being pressed on the listview
			this.richTextBox1.Focus();
		}

		private void listBoxAutoComplete_DoubleClick(object sender, System.EventArgs e)
		{
			// Item double clicked, select it
			if ( this.listBoxAutoComplete.SelectedItems.Count == 1 )
			{
				this.wordMatched = true;
				this.selectItem();
				this.listBoxAutoComplete.Hide();
				this.richTextBox1.Focus();
				this.wordMatched = false;
			}
		}

		private void listBoxAutoComplete_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Make sure when an item is selected, control is returned back to the richtext
			this.richTextBox1.Focus();
		}

		private void textBoxTooltip_Enter(object sender, System.EventArgs e)
		{
			// Stop the fake tooltip's text being selected
			this.richTextBox1.Focus();
		}

		#endregion

		#region Util methods

		/// <summary>
		/// Takes an assembly filename, opens it and retrieves all types.
		/// </summary>
		/// <param name="filename">Filename to open</param>
		private void readAssembly(string filename)
		{
			this.treeViewItems.Nodes.Clear();
			namespaces = new Hashtable();
			assembly = Assembly.LoadFrom(this.openFileDialog1.FileName); 
 
			Type [] assemblyTypes = assembly.GetTypes();  
			this.treeViewItems.Nodes.Clear();

			// Cycle through types
			foreach (Type type in assemblyTypes)  
			{
				if ( type.Namespace != null )
				{
					if ( namespaces.ContainsKey(type.Namespace) )
					{
						// Already got namespace, add the class to it
						TreeNode treeNode = (TreeNode) namespaces[type.Namespace];
						treeNode = treeNode.Nodes.Add(type.Name);
						this.addMembers(treeNode,type);

						if ( type.IsClass )
						{
							treeNode.Tag = MemberTypes.Custom;
						}
					}
					else
					{
						// New namespace
						TreeNode membersNode = null;

						if ( type.Namespace.IndexOf(".") != -1 )
						{
							// Search for already existing parts of the namespace
							nameSpaceNode = null;
							foundNode = false;

							this.currentPath = "";
							searchTree(this.treeViewItems.Nodes,type.Namespace,false);

							// No existing namespace found
							if ( nameSpaceNode == null )
							{
								// Add the namespace
								string[] parts = type.Namespace.Split('.');

								TreeNode treeNode = treeViewItems.Nodes.Add(parts[0]);
								string sNamespace = parts[0];

								if ( !namespaces.ContainsKey(sNamespace) )
								{
									namespaces.Add(sNamespace,treeNode);
								}

								for (int i=1;i < parts.Length;i++)
								{
									treeNode = treeNode.Nodes.Add(parts[i]);
									sNamespace += "." +parts[i];
									if ( !namespaces.ContainsKey(sNamespace) )
									{
										namespaces.Add(sNamespace,treeNode);
									}
								}

								membersNode = treeNode.Nodes.Add(type.Name);
							}
							else
							{
								// Existing namespace, add this namespace to it,
								// and add the class
								string[] parts = type.Namespace.Split('.');
								TreeNode newNamespaceNode = null;
		
								if ( !namespaces.ContainsKey(type.Namespace) )
								{
									newNamespaceNode = nameSpaceNode.Nodes.Add(parts[parts.Length-1]);
									namespaces.Add(type.Namespace,newNamespaceNode);
								}
								else
								{
									newNamespaceNode = (TreeNode) namespaces[type.Namespace];
								}

								if ( newNamespaceNode != null )
								{
									membersNode = newNamespaceNode.Nodes.Add(type.Name);
									if ( type.IsClass )
									{
										membersNode.Tag = MemberTypes.Custom;
									}
								}
							}

						}
						else
						{
							// Single root namespace, add to root
							membersNode = treeViewItems.Nodes.Add(type.Namespace);
						}

						// Add all members
						if ( membersNode != null )
						{
							this.addMembers(membersNode,type);
						}
					}
				}
					
			}
		}

		/// <summary>
		/// Adds all members to the node's children, grabbing the parameters
		/// for methods.
		/// </summary>
		/// <param name="treeNode"></param>
		/// <param name="type"></param>
		private void addMembers(TreeNode treeNode,System.Type type)
		{
			// Get all members except methods
			MemberInfo[] memberInfo = type.GetMembers();
			for (int j=0;j < memberInfo.Length;j++)
			{		
				if ( memberInfo[j].ReflectedType.IsPublic && memberInfo[j].MemberType != MemberTypes.Method )
				{
					TreeNode node = treeNode.Nodes.Add(memberInfo[j].Name);
					node.Tag = memberInfo[j].MemberType;					
				}
			}

			// Get all methods
			MethodInfo[] methodInfo = type.GetMethods();
			for (int j=0;j < methodInfo.Length;j++)
			{
				TreeNode node = treeNode.Nodes.Add(methodInfo[j].Name);
				string parms = "";

				ParameterInfo[] parameterInfo = methodInfo[j].GetParameters();
				for (int f=0;f < parameterInfo.Length;f++)
				{
					parms += parameterInfo[f].ParameterType.ToString()+ " " +parameterInfo[f].Name+ ", ";
				}

				// Knock off remaining ", "
				if ( parms.Length > 2 )
				{
					parms = parms.Substring(0,parms.Length -2);
				}

				node.Tag = parms;
			}
		}

		/// <summary>
		/// Searches the tree view for a namespace, saving the node. The method
		/// stops and returns as soon as the namespace search can't find any
		/// more items in its path, unless continueUntilFind is true.
		/// </summary>
		/// <param name="treeNodes"></param>
		/// <param name="path"></param>
		/// <param name="continueUntilFind"></param>
		private void searchTree(TreeNodeCollection treeNodes,string path,bool continueUntilFind)
		{
			if ( this.foundNode )
			{
				return;
			}

			string p = "";
			int n = 0;
			n = path.IndexOf(".");

			if ( n != -1)
			{
				p = path.Substring(0,n);

				if ( currentPath != "" )
				{
					currentPath += "." +p;
				}
				else
				{
					currentPath = p;
				}

				// Knock off the first part
				path = path.Remove(0,n+1);
			}
			else
			{
				currentPath += "." +path;
			}

			for (int i=0;i < treeNodes.Count;i++)
			{
				if ( treeNodes[i].FullPath == currentPath )
				{
					if ( continueUntilFind )
					{
						nameSpaceNode = treeNodes[i];
					}

					nameSpaceNode = treeNodes[i];

					// got a dot, continue, or return
					this.searchTree(treeNodes[i].Nodes,path,continueUntilFind);
					
				}
				else if ( !continueUntilFind )
				{
					foundNode = true;
					return;
				}
			}
		}

		/// <summary>
		/// Searches the tree until the given path is found, storing
		/// the found node in a member var.
		/// </summary>
		/// <param name="path"></param>
		/// <param name="treeNodes"></param>
		private void findNode(string path,TreeNodeCollection treeNodes)
		{
			for (int i=0;i < treeNodes.Count;i++)
			{
				if ( treeNodes[i].FullPath == path )
				{
					this.findNodeResult = treeNodes[i];
					break;
				}
				else if ( treeNodes[i].Nodes.Count > 0 )
				{
					this.findNode(path,treeNodes[i].Nodes);
				}
			}
		}

		/// <summary>
		/// Called when a "." is pressed - the previous word is found,
		/// and if matched in the treeview, the members listbox is
		/// populated with items from the tree, which are first sorted.
		/// </summary>
		/// <returns>Whether an items are found for the word</returns>
		private bool populateListBox()
		{
			bool result = false;
			string word = this.getLastWord();

			//System.Diagnostics.Debug.WriteLine(" - Path: " +word);

			if ( word != "" )
			{
				findNodeResult = null;
				findNode(word,this.treeViewItems.Nodes);		

				if (this.findNodeResult != null )
				{
					this.listBoxAutoComplete.Items.Clear();

					if ( this.findNodeResult.Nodes.Count > 0 )
					{
						result = true;
						
						// Sort alphabetically (this could be replaced with
						// a sortable treeview)
						MemberItem[] items = new MemberItem[this.findNodeResult.Nodes.Count];
						for (int n=0;n < this.findNodeResult.Nodes.Count;n++)
						{
							MemberItem memberItem = new MemberItem();
							memberItem.DisplayText = this.findNodeResult.Nodes[n].Text;
							memberItem.Tag = this.findNodeResult.Nodes[n].Tag;

							if ( this.findNodeResult.Nodes[n].Tag != null )
							{
								System.Diagnostics.Debug.WriteLine(this.findNodeResult.Nodes[n].Tag.GetType().ToString());
							}
							
							items[n] = memberItem;
						}
						Array.Sort(items);

						for (int n=0;n < items.Length;n++)
						{
							int imageindex = 0;

							if ( items[n].Tag != null )
							{
								// Default to method (contains text for parameters)
								imageindex = 2;
								if ( items[n].Tag is MemberTypes)
								{
									MemberTypes memberType = (MemberTypes) items[n].Tag;
								
									switch ( memberType )
									{
										case MemberTypes.Custom:
											imageindex = 1;
											break;
										case MemberTypes.Property:
											imageindex = 3;
											break;
										case MemberTypes.Event:
											imageindex = 4;
											break;
									}
								}
							}

							this.listBoxAutoComplete.Items.Add(new GListBoxItem(items[n].DisplayText,imageindex) );
						}
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Autofills the selected item in the member listbox, by
		/// taking everything before and after the "." in the richtextbox,
		/// and appending the word in the middle.
		/// </summary>
		private void selectItem()
		{
			if ( this.wordMatched )
			{
				int selstart = this.richTextBox1.SelectionStart;
				int prefixend = this.richTextBox1.SelectionStart - typed.Length;
				int suffixstart = this.richTextBox1.SelectionStart + typed.Length;
						
				if ( suffixstart >= this.richTextBox1.Text.Length )
				{
					suffixstart = this.richTextBox1.Text.Length;
				}

				string prefix = this.richTextBox1.Text.Substring(0,prefixend);
				string fill = this.listBoxAutoComplete.SelectedItem.ToString();
				string suffix = this.richTextBox1.Text.Substring(suffixstart,this.richTextBox1.Text.Length - suffixstart);
			
				this.richTextBox1.Text = prefix + fill + suffix;
				this.richTextBox1.SelectionStart = prefix.Length + fill.Length;
			}
		}

		/// <summary>
		/// Searches backwards from the current caret position, until
		/// a space or newline is found.
		/// </summary>
		/// <returns>The previous word from the carret position</returns>
		private string getLastWord()
		{
			string word = "";

			int pos = this.richTextBox1.SelectionStart;
			if ( pos > 1 )
			{
				
				string tmp = "";
				char f = new char();
				while ( f != ' ' && f != 10 && pos > 0 )
				{
					pos--;
					tmp = this.richTextBox1.Text.Substring(pos,1);
					f = (char) tmp[0];
					word += f;	
				}

				char[] ca = word.ToCharArray();
				Array.Reverse( ca );
				word = new String( ca );

			}
			return word.Trim();
			
		}
		#endregion
	}

	#region MemberItem class
	/// <summary>
	/// Used for storing member items which are then
	/// alphabetically sorted.
	/// </summary>
	public class MemberItem : IComparable
	{
		public string DisplayText;
		public object Tag;

		public int CompareTo(object obj)
		{
			int result = 1;
			if ( obj != null )
			{
				if ( obj is MemberItem )
				{
					MemberItem memberItem = (MemberItem) obj;
					return ( this.DisplayText.CompareTo(memberItem.DisplayText) );
				}
				else
				{
					throw new ArgumentException();
				}
			}
			

			return result;
		}
	}
	#endregion
}
