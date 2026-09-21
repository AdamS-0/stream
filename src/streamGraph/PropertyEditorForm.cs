/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 16.05.2026
 * Time: 09:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace streamGraph {
    /// <summary>
    /// Description of PropertyEditorForm.
    /// </summary>
    public partial class PropertyEditorForm : Form {
        
        public PropertyEditorForm() {
            InitializeComponent();
        }
        
        public void ShowPropertyOf(object obj) {
            if ( obj == null ) { return; }
            propertyGrid1.SelectedObject = obj;
            
            Control parent = ((Control)obj).Parent;
            string objPath = obj.ToString();
            
//            lblObjName.Text = objPath;
            this.Show();
        }
        
        void PropEditorFormClosing ( object sender, FormClosingEventArgs e ) {
            e.Cancel = true;
            this.Hide();
        }
        
    }
}
