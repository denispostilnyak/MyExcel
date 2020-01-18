using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Antlr4.Runtime;
namespace lab_2
{
    public partial class Form1 : Form
    {
        const int st = 100;
        const int st1 = 20;
        int COLUMNS = 0;
        int ROWS = 0;
        CreateNewEl NewEl = new CreateNewEl();
        Cell[,] table = new Cell[st, st];
        Cell[,] SaveTable = new Cell[st1, st1];
        List<string> ColumnList = new List<string>();
        public Form1()
        {
            InitializeComponent();
            for (int i = 0; i < st; ++i)
            {
                for (int j = 0; j < st; j++)
                {
                    table[i, j] = new Cell();
                }
            }
            for (int i = 0; i < st1; ++i)
            {
                for (int j = 0; j < st1; j++)
                {
                    SaveTable[i, j] = new Cell();
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            COLUMNS = 5;
            ROWS = 10;
            string temp = null;
            char c = 'A';
            Cell[,] table = new Cell[st, st];
            for (int i = 0; i < COLUMNS; ++i)
            {
                temp += c;
                dataGridView1.Columns.Add(Name, temp);
                c++;
                temp = null;

            }
            for (int i = 0; i < ROWS; ++i)
            {
                dataGridView1.Rows.Add();
            }
            for (int i = 0; i < dataGridView1.Rows.Count; ++i)
            {
                dataGridView1.Rows[i].HeaderCell.Value = i.ToString();
            }
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {

                    dataGridView1.Rows[i].Cells[j].Value = " ";
                }
            }
        }
        
        private void Button3_Click(object sender, EventArgs e)
        {
            addColumnInTheEnd();
        }
        public void addColumnInTheEnd()
        {
            NewEl.AddColumn(dataGridView1, COLUMNS);
            COLUMNS++;
            for (int i = 0; i < dataGridView1.RowCount - 1; i++)
            {


                dataGridView1.Rows[i].Cells[COLUMNS - 1].Value = " ";

            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            AddRowInTheEnd();
            
        }
        public void AddRowInTheEnd()
        {
            NewEl.AddRow(ROWS, dataGridView1);
            ROWS++;

            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {

                dataGridView1.Rows[ROWS - 1].Cells[j].Value = " ";
            }
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            RemoveRowInTheEnd();
        }
        public void RemoveRowInTheEnd()
        {

            bool flag = true;
            if (dataGridView1.RowCount <= 2)
            {
                MessageBox.Show("Неможливо видалити");
                flag = false;
            }
            ClassBaseSys sys = new ClassBaseSys();
            ColumnList.Clear();
            if (flag)
            {
                for (int i = 0; i < COLUMNS; ++i)
                {
                    string res = "";
                    res = sys.ToEx(i);
                    res += (ROWS - 1).ToString();
                    ColumnList.Add(res);
                }

                RefreshForColumn(ColumnList);

                dataGridView1.Rows.RemoveAt(ROWS - 1);
                ROWS--;
                dataGridView1.Rows[ROWS].HeaderCell.Value = ROWS.ToString();
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            RemoveColumnEnd();
        }
        public void RemoveColumnEnd()

        {
            bool columnIsEmpty = true;
            for (int j = 0; j < dataGridView1.RowCount - 1; ++j)
            {
                char c = Char.Parse(dataGridView1.Rows[j].Cells[COLUMNS - 1].Value.ToString());
                if (COLUMNS <= 2)
                {
                    columnIsEmpty = false;
                }
            }
            if (columnIsEmpty == false)
            {
                MessageBox.Show("Неможливо видалити");
            }
            else
            {
                ClassBaseSys sys = new ClassBaseSys();
                ColumnList.Clear();
                for (int i = 0; i < ROWS; ++i)
                {
                    string res = "";
                    res = sys.ToEx(COLUMNS - 1);
                    res += i.ToString();
                    ColumnList.Add(res);
                }

                RefreshForColumn(ColumnList);
                if (columnIsEmpty == true)
                {

                    dataGridView1.Columns.RemoveAt(COLUMNS - 1);
                    COLUMNS--;
                }
                if (dataGridView1.ColumnCount <= 2)
                {
                    MessageBox.Show("Подальше видалення неможливе");
                    return;
                }
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = table[dataGridView1.CurrentCell.RowIndex, dataGridView1.CurrentCell.ColumnIndex].exp;
        }
        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            string temp = (string)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            int r = dataGridView1.CurrentCell.RowIndex;
            int c = dataGridView1.CurrentCell.ColumnIndex;
            if (temp != null)
            {
               
                addNewValue(temp, r, c);
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                string temp = (string)textBox1.Text;
                int r = dataGridView1.CurrentCell.RowIndex;
                int c = dataGridView1.CurrentCell.ColumnIndex;

                addNewValue(temp, r, c);
            }
        }
        

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SavePath();
           
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
           LoadFile();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            RemoveRowAt();

        }
        public void RemoveRowAt()
        {
            if (ROWS >= 2)
            {


                int ii = dataGridView1.CurrentRow.Index;
                ClassBaseSys sys = new ClassBaseSys();
                ColumnList.Clear();
                for (int i = 0; i < COLUMNS; ++i)
                {
                    string res = "";
                    res = sys.ToEx(i);
                    res += (ii).ToString();
                    ColumnList.Add(res);
                }

                RefreshForColumn(ColumnList);
                dataGridView1.Rows.RemoveAt(ii);
                ROWS--;
                int k = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    row.HeaderCell.Value = k.ToString();
                    k++;

                }
            }
            else MessageBox.Show("Подальше видалення неможливе");
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            RemoveColumnAt();
        }
        public void RemoveColumnAt()
        {
            if (COLUMNS > 2)
            {
                int ii = dataGridView1.CurrentCell.ColumnIndex;
                ClassBaseSys sys1 = new ClassBaseSys();
                ColumnList.Clear();
                for (int i = 0; i < ROWS; ++i)
                {
                    string res = "";
                    res = sys1.ToEx(ii);
                    res += i.ToString();
                    ColumnList.Add(res);
                }

                RefreshForColumn(ColumnList);
                dataGridView1.Columns.RemoveAt(ii);
                COLUMNS--;
                int k = 0;
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    ClassBaseSys sys = new ClassBaseSys();
                    string res = sys.ToEx(k);
                    col.HeaderCell.Value = res;
                    k++;

                }
            }
            else MessageBox.Show("Подальше видалення неможливе");
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            InsertRowInto();
        }
        public void InsertRowInto()
        {
            int i = dataGridView1.CurrentRow.Index;
            dataGridView1.Rows.Insert(i);
            ROWS++;
            int k = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                row.HeaderCell.Value = k.ToString();
                k++;

            }
        }
        private void LoadFile()
        {
            DialogResult result = MessageBox.Show(
           "Do you want to save your Document ?",
           "You are trying to load new file",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Information,
           MessageBoxDefaultButton.Button1,
           MessageBoxOptions.RightAlign);
            if (result == DialogResult.Yes)
            {
                SavePath();
            }

            System.IO.Stream myStr = null;
            OpenFileDialog OpenTags = new OpenFileDialog();
            OpenTags.Filter = "All file (*.*) | *.*| Text file |*.txt";
            OpenTags.FilterIndex = 2;
            if (OpenTags.ShowDialog() == DialogResult.OK)
            {
                if ((myStr = OpenTags.OpenFile()) != null)
                {
                    StreamReader myRead = new StreamReader(myStr, System.Text.Encoding.Default);
                    string[] str;

                    int num = 0;
                    int numCol = 0; ;

                    string[] str1 = myRead.ReadToEnd().Split('\n');
                    num = str1.Count();
                    int i1 = 0;

                    while (i1 < str1[0].Length)
                    {

                        if (str1[0][i1] == ':')
                        {
                            numCol++;
                        }
                        i1++;
                    }
                    while (COLUMNS <= numCol)
                    {
                        NewEl.AddColumn(dataGridView1, COLUMNS);
                        COLUMNS++;
                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {


                            dataGridView1.Rows[i].Cells[COLUMNS - 1].Value = " ";

                        }
                    }
                    while (ROWS <= num)
                    {
                        NewEl.AddRow(ROWS, dataGridView1);
                        ROWS++;

                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {

                            dataGridView1.Rows[ROWS - 1].Cells[j].Value = " ";
                        }
                    }
                    dataGridView1.Rows[ROWS].HeaderCell.Value = ((int.Parse(dataGridView1.Rows[ROWS - 1].HeaderCell.Value.ToString()))).ToString();
                    dataGridView1.RowCount = num + 1;
                    for (int i = 0; i < num; i++)
                    {

                        str = str1[i].Split(':');
                        for (int j = 0; j < dataGridView1.ColumnCount; j++)
                        {

                            try
                            {

                                addNewValue(str[j], i, j);
                            }
                            catch
                            {

                            }
                        }

                    }

                }
            }
            textBox2.Text = OpenTags.FileName;
        }


        private void SavePath()
        {
            System.IO.Stream myStream;
            SaveFileDialog saveTags = new SaveFileDialog();
            saveTags.Filter = "All file (*.*) | *.*| Text file |*.txt";
            saveTags.FilterIndex = 2;


            if (saveTags.ShowDialog() == DialogResult.OK)

            {
                if ((myStream = saveTags.OpenFile()) != null)
                {
                    StreamWriter myWriter = new StreamWriter(myStream, System.Text.Encoding.Default);
                    try
                    {

                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {
                            for (int j = 0; j < dataGridView1.ColumnCount; j++)
                            {
                                myWriter.Write(table[i, j].exp.ToString()); ;
                                if ((dataGridView1.ColumnCount - j) != 1) myWriter.Write(":");
                            }

                            if (((dataGridView1.RowCount - 1) - i - 1) != 0) myWriter.WriteLine();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        myWriter.Close();
                    }
                }
                myStream.Close();

            }
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dataGridView1.CurrentCell != null) 
            {
                var currRow = dataGridView1.CurrentCell.RowIndex;
                var currCol = dataGridView1.CurrentCell.ColumnIndex;
                if (dataGridView1[currCol, currRow].Value != null) 
                {
                    textBox1.Text = table[currRow, currCol].exp.ToString();
                }
            }
        }
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Do you want to close your programm",
            "Danger",
            MessageBoxButtons.OKCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.RightAlign);
            if (result == DialogResult.OK)
            {
                DialogResult result1 = MessageBox.Show(
      "Do you want to save your programm",
      "Danger",
      MessageBoxButtons.OKCancel,
      MessageBoxIcon.Information,
      MessageBoxDefaultButton.Button1,
      MessageBoxOptions.RightAlign);
                if(result1==DialogResult.OK)
                SavePath();
            }
           
            if (result==DialogResult.Cancel)
            {
                
                e.Cancel = true;
            }

        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
            { 
                var currRow = dataGridView1.CurrentCell.RowIndex;
                var currCol = dataGridView1.CurrentCell.ColumnIndex;
                if (dataGridView1[currCol, currRow].Value != null)
                {
                    textBox1.Text = table[currRow, currCol].exp.ToString();
                }

            }
        }
        public void RefreshForColumn(List<string> curList)
        {
            int uni1 = 64;
            int uni2 = 91;
            int uni3 = 47;
            int uni4 = 58;
            for (int i = 0; i < st1; ++i)
            {
                for (int j = 0; j < st1; ++j) 
                {
                    if (SaveTable[i, j].value_1 != "0") 
                    {
                        int t2;
                        string exptession = SaveTable[i, j].value_1;
                        int h = 0;
                        while (h < exptession.Length)
                        {
                            int t1 = (int)exptession[h];

                            string resultC = "";
                            string resultR = "";

                            if ((t1 > uni1) && (t1 < uni2))
                            {
                                
                                resultC += exptession[h].ToString();
                                h++;
                                t2 = (int)exptession[h];
                                while ((t2 > uni1 && t2 < uni2))
                                {
                                    resultC += exptession[h];
                                    h++;
                                    t2 = (int)exptession[h];
                                }
                                while ((t2 > uni3 && t2 < uni4))
                                {
                                    resultR += exptession[h];
                                    h++;
                                    if (h < exptession.Length)
                                    {
                                        t2 = (int)exptession[h];
                                    }
                                    else t2 = 100;

                                }
                                ClassBaseSys ress = new ClassBaseSys();
                                int cc = ress.FromEx(resultC);
                                int rr = int.Parse(resultR);
                               foreach(string str in curList)
                               {
                                    
                                    if(str==resultC+resultR)
                                    {
                                        if (curList.Count == 1)
                                        {
                                            if (circle(table[i, j], i, j))
                                            {
                                                string str1 = "";
                                                str1 = table[i, j].exp.ToString();
                                                addNewValue(str1, i, j);
                                                return;
                                            }
                                            else return;
                                        }
                                        else
                                        {
                                            dataGridView1.Rows[i].Cells[j].Value = 0;
                                            table[i, j].value_1 = "0";
                                            table[i, j].exp = "0";
                                        }
                                    }
                               }
                            }
                            h++;
                        }
                    }
                }
            }
        }

        public void addNewValue(string exptession, int r, int c)
        {
            int uni1 = 64;
            int uni2 = 91;
            int uni3 = 47;
            int uni4 = 58;
            int LetRow;
            int LetCol;
            if (exptession == "" || exptession == " ")
            {
                table[r, c].value_1 = exptession;
                table[r, c].exp = "";
                dataGridView1.CurrentCell.Value = exptession;
                return;
            }
            table[r, c].exp = exptession;
            int h = 0;
            exptession = exptession.Replace(" ", "");
            if (exptession != null)
            {
                if (exptession[0] == '=')
                {

                    int t2;
                    int kk = 0;
                    int k1 = 0;
                    int calc1 = 0;
                    int calc2 = 0;

                    exptession = exptession.Replace(exptession[0], ' ');
                    exptession = exptession.Replace(" ", "");
                    string test = exptession;
                    while (k1 < test.Length)
                    {
                        if(test[k1]=='(')
                        {
                            calc1++;
                        }
                        if(test[k1]==')')
                        {
                            calc2++;
                        }
                        k1++;
                    }
                    if(calc1!=calc2)
                    {
                        dataGridView1.Rows[r].Cells[c].Value = "Error";
                        table[r, c].value_1 = "0";
                        return;
                    }
                    while (h < test.Length)
                    {


                        int t1 = (int)test[h];

                        string resultC = "";
                        string resultR = "";

                        if ((t1 > uni1) && (t1 < uni2))
                        {
                            if (kk == 0)
                            {
                                SaveTable[r, c].value_1 = exptession;
                            }

                            resultC += test[h].ToString();
                            h++;
                            t2 = (int)test[h];
                            while ((t2 > uni1 && t2 < uni2))
                            {
                                resultC += test[h];
                                h++;
                                t2 = (int)test[h];
                            }
                            while ((t2 > uni3 && t2 < uni4))
                            {
                                resultR += test[h];
                                h++;
                                if (h < test.Length)
                                {
                                    t2 = (int)test[h];
                                }
                                else t2 = 100;


                            }
                            ClassBaseSys ress = new ClassBaseSys();

                            int cc = ress.FromEx(resultC);
                            int rr = int.Parse(resultR);
                            if (cc > COLUMNS || rr > ROWS)
                            {
                                table[r, c].value_1 = "error";
                                table[r, c].exp = "";
                                dataGridView1.Rows[r].Cells[c].Value = "error";
                                return;
                            }
                            LetCol = cc;
                            LetRow = rr;
                            string resss = dataGridView1.Rows[rr].Cells[cc].Value.ToString();
                            table[rr, cc].dependents.Add(table[r, c].getName(c, r));
                            for (int i = 0; i < table[r, c].dependents.Count; ++i)
                            {
                                table[rr, cc].dependents.Add(table[r, c].dependents[i]);
                            }
                            exptession = exptession.Replace(resultC + resultR, resss);
                            kk++;
                        }
                        h++;
                    }
                    int hh = 0;
                    bool f = false;
                    while (hh < exptession.Length)
                    {
                        if (!Char.IsDigit(exptession[hh]) && exptession != " ")
                        {
                            f = true;
                            break;
                        }
                        hh++;
                    }
                    if (f)
                    {
                        for (int i = 0; i < exptession.Length; ++i)
                        {
                            if (exptession[i] == '/' && exptession[i + 1] == '0')
                            {
                                table[r, c].value_1 = "0";
                                dataGridView1.Rows[r].Cells[c].Value = "divide by zero";
                                return;
                            }
                        }
                        ToPolandNotation cur = new ToPolandNotation();
                        double res = cur.Calculate(exptession);
                        if (circle(table[r, c], r, c))
                        {
                            if (dataGridView1.Rows[r].Cells[c].Value.ToString() == "#CIRCLE")
                            {
                                dataGridView1.Rows[r].Cells[c].Value = "0";
                            }
                            table[r, c].value_1 = res.ToString();

                            dataGridView1.Rows[r].Cells[c].Value = res;
                            ColumnList.Clear();
                            ClassBaseSys test1 = new ClassBaseSys();
                            string c1 = test1.ToEx(c);
                            ColumnList.Add(c1 + r.ToString());
                            RefreshForColumn(ColumnList);
                        }
                        else
                        { MessageBox.Show("NO"); return; }


                    }
                    else
                    {
                        if (exptession == " " || exptession == "")
                        {
                            table[r, c].value_1 = "0";
                            dataGridView1.Rows[r].Cells[c].Value = "0";
                            table[r, c].exp = "";
                        }
                        else
                        {
                            if (dataGridView1.Rows[r].Cells[c].Value.ToString() == "#CIRCLE")
                            {
                                table[r, c].dependents.Clear();
                                dataGridView1.Rows[r].Cells[c].Value = "0";
                            }
                            table[r, c].value_1 = exptession;

                            dataGridView1.Rows[r].Cells[c].Value = exptession;
                        }

                        if (circle(table[r, c], r, c))
                        {


                            ColumnList.Clear();
                            ClassBaseSys test1 = new ClassBaseSys();
                            string c1 = test1.ToEx(c);
                            ColumnList.Add(c1 + r.ToString());
                            RefreshForColumn(ColumnList);
                        }
                    }

                }
                else
                {
                    table[r, c].value_1 = exptession;
                    table[r, c].exp = exptession;
                    dataGridView1.Rows[r].Cells[c].Value = exptession;
                    ColumnList.Clear();
                    ClassBaseSys test1 = new ClassBaseSys();
                    string c1 = test1.ToEx(c);
                    ColumnList.Add(c1 + r.ToString());
                    RefreshForColumn(ColumnList);
                }
            }
        }
        public bool circle(Cell cell, int r, int c)
        {
            for (int i = 0; i < cell.dependents.Count; ++i)
            {
                int t1 = (int)cell.dependents[i][0] - 65;
                int t2 = (int)cell.dependents[i][1] - 48;
                for (int j = 0; j < table[t2, t1].dependents.Count; ++j)
                {
                    if (table[t2, t1].dependents[j] == table[r, c].getName(r, c))
                    {
                        nameCircle(cell, r, c);
                        nameCircle(table[t2, t1], t2, t1);
                        table[t2, t1].dependents.RemoveAt(j);
                        return false;
                    }
                }

            }
            return true;
        }
        public void nameCircle(Cell cell, int r, int c)
        {
            for (int f1 = 0; f1 < cell.dependents.Count; ++f1)
            {
                int _t2 = (int)cell.dependents[f1][1] - 48;
                int _t1 = (int)cell.dependents[f1][0] - 65;
                dataGridView1.Rows[_t2].Cells[_t1].Value = "#CIRCLE";
                table[_t2, _t1].value_1 = "#CIRCLE";

            }
        }
    }
}