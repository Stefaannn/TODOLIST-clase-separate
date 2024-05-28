using System;
using System.Data;
using System.Windows.Forms;

namespace To_Do_List_App
{
    public partial class BaseToDoListForm : Form
    {
        protected DataTable todoList = new DataTable();
        protected bool isEditing = false;
        protected int completedTasksCounter = 0;

        public BaseToDoListForm()
        {
            InitializeComponent();
        }

        protected virtual void BaseToDoListForm_Load(object sender, EventArgs e)
        {
            // Create columns
            todoList.Columns.Add("Title");
            todoList.Columns.Add("Description");
            todoList.Columns.Add("Difficulty"); // Adăugare coloană pentru dificultate

            // Point our DataGridView to our DataSource
            toDoListView.DataSource = todoList;

            // Initialize difficulty ComboBox
            difficultyComboBox.Items.AddRange(new string[] { "Low", "Medium", "High" });
            difficultyComboBox.SelectedIndex = 0; // Setare valoare implicită
            difficultyComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            // Adaugă task-uri generice în funcție de ora curentă
            AddGenericTasks();
        }

        protected virtual void AddGenericTasks()
        {
            DateTime now = DateTime.Now;
            string taskTitle = "";
            string taskDescription = "";
            string taskDifficulty = "Medium"; // Presupunem dificultate medie pentru task-urile generice

            if (now.Hour < 8)
            {
                taskTitle = "Igiena de dimineata";
                taskDescription = "Realizează activități de igienă personală.";
            }
            else if (now.Hour < 18)
            {
                taskTitle = "Petrece timp cu cei apropiati";
                taskDescription = "Petrece timp de calitate cu familia și prietenii.";
            }
            else if (now.Hour < 22)
            {
                taskTitle = "Citeste o carte/Vizioneaza un film";
                taskDescription = "Relaxează-te citind o carte sau vizionând un film.";
            }
            else
            {
                taskTitle = "Pregateste-te de odihna";
                taskDescription = "Pregătește-te pentru odihna de noapte.";
            }

            todoList.Rows.Add(taskTitle, taskDescription, taskDifficulty);
        }

        protected virtual void newButton_Click(object sender, EventArgs e)
        {
            titleTextbox.Text = "";
            descriptionTextbox.Text = "";
            difficultyComboBox.SelectedIndex = 0; // Resetează dificultatea la "Low"
        }

        protected virtual void editButton_Click(object sender, EventArgs e)
        {
            isEditing = true;
            titleTextbox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[0].ToString();
            descriptionTextbox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[1].ToString();
            difficultyComboBox.Text = todoList.Rows[toDoListView.CurrentCell.RowIndex].ItemArray[2].ToString(); // Setează dificultatea
        }

        protected virtual void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        protected virtual void saveButton_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Title"] = titleTextbox.Text;
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Description"] = descriptionTextbox.Text;
                todoList.Rows[toDoListView.CurrentCell.RowIndex]["Difficulty"] = difficultyComboBox.Text; // Actualizează dificultatea
            }
            else
            {
                todoList.Rows.Add(titleTextbox.Text, descriptionTextbox.Text, difficultyComboBox.Text); // Adaugă dificultatea
            }
            // Clear fields
            titleTextbox.Text = "";
            descriptionTextbox.Text = "";
            difficultyComboBox.SelectedIndex = 0; // Resetează ComboBox-ul pentru dificultate
            isEditing = false;
        }

        protected virtual void completeButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (toDoListView.CurrentCell != null && toDoListView.CurrentCell.RowIndex >= 0)
                {
                    // Obține dificultatea task-ului selectat
                    string difficulty = todoList.Rows[toDoListView.CurrentCell.RowIndex]["Difficulty"].ToString();

                    // Actualizează contorul în funcție de dificultate
                    if (difficulty == "High")
                    {
                        completedTasksCounter += 2;
                    }
                    else
                    {
                        completedTasksCounter += 1;
                    }

                    // Actualizează label-ul contorului
                    completedTasksLabel.Text = "Tasks Completed: " + completedTasksCounter.ToString();

                    // Șterge task-ul marcat ca fiind completat
                    todoList.Rows[toDoListView.CurrentCell.RowIndex].Delete();
                }
                else
                {
                    MessageBox.Show("Select a task to complete.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
