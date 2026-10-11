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
        PropertyEditorForm _propEditor = new PropertyEditorForm();
            
        int NSamples = 100;
        string _pattern = "test(.*)";
        string _graphName = "";
        string _lastValue = "";
        
        static List<float> dataX = new List<float>();
        static List<List<float>> dataYs = new List<List<float>>();
        static List<VisBox.Series> _series = new List<VisBox.Series>();
        
//        VisBox.Series ser1 = new VisBox.Series(ref dataX, ref dataY, new VisBox.Marker(Color.Blue, ' '), new Pen(Color.Blue), "Data1");
        
        Timer tim1 = new Timer();
        
        public static readonly Color[] ChartColors = new Color[] {
            Color.Gold,
            Color.FromArgb(61, 149, 210),  // niebieski
            Color.FromArgb(255, 127, 14),  // pomarańczowy
            Color.FromArgb(44, 160, 44),   // zielony
            Color.FromArgb(214, 39, 40),   // czerwony
            Color.FromArgb(148, 103, 189), // fioletowy
            Color.FromArgb(140, 86, 75),   // brązowy
            Color.FromArgb(227, 119, 194), // różowy
            Color.FromArgb(127, 127, 127), // szary
            Color.FromArgb(188, 189, 34),  // oliwkowy
            Color.FromArgb(23, 190, 207)   // turkusowy
        };
        
        public Graph(string pattern = "(.*)", int numSamples = 100, string graphName = "") {
            InitializeComponent();
            if( numSamples <= 0 ) { numSamples = 10; }
            
            _pattern = pattern;
            NSamples = numSamples;
            _graphName = graphName;
            
            int noSer = (new Regex(_pattern)).GetGroupNumbers().Length - 1;
            
            Console.WriteLine( "Detected series: " + noSer );
            for ( int idxSer = 0; idxSer < noSer; idxSer++ ) {
                List<float> dataYi = new List<float>();
                dataYs.Add( dataYi );
                VisBox.Series serI = new VisBox.Series(ref dataX, ref dataYi, new VisBox.Marker(ChartColors[idxSer % ChartColors.Length], ' '),
                                                       new Pen(ChartColors[idxSer % ChartColors.Length]), "Data" + idxSer );
                _series.Add(serI);
                plot1.AddSeries(ref serI);
            }
            
            tim1.Interval = 100;
            tim1.Tick += tim1_tick;
            
            /* Fill series with zeros */
            for (int idx = 0; idx < NSamples; idx++) {
                dataX.Add(idx);
                for( int idxSer = 0; idxSer < dataYs.Count; idxSer++ ) {
                    dataYs[idxSer].Add(0);
                }
            }
            
            plot1.gridX.Autoscale = true;
            plot1.gridX.LineColor = Color.DimGray;
            plot1.AxisLimitsX.Autoscale = true;
            
            plot1.ColorInner = Color.Black;
            plot1.gridY.Pen.Color = Color.Green;
            plot1.AxisLimitsY.Autoscale = false;
            plot1.gridY.Dots = new List<VisBox.GridDot> { new VisBox.GridDot(0, "zero"), new VisBox.GridDot(1, "One"), new VisBox.GridDot(-.1f, "negative") };
            plot1.gridY.Format = "0.000";
            plot1.gridY.LabelBrush = new SolidBrush(Color.Orange);
            plot1.gridY.ShowLables = true;
            plot1.gridY.Autoscale = true;
            
            plot1.gridYminor.GridOn = true;
            plot1.gridYminor.Autoscale = true;
            plot1.gridYminor.ScaleFactor = 0.25f;
            plot1.gridYminor.ShowLables = false;
            
        }
        
        
        void GraphLoad(object sender, EventArgs e) {
            tim1.Start();
        }
        
        bool firstSample = true;
        public void DataReceived(byte[] dataIn) {
            List<float> firstVals = new List<float>();
            
            string strIn = Encoding.Default.GetString(dataIn);
            
            MatchCollection matches = Regex.Matches(strIn, _pattern);
            
            foreach( Match m in matches ) {
                if( !m.Success ) { continue; }
                Console.WriteLine(m.Groups[0].Value);
                for ( int idxSer = 0; ( idxSer + 1)  < m.Groups.Count; idxSer++ ) {
                    try {
                        /* Get new values for all series */
                        firstVals.Add( (float)Convert.ToDouble(m.Groups[1 + idxSer].Value.Replace(".", ",")) );
                    } catch {}
                }
            }
            
            if( firstSample && firstVals.Count > 0 ) {
                firstSample = false;
                for( int idxSer = 0; idxSer < dataYs.Count & dataYs.Count == firstVals.Count; idxSer++ ) {
                    for( int idx = 0; idx < dataYs[idxSer].Count; idx++ ) {
                        /* Set all values in current series to one value */
                        dataYs[idxSer][idx] = firstVals[idxSer];
                    }
                }
            }
            
            if ( firstVals.Count <= 0 ) { return; }
            
            /* Roll data */
            for( int idxSer = 0; idxSer < dataYs.Count; idxSer++ ) {
                for( int idx = 0; idx < (NSamples - 1); idx++ ) {
                    dataYs[idxSer][idx] = dataYs[idxSer][idx + 1];
                }
            }
            
            /* Set new value at the end fo each series */
            for( int idxSer = 0; idxSer < dataYs.Count && firstVals.Count == dataYs.Count; idxSer++ ) {
                dataYs[idxSer][dataYs[idxSer].Count - 1] = firstVals[idxSer];
            }
            
            if( firstVals.Count > 0 ) {
                _lastValue = "";
                for( int idxSer = 0; idxSer < dataYs.Count && dataYs.Count == firstVals.Count; idxSer++ ) {
                    _lastValue += ";" + firstVals[idxSer].ToString();
                }
            }
            
            if ( dataYs.Count > 0 ) {
                if ( dataYs[0].Count > 0 ) {
                    float valMin = 0, valMax = 0;
                    
                    valMin = valMax = dataYs[0][0];
                    /* Get maxs & mins */
                    for( int idxSer = 0; idxSer < dataYs.Count; idxSer++ ) {
                        for( int idx = 0; idx < (NSamples - 1); idx++ ) {
                            dataYs[idxSer][idx] = dataYs[idxSer][idx + 1];
                            if ( valMin > dataYs[idxSer][idx] ) {
                                valMin = dataYs[idxSer][idx];
                            }
                            
                            if ( valMax < dataYs[idxSer][idx] ) {
                                valMax = dataYs[idxSer][idx];
                            }
                        }
                    }
                    
                    float deltaVal = Math.Abs( valMax - valMin );
                    valMin -= 0.1f*deltaVal;
                    valMax += 0.1f*deltaVal;
                    plot1.AxisLimitsY.Min = valMin;
                    plot1.AxisLimitsY.Max = valMax;
                }
            }
            
        }
        
        
        void tim1_tick(object sender, EventArgs e) {
            Text = _graphName + " : " +  _lastValue;
            plot1.Draw();
        }

        void AlwaysOnTopToolStripMenuItemCheckedChanged(object sender, EventArgs e) {
            this.TopMost = alwaysOnTopToolStripMenuItem.Checked;
        }
        
        void BorderToolStripMenuItemCheckedChanged ( object sender, EventArgs e ) {
            this.FormBorderStyle = borderToolStripMenuItem.Checked ? FormBorderStyle.SizableToolWindow : FormBorderStyle.None;
        }
        
        void PropertyExplorerToolStripMenuItemCheckedChanged ( object sender, EventArgs e ) {
            if ( propertyExplorerToolStripMenuItem.Checked ) {
                _propEditor.ShowPropertyOf( plot1 );
            } else {
                _propEditor.Hide();
            }
        }
    }
}
