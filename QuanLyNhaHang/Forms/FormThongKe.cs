using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using PdfFont = iTextSharp.text.Font;





namespace QuanLyNhaHang.Forms
{
    public partial class FormThongKe : Form
    {
        private readonly string connStr =
            @"Data Source=.;Initial Catalog=RestaurantContextDB;Integrated Security=True";

        public FormThongKe()
        {
            InitializeComponent();

            Load += FormThongKe_Load;
            btnThongKe.Click += BtnThongKe_Click;
            btnThoat.Click += (s, e) => Close();
        }

        private void FormThongKe_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;

            dtpFrom.Value = new DateTime(now.Year, now.Month, 1);

            dtpTo.Value = new DateTime(now.Year, now.Month,
                DateTime.DaysInMonth(now.Year, now.Month));


            cboLocTheo.Items.AddRange(new string[]
            {
                "Doanh thu theo ngày (trong tháng)",
                "Doanh thu theo tháng (trong năm)",
                "Doanh thu theo năm",
                "Doanh thu món (Cao->thấp)",
                "Doanh thu món (Thấp->cao)",
                "Doanh thu theo nhân viên",
                "Doanh thu theo Bàn phục vụ"
            });

            cboLocTheo.SelectedIndex = 0;

            chartThongKe.Series.Clear();
            lblTongDoanhThu.Visible = false;

            SetupChart();
        }

        //setup chart
        private void SetupChart()
        {
            chartThongKe.Series.Clear();
            chartThongKe.Legends.Clear();

          
            chartThongKe.ChartAreas.Clear(); // clear chart cũ

            // tạo chart mới tên main
            var ca = chartThongKe.ChartAreas.Add("Main");

            //cấu hình cho các cột
            ca.AxisX.MajorGrid.Enabled = false;
            ca.AxisY.MajorGrid.Enabled = true;

            ca.AxisX.Interval = 1;
            ca.AxisY.LabelStyle.Format = "#,###";

            ca.AxisX.TitleFont = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);
            ca.AxisY.TitleFont = new System.Drawing.Font("Segoe UI", 12, System.Drawing.FontStyle.Bold);

            ca.AxisY.IsStartedFromZero = true;
            ca.AxisY.ScaleBreakStyle.Enabled = false;
            ca.AxisY.Minimum = double.NaN;
            ca.AxisY.Maximum = double.NaN;
        }

        //event btnThongKe click
        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            if (dtpFrom.Value > dtpTo.Value)
            {
                MessageBox.Show("Khoảng thời gian không hợp lệ!");
                return;
            }

            //thống kê theo kiểu lọc
            switch (cboLocTheo.SelectedIndex)
            {
                case 0:   // theo ngày (cùng tháng năm)
                    if (dtpFrom.Value.Month != dtpTo.Value.Month || dtpFrom.Value.Year != dtpTo.Value.Year)
                    {
                        MessageBox.Show("Xem theo ngày chỉ áp dụng trong cùng 1 tháng & năm!");
                        return;
                    }
                    break;

                case 1: //theo tháng(cùng năm)
                    if (dtpFrom.Value.Year != dtpTo.Value.Year)
                    {
                        MessageBox.Show("Xem theo tháng chỉ áp dụng trong cùng 1 năm!");
                        return;
                    }
                    break;

                case 2: // theo năm
                    dtpFrom.Value = new DateTime(dtpFrom.Value.Year, 1, 1);
                    dtpTo.Value = new DateTime(dtpTo.Value.Year, 12, 31);
                    break;

                default:
                    // các kiểu lọc khác k cần check
                    break;
            }

            LoadThongKe();
            LoadTongDoanhThu();
        }

        // load chart theo kieu lọc
        private void LoadThongKe()
        {
            SetupChart(); // refersh chart

            Series s = new Series("Thống kê");
            s.ChartType = SeriesChartType.Column;
            s.IsValueShownAsLabel = true;
            s.XValueType = ChartValueType.Int32;
            s.IsXValueIndexed = true;

            chartThongKe.Series.Add(s);

            DataTable dt = new DataTable();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                chartThongKe.ChartAreas[0].AxisY.Title = "(Doanh thu)";



                

                // 0 : ngày
                if (cboLocTheo.SelectedIndex == 0)
                {
                    chartThongKe.ChartAreas[0].AxisX.Title = "(Ngày)";

                    cmd.CommandText = @"
                SELECT DAY(NgayLap) AS NgayLap, 
                       SUM(TongTien) AS DoanhThu,
                       COUNT(MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon
                WHERE TrangThaiThanhToan = N'Đã thanh toán'
                  AND NgayLap BETWEEN @f AND @t
                GROUP BY DAY(NgayLap)";
                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                    dt.Load(cmd.ExecuteReader());
                    dgvThongKe.DataSource = dt;

                    if (dgvThongKe.Columns.Contains("NgayLap"))
                        dgvThongKe.Columns["NgayLap"].HeaderText = "Ngày";

                    if (dgvThongKe.Columns.Contains("DoanhThu"))
                        dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";

                    if (dgvThongKe.Columns.Contains("SoLuongHoaDon"))
                        dgvThongKe.Columns["SoLuongHoaDon"].HeaderText = "Số lượng hóa đơn";

                    foreach (DataRow row in dt.Rows)
                    {
                        int ngay = Convert.ToInt32(row["NgayLap"]);
                        decimal doanhThu = Convert.ToDecimal(row["DoanhThu"]);
                        s.Points.AddXY(ngay, doanhThu);
                    }
                }

                // 1:Tháng
                else if (cboLocTheo.SelectedIndex == 1)
                {
                    chartThongKe.ChartAreas[0].AxisX.Title = "(Tháng)";

                    cmd.CommandText = @"
                SELECT MONTH(NgayLap) AS Thang,
                       SUM(TongTien) AS DoanhThu,
                       COUNT(MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon
                WHERE TrangThaiThanhToan = N'Đã thanh toán'
                  AND NgayLap BETWEEN @f AND @t
                GROUP BY MONTH(NgayLap)";
                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                    dt.Load(cmd.ExecuteReader());
                    dgvThongKe.DataSource = dt;

                    if (dgvThongKe.Columns.Contains("Thang"))
                        dgvThongKe.Columns["Thang"].HeaderText = "Tháng";

                    if (dgvThongKe.Columns.Contains("DoanhThu"))
                        dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";

                    if (dgvThongKe.Columns.Contains("SoLuongHoaDon"))
                        dgvThongKe.Columns["SoLuongHoaDon"].HeaderText = "Số lượng hóa đơn";

                    foreach (DataRow row in dt.Rows)
                    {
                        int thang = Convert.ToInt32(row["Thang"]);
                        decimal doanhThu = Convert.ToDecimal(row["DoanhThu"]);
                        s.Points.AddXY(thang, doanhThu);
                    }
                }

                // 2:Năm
                else if (cboLocTheo.SelectedIndex == 2)
                {
                    chartThongKe.ChartAreas[0].AxisX.Title = "(Năm)";

                    cmd.CommandText = @"
                SELECT YEAR(NgayLap) AS Nam, 
                       SUM(TongTien) AS DoanhThu,
                       COUNT(MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon
                WHERE TrangThaiThanhToan = N'Đã thanh toán'
                  AND NgayLap BETWEEN @f AND @t
                GROUP BY YEAR(NgayLap)
                ORDER BY YEAR(NgayLap)";
                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                    dt.Load(cmd.ExecuteReader());
                    dgvThongKe.DataSource = dt;

                    if (dgvThongKe.Columns.Contains("Nam"))
                        dgvThongKe.Columns["Nam"].HeaderText = "Năm";

                    if (dgvThongKe.Columns.Contains("DoanhThu"))
                        dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";

                    if (dgvThongKe.Columns.Contains("SoLuongHoaDon"))
                        dgvThongKe.Columns["SoLuongHoaDon"].HeaderText = "Số lượng hóa đơn";

                    foreach (DataRow r in dt.Rows)
                        s.Points.AddXY(Convert.ToInt32(r["Nam"]),
                                       Convert.ToDecimal(r["DoanhThu"]));
                }

                //3: món cao=> thấp
                else if (cboLocTheo.SelectedIndex == 3)
                {
                    chartThongKe.ChartAreas[0].AxisX.Title = "(Món ăn)";

                    cmd.CommandText = @"
                SELECT m.TenMon,
                       SUM(c.ThanhTien) AS DoanhThu,
                       COUNT(h.MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon h
                JOIN HoaDonChiTiet c ON h.MaHoaDon=c.MaHoaDon
                JOIN MonAn m ON c.MaMon=m.MaMon
                WHERE h.TrangThaiThanhToan=N'Đã thanh toán'
                AND h.NgayLap BETWEEN @f AND @t
                GROUP BY m.TenMon
                ORDER BY DoanhThu DESC";

                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                    dt.Load(cmd.ExecuteReader());
                    dgvThongKe.DataSource = dt;

                    if (dgvThongKe.Columns.Contains("TenMon"))
                        dgvThongKe.Columns["TenMon"].HeaderText = "Tên món";

                    if (dgvThongKe.Columns.Contains("DoanhThu"))
                        dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";

                    if (dgvThongKe.Columns.Contains("SoLuongHoaDon"))
                        dgvThongKe.Columns["SoLuongHoaDon"].HeaderText = "Số lượng hóa đơn";

                    foreach (DataRow r in dt.Rows)
                        s.Points.AddXY(r["TenMon"].ToString(),
                                       Convert.ToDecimal(r["DoanhThu"]));
                }

                // 4. món thấp=> cao
                else if (cboLocTheo.SelectedIndex == 4)
                {
                    chartThongKe.ChartAreas[0].AxisX.Title = "(Món ăn)";

                    cmd.CommandText = @"
                SELECT m.TenMon, 
                       SUM(c.ThanhTien) AS DoanhThu,
                       COUNT(h.MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon h
                JOIN HoaDonChiTiet c ON h.MaHoaDon=c.MaHoaDon
                JOIN MonAn m ON c.MaMon=m.MaMon
                WHERE h.TrangThaiThanhToan=N'Đã thanh toán'
                AND h.NgayLap BETWEEN @f AND @t
                GROUP BY m.TenMon
                ORDER BY DoanhThu ASC";

                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                    dt.Load(cmd.ExecuteReader());
                    dgvThongKe.DataSource = dt;

                    if (dgvThongKe.Columns.Contains("TenMon"))
                        dgvThongKe.Columns["TenMon"].HeaderText = "Tên món";

                    if (dgvThongKe.Columns.Contains("DoanhThu"))
                        dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";

                    if (dgvThongKe.Columns.Contains("SoLuongHoaDon"))
                        dgvThongKe.Columns["SoLuongHoaDon"].HeaderText = "Số lượng hóa đơn";

                    foreach (DataRow r in dt.Rows)
                        s.Points.AddXY(r["TenMon"].ToString(),
                                       Convert.ToDecimal(r["DoanhThu"]));
                }

                // 5. doanh thu theo nhân viên
                else if (cboLocTheo.SelectedIndex == 5)
                {
                    chartThongKe.ChartAreas[0].AxisX.Title = "(Nhân viên)";

                    cmd.CommandText = @"
                SELECT n.TenNV, 
                       SUM(h.TongTien) AS DoanhThu, 
                       COUNT(h.MaHoaDon) AS SoLuongHoaDon
                FROM HoaDon h
                JOIN NhanVien n ON h.MaNV=n.MaNV
                WHERE TrangThaiThanhToan=N'Đã thanh toán'
                AND NgayLap BETWEEN @f AND @t
                GROUP BY n.TenNV
                ORDER BY DoanhThu DESC";

                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                    dt.Load(cmd.ExecuteReader());
                    dgvThongKe.DataSource = dt;

                    if (dgvThongKe.Columns.Contains("TenNV"))
                        dgvThongKe.Columns["TenNV"].HeaderText = "Tên nhân viên";

                    if (dgvThongKe.Columns.Contains("DoanhThu"))
                        dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";

                    if (dgvThongKe.Columns.Contains("SoLuongHoaDon"))
                        dgvThongKe.Columns["SoLuongHoaDon"].HeaderText = "Số lượng hóa đơn";

                    foreach (DataRow r in dt.Rows)
                        s.Points.AddXY(r["TenNV"].ToString(),
                                       Convert.ToDecimal(r["DoanhThu"]));
                }

                //6. theo bàn
                else
                {
                    chartThongKe.ChartAreas[0].AxisX.Title = "(Bàn)";

                    cmd.CommandText = @"
                    SELECT b.TenBan,
                        SUM(h.TongTien) AS DoanhThu,
                        COUNT(h.MaHoaDon) AS SoLuongHoaDon
                    FROM HoaDon h
                    JOIN Ban b ON h.MaBan = b.MaBan
                    WHERE TrangThaiThanhToan = N'Đã thanh toán'
                    AND NgayLap BETWEEN @f AND @t
                    GROUP BY b.TenBan
                    ORDER BY DoanhThu DESC";

                    cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                    cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                    dt.Load(cmd.ExecuteReader());
                    dgvThongKe.DataSource = dt;

                    //đổi tiêu đề cột theo loại lọc
                    if (dgvThongKe.Columns.Contains("TenBan"))
                        dgvThongKe.Columns["TenBan"].HeaderText = "Tên bàn";

                    if (dgvThongKe.Columns.Contains("SoLuongHoaDon"))
                        dgvThongKe.Columns["SoLuongHoaDon"].HeaderText = "Số lượng hóa đơn";

                    if (dgvThongKe.Columns.Contains("DoanhThu"))
                    {
                        dgvThongKe.Columns["DoanhThu"].HeaderText = "Doanh thu";
                        dgvThongKe.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                        dgvThongKe.Columns["DoanhThu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }

                    foreach (DataRow r in dt.Rows)
                    {
                        DataPoint dp = new DataPoint();
                        dp.XValue = s.Points.Count + 1; // stt
                        dp.YValues = new double[] { Convert.ToDouble(r["DoanhThu"]) };
                        dp.AxisLabel = r["TenBan"].ToString();

                        s.Points.Add(dp);
                    }
                }

                dgvThongKe.DataSource = dt;
            }
        }

        // load tổng doanh thu vào lblTongDoanhThu
        private void LoadTongDoanhThu()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(@"
            SELECT SUM(TongTien)
            FROM HoaDon
            WHERE TrangThaiThanhToan = N'Đã thanh toán'
              AND NgayLap BETWEEN @f AND @t", conn);

                cmd.Parameters.AddWithValue("@f", dtpFrom.Value.Date);
                cmd.Parameters.AddWithValue("@t", dtpTo.Value.Date);

                object rs = cmd.ExecuteScalar();
                lblTongDoanhThu.Text = rs == DBNull.Value ? "0 VND"
                    : Convert.ToDecimal(rs).ToString("N0") + " VND";
            }

            lblTongDoanhThu.Visible = true;
        }

        //event btnIn click
        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (chartThongKe.Series.Count == 0 || chartThongKe.Series[0].Points.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để in báo cáo!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SaveFileDialog sfd = new SaveFileDialog();
                string timeName = DateTime.Now.ToString("ddMMyyyy_HHmmss");

                sfd.FileName = $"ThongKe_{timeName}";
                sfd.Filter =
                    "PDF File|*.pdf|Word File|*.docx|Excel File|*.xlsx";
                sfd.DefaultExt = "pdf";

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                string ext = Path.GetExtension(sfd.FileName).ToLower();

                // in ra pdf
                if (ext == ".pdf")
                {
                    string chartImagePath = Path.Combine(Path.GetTempPath(), $"chart_{timeName}.png");
                    chartThongKe.SaveImage(chartImagePath,
                        System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);

                    using (FileStream stream = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        Document pdf = new Document(PageSize.A4.Rotate(), 40f, 40f, 40f, 40f);
                        PdfWriter.GetInstance(pdf, stream);
                        pdf.Open();

                        BaseFont bf = BaseFont.CreateFont(
                            @"C:\Windows\Fonts\arial.ttf",
                            BaseFont.IDENTITY_H,
                            BaseFont.EMBEDDED
                        );

                        iTextSharp.text.Font fontTitle = new iTextSharp.text.Font(bf, 20, iTextSharp.text.Font.BOLD);
                        iTextSharp.text.Font fontText = new iTextSharp.text.Font(bf, 12);
                        iTextSharp.text.Font fontBold = new iTextSharp.text.Font(bf, 13, iTextSharp.text.Font.BOLD);

                        Paragraph title = new Paragraph("BÁO CÁO THỐNG KÊ NHÀ HÀNG", fontTitle);
                        title.Alignment = Element.ALIGN_CENTER;
                        title.SpacingAfter = 20f;
                        pdf.Add(title);

                        pdf.Add(new Paragraph($"Từ ngày: {dtpFrom.Value:dd/MM/yyyy}", fontText));
                        pdf.Add(new Paragraph($"Đến ngày: {dtpTo.Value:dd/MM/yyyy}", fontText));
                        pdf.Add(new Paragraph($"Kiểu thống kê: {cboLocTheo.Text}", fontText));
                        pdf.Add(new Paragraph("\n"));

                        Paragraph tong = new Paragraph($"Tổng doanh thu: {lblTongDoanhThu.Text}", fontBold);
                        tong.SpacingAfter = 10f;
                        pdf.Add(tong);

                        pdf.Add(new Paragraph("Biểu đồ thống kê:", fontBold));
                        pdf.Add(new Paragraph("\n"));

                        iTextSharp.text.Image chartImg = iTextSharp.text.Image.GetInstance(chartImagePath);
                        chartImg.Alignment = Element.ALIGN_CENTER;
                        chartImg.ScaleToFit(780f, 380f);
                        pdf.Add(chartImg);

                        pdf.Add(new Paragraph("\nDữ liệu chi tiết:", fontBold));
                        pdf.Add(new Paragraph("\n"));

                        PdfPTable table = new PdfPTable(dgvThongKe.Columns.Count);
                        table.WidthPercentage = 100;

                        foreach (DataGridViewColumn col in dgvThongKe.Columns)
                        {
                            PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText, fontBold));
                            cell.HorizontalAlignment = Element.ALIGN_CENTER;
                            cell.BackgroundColor = new BaseColor(220, 220, 220);
                            table.AddCell(cell);
                        }

                        foreach (DataGridViewRow row in dgvThongKe.Rows)
                        {
                            if (!row.IsNewRow)
                            {
                                foreach (DataGridViewCell cell in row.Cells)
                                {
                                    string text = cell.Value == null ? "" : cell.Value.ToString();
                                    table.AddCell(new Phrase(text, fontText));
                                }
                            }
                        }

                        pdf.Add(table);
                        pdf.Close();
                    }
                }
                // in ra word
                else if (ext == ".docx")
                {
                    ExportToWord(sfd.FileName);
                }
                //in ra excel
                else if (ext == ".xlsx")
                {
                    ExportToExcel(sfd.FileName);
                }

                MessageBox.Show("Xuất báo cáo thành công!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi in báo cáo: " + ex.Message);
            }
        }



        private void ExportToWord(string path)
        {
            Word.Application word = new Word.Application();
            Word.Document doc = word.Documents.Add();

            word.Visible = false;

            Word.Paragraph p = doc.Content.Paragraphs.Add();
            p.Range.Text = "BÁO CÁO THỐNG KÊ NHÀ HÀNG";
            p.Range.InsertParagraphAfter();

            doc.Content.Text += "\nTừ ngày: " + dtpFrom.Value.ToShortDateString();
            doc.Content.Text += "\nĐến ngày: " + dtpTo.Value.ToShortDateString();
            doc.Content.Text += "\nKiểu thống kê: " + cboLocTheo.Text + "\n\n";

            // chart
            string imgPath = Path.Combine(Path.GetTempPath(), "chart.png");
            Bitmap bmp = new Bitmap(chartThongKe.Width, chartThongKe.Height);
            chartThongKe.DrawToBitmap(bmp, chartThongKe.ClientRectangle);
            bmp.Save(imgPath, ImageFormat.Png);

            doc.InlineShapes.AddPicture(imgPath);

            // table
            int rows = dgvThongKe.Rows.Count;
            int cols = dgvThongKe.Columns.Count;

            Word.Table tbl = doc.Tables.Add(doc.Bookmarks.get_Item("\\endofdoc").Range,
                                             rows + 1, cols);

            // header
            for (int c = 0; c < cols; c++)
                tbl.Cell(1, c + 1).Range.Text = dgvThongKe.Columns[c].HeaderText;

            // data
            for (int r = 0; r < rows; r++)
                for (int c = 0; c < cols; c++)
                    tbl.Cell(r + 2, c + 1).Range.Text =
                        dgvThongKe.Rows[r].Cells[c].Value?.ToString();

            doc.SaveAs2(path);
            doc.Close();
            word.Quit();
        }



        private void ExportToExcel(string path)
        {
            Excel.Application app = new Excel.Application();
            Excel.Workbook wb = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet ws = wb.ActiveSheet;

            ws.Cells[1, 1] = "BÁO CÁO THỐNG KÊ NHÀ HÀNG";
            ws.Cells[2, 1] = "Từ ngày: " + dtpFrom.Value.ToShortDateString();
            ws.Cells[3, 1] = "Đến ngày: " + dtpTo.Value.ToShortDateString();
            ws.Cells[4, 1] = "Kiểu thống kê: " + cboLocTheo.Text;

            // header
            for (int i = 0; i < dgvThongKe.Columns.Count; i++)
                ws.Cells[6, i + 1] = dgvThongKe.Columns[i].HeaderText;

            // data
            for (int r = 0; r < dgvThongKe.Rows.Count; r++)
                for (int c = 0; c < dgvThongKe.Columns.Count; c++)
                    ws.Cells[r + 7, c + 1] =
                        dgvThongKe.Rows[r].Cells[c].Value?.ToString();

            // chart
            string img = Path.Combine(Path.GetTempPath(), "chart.png");
            Bitmap bmp = new Bitmap(chartThongKe.Width, chartThongKe.Height);
            chartThongKe.DrawToBitmap(bmp, chartThongKe.ClientRectangle);
            bmp.Save(img, ImageFormat.Png);

            ws.Shapes.AddPicture(img,
                Microsoft.Office.Core.MsoTriState.msoFalse,
                Microsoft.Office.Core.MsoTriState.msoCTrue,
                10, 250, 700, 350);

            wb.SaveAs(path);
            wb.Close();
            app.Quit();
        }

    }
}
