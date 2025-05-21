/*
 * Created by SharpDevelop.
 * User: AdamS-0
 * Date: 18.05.2025
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace streamGraph {
    /// <summary>
    /// Description of Graph.
    /// </summary>
    public partial class Graph : Form {
        
        int NSamples = 100;
        string _pattern = "test(.*)";
        string _graphName = "";
        string _lastValue = "";
        
        static List<float> dataX = new List<float>();
        static List<float> dataY = new List<float>();
        
        VisBox.Series ser1 = new VisBox.Series(ref dataX, ref dataY, new VisBox.Marker(Color.Blue, ' '), new Pen(Color.Blue), "Data1");
        
        Timer tim1 = new Timer();
        
        public Graph(string pattern = "(.*)", int numSamples = 100, string graphName = "") {
            InitializeComponent();
            if( numSamples <= 0 ) { numSamples = 10; }
            
            _pattern = pattern;
            NSamples = numSamples;
            _graphName = graphName;
            
            tim1.Interval = 100;
            tim1.Tick += tim1_tick;
            
            /* Fill series with zeros */
            for (int idx = 0; idx < NSamples; idx++) {
                dataX.Add(idx);
                dataY.Add(0);
            }
            
            ser1.Pen.Color = Color.Gold;
            
            plot1.ColorInner = Color.Black;
            plot1.gridY.Pen.Color = Color.Green;
            plot1.AxisLimitsY.Min = -.4f;
            plot1.AxisLimitsY.Max = 1.1f;
            plot1.AxisLimitsY.Use = false;
            plot1.AxisLimitsY.Margin = 0.1f;
            plot1.gridY.Dots = new List<VisBox.GridDot> { new VisBox.GridDot(0, "zero"), new VisBox.GridDot(1, "One"), new VisBox.GridDot(-.1f, "negative") };
            plot1.gridY.Format = "0.000";
            plot1.gridY.LabelBrush = new SolidBrush(Color.Orange);
            
            
            plot1.gridYminor.GridOn = true;
            plot1.gridYminor.ScaleFactor = 0.5f;
            plot1.gridYminor.ShowLables = false;
            
            plot1.AddSeries(ref ser1);
        }
        
        
        void GraphLoad(object sender, EventArgs e) {
            tim1.Start();
        }
        
        bool firstSample = true;
        public void DataReceived(byte[] dataIn) {
            List<float> vals = new List<float>();
            
            string strIn = Encoding.Default.GetString(dataIn);
            
            MatchCollection matches = Regex.Matches(strIn, _pattern);
            
            foreach( Match m in matches ) {
                if( !m.Success ) { continue; }
                Console.WriteLine(m.Groups[0].Value);
                if( m.Groups.Count > 1 ) {
                    try {
                        vals.Add( (float)Convert.ToDouble(m.Groups[1].Value.Replace(".", ",")) );
                    } catch {}
                }
            }
            
            if( firstSample&& vals.Count > 0 ) {
                firstSample = false;
                for( int idx = 0; idx < dataY.Count; idx++ ) {
                    dataY[idx] = vals[0];
                }
            }
            
            for( int idx = 0; idx < (NSamples - vals.Count); idx++ ) {
                dataY[idx] = dataY[idx + vals.Count];
            }
            
            for( int idx = 0; idx < vals.Count; idx++ ) {
                dataY[(NSamples - vals.Count) + idx] = vals[idx];
            }
            
            if( vals.Count > 0 ) {
                _lastValue = vals[vals.Count - 1].ToString();
            }
            
            
        }
        
        
        
        int cntr1 = 0;
        void tim1_tick(object sender, EventArgs e) {
            Text = _graphName + " : " +  _lastValue;
            plot1.Draw();
        }

        void AlwaysOnTopToolStripMenuItemCheckedChanged(object sender, EventArgs e) {
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }
    }
}
