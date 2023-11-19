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

        private async void StartVisualSim_Click(object sender, RoutedEventArgs e)
        {
            btnStart_CmdOutput.Visibility = Visibility.Collapsed;
            btnStart_VisualDisplay.Visibility = Visibility.Collapsed;
            //btnStart_Sim
            string output = string.Empty;


            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"C:\Users\lucei\source\repos\Project-3--Warehouse-Sim\Project 3- Warehouse Sim\bin\Debug\net6.0\Project 3- Warehouse Sim.exe"; // Replace with the path to your other project's executable
            // C:\Users\lucei\source\repos\Project-3--Warehouse-Sim\Project 3- Warehouse Sim\bin\Debug\net6.0\Project 3- Warehouse Sim.exe
            //@".\Stuff\adjectives.txt"
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;


            Process process = new Process();
            process.StartInfo = startInfo;
            process.StartInfo.Arguments = "2";
            process.Start();
            process.WaitForExit();

            using (System.IO.StreamReader reader = process.StandardOutput)
            {
                while(reader.Peek() != -1 )
                {

                    output = reader.ReadLine();
                    
                    await Task.Delay(1000);
                    UpdateUI(output);
                    
                }
                //output = reader.ReadLine();
                //outputTextBox.Text = output;

            }
            btnReturn.Visibility = Visibility.Visible;



        }
        private async void UpdateUI(string dataFromSimCmdLine)
        {

            //string[] listedDataByLine = dataFromSimCmdLine.Split(",");



            // Create visual representations for each dock
            //if (listedDataByLine.Length == 1 ) // shotty maybe: changEeEeE???????
            //{
            //    DrawDocks(dataFromSimCmdLine);
            //}

            // Clear existing elements before adding new ones (optional)


            // await Task.Delay(2000);
            
            await Task.Delay(2000);
            warehouseCanvas.Children.Clear();
            string[] listedDataByLine = dataFromSimCmdLine.Split(",");
            
            
            
            if (listedDataByLine.Length == 1)
            {
                numDocks = Convert.ToInt32(listedDataByLine[0]);
            }

            PopulateTrucksAndDocks(listedDataByLine, numDocks);






        }

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

       

        private async void btnStart_CmdOutput_Click(object sender, RoutedEventArgs e)
        {
            btnStart_CmdOutput.Visibility = Visibility.Collapsed;
            btnStart_VisualDisplay.Visibility = Visibility.Collapsed;
            string output = string.Empty;
            string outputSelectorForCmdOutput = "1";
            string line;

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName = @"C:\Users\lucei\source\repos\Project-3--Warehouse-Sim\Project 3- Warehouse Sim\bin\Debug\net6.0\Project 3- Warehouse Sim.exe";
            startInfo.Arguments = outputSelectorForCmdOutput;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            Process WareHouseSim = new Process();
            WareHouseSim.StartInfo = startInfo;
            WareHouseSim.Start();
            


            using (System.IO.StreamReader reader = WareHouseSim.StandardOutput)
            {
                while (( line = reader.ReadLine()) != null)
                {

                    output = reader.ReadLine();

                    

                    txtBlock_CmdOutput.Text += output;

                }
                //output = reader.ReadLine();
                //outputTextBox.Text = output;

            }


            WareHouseSim.WaitForExit();
            btnReturn.Visibility = Visibility.Visible;
        }

       
        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            btnStart_CmdOutput.Visibility = Visibility.Visible;
            btnStart_VisualDisplay.Visibility = Visibility.Visible;
        }
    }
}