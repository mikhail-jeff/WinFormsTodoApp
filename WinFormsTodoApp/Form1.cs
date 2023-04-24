using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsTodoApp
{
    public partial class WinformsTodoApp : Form
    {
        //Object to  Connect to DB
        public static IMongoClient client = new MongoClient();

        //Object to Create DB name
        public static IMongoDatabase db = client.GetDatabase("winformsdatabase");

        //Object for Collection name
        public static IMongoCollection<Todo> collection = db.GetCollection<Todo>("todos");

        public WinformsTodoApp()
        {
            InitializeComponent();
            cbComplete.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Add Todo
            string text = textTodo.Text;
            string complete = cbComplete.Text;
            DateTime date = DateTime.Now;

            

            Todo todo = new Todo(text, complete, date);

            collection.InsertOne(todo);
            loadData();

            textID.Text = "";
            textTodo.Text = "";
            cbComplete.Text = "";
        }

        public void loadData()
        {
            List<Todo> list = collection.AsQueryable().ToList();
            dataGridView1.DataSource = list;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string text = textTodo.Text;
            string complete = cbComplete.Text;

            var update = Builders<Todo>.Update
                                    .Set(x => x.Text, text)
                                    .Set(x => x.Complete, complete);


            collection.UpdateOne(x => x.Id == ObjectId.Parse(textID.Text), update);
            loadData();

            textID.Text = "";
            textTodo.Text = "";
            cbComplete.Text = "";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textTodo.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            cbComplete.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            collection.DeleteOne(x => x.Id == ObjectId.Parse(textID.Text));
            loadData();

            textID.Text = "";
            textTodo.Text = "";
            cbComplete.Text = "";
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            var filterDefinition = Builders<Todo>.Filter.Empty;
            collection.DeleteMany(filterDefinition);
            loadData();

            textID.Text = "";
            textTodo.Text = "";
            cbComplete.Text = "";
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            textID.Text = "";
            textTodo.Text = "";
            cbComplete.Text = "";
        }

        private void WinformsTodoApp_Load(object sender, EventArgs e)
        {
            List<Todo> list = collection.AsQueryable().ToList();
            dataGridView1.DataSource = list;
        }
    }
}
