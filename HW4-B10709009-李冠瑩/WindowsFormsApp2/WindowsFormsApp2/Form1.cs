using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'pizzaDBDataSet.訂購明細' 資料表。您可以視需要進行移動或移除。
            this.訂購明細TableAdapter.Fill(this.pizzaDBDataSet.訂購明細);

            // TODO: 這行程式碼會將資料載入 'pizzaDBDataSet.訂購單' 資料表。您可以視需要進行移動或移除。
            this.訂購單TableAdapter.Fill(this.pizzaDBDataSet.訂購單);
            // TODO: 這行程式碼會將資料載入 'pizzaDBDataSet.餐點' 資料表。您可以視需要進行移動或移除。
            this.餐點TableAdapter.Fill(this.pizzaDBDataSet.餐點);
            // TODO: 這行程式碼會將資料載入 'pizzaDBDataSet.鄉鎮市區' 資料表。您可以視需要進行移動或移除。
            this.鄉鎮市區TableAdapter.Fill(this.pizzaDBDataSet.鄉鎮市區);
            // TODO: 這行程式碼會將資料載入 'pizzaDBDataSet.縣市' 資料表。您可以視需要進行移動或移除。
            this.縣市TableAdapter.Fill(this.pizzaDBDataSet.縣市);
            // TODO: 這行程式碼會將資料載入 'pizzaDBDataSet.會員' 資料表。您可以視需要進行移動或移除。
            this.會員TableAdapter.Fill(this.pizzaDBDataSet.會員);

            comboBox1.Enabled = true;
            groupBox1.Enabled = false;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            textBox5.Enabled = false;
            dateTimePicker2.Enabled = false;

        }
        private void button12_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public Boolean x;

        bool check = true;

        private void button1_Click(object sender, EventArgs e)
        {
            //Connection物件所連接的字串參數設定

            using (SqlConnection cn1 = new SqlConnection())
            {

                cn1.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|PizzaDB.mdf;" + "Integrated Security=True";
                cn1.Open();
                string selectCmd = "SELECT * FROM 會員 WHERE 姓名=N'" + comboBox1.Text + "'";
                SqlCommand cmd1 = new SqlCommand(selectCmd, cn1);
                SqlDataReader dr1 = cmd1.ExecuteReader();

                if (dr1.Read())
                {
                    check = true;
                    groupBox1.Enabled = true;
                    textBox1.Text = dr1["會員卡號"].ToString();
                    if (Convert.ToBoolean(dr1["性別"]) == true)
                    {
                        radioButton1.Enabled = true;
                        radioButton1.Checked = true;
                    }
                    else
                    {
                        radioButton2.Enabled = true;
                        radioButton2.Checked = true;
                    }
                    dateTimePicker1.Value = Convert.ToDateTime(dr1["出生日期"]);
                    textBox2.Text = dr1["電子信箱"].ToString();
                    comboBox2.Text = dr1["通訊地址_縣市"].ToString();
                    comboBox3.Text = dr1["通訊地址_鄉鎮市區"].ToString();
                    textBox3.Text = dr1["手機號碼"].ToString();
                    textBox4.Text = dr1["通訊地址_街道名"].ToString();

                }
                else
                {
                    check = false;
                    MessageBox.Show("會員資料不存在");
                    groupBox1.Enabled = true;
                    groupBox2.Enabled = true;
                    textBox1.ReadOnly = true;
                    dateTimePicker1.MaxDate = DateTime.Now;
                    dateTimePicker1.MinDate = new DateTime(1980, 1, 1);

                }


            }


        }


        private void button2_Click(object sender, EventArgs e)
        {

            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|PizzaDB.mdf;" + "Integrated Security=True";
                cn.Open();
                if (radioButton1.Checked == true)
                {
                    x = true;
                }
                else
                {
                    x = false;
                }
                if (check)
                {
                    try
                    {
                        if (textBox3.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || textBox4.Text == "")
                        {
                            MessageBox.Show("未填妥資料");
                        }
                        else
                        {
                            string updatestr = "UPDATE 會員 SET 姓名=@name,出生日期=@day,性別=@sex,電子信箱=@email,手機號碼=@tel,通訊地址_縣市=@county,通訊地址_鄉鎮市區=@city,通訊地址_街道名=@road";
                            SqlCommand updatecmd = new SqlCommand(updatestr, cn);
                            updatecmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                            updatecmd.Parameters.Add(new SqlParameter("@day", SqlDbType.Date));
                            updatecmd.Parameters.Add(new SqlParameter("@sex", SqlDbType.Bit));
                            updatecmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar));
                            updatecmd.Parameters.Add(new SqlParameter("@tel", SqlDbType.NVarChar));
                            updatecmd.Parameters.Add(new SqlParameter("@county", SqlDbType.NVarChar));
                            updatecmd.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar));
                            updatecmd.Parameters.Add(new SqlParameter("@road", SqlDbType.NVarChar));
                            updatecmd.Parameters["@name"].Value = comboBox1.Text;
                            updatecmd.Parameters["@sex"].Value = x;
                            updatecmd.Parameters["@day"].Value = dateTimePicker1.Value;
                            updatecmd.Parameters["@email"].Value = textBox2.Text;
                            updatecmd.Parameters["@tel"].Value = textBox3.Text;
                            updatecmd.Parameters["@county"].Value = comboBox2.Text;
                            updatecmd.Parameters["@city"].Value = comboBox3.Text;
                            updatecmd.Parameters["@road"].Value = textBox4.Text;
                            updatecmd.ExecuteNonQuery();
                            MessageBox.Show("更新成功");
                          
                            check = true;
                            if (check)
                            {
                                groupBox3.Enabled = true;
                                dateTimePicker2.Enabled = true;
                            }



                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("更新失敗");

                    }

                }
                else
                {

                    try
                    {
                        if (textBox3.Text == "" || comboBox2.Text == "" || comboBox3.Text == "" || textBox4.Text == "")
                        {
                            MessageBox.Show("未填妥資料");

                        }
                        else
                        {
                            string insertstr = "INSERT INTO 會員(姓名,出生日期,性別,電子信箱,手機號碼,通訊地址_縣市,通訊地址_鄉鎮市區,通訊地址_街道名)"
                            + "VALUES(@name,@day,@sex,@email,@tel,@county,@city,@road)";
                            SqlCommand insertcmd = new SqlCommand(insertstr, cn);
                            insertcmd.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                            insertcmd.Parameters.Add(new SqlParameter("@day", SqlDbType.Date));
                            insertcmd.Parameters.Add(new SqlParameter("@sex", SqlDbType.Bit));
                            insertcmd.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar));
                            insertcmd.Parameters.Add(new SqlParameter("@tel", SqlDbType.NVarChar));
                            insertcmd.Parameters.Add(new SqlParameter("@county", SqlDbType.NVarChar));
                            insertcmd.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar));
                            insertcmd.Parameters.Add(new SqlParameter("@road", SqlDbType.NVarChar));
                            insertcmd.Parameters["@name"].Value = comboBox1.Text;
                            insertcmd.Parameters["@sex"].Value = x;
                            insertcmd.Parameters["@day"].Value = dateTimePicker1.Value;
                            insertcmd.Parameters["@email"].Value = textBox2.Text;
                            insertcmd.Parameters["@tel"].Value = textBox3.Text;
                            insertcmd.Parameters["@county"].Value = comboBox2.Text;
                            insertcmd.Parameters["@city"].Value = comboBox3.Text;
                            insertcmd.Parameters["@road"].Value = textBox4.Text;
                            insertcmd.ExecuteNonQuery();
                            MessageBox.Show("新增成功");   
                          check = true;
                            if (check)
                            {
                                groupBox3.Enabled = true;
                                dateTimePicker2.Enabled = true;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("新增失敗");

                    }

                }

            }


        }

        int count = 0;
        double sum;
        class Str
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public string pza { get; set; }
        }
        List<Str> liststr = new List<Str>();
        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.MinDate = DateTime.Now;

        }
        private void button4_Click(object sender, EventArgs e)
        {
            int y = 0;
            using (SqlConnection cn2 = new SqlConnection())
            {
                cn2.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|PizzaDB.mdf;" + "Integrated Security=True";
                cn2.Open();
                string pizzacmd = "SELECT * FROM 餐點 WHERE 餐點名稱='" + comboBox7.Text + "'";
                SqlCommand pzcmd = new SqlCommand(pizzacmd, cn2);
                SqlDataReader dr2 = pzcmd.ExecuteReader();

                if (dr2.Read())
                {

                    string pstring = dr2["售價"].ToString();
                    y = int.Parse(pstring);
                    if (comboBox8.Text == "芝心")
                        y += 80;
                    else if (comboBox8.Text == "酥香菠蘿芝心")
                        y += 100;
                    else if (comboBox8.Text == "酥香菠蘿")
                        y += 40;
                    else
                        y += 0;
                    textBox6.Text = y.ToString();
                }

            }
            liststr.Add(new Str() { Name = comboBox7.Text + '/' + comboBox8.Text + textBox6.Text, Price = y,pza=comboBox8.Text });
            listBox1.Items.Add(comboBox7.Text + '/' + comboBox8.Text + '/' + textBox6.Text);
            textBox11.Text = listBox1.Items.Count.ToString();
            int n = int.Parse(textBox11.Text); //餐點總項數
            sum = double.Parse(textBox12.Text);
            if (count == 0)
                sum += y;
            else
            {
                if (count == 1)
                    sum = sum / 0.9 + y;
                else if (count == 2)
                    sum = sum / 0.8 + y;
                else if (count == 3)
                    sum = sum / 0.7 + y;
            }
            int r = int.Parse(textBox13.Text);
            if (sum >= 1000 && sum < 2000)
            {
                count = 1;
                r = 90;
                sum = sum * 0.9;
            }
            else if (sum >= 2000 && sum < 3500)
            {
                count = 2;
                r = 80;
                sum = sum * 0.8;
            }
            else if (sum >= 3500)
            {
                count = 3;
                r = 70;
                sum = sum * 0.7;
            }
            textBox12.Text = sum.ToString();
            textBox13.Text = r.ToString();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            int y = 0;
            using (SqlConnection cn3 = new SqlConnection())
            {
                cn3.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|PizzaDB.mdf;" + "Integrated Security=True";
                cn3.Open();
                string dessertcmd = "SELECT * FROM 餐點 WHERE 餐點名稱='" + comboBox9.Text + "'";
                SqlCommand dtcmd = new SqlCommand(dessertcmd, cn3);
                SqlDataReader dr3 = dtcmd.ExecuteReader();
                if (dr3.Read())
                {
                    string pstring = dr3["售價"].ToString();
                    y = int.Parse(pstring);
                    textBox7.Text = y.ToString();
                }

            }
            liststr.Add(new Str() { Name = comboBox9.Text + '/' + textBox7.Text, Price = y });
            listBox1.Items.Add(comboBox9.Text + '/' + textBox7.Text);
            textBox11.Text = listBox1.Items.Count.ToString();
            int n = int.Parse(textBox11.Text); //餐點總項數
            sum = double.Parse(textBox12.Text);
            if (count == 0)
                sum += y;
            else
            {
                if (count == 1)
                    sum = sum / 0.9 + y;
                else if (count == 2)
                    sum = sum / 0.8 + y;
                else if (count == 3)
                    sum = sum / 0.7 + y;
            }
            int r = int.Parse(textBox13.Text);
            if (sum >= 1000 && sum < 2000)
            {
                count = 1;
                r = 90;
                sum = sum * 0.9;
            }
            else if (sum >= 2000 && sum < 3500)
            {
                count = 2;
                r = 80;
                sum = sum * 0.8;
            }
            else if (sum >= 3500)
            {
                count = 3;
                r = 70;
                sum = sum * 0.7;
            }
            textBox12.Text = sum.ToString();
            textBox13.Text = r.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int y = 0;
            using (SqlConnection cn4 = new SqlConnection())
            {
                cn4.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|PizzaDB.mdf;" + "Integrated Security=True";
                cn4.Open();
                string drinkcmd = "SELECT * FROM 餐點 WHERE 餐點名稱='" + comboBox10.Text + "'";
                SqlCommand dkcmd = new SqlCommand(drinkcmd, cn4);
                SqlDataReader dr4 = dkcmd.ExecuteReader();
                if (dr4.Read())
                {
                    string pstring = dr4["售價"].ToString();
                    y = int.Parse(pstring);
                    textBox8.Text = y.ToString();
                }

            }
            liststr.Add(new Str() { Name = comboBox10.Text + '/' + textBox8.Text, Price = y });
            listBox1.Items.Add(comboBox10.Text + '/' + textBox8.Text);
            textBox11.Text = listBox1.Items.Count.ToString();
            int n = int.Parse(textBox11.Text); //餐點總項數
            sum = double.Parse(textBox12.Text);
            if (count == 0)
                sum += y;
            else
            {
                if (count == 1)
                    sum = sum / 0.9 + y;
                else if (count == 2)
                    sum = sum / 0.8 + y;
                else if (count == 3)
                    sum = sum / 0.7 + y;
            }
            int r = int.Parse(textBox13.Text);
            if (sum >= 1000 && sum < 2000)
            {
                count = 1;
                r = 90;
                sum = sum * 0.9;
            }
            else if (sum >= 2000 && sum < 3500)
            {
                count = 2;
                r = 80;
                sum = sum * 0.8;
            }
            else if (sum >= 3500)
            {
                count = 3;
                r = 70;
                sum = sum * 0.7;
            }
            textBox12.Text = sum.ToString();
            textBox13.Text = r.ToString();
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
         
            int y = 0;
            using (SqlConnection cn5 = new SqlConnection())
            {
                cn5.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|PizzaDB.mdf;" + "Integrated Security=True";
                cn5.Open();
                string mixcmd = "SELECT * FROM 餐點 WHERE 餐點名稱='" + comboBox11.Text + "'";
                SqlCommand mcmd = new SqlCommand(mixcmd, cn5);
                SqlDataReader dr5 = mcmd.ExecuteReader();
                if (dr5.Read())
                {
                    string pstring = dr5["售價"].ToString();
                    y = int.Parse(pstring);
                    textBox9.Text = y.ToString();
                }

            }
            liststr.Add(new Str() { Name = comboBox11.Text + '/' + textBox9.Text, Price = y });
            listBox1.Items.Add(comboBox11.Text + '/' + textBox9.Text);
            textBox11.Text = listBox1.Items.Count.ToString();
            int n = int.Parse(textBox11.Text); //餐點總項數
            sum = double.Parse(textBox12.Text);
            if (count == 0)
                sum += y;
            else
            {
                if (count == 1)
                    sum = sum / 0.9 + y;
                else if (count == 2)
                    sum = sum / 0.8 + y;
                else if (count == 3)
                    sum = sum / 0.7 + y;
            }
            int r = int.Parse(textBox13.Text);
            if (sum >= 1000 && sum < 2000)
            {
                count = 1;
                r = 90;
                sum = sum * 0.9;
            }
            else if (sum >= 2000 && sum < 3500)
            {
                count = 2;
                r = 80;
                sum = sum * 0.8;
            }
            else if (sum >= 3500)
            {
                count = 3;
                r = 70;
                sum = sum * 0.7;
            }
            textBox12.Text = sum.ToString();
            textBox13.Text = r.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            textBox11.Text = "0";
            textBox12.Text = "0";
            textBox13.Text = "0";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string selectdelete = listBox1.SelectedItem.ToString();
            listBox1.SelectedIndex = listBox1.FindString(selectdelete);
            int m = listBox1.SelectedIndex;
            string rstr = textBox13.Text;
            if (textBox13.Text == "0")
                sum = sum - liststr[m].Price;
            else if (textBox13.Text == "90")
                sum = sum / 0.9 - liststr[m].Price;
            else if (textBox13.Text == "80")
                sum = sum / 0.8 - liststr[m].Price;
            else if (textBox13.Text == "70")
                sum = sum / 0.7 - liststr[m].Price;
            if (sum > 3500)
            {
                textBox13.Text = 70.ToString();
                sum *= 0.7;
            }
            else if (sum >= 2000 && sum < 3500)
            {
                textBox13.Text = 80.ToString();
                sum *= 0.8;
            }
            else if (sum >= 1000 && sum < 2000)
            {
                textBox13.Text = 90.ToString();
                sum *= 0.9;
            }
            liststr.RemoveAt(m);
            listBox1.Items.Remove(selectdelete);
            textBox11.Text = listBox1.Items.Count.ToString();
            textBox12.Text = sum.ToString();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
            comboBox6.Enabled = true;
            textBox5.Enabled = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            comboBox4.Enabled = true;
            comboBox5.Enabled = true;
            comboBox6.Enabled = false;
            textBox5.Enabled = true;
        }
        Boolean z;
        string z1;
        string pzastring;
        int ordernum=0;
        private void button10_Click(object sender, EventArgs e)
        {
            ordernum++;
            if (listBox1.Items.ToString() == "")
            {
                MessageBox.Show("至少選擇一項餐點");
            }
            if (comboBox6.Text == "" && radioButton3.Checked == true)
            {
                MessageBox.Show("請選擇門市地址");
            }
            if (textBox5.Text == "" && radioButton4.Checked)
            {
                MessageBox.Show("請選擇外送地址");
            }
            using (SqlConnection cn6 = new SqlConnection())
            {
                if (radioButton3.Checked)
                    z = true;
                else if (radioButton4.Checked)
                    z = false;
                if (comboBox6.Enabled)
                    z1 = comboBox6.Text;
                else if (textBox5.Enabled)
                    z1 = textBox5.Text;
                int pos = DateTime.Now.ToString().LastIndexOf("午");
                string datestring = DateTime.Now.ToShortDateString() + DateTime.Now.ToString().Remove(0, pos + 2);
                
                cn6.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;" + "AttachDbFilename=|DataDirectory|PizzaDB.mdf;" + "Integrated Security=True";
                cn6.Open();
                string insertstr = "INSERT INTO 訂購單(會員卡號,訂購日期,取餐方式,取餐日期,門市_外送地址_縣市,門市_外送地址_鄉鎮市區,門市名稱_外送街道名,折扣百分比)"
                                + "VALUES(@id,@buyday,@way,@takeday,@county,@city,@road,@rate)";
                SqlCommand insertcmd6 = new SqlCommand(insertstr, cn6);
                insertcmd6.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));
                insertcmd6.Parameters.Add(new SqlParameter("@buyday", SqlDbType.DateTime));
                insertcmd6.Parameters.Add(new SqlParameter("@way", SqlDbType.Bit));
                insertcmd6.Parameters.Add(new SqlParameter("@takeday", SqlDbType.Date));
                insertcmd6.Parameters.Add(new SqlParameter("@county", SqlDbType.NVarChar));
                insertcmd6.Parameters.Add(new SqlParameter("@city", SqlDbType.NVarChar));
                insertcmd6.Parameters.Add(new SqlParameter("@road", SqlDbType.NVarChar));
                insertcmd6.Parameters.Add(new SqlParameter("@rate", SqlDbType.Int));
                insertcmd6.Parameters["@id"].Value = textBox1.Text;
                insertcmd6.Parameters["@buyday"].Value = DateTime.Today.ToShortDateString();
                insertcmd6.Parameters["@way"].Value = z;
                insertcmd6.Parameters["@takeday"].Value = dateTimePicker2.Value.ToShortDateString();
                insertcmd6.Parameters["@county"].Value = comboBox4.Text;
                insertcmd6.Parameters["@city"].Value = comboBox5.Text;
                insertcmd6.Parameters["@road"].Value =z1;
                insertcmd6.Parameters["@rate"].Value = int.Parse(textBox13.Text);

                insertcmd6.ExecuteNonQuery();
                int number = int.Parse(textBox11.Text);
                string insertstr1 = "INSERT INTO 訂購明細(訂單編號,餐點名稱,餅皮,金額)"
                               + "VALUES(@ordernum,@mealname,@pza,@sum)";
                SqlCommand insertcmd7 = new SqlCommand(insertstr1, cn6);
                insertcmd7.Parameters.Add(new SqlParameter("@ordernum", SqlDbType.Int));
                insertcmd7.Parameters.Add(new SqlParameter("@mealname", SqlDbType.NVarChar));
                insertcmd7.Parameters.Add(new SqlParameter("@pza", SqlDbType.NVarChar));
                insertcmd7.Parameters.Add(new SqlParameter("@sum", SqlDbType.Int));
                
                for (int i = 0; i < number; i++)
                {
                    insertcmd7.Parameters["@ordernum"].Value =ordernum;
                    insertcmd7.Parameters["@mealname"].Value = liststr[i].Name;
                    pzastring = liststr[i].pza;
                    if (liststr[i].Name == comboBox7.Text + '/' + comboBox8.Text)
                    {
                       
                        insertcmd7.Parameters["@pza"].Value = pzastring;
                    }
                    else 
                    {
                        pzastring = "";
                        insertcmd7.Parameters["@pza"].Value = pzastring;
                    }
                    insertcmd7.Parameters["@sum"].Value = liststr[i].Price;

                    insertcmd7.ExecuteNonQuery();
                } 
               
                MessageBox.Show("結帳成功");
                listBox1.Items.Clear();
            }

            this.訂購單TableAdapter.Fill(this.pizzaDBDataSet.訂購單);
            this.訂購明細TableAdapter.Fill(this.pizzaDBDataSet.訂購明細);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
