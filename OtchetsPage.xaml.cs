using System;
using System.Collections.Generic;
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
using System.Globalization;
using System.Threading;
using System.Windows.Shapes;

using Word = Microsoft.Office.Interop.Word;
using Excel = Microsoft.Office.Interop.Excel;

namespace MyClass
{
    /// <summary>
    /// Логика взаимодействия для OtchetsPage.xaml
    /// </summary>
    public partial class OtchetsPage : Page
    {
     //   Entities entities = new Entities();
        public OtchetsPage()
        {
            InitializeComponent();
        }

        private void BExel_Click(object sender, RoutedEventArgs e)
        {
            var db = ClassEntities.GetContext().Ucheniki.ToList().Where(p => p.Klass_id == Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            var alluch = db.ToList();
            var app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            int startRowIndex = 1;
            Excel.Worksheet sheet1 = app.Worksheets.Item[1];
            sheet1.Name = "Документы обучающихся";
            sheet1.Columns.ColumnWidth = 20;
            sheet1.Cells[1][startRowIndex] = "ФИО ";
            sheet1.Cells[2][startRowIndex] = "Дата рождения";
            sheet1.Cells[3][startRowIndex] = "Серия и номер свид. о рождении";
            sheet1.Cells[4][startRowIndex] = "Кем выдано";
            sheet1.Cells[5][startRowIndex] = "Дата выдачи";
            sheet1.Cells[6][startRowIndex] = "Серия и номер паспорта";
            sheet1.Cells[7][startRowIndex] = "Кем выдан";
            sheet1.Cells[8][startRowIndex] = "Дата выдачи";
            sheet1.Cells[9][startRowIndex] = "Снилс";
            sheet1.Cells[10][startRowIndex] = "ИНН";
            sheet1.Cells[11][startRowIndex] = "Медицинский полис";
            foreach (var uch in alluch)
            {
                startRowIndex++;
                for (int i = 0; i < alluch.Count; i++)
                {
                    sheet1.Cells[1][startRowIndex] = uch.F_uch + " " + uch.I_uch + " " + uch.O_uch;
                    sheet1.Cells[2][startRowIndex] = uch.Datarozh_uch;
                    sheet1.Cells[3][startRowIndex] = uch.Dock.Seriya_svid +" "+ uch.Dock.Num_svid;
                    sheet1.Cells[4][startRowIndex] = uch.Dock.Kemvid_svid;
                    if (uch.Dock.Datavid_svid != null)
                    {
                        sheet1.Cells[5][startRowIndex] = uch.Dock.Datavid_svid;
                    }                   
                    sheet1.Cells[6][startRowIndex] = uch.Dock.Seriya_pass + " " + uch.Dock.Num_pass;
                    sheet1.Cells[7][startRowIndex] = uch.Dock.Kemvid_pass;
                    if(uch.Dock.Datavid_pass != null)
                    {
                        sheet1.Cells[8][startRowIndex] = uch.Dock.Datavid_pass;
                    }                  
                    sheet1.Cells[9][startRowIndex] = uch.Dock.Snils;
                    sheet1.Cells[10][startRowIndex] = uch.Dock.INN;
                    sheet1.Cells[11][startRowIndex] = uch.Dock.Medpolis;
                }
            }
            app.Visible = true;
          
        }

        private void BWord_Click(object sender, RoutedEventArgs e)
        {
            var db = ClassEntities.GetContext().Ucheniki.ToList().Where(p => p.Klass_id == 
                Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass);
            var alluch = db.ToList();

           

            var app = new Word.Application();
            Word.Document doc = app.Documents.Add();
            foreach (var uch in alluch)
            {
                Word.Paragraph uchParagraph = doc.Paragraphs.Add();
                Word.Range uchRange = uchParagraph.Range;
                uchRange.Text = "Личная карточка обучающегося";
                uchParagraph.set_Style("Подзаголовок");
                uchParagraph.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                uchRange.InsertParagraphAfter();
                Word.Paragraph uchParagraphFIO = doc.Paragraphs.Add();
                Word.Range uchRangeFIO = uchParagraphFIO.Range;
                uchRangeFIO.Text = uch.F_uch + " " + uch.I_uch + " " + uch.O_uch;
                uchParagraphFIO.set_Style("Подзаголовок");
                uchParagraphFIO.Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                uchRangeFIO.InsertParagraphAfter();
                Word.Paragraph uchParagraphDataR = doc.Paragraphs.Add();
                Word.Range uchRangeDataR = uchParagraphDataR.Range;
                uchRangeDataR.Text = "1) Дата рождения: " + uch.Datarozh_uch.ToShortDateString();
                uchParagraphDataR.set_Style("Обычный");
                uchRangeDataR.InsertParagraphAfter();
                Word.Paragraph uchParagraphAdres = doc.Paragraphs.Add();
                Word.Range uchRangeAdres = uchParagraphAdres.Range;
                uchRangeAdres.Text = "2) Адрес проживания: " + uch.Adres_uch;
                uchParagraphAdres.set_Style("Обычный");
                uchRangeAdres.InsertParagraphAfter();
                Word.Paragraph uchParagraphUsl = doc.Paragraphs.Add();
                Word.Range uchRangeUsl = uchParagraphUsl.Range;
                uchRangeUsl.Text = "2.1) Жилищные условия проживания семьи (нужное подчеркнуть): проживание в коммунальных квартирах(общежитиях), в отельных квартирах, арендуют жильё, в частном доме.";
                uchParagraphUsl.set_Style("Обычный");
                uchRangeUsl.InsertParagraphAfter();
                Word.Paragraph uchParagraphSvid = doc.Paragraphs.Add();
                Word.Range uchRangeSvid = uchParagraphSvid.Range;
                uchRangeSvid.Text = "3) Свидетельство о рождении обучающегося \n" + "серия и номер: " + uch.Dock.Seriya_svid + " " + uch.Dock.Num_svid + " выдано: " + uch.Dock.Kemvid_svid;
                uchParagraphSvid.set_Style("Обычный");
                uchRangeSvid.InsertParagraphAfter();
                Word.Paragraph uchParagraphPass = doc.Paragraphs.Add();
                Word.Range uchRangePass = uchParagraphPass.Range;
                uchRangePass.Text = "4) Паспорт обучающегося \n" + "серия и номер: " + uch.Dock.Seriya_pass + " " + uch.Dock.Num_pass + " выдано: " + uch.Dock.Kemvid_pass;
                uchParagraphPass.set_Style("Обычный");
                uchRangePass.InsertParagraphAfter();
                Word.Paragraph uchParagraphZach = doc.Paragraphs.Add();
                Word.Range uchRangeZach = uchParagraphZach.Range;
                uchRangeZach.Text = "5) Из какого образовательного учреждения поступил:\n";
                uchParagraphZach.set_Style("Обычный");
                uchRangeZach.InsertParagraphAfter();
                Word.Paragraph uchParagraphMed = doc.Paragraphs.Add();
                Word.Range uchRangeMed = uchParagraphMed.Range;
                uchRangeMed.Text = "6) Медицинский полис обучающегося: " + uch.Dock.Medpolis;
                uchParagraphMed.set_Style("Обычный");
                uchRangeMed.InsertParagraphAfter();

                Word.Paragraph uchParagraphPhone = doc.Paragraphs.Add();
                Word.Range uchRangePhone = uchParagraphPhone.Range;
                uchRangePhone.Text = "7) Контактный телефон обучающегося: " + uch.Phone_uch;
                uchParagraphPhone.set_Style("Обычный");
                uchRangePhone.InsertParagraphAfter();

                Word.Paragraph uchParagraphR = doc.Paragraphs.Add();
                Word.Range uchRangeR = uchParagraphR.Range;
                uchRangeR.Text = "Родители:";
                uchParagraphR.set_Style("Обычный");

                var roduch = ClassEntities.GetContext().RodUch.ToList().Where(p => p.Uch_id == uch.ID_uch);
                var allrod = roduch.ToList();
                if (roduch != null)
                {
                    foreach (var rod in allrod)
                    {
                        Word.Paragraph uchParagraphRVidrod = doc.Paragraphs.Add();
                        Word.Range uchRangeRVidrod = uchParagraphR.Range;
                        uchRangeRVidrod.Text = "Родство: " + rod.Roditeli.Vidrods.N_vidrod;
                        uchParagraphRVidrod.set_Style("Обычный");
                        
                        Word.Paragraph uchParagraphRFIO = doc.Paragraphs.Add();
                        Word.Range uchRangeRFIO = uchParagraphR.Range;
                        uchRangeRFIO.Text = "ФИО: " + rod.Roditeli.F_rod + " " + rod.Roditeli.I_rod + " " + rod.Roditeli.O_rod;
                        uchParagraphRFIO.set_Style("Обычный");
                        uchRangeRFIO.InsertParagraphAfter();

                        Word.Paragraph uchParagraphR_Mestorab = doc.Paragraphs.Add();
                        Word.Range uchRangeR_Mestorab = uchParagraphR_Mestorab.Range;
                        uchRangeR_Mestorab.Text = "Место работы: " + rod.Roditeli.Mestorab_rod;
                        uchParagraphR_Mestorab.set_Style("Обычный");
                        uchRangeR_Mestorab.InsertParagraphAfter();
                        Word.Paragraph uchParagraphR_Phone = doc.Paragraphs.Add();
                        Word.Range uchRangeR_Phone = uchParagraphR_Phone.Range;
                        uchRangeR_Phone.Text = "Контактный телефон: " + rod.Roditeli.Phone_rod;
                        uchParagraphR_Phone.set_Style("Обычный");
                        uchRangeR_Phone.InsertParagraphAfter();

                    }

                    

                }
             

                if (uch != alluch.LastOrDefault())
                {
                    doc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                }
            }
            app.Visible = true;
            
        }

        private void BMer_Click(object sender, RoutedEventArgs e)
        {
            var db = ClassEntities.GetContext().Meropriyatia.ToList().Where(p => p.Klass_id == 
                Bduser.SetUser().KlRuks.First().Klasses.First().ID_klass).OrderBy(p=>p.Data_mer);//список сортированный по дате
            var finishmer = db.ToList().Where(p=>p.Status_id==1); //список только проведенных
            var allmer = finishmer.ToList();
            var app = new Excel.Application();
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            int startRowIndex = 5;

            Excel.Worksheet sheet1 = app.Worksheets.Item[1];
           
            sheet1.Name = "Воспитательные мероприятия";
             Excel.Range headerRange = sheet1.Range[sheet1.Cells[1][1], sheet1.Cells[8][3]];
            headerRange.Merge();
            headerRange.Value = "Перечень воспитательных мероприятий, \n проведенных классным руководителем - " 
                + Bduser.SetUser().KlRuks.First().F_klruk + " " + Bduser.SetUser().KlRuks.First().I_klruk + " " 
                + Bduser.SetUser().KlRuks.First().O_klruk +"\n" + " и в которых приняли участие учащиеся " + Bduser.SetUser().KlRuks.First().Klasses.First().N_klass + " класса";
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                sheet1.Cells[1][startRowIndex] = "Дата мероприятия";
            sheet1.Cells[2][startRowIndex] = "Тип мероприятия";
            sheet1.Cells[3][startRowIndex] = "Название мероприятия";

          
            foreach (var mer in allmer)
            {
                startRowIndex++;
                for (int i = 0; i < allmer.Count; i++)
                {
                    
                    sheet1.Cells[1][startRowIndex] = mer.Data_mer;
                    sheet1.Cells[2][startRowIndex] = mer.VidMer.N_vidmer;
                    sheet1.Cells[3][startRowIndex] = mer.N_mer;
                    
                }

                Excel.Range rangeBorders = sheet1.Range[sheet1.Cells[1][5], sheet1.Cells[3][startRowIndex]];
                rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                     rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                     rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                      rangeBorders.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                       rangeBorders.Borders[Excel.XlBordersIndex.xlInsideHorizontal].LineStyle =
                       rangeBorders.Borders[Excel.XlBordersIndex.xlInsideVertical].LineStyle = Excel.XlLineStyle.xlContinuous;
                sheet1.Columns.AutoFit();



            }
            app.Visible = true;
            
        }
    }
}
