using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
/**
*--------------------------------------------------------------------
* File name: MainWindow.xaml.cs
* Project name: Project 3
*--------------------------------------------------------------------
* Author’s name and email: Verdun@etsu.edu
* Course-Section: CSCI-2210-001
* Creation Date: 11/17/23
* -------------------------------------------------------------------
*/

namespace Project_3___VisualizatioOfSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public int numDocks = 0;

        public MainWindow()
        {

            InitializeComponent();


            


        }

        /// <summary>
        /// Contains execution logic for visual display, also clears buttons. communicates with Project 3- Warehouse Sim for data. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void StartVisualSim_Click(object sender, RoutedEventArgs e)
        {
            btnStart_CmdOutput.Visibility = Visibility.Collapsed;
            btnStart_VisualDisplay.Visibility = Visibility.Collapsed;
            // clears window for visual 
            
            string output = string.Empty;


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"Project 3- Warehouse Sim.exe";  
            
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;


            Process process = new Process();
            process.StartInfo = startInfo;
            process.StartInfo.Arguments = "2"; // command line command selector/ decides what output is printed to console.
            process.Start();
            process.WaitForExit();

            using (System.IO.StreamReader reader = process.StandardOutput)
            {
                while(reader.Peek() != -1 )
                {

                    output = reader.ReadLine();
                    
                    await Task.Delay(1000); // allows for frame by frame
                    UpdateUI(output);
                    
                }
                

            }
            btnReturn.Visibility = Visibility.Visible; // allows for return back to menu



        }

        /// <summary>
        /// used to prepare and call PopulateTrucksAndDocks
        /// </summary>
        /// <param name="dataFromSimCmdLine"></param>
        private async void UpdateUI(string dataFromSimCmdLine)
        {

            
            await Task.Delay(1000); 
            warehouseCanvas.Children.Clear();
            string[] listedDataByLine = dataFromSimCmdLine.Split(","); // used to differentiate docks and other data types
            
            
            
            if (listedDataByLine.Length == 1)
            {
                numDocks = Convert.ToInt32(listedDataByLine[0]);
            }

            PopulateTrucksAndDocks(listedDataByLine, numDocks);



        }


        /// <summary>
        /// Gives the logic for the set up of Docks and trucks using a stackpanel and canvas
        /// </summary>
        /// <param name="listedDataByLine"></param>
        /// <param name="numDocks"></param>
        private void PopulateTrucksAndDocks(string[] listedDataByLine, int numDocks)
        {
           

            int numTrucksPerDock;
            List<int> truckLines = new List<int>(20);

            if (listedDataByLine.Length > 1)
            {

                double dockOffset = 50; // Offset between docks horizontally
                double dockYPosition = 50; // Y position of docks


                // Calculate the center of the window
                double centerX = warehouseGrid.ActualWidth / 2;
                double centerY = warehouseGrid.ActualHeight / 2;

                // Calculate the starting position for the warehouse layout
                double layoutStartX = centerX - (numDocks * dockOffset) / 2;

                for (int i = 0; i < listedDataByLine.Count() - 2; i++)
                {

                    truckLines.Add(Convert.ToInt32(listedDataByLine[i + 1]));

                    
                    i++;


                }
                for (int dockIndex = 0; dockIndex < numDocks; dockIndex++)
                {
                    numTrucksPerDock = truckLines[dockIndex];
                    DrawDock(layoutStartX, dockIndex, dockOffset, dockYPosition);
                    DrawTrucks(numTrucksPerDock, layoutStartX, dockIndex, dockOffset, dockYPosition);

                }
            }
            
            




            




        }


        private async void DrawDock(double layoutStartX, int dockIndex, double dockOffset, double dockYPosition)
        {
            // Create a Dock (Ellipse) at different horizontal positions
            Ellipse dockEllipse = new Ellipse
            {
                Width = 40,
                Height = 40,
                Fill = Brushes.Green // Set appearance/color as desired
            };

            // Set position of the dock on the canvas
            Canvas.SetLeft(dockEllipse, layoutStartX + dockIndex * dockOffset);
            Canvas.SetTop(dockEllipse, dockYPosition);

            // Add the Dock to the Canvas
            warehouseCanvas.Children.Add(dockEllipse);
        }

        private async void DrawTrucks( int numTrucksPerDock, double layoutStartX, int dockIndex, double dockOffset, double dockYPosition)
        {
            for (int truckIndex = 0; truckIndex < numTrucksPerDock; truckIndex++)
            {
                Ellipse truckEllipse = new Ellipse
                {
                    Width = 20,
                    Height = 20,
                    Fill = Brushes.Blue // Set appearance/color as desired
                };

                // Set position of the truck below the dock on the canvas
                Canvas.SetLeft(truckEllipse, layoutStartX + dockIndex * dockOffset);
                Canvas.SetTop(truckEllipse, dockYPosition + 50 + truckIndex * 30); // Adjust vertical position of trucks

                // Add the Truck to the Canvas
                warehouseCanvas.Children.Add(truckEllipse);
            }
        }

       

        /// <summary>
        /// logic for calling the ware.run method from menu. TO DO: find reason why it reads too quick and misses first few
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        private async void btnStart_CmdOutput_Click(object sender, RoutedEventArgs e)
        {
            btnStart_CmdOutput.Visibility = Visibility.Collapsed;
            btnStart_VisualDisplay.Visibility = Visibility.Collapsed;
            txtBlock_CmdOutput.Visibility = Visibility.Visible;
            string output = string.Empty;
            string outputSelectorForCmdOutput = "1";
            string line;

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = @"Project 3- Warehouse Sim.exe";
            startInfo.Arguments = outputSelectorForCmdOutput;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            Process WareHouseSim = new Process();
            WareHouseSim.StartInfo = startInfo;
            WareHouseSim.Start();
            


            using (System.IO.StreamReader reader = WareHouseSim.StandardOutput)
            {
                await Task.Delay(1000);
                while (( line = reader.ReadLine()) != null)
                {
                    
                    output = reader.ReadLine() + "\n";

                    

                    txtBlock_CmdOutput.Text += output;

                }
                

            }


            WareHouseSim.WaitForExit();
            btnReturn.Visibility = Visibility.Visible;
        }

       /// <summary>
       /// allows for traversal between menu and two sim options
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            btnStart_CmdOutput.Visibility = Visibility.Visible;
            btnStart_VisualDisplay.Visibility = Visibility.Visible;
            btnReturn.Visibility = Visibility.Collapsed;
            txtBlock_CmdOutput.Visibility = Visibility.Collapsed;
            warehouseCanvas.Children.Clear();
            
            txtBlock_CmdOutput.Clear();
        }
    }
}