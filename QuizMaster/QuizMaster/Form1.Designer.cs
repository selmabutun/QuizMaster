using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace QuizMaster
{
	public partial class Form1 : Form
	{
		private int questionIndex = 0;
		private int scores = 0;

		public Form1(string a)
		{
			InitializeComponent();
			LoadQuestions();
		}

		private void LoadQuestions()
		{
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				connection.Open();
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Questions", connection);
				questionsTable = new DataTable();
				adapter.Fill(questionsTable);
			}

			DisplayQuestion();
		}

		private void DisplayQuestion()
		{
			if (questionIndex >= questionsTable.Rows.Count)
			{
				lblUsername.Visible = false;
				label1.Visible = true;
				lblScore.Visible = true;
				lblScore.Text = $"Your Score: {scores}";
				button1.Enabled = false;
				return;
			}

			DataRow row = questionsTable.Rows[questionIndex];

			label1.Text = row["QuestionText"].ToString();
			radioButton1.Text = row["OptionA"].ToString();
			radioButton2.Text = row["OptionB"].ToString();
			radioButton3.Text = row["OptionC"].ToString();
			radioButton4.Text = row["OptionD"].ToString();

			radioButton1.Checked = false;
			radioButton2.Checked = false;
			radioButton3.Checked = false;
			radioButton4.Checked = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (questionIndex >= questionsTable.Rows.Count)
				return;

			DataRow row = questionsTable.Rows[questionIndex];
			string correctAnswer = row["CorrectOption"].ToString();

			if ((radioButton1.Checked && correctAnswer == "A") ||
				(radioButton2.Checked && correctAnswer == "B") ||
				(radioButton3.Checked && correctAnswer == "C") ||
				(radioButton4.Checked && correctAnswer == "D"))
			{
				scores++;
			}

			questionIndex++;
			DisplayQuestion();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private System.Windows.Forms.Label lblUsername;
		private TextBox textBox1;
		private TextBox textBox2;
		private Button button1;
		private System.Windows.Forms.Label lblQuestion;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton4;
		private Button btnNext;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblScore;
		private Button button3;
		private Button btnStartQuiz;
	}
}
