using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XiaoXiong.CheckQOH.Model;

namespace XiaoXiong.CheckQOH
{
    public class CheckFuncs
    {
        //public void oldXiaoXiong()
        //{
        //    Dictionary<string, int> letterIndex = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 }, { "C", 3 }, { "D", 4 }, { "E", 5 }, { "F", 6 }, { "G", 7 }, { "H", 8 }, { "I", 9 }, { "J", 10 }, { "K", 11 }, { "L", 12 }, { "M", 13 }, { "N", 14 } };
        //    // SpreadsheetLight works on the idea of a currently selected worksheet.
        //    // If no worksheet name is provided on opening an existing spreadsheet,
        //    // the first available worksheet is selected.
        //    Console.WriteLine("Doesn't support the extension .xls");
        //    Console.WriteLine(@"Put your file at D:\Downloads and name it accountmoveline.xlsx");
        //    SLDocument sl = new SLDocument(@"D:\Open Orders Report.xlsm");
        //    int row = 969;
        //    int column = 10;

        //    //Get debit 如果重复 debit.value += 1
        //    Dictionary<string, int> debit = new Dictionary<string, int>();
        //    for (int i = 2; i < row; i++)
        //    {
        //        string currentDebit = sl.GetCellValueAsString(i, letterIndex["F"]);
        //        if (!string.IsNullOrEmpty(currentDebit))
        //        {
        //            if (debit.Keys.Contains(currentDebit))
        //            {
        //                debit[currentDebit] += 1;
        //            }
        //            else
        //            {
        //                debit.Add(currentDebit, 1);
        //            }
        //        }
        //    }

        //    //Get credit 如果重复 credit.value += 1
        //    Dictionary<string, int> credit = new Dictionary<string, int>();
        //    for (int i = 2; i < row; i++)
        //    {
        //        string currentCredit = sl.GetCellValueAsString(i, letterIndex["H"]);
        //        if (!string.IsNullOrEmpty(currentCredit))
        //        {
        //            if (credit.Keys.Contains(currentCredit))
        //            {
        //                credit[currentCredit] += 1;
        //            }
        //            else
        //            {
        //                credit.Add(currentCredit, 1);
        //            }
        //        }
        //    }

        //    // 可以删的debit数
        //    List<string> needToDelete = new List<string>();
        //    foreach (var item in credit)
        //    {
        //        //debit里有这个credit
        //        if (debit.ContainsKey(item.Key))
        //        {
        //            Console.WriteLine(item.Key);
        //            for (int i = 2; i < row; i++)
        //            {
        //                string currentDebit = sl.GetCellValueAsString(i, letterIndex["G"]);
        //                string currentCredit = sl.GetCellValueAsString(i, letterIndex["H"]);

        //                //如果item.Key出现在debit或credit行
        //                if (item.Key == currentDebit || item.Key == currentCredit)
        //                {
        //                    //如果这个数只在debit和credit里出现一次 - 标记行为红色
        //                    if (debit[item.Key] == 1 && item.Value == 1)
        //                    {
        //                        SLStyle style = sl.CreateStyle();
        //                        style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightCoral, System.Drawing.Color.CornflowerBlue);
        //                        sl.SetRowStyle(i, style);
        //                        //sl.DeleteRow(i, 1);
        //                    }
        //                    else if (debit[item.Key] > 1 || item.Value > 1)
        //                    {
        //                        SLStyle style = sl.CreateStyle();
        //                        style.Fill.SetPattern(PatternValues.Solid, System.Drawing.Color.LightSkyBlue, System.Drawing.Color.CornflowerBlue);
        //                        sl.SetRowStyle(i, style);
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    sl.SaveAs(@"D:\Downloads\accountmoveline Cleaned.xlsx");
        //}
        public static void ShipCheckd()
        {
            SLDocument sl = new SLDocument();

            sl.SetCellValue("A1", 123456789.12345);
            sl.SetCellValue(2, 1, -123456789.12345);
            sl.SetCellValue(3, 1, new DateTime(2123, 4, 15));
            sl.SetCellValue(4, 1, 12.3456);
            sl.SetCellValue(5, 1, 12.3456);
            sl.SetCellValue("A6", 123456789.12345);

            SLStyle style = sl.CreateStyle();
            style.FormatCode = "#,##0.000";
            sl.SetCellStyle("A1", style);

            style = sl.CreateStyle();
            style.FormatCode = "$#,##0.00_);[Red]($#,##0.00)";
            sl.SetCellStyle(2, 1, style);

            style = sl.CreateStyle();
            style.FormatCode = "yyyy/m/d";
            sl.SetCellStyle(3, 1, style);

            // we can just reassign like this because the only property
            // we just used was the FormatCode property

            style.FormatCode = "0.00%";
            sl.SetCellStyle("A4", style);

            // this means "number with fractional part (2 digit denominator)"
            style.FormatCode = "# ??/??";
            sl.SetCellStyle(5, 1, style);

            style.FormatCode = "0.000E+00";
            sl.SetCellStyle(6, 1, style);
            sl.SetCellValue(7, 1, new DateTime(2123, 4, 15), "yyyy/d/m");

            sl.SaveAs("NumberFormat.xlsx");

            Console.WriteLine("End of program");
        }

        public static void ShipCheck(string fileName)
        {
            Dictionary<string, int> letterIndex = new Dictionary<string, int>() { { "A", 1 }, { "B", 2 }, { "C", 3 }, { "D", 4 }, { "E", 5 }, { "F", 6 }, { "G", 7 }, { "H", 8 }, { "I", 9 }, { "J", 10 }, { "K", 11 }, { "L", 12 }, { "M", 13 }, { "N", 14 } };
            int startRow = 2;
            string interRef;
            double rToShip;
            DateTime shipDate;
            double remain;
            bool found;
            List<QOH> qOHs = new List<QOH>();
            List<ComingPO> comingPOs = new List<ComingPO>();
            List<BillDetail> billDetails = new List<BillDetail>();

            SLDocument sl = new SLDocument(@fileName, "Qty on Hand");
            //string dateFormat1 = "dd/MM/yyyy HH:mm:ss";
            SLStyle dateFormat = new SLStyle();
            dateFormat.FormatCode = "yyyy/m/d";

            var sheetInfo = sl.GetWorksheetStatistics();

            for (int i = startRow; i <= sheetInfo.EndRowIndex; i++)
            {
                QOH qoh = new QOH();
                qoh.Id = i;
                qoh.QOHInternalRef = sl.GetCellValueAsString($"A{i}");
                qoh.Qty = sl.GetCellValueAsDouble($"B{i}");
                qOHs.Add(qoh);
            }

            sl.SelectWorksheet("Coming POs");
            sheetInfo = sl.GetWorksheetStatistics();
            for (int i = startRow; i <= sheetInfo.EndRowIndex; i++)
            {
                ComingPO comingPO = new ComingPO();
                comingPO.Id = i;
                comingPO.CPOInternalRef = sl.GetCellValueAsString($"D{i}");
                comingPO.Qty = sl.GetCellValueAsDouble($"G{i}");
                comingPO.ComingDate = sl.GetCellValueAsDateTime($"C{i}");
                comingPOs.Add(comingPO);
            }
            comingPOs = comingPOs.OrderBy(x => x.ComingDate).ThenBy(n => n.Id).ToList();


            sl.SelectWorksheet("Detail Data for Bill Date");
            sheetInfo = sl.GetWorksheetStatistics();

            //把数据放到list然后reorder by date 然后再放回去
            for (int i = startRow; i <= sheetInfo.EndRowIndex; i++)
            {

                BillDetail billDetail = new BillDetail();
                billDetail.Id = i;
                billDetail.DeliveryOrder = sl.GetCellValueAsString($"B{i}");
                billDetail.DeliveryStatus = sl.GetCellValueAsString($"C{i}");
                billDetail.CreatedOn = sl.GetCellValueAsDateTime($"D{i}");
                billDetail.ScheduledDate = sl.GetCellValueAsDateTime($"E{i}");
                billDetail.ShipToPartnerInterRef = sl.GetCellValueAsString($"F{i}");
                billDetail.ShipToPartnerName = sl.GetCellValueAsString($"G{i}");
                billDetail.SalesOrder = sl.GetCellValueAsString($"H{i}");
                billDetail.SalesPerson = sl.GetCellValueAsString($"I{i}");
                billDetail.ProductInternalCategory = sl.GetCellValueAsString($"J{i}");
                billDetail.Product = sl.GetCellValueAsString($"K{i}");
                billDetail.ProductInternalRef = sl.GetCellValueAsString($"L{i}");
                billDetail.UoM = sl.GetCellValueAsString($"M{i}");
                billDetail.QuantityOrdered = sl.GetCellValueAsDouble($"N{i}");
                billDetail.QuantityDelivered = sl.GetCellValueAsDouble($"O{i}");
                billDetail.QuantityInvoiced = sl.GetCellValueAsDouble($"P{i}");
                billDetail.UniPrice = sl.GetCellValueAsDecimal($"Q{i}");
                billDetail.OrderLineSubtotal = sl.GetCellValueAsDecimal($"R{i}");
                billDetail.Category = sl.GetCellValueAsString($"S{i}");
                billDetail.ProductMajorClassification = sl.GetCellValueAsString($"T{i}");
                billDetail.RemainingToShip = sl.GetCellValueAsDouble($"U{i}");
                billDetail.RemainingToInvoice = sl.GetCellValueAsDecimal($"V{i}");
                billDetail.ReservedQuantity = sl.GetCellValueAsDecimal($"W{i}");
                billDetail.Reserved = sl.GetCellValueAsDecimal($"X{i}");
                billDetail.Unreserved = sl.GetCellValueAsDecimal($"Y{i}");
                billDetail.StatusMonth = sl.GetCellValueAsString($"Z{i}");
                billDetail.StatusRange = sl.GetCellValueAsString($"AA{i}");
                billDetail.SalesOrderCustomer = sl.GetCellValueAsString($"AB{i}");
                billDetails.Add(billDetail);
            }
            billDetails = billDetails.OrderBy(x => x.ProductInternalRef).ThenBy(n => n.ScheduledDate).ToList();
            for (int i = startRow; i <= billDetails.Count; i++)
            {
                int j = i - startRow;
                sl.SetCellValue($"B{i}", billDetails[j].DeliveryOrder);
                sl.SetCellValue($"C{i}", billDetails[j].DeliveryStatus);
                sl.SetCellValue($"D{i}", billDetails[j].CreatedOn);
                sl.SetCellValue($"E{i}", billDetails[j].ScheduledDate);
                sl.SetCellValue($"F{i}", billDetails[j].ShipToPartnerInterRef);
                sl.SetCellValue($"G{i}", billDetails[j].ShipToPartnerName);
                sl.SetCellValue($"H{i}", billDetails[j].SalesOrder);
                sl.SetCellValue($"I{i}", billDetails[j].SalesPerson);
                sl.SetCellValue($"J{i}", billDetails[j].ProductInternalCategory);
                sl.SetCellValue($"K{i}", billDetails[j].Product);
                sl.SetCellValue($"L{i}", billDetails[j].ProductInternalRef);
                sl.SetCellValue($"M{i}", billDetails[j].UoM);
                sl.SetCellValue($"N{i}", (int)billDetails[j].QuantityOrdered);
                sl.SetCellValue($"O{i}", (int)billDetails[j].QuantityDelivered);
                sl.SetCellValue($"P{i}", (int)billDetails[j].QuantityInvoiced);
                sl.SetCellValue($"Q{i}", (decimal)billDetails[j].UniPrice);
                sl.SetCellValue($"R{i}", (decimal)billDetails[j].OrderLineSubtotal);
                sl.SetCellValue($"S{i}", billDetails[j].Category);
                sl.SetCellValue($"T{i}", billDetails[j].ProductMajorClassification);
                sl.SetCellValue($"U{i}", billDetails[j].RemainingToShip);
                sl.SetCellValue($"V{i}", (decimal)billDetails[j].RemainingToInvoice);
                sl.SetCellValue($"W{i}", (decimal)billDetails[j].ReservedQuantity);
                sl.SetCellValue($"X{i}", (decimal)billDetails[j].Reserved);
                sl.SetCellValue($"Y{i}", (decimal)billDetails[j].Unreserved);
                sl.SetCellValue($"Z{i}", billDetails[j].StatusMonth);
                sl.SetCellValue($"AA{i}", billDetails[j].StatusRange);
                sl.SetCellValue($"AB{i}", billDetails[j].SalesOrderCustomer);
            }

            //string acText = sl.GetCellValueAsString("AC1");
            //string adText = sl.GetCellValueAsString("AD1");
            //sl.ClearColumnContent($"AC", $"AD");
            //sl.SetCellValue("AC1", acText);
            //sl.SetCellValue("AD1", adText);


            for (int i = startRow; i < sheetInfo.EndRowIndex; i++)
            {
                found = false;
                shipDate = sl.GetCellValueAsDateTime($"E{i}");
                interRef = sl.GetCellValueAsString($"L{i}").Trim();
                rToShip = sl.GetCellValueAsDouble($"U{i}");
                var acRow = sl.GetCellValueAsString($"AC{i}");

                if (rToShip != 0)
                {
                    //从仓库里找
                    foreach (var item in qOHs)
                    {
                        if (interRef == item.QOHInternalRef.Trim())
                        {
                            found = true;
                            remain = item.Qty - rToShip;
                            //如果仓库不够或者没有
                            if (remain < 0)
                            {
                                remain = remain * -1;
                                double cRemain;
                                //如果仓库有货但不够
                                if (item.Qty > 0)
                                {
                                    sl.SetCellValue($"AC{i}", shipDate);
                                    sl.SetCellStyle($"AC{i}", dateFormat);
                                    sl.SetCellValue($"AD{i}", item.Qty);
                                    item.QOHInternalRef = item.QOHInternalRef + " - chekced";

                                }

                                foreach (var cItem in comingPOs)
                                {
                                    if (interRef == cItem.CPOInternalRef.Trim())
                                    {
                                        //var aaa = comingPOs.IndexOf(cItem);
                                        cRemain = cItem.Qty - remain;
                                        char letterDate = 'C';
                                        char letterNumber = 'D';
                                        var a = sl.GetCellValueAsString($"A{letterDate}{i}");
                                        
                                        if (!string.IsNullOrWhiteSpace(sl.GetCellValueAsString($"A{letterDate}{i}")))
                                        {
                                            letterDate = 'E';
                                            letterNumber = 'F';
                                            var b = sl.GetCellValueAsString($"A{letterDate}{i}");
                                            if (!string.IsNullOrWhiteSpace(sl.GetCellValueAsString($"A{letterDate}{i}")))
                                            {
                                                letterDate = 'G';
                                                letterNumber = 'H';
                                            }
                                        }

                                        //如果coming不够或者没有
                                        if (cRemain < 0)
                                        {
                                            sl.SetCellValue($"A{letterDate}{i}", cItem.ComingDate);
                                            sl.SetCellStyle($"A{letterDate}{i}", dateFormat);
                                            sl.SetCellValue($"A{letterNumber}{i}", cItem.Qty);
                                            cItem.CPOInternalRef = cItem.CPOInternalRef + " - chekced";
                                            remain = cRemain * -1;
                                        }
                                        else if (cRemain == 0)
                                        {
                                            sl.SetCellValue($"A{letterDate}{i}", cItem.ComingDate);
                                            sl.SetCellStyle($"A{letterDate}{i}", dateFormat);
                                            sl.SetCellValue($"A{letterNumber}{i}", cItem.Qty);
                                            cItem.CPOInternalRef = cItem.CPOInternalRef + " - chekced";
                                            //如果够ship了就不用找了
                                            break;
                                        }
                                        else if (cRemain > 0)
                                        {
                                            sl.SetCellValue($"A{letterDate}{i}", cItem.ComingDate);
                                            sl.SetCellStyle($"A{letterDate}{i}", dateFormat);
                                            sl.SetCellValue($"A{letterNumber}{i}", remain);
                                            cItem.Qty = cRemain;
                                            //如果够ship了就不用找了
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (remain == 0)
                            {
                                sl.SetCellValue($"AC{i}", shipDate);
                                sl.SetCellStyle($"AC{i}", dateFormat);
                                sl.SetCellValue($"AD{i}", rToShip);
                                item.QOHInternalRef = item.QOHInternalRef + " - chekced";
                            }
                            else if (remain > 0)
                            {
                                sl.SetCellValue($"AC{i}", shipDate);
                                sl.SetCellStyle($"AC{i}", dateFormat);
                                sl.SetCellValue($"AD{i}", rToShip);
                                item.Qty = remain;
                            }
                            break;
                        }
                    }
                    //如果仓库没找到 找订单
                    if (!found)
                    {
                        foreach (var cItem in comingPOs)
                        {
                            //订单和detail对比 interReference
                            if (interRef == cItem.CPOInternalRef.Trim())
                            {
                                //var aaa = comingPOs.IndexOf(cItem);
                                remain = cItem.Qty - rToShip;
                                char letterDate = 'C';
                                char letterNumber = 'D';
                                var a = sl.GetCellValueAsString($"A{letterDate}{i}");
                                if (!string.IsNullOrWhiteSpace(sl.GetCellValueAsString($"A{letterDate}{i}")))
                                {
                                    letterDate = 'E';
                                    letterNumber = 'F';
                                    var b = sl.GetCellValueAsString($"A{letterDate}{i}");
                                    if (!string.IsNullOrWhiteSpace(sl.GetCellValueAsString($"A{letterDate}{i}")))
                                    {
                                        letterDate = 'G';
                                        letterNumber = 'H';
                                    }
                                }

                                //如果shipdate 比订单晚就用shipdate
                                DateTime tempDate = shipDate > cItem.ComingDate? shipDate: cItem.ComingDate;

                                //如果coming不够或者没有
                                if (remain < 0)
                                {
                                    sl.SetCellValue($"A{letterDate}{i}", tempDate);
                                    sl.SetCellStyle($"A{letterDate}{i}", dateFormat);
                                    sl.SetCellValue($"A{letterNumber}{i}", cItem.Qty);
                                    cItem.CPOInternalRef = cItem.CPOInternalRef + " - chekced";
                                    rToShip = remain * -1;
                                }
                                else if (remain == 0)
                                {
                                    sl.SetCellValue($"A{letterDate}{i}", tempDate);
                                    sl.SetCellStyle($"A{letterDate}{i}", dateFormat);
                                    sl.SetCellValue($"A{letterNumber}{i}", cItem.Qty);
                                    cItem.CPOInternalRef = cItem.CPOInternalRef + " - chekced";
                                    //如果够ship了就不用找了
                                    break;
                                }
                                else if (remain > 0)
                                {
                                    sl.SetCellValue($"A{letterDate}{i}", tempDate);
                                    sl.SetCellStyle($"A{letterDate}{i}", dateFormat);
                                    sl.SetCellValue($"A{letterNumber}{i}", rToShip);
                                    cItem.Qty = remain;
                                    //如果够ship了就不用找了
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            sl.SaveAs("Results.xlsx");
        }

        public static void CheckComingOrder(int qty)
        {

        }
    }
}
