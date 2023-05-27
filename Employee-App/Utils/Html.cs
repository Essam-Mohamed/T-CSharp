using Aspose.Html;
using Employee_App.DTO;

namespace Employee_App.Utils
{
    internal class Html : IHtml
    {
        public void GenerateHtmlTable(List<EmployeeWithTotalWorksHours> employees)
        {
            // Create an HTML document
            using (var document = new HTMLDocument())
            {
                // Create the table element
                var table = (HTMLTableElement)document.CreateElement("table");
                table.Style.TextAlign = "center";
                table.Style.Margin = "0 auto";

                // Add table headers
                var headers = new[] { "Name", "Total Time Worked" };
                var headerRow = (HTMLTableRowElement)document.CreateElement("tr");
                foreach (var header in headers)
                {
                    var th = (HTMLTableCellElement)document.CreateElement("th");
                    th.TextContent = header;
                    headerRow.AppendChild(th);
                }
                table.AppendChild(headerRow);

                // Add table rows
                foreach (var employee in employees)
                {
                    var row = (HTMLTableRowElement)document.CreateElement("tr");

                    var nameCell = (HTMLTableCellElement)document.CreateElement("td");
                    nameCell.TextContent = employee.EmployeeName;
                    row.AppendChild(nameCell);

                    var timeWorkedCell = (HTMLTableCellElement)document.CreateElement("td");
                    timeWorkedCell.TextContent = employee.TotalWorkHours.ToString();
                    row.AppendChild(timeWorkedCell);



                    // Add color to the row if time worked is less than 100 hours
                    if (employee.TotalWorkHours < 100)
                    {
                        row.Style.BackgroundColor = "rgb(252, 111, 111)";
                    }

                    table.AppendChild(row);
                }

                document.Body.AppendChild(table);



                document.Save("../../../../HtmlPage/employee'sPage.html");
                Console.WriteLine("Html Page Created in HtmlPage Folder.");

            }
        }
    }
}
