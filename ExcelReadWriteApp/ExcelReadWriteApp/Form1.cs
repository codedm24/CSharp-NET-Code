using OfficeOpenXml;

namespace ExcelReadWriteApp
{
    public partial class Form1 : Form
    {
        private List<Tuple<string, string, string>> excelData = new List<Tuple<string, string, string>>();
        
        public Form1()
        {
            InitializeComponent();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        private void buttonReadExcel_Click(object sender, EventArgs e)
        {
            string fileName = @"F:\Projects\Windows\ExcelReadWriteApp\DemoExcelFIle.xlsx";

            listBox1.Items.Add($"Reading Data from {fileName}");

            excelData = new List<Tuple<string, string, string>>();

            using (ExcelPackage excelPackage = new ExcelPackage(fileName))
            {
                listBox1.Items.Clear();

                listBox1.Items.Add("Reading Data from Sheet 1");
                
                ExcelWorksheet worksheet1 = excelPackage.Workbook.Worksheets[0];
                
                string? id = string.Empty;
                string? name = string.Empty;
                string? mobileno = string.Empty;
                
                for (int row = 1; row <= worksheet1.Dimension.End.Row; row++)
                {
                    id = worksheet1.Cells[row, 1].Value != null ? worksheet1.Cells[row, 1].Value.ToString(): string.Empty;
                    name = worksheet1.Cells[row, 2].Value != null ? worksheet1.Cells[row, 2].Value.ToString() : string.Empty;
                    mobileno = worksheet1.Cells[row, 10].Value != null ? worksheet1.Cells[row, 10].Value.ToString() : string.Empty;
                    if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(mobileno) && id!="ID" && name != "Name" && mobileno != "MobileNo")
                    {
                        listBox1.Items.Add($"Person Detail: {id}, {name}, {mobileno}");
                        excelData.Add(new Tuple<string, string, string>(worksheet1.Cells[row, 1].Text, worksheet1.Cells[row, 2].Text, worksheet1.Cells[row, 10].Text));
                    }
                }
            }
        }

        private void buttonWriteExcel_Click(object sender, EventArgs e)
        {
            string fileName = @"F:\Projects\Windows\ExcelReadWriteApp\DemoExcelFIle.xlsx";

            listBox1.Items.Add($"Writing Data to {fileName}");

            if (excelData.Count() == 0)
                return;

            using (ExcelPackage excelPackage = new ExcelPackage(fileName))
            {
                listBox1.Items.Add("Writing Data to Sheet 2");

                ExcelWorksheet worksheet2 = excelPackage.Workbook.Worksheets[1];
                string? id = string.Empty;
                string? mobileno = string.Empty;
                string? imagePath = string.Empty;

                for (int row = 1; row <= worksheet2.Dimension.End.Row; row++)
                {
                    id = worksheet2.Cells[row, 1].Value != null ? worksheet2.Cells[row, 1].Value.ToString() : string.Empty;
                    mobileno = worksheet2.Cells[row, 2].Value != null ? worksheet2.Cells[row, 2].Value.ToString() : string.Empty;
                    Tuple<string,string,string>? item = excelData.Find(item => item.Item1 == id && item.Item3 == mobileno);
                    if (item != null)
                    {
                        imagePath = Directory.GetCurrentDirectory() + "/" + id + "_" + mobileno + ".jpg";
                        worksheet2.Cells[row, 3].Value = imagePath;
                        listBox1.Items.Add($"Person Detail: {id}, {mobileno}, {imagePath}");
                    }
                }

                excelPackage.Save();
            }
        }
    }
}