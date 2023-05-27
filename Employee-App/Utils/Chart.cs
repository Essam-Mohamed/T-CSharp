using Employee_App.DTO;
using System.Drawing;

namespace Employee_App.Utils
{
    internal class Chart : IChartService
    {
        public void GeneratePieChart(List<EmployeeWithTotalWorksHours> employees)
        {

            // Calculate the total hours for all employees
            int totalHours = 0;

            foreach (var employee in employees)
            {
                totalHours += employee.TotalWorkHours;
            }

            // Calculate the angles and assign colors for each employee
            Dictionary<string, float> employeeAngles = new Dictionary<string, float>();
            Dictionary<string, Color> employeeColors = new Dictionary<string, Color>();

            float startAngle = 0;
            Random random = new Random();

            foreach (var employee in employees)
            {
                float percentage = employee.TotalWorkHours / (float)totalHours * 100;
                float sweepAngle = (float)(360 * percentage / 100);

                employeeAngles.Add(employee.EmployeeName ?? "", sweepAngle);
                employeeColors.Add(employee.EmployeeName ?? "", GetRandomColor(random));

                startAngle += sweepAngle;
            }

            // Create a Bitmap object to draw the pie chart
            int width = 400;
            int height = 400;
            Bitmap chartImage = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(chartImage);
            Rectangle rect = new Rectangle(0, 0, width, height);

            // Clear the background
            graphics.Clear(Color.White);

            // Draw the pie chart segments and labels
            startAngle = 0;

            foreach (var employee in employees)
            {
                float sweepAngle = employeeAngles[employee.EmployeeName ?? ""];
                Color segmentColor = employeeColors[employee.EmployeeName ?? ""];

                // Draw the pie chart segment
                using (var brush = new SolidBrush(segmentColor))
                {
                    graphics.FillPie(brush, rect, startAngle, sweepAngle);
                }

                // Calculate the label position
                float labelAngle = startAngle + sweepAngle / 2;
                float labelRadius = width / 2 * 0.8f;
                float labelX = (float)(width / 2 + labelRadius * Math.Cos(labelAngle * Math.PI / 180));
                float labelY = (float)(height / 2 + labelRadius * Math.Sin(labelAngle * Math.PI / 180));

                // Draw the label text
                string labelText = $"{employee.EmployeeName} ({Math.Round((float)employee.TotalWorkHours / totalHours, 2) * 100}%)";
                graphics.DrawString(labelText, new Font("Arial", 6), Brushes.Black, labelX - 36, labelY-5);

                // Update the start angle for the next segment
                startAngle += sweepAngle;
            }

            string folderPath = "../../../../images";

            // Create the folder if it doesn't exist
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }


            // Save the chart as a PNG image file
            chartImage.Save("../../../../images/pie_chart.png", System.Drawing.Imaging.ImageFormat.Png);

            Console.WriteLine("Pie chart generated successfully! in images Folder.");
        }


        static Color GetRandomColor(Random random)
        {
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

    }
}

