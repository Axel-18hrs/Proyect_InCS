using iText.Kernel.Pdf;
using iText.Layout.Element;
using Newtonsoft.Json;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace appSpotifySongs
{
    public partial class Form1 : Form
    {
        private Dictionary<string, string> cambios = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();
            string query = "SELECT * FROM Songs_Sp.dbo.spotify_songs";
            DataTable dataTable = Connection.GetDataTable(query);
            dgvSongs.DataSource = dataTable;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormClosed += MiFormulario_Closed;
        }

        private void MiFormulario_Closed(object sender, EventArgs e)
        {
            // Manejar el evento aquí
            MessageBox.Show("La ventana se ha cerrado.");
            Beggining.Instancia.Show();
        }

        private void btnCSV_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Files CSV (*.csv)|*.csv|All files (*.*)|*.*";
            saveFileDialog.Title = "Save file CSV";

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            ExportarDataGridViewCSV(saveFileDialog.FileName);
        }

        private void ExportarDataGridViewCSV( string fileName)
        {
            if (dgvSongs.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < dgvSongs.Columns.Count; i++)
            {
                sb.Append(dgvSongs.Columns[i].HeaderText);
                if (i < dgvSongs.Columns.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.AppendLine();

            foreach (DataGridViewRow row in dgvSongs.Rows)
            {
                for (int i = 0; i < dgvSongs.Columns.Count; i++)
                {
                    sb.Append(row.Cells[i].Value);
                    if (i < dgvSongs.Columns.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.AppendLine();
            }

            try
            {
                File.WriteAllText(fileName, sb.ToString());
                MessageBox.Show("Los datos se han exportado correctamente a " + fileName, "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al exportar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXML_Click(object sender, EventArgs e)
        {
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.Title = "Save XML File";

            // Show a TextBox to enter the file name
            TextBox txtFileName = new TextBox();
            txtFileName.Text = "data"; // Default file name
            saveFileDialog.FileName = txtFileName.Text;

            // Show the save file dialog
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Export DataGridView data to the selected XML file
                ExportDataGridViewToXML(dgvSongs, saveFileDialog.FileName);
            }
        }

        private void ExportDataGridViewToXML(DataGridView dataGridView, string fileName)
        {
            XmlDocument xmlDocument = new XmlDocument();
            XmlElement rootNode = xmlDocument.CreateElement("Data");
            xmlDocument.AppendChild(rootNode);

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                XmlElement rowElement = xmlDocument.CreateElement("Row");

                foreach (DataGridViewCell cell in row.Cells)
                {
                    string cellValue = cell.Value?.ToString() ?? "";
                    XmlElement cellElement = xmlDocument.CreateElement("Cell");
                    cellElement.InnerText = cellValue;
                    rowElement.AppendChild(cellElement);
                }
                rootNode.AppendChild(rowElement);
            }

            try
            {
                xmlDocument.Save(fileName);
                MessageBox.Show("Data exported successfully to " + fileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnJSON_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json|All files (*.*)|*.*";
            saveFileDialog.Title = "Save JSON File";
            saveFileDialog.FileName = "data";

            // Show the save file dialog
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Export DataGridView data to the selected JSON file
                ExportDataGridViewToJSON(dgvSongs, saveFileDialog.FileName);
            }
        }

        private void ExportDataGridViewToJSON(DataGridView dataGridView, string fileName)
        {
            List<Dictionary<string, object>> dataList = new List<Dictionary<string, object>>();

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                Dictionary<string, object> rowData = new Dictionary<string, object>();

                foreach (DataGridViewCell cell in row.Cells)
                {
                    rowData[dataGridView.Columns[cell.ColumnIndex].HeaderText] = cell.Value;
                }

                dataList.Add(rowData);
            }

            string jsonData = JsonConvert.SerializeObject(dataList, Newtonsoft.Json.Formatting.Indented);

            try
            {
                File.WriteAllText(fileName, jsonData);
                MessageBox.Show("Data exported successfully to " + fileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Show the save file dialog
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf|All files (*.*)|*.*";
            saveFileDialog.Title = "Save PDF File";
            saveFileDialog.FileName = "data";

            // Show the save file dialog
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Export DataGridView data to the selected PDF file
                ExportDataGridViewToPDF(dgvSongs, saveFileDialog.FileName);
            }
        }

        private void ExportDataGridViewToPDF(DataGridView dataGridView, string fileName)
        {
            try
            {
                // Create a PdfWriter for the PDF file
                PdfWriter pdfWriter = new PdfWriter(fileName);
                PdfDocument pdfDocument = new PdfDocument(pdfWriter);
                iText.Layout.Document document = new iText.Layout.Document(pdfDocument);

                // Create a table with the same number of columns as the DataGridView
                Table table = new Table(dataGridView.Columns.Count);

                // Add column headers to the table
                foreach (DataGridViewColumn column in dataGridView.Columns)
                {
                    table.AddHeaderCell(column.HeaderText);
                }

                // Add data rows to the table
                foreach (DataGridViewRow row in dataGridView.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        table.AddCell(cell.Value?.ToString());
                    }
                }

                // Add the table to the document
                document.Add(table);

                // Close the document
                document.Close();

                MessageBox.Show("Data exported successfully to " + fileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSongs_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Obtener el valor de la primera celda en la fila donde se activó el evento
                string firstCellValue = dgvSongs.Rows[e.RowIndex].Cells[0].Value.ToString();

                // Obtener el nombre de la columna modificada
                string columnName = dgvSongs.Columns[e.ColumnIndex].Name;

                // Construir la clave utilizando el valor de la primera celda y el nombre de la columna modificada
                string key = $"{firstCellValue}-{columnName}";

                // Guardar el valor de la celda modificada en el diccionario cambios usando la clave
                cambios[key] = dgvSongs.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }

        private void btnSaveSQL_Click(object sender, EventArgs e)
        {
            foreach (var cambio in cambios)
            {
                string[] indices = cambio.Key.Split('-');
                string firstCellValue = indices[0];
                string columnName = indices[1];
                string newValue = cambio.Value;

                // Construir la consulta SQL parametrizada para actualizar la celda modificada en la base de datos
                string query = $"UPDATE Songs_Sp.dbo.spotify_songs SET {columnName} = @NewValue WHERE track_id = @FirstCellValue";

                // Ejecutar la consulta SQL con parámetros
                Connection.ExecuteQuery(query, new SqlParameter("@NewValue", newValue), new SqlParameter("@FirstCellValue", firstCellValue));

                // Mostrar un mensaje indicando que se actualizó la celda
                MessageBox.Show($"Se actualizó la celda en la fila {firstCellValue} y columna {columnName} con el valor {newValue}");
            }

            cambios.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cambios.Clear();
            string query = "SELECT * FROM Songs_Sp.dbo.spotify_songs";
            DataTable dataTable = Connection.GetDataTable(query);
            dgvSongs.DataSource = null;
            dgvSongs.DataSource = dataTable;
            MessageBox.Show("Se han revertido los cambios.");
        }
    }
}
