using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HPSF;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NPOI.HSSF.Util;
using SpreadsheetLight;

namespace ExcelNPOI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CheckDetailButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName1 = "detail sheet.xlsx";
            string fileName2 = "rec sheet.xlsx";
            if (!string.IsNullOrWhiteSpace(fileCheckO.Text) && !string.IsNullOrWhiteSpace(fileCheckF.Text))
            {
                //string fileName1 = fileCheckO.Text;
                //string fileName2 = fileCheckF.Text;
                DateTime date = DateTime.Parse(fileDate.Text);
                //导出到excel
                //1. 创建工作簿对象
                IWorkbook workbook = new XSSFWorkbook(fileName2);

                ////2. 在该工作簿中创建工作表对象
                ISheet sheet = workbook.GetSheetAt(0);
                ////2.1像该工作表中插入行和单元格
                //for (int i = 0; i < list.Count; i++)
                //{
                //    //在sheet中创建一行
                //    IRow row = sheet.CreateRow(i);
                //    //在该行中创建单元格
                //    row.CreateCell(0).SetCellValue(list[i]);
                //}

                ////3. 写入，把内存中的workbook对象写入到磁盘
                //using (FileStream fsWirte = File.OpenWrite("xxx.xlsx"))
                //{
                //    workbook.Write(fsWirte);
                ////}
                //ICellStyle style = workbook.CreateCellStyle();
                //style.FillForegroundColor = IndexedColors.Green.Index;
                decimal? total = 0m;
                try
                {
                    List<TransA> transA = new List<TransA>();
                    List<TransB> transB = new List<TransB>();
                    errorLable.Content = "";

                    transA = ReadExcel1(fileName1);

                    transB = ReadExcle2(fileName2);

                    foreach (var b in transB)
                    {
                        foreach (var a in transA)
                        {
                            if (a.Description.Contains(b.Label) || a.Description.Contains(b.Name))
                            {
                                IRow row = sheet.GetRow(b.RowNumber);
                                ICellStyle style = workbook.CreateCellStyle();
                                IFont font = workbook.CreateFont();
                                font.Color = HSSFColor.Red.Index;
                                style.SetFont(font);
                                if (a.Amount == b.Debit)
                                {
                                    total += b.Debit;
                                    //style.FillBackgroundColor = IndexedColors.LightGreen.Index;
                                    //style.FillPattern = FillPattern.LeastDots;
                                    row.GetCell(3).CellStyle = style;
                                }
                            }
                        }
                    }
                    using (FileStream fsWirte = File.Create("Checked.xlsx"))
                    {
                        workbook.Write(fsWirte);
                    }
                    errorLable.Content = "Done";

                }
                catch (Exception ex)
                {
                    errorLable.Content = "把Excle关了试试";
                    errorLable.Content = ex.Message;
                }

            }
        }

        private static List<TransA> ReadExcel1(string fileName)
        {
            try
            {
                using (FileStream fsRead = File.OpenRead(fileName))
                {
                    List<TransA> transAs = new List<TransA>();
                    //读取excel
                    //1. 创建一个workbook对象
                    //把文件读取到wk对象中
                    IWorkbook wk = new XSSFWorkbook(fsRead);
                    //遍历每一个sheet
                    for (int i = 0; i < 1; i++)
                    {
                        //获取每个工作表对象
                        ISheet nsheet = wk.GetSheetAt(i);
                        for (int r = 0; r < nsheet.LastRowNum; r++)
                        {
                            //获取工作表中的每一行
                            IRow currentRow = nsheet.GetRow(r + 1);
                            var kk = currentRow.GetCell(0);
                            if (kk == null)
                            {
                                break;
                            }
                            //便利每个单元格
                            TransA transA = new TransA();
                            transA.Description = currentRow.Cells[0].StringCellValue;
                            transA.Amount = decimal.Parse(currentRow.Cells[1].ToString());
                            transAs.Add(transA);
                        }
                    }
                    transAs = transAs
                        .GroupBy((x) => x.Description)
                        .Select(de => new TransA
                        {
                            Description = de.Key,
                            Amount = de.Sum(d => d.Amount)
                        }).ToList();
                    return transAs;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static List<TransB> ReadExcle2(string fileName)
        {
            
            //把rec sheet拿出来

            List<TransB> transBs = new List<TransB>();
            //把文件读取到wk对象中
            using (FileStream fsRead = File.OpenRead(fileName))
            {
                IWorkbook wk = new XSSFWorkbook(fsRead);
                //遍历每一个sheet
                for (int i = 0; i < 1; i++)
                {
                    //获取每个工作表对象
                    ISheet nsheet = wk.GetSheetAt(i);
                    for (int r = 0; r < nsheet.LastRowNum; r++)
                    {
                        //获取工作表中的每一行
                        IRow currentRow = nsheet.GetRow(r + 1);
                        var kk = currentRow.GetCell(0);
                        if (kk == null)
                        {
                            break;
                        }
                        //便利每个单元格
                        TransB transB = new TransB();
                        transB.Label = currentRow.Cells[1].StringCellValue;
                        //dynamic n = currentRow.Cells[2];
                        //if (!Convert.ToBoolean(currentRow.Cells[2]))
                        //{
                        //    transB.Name = currentRow.Cells[2].StringCellValue;
                        //}
                        //transB.Name = "";
                        var isRed = currentRow.Cells[3];
                        transB.Id = r;
                        transB.Name = currentRow.Cells[2].StringCellValue;
                        string deb = currentRow.Cells[3].ToString();
                        transB.Debit = string.IsNullOrEmpty(deb) ? null : (decimal?)decimal.Parse(currentRow.Cells[3].ToString());
                        string cre = currentRow.Cells[4].ToString();
                        transB.Credit = string.IsNullOrEmpty(cre) ? null : (decimal?)decimal.Parse(currentRow.Cells[4].ToString());
                        transB.RowNumber = r + 1;
                        transBs.Add(transB);
                    }
                }
            }
            return transBs;

        }

        private void SelfCheckSheet_Click(object sender, RoutedEventArgs e)
        {
            Dictionary<string, int> letterIndex = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 }, { "C", 3 }, { "D", 4 }, { "E", 5 }, { "F", 6 }, { "G", 7 }, { "H", 8 }, { "I", 9 }, { "J", 10 }, { "K", 11 }, { "L", 12 }, { "M", 13 }, { "N", 14 } };
            List<TransB> transBs = new List<TransB>();
            string fileName2 = "rec sheet.xlsx";

            decimal? debit = 0;
            decimal? credit = 0;
            decimal? balance = 0;
            using (SLDocument sl = new SLDocument(fileName2))
            {
                var row = sl.GetCells();
                for (int i = 2; i <= row.Count; i++)
                {
                    TransB transB = new TransB();
                    var isChecked = sl.GetCellStyle(i, 4).Font.FontColor.R == 255;
                    if (isChecked)
                    {
                        break;
                    }
                    transB.Id = i;
                    transB.Date = sl.GetCellValueAsDateTime(i, letterIndex["A"]);
                    transB.Label = sl.GetCellValueAsString(i, letterIndex["B"]);
                    transB.Name = sl.GetCellValueAsString(i, letterIndex["C"]);
                    transB.Debit = sl.GetCellValueAsDecimal(i, letterIndex["D"]);
                    transB.Credit = sl.GetCellValueAsDecimal(i, letterIndex["E"]);
                    transBs.Add(transB);
                }
                transBs.Reverse();
            int counter = 0;
                for (int i = 2; i <= transBs.Count; i++)
                {
                    if (transBs[i].Credit != null)
                    {
                        credit += transBs[i].Credit;
                        counter++;
                        if (counter == 2 && transBs[i].Label.Contains("Tansfer"))
                        {
                            break;
                        }
                    }
                }
                foreach (var item in transBs)
                {
                    if (item.Debit != null)
                    {
                        debit += item.Debit;

                    }
                    else if (item.Credit != null)
                    {
                        credit += item.Credit;
                        counter++;
                        if (counter == 2)
                        {
                            break;
                        }
                    }
                }
            }
            balance = debit - credit;
            if (balance != 0)
            {
                errorLable.Content = " Debit - Credit = " + balance.ToString();
            }
            else
            {
                using (SLDocument sl = new SLDocument(fileName2))
                {
                    var row = sl.GetCells();
                    for (int i = 2; i <= row.Count; i++)
                    {
                        
                    }
                }
                errorLable.Content = " Balance = 0 ";
            }
        }
    }
}
