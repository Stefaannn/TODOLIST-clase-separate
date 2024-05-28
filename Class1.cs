using System;
using System.Windows.Forms;

namespace To_Do_List_App
{
    public partial class ToDoListForm : BaseToDoListForm
    {
        public ToDoListForm()
        {
            InitializeComponent();
        }

        private void ToDoListForm_Load(object sender, EventArgs e)
        {
            base.BaseToDoListForm_Load(sender, e); // Apelăm metoda din clasa de bază
        }

        private void newButton_Click(object sender, EventArgs e)
        {
            base.newButton_Click(sender, e); // Apelăm metoda din clasa de bază
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            base.editButton_Click(sender, e); // Apelăm metoda din clasa de bază
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            base.deleteButton_Click(sender, e); // Apelăm metoda din clasa de bază
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            base.saveButton_Click(sender, e); // Apelăm metoda din clasa de bază
        }

        private void completeButton_Click(object sender, EventArgs e)
        {
            base.completeButton_Click(sender, e); // Apelăm metoda din clasa de bază
        }
    }
}
