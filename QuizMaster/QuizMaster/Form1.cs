using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuizMaster
{
	public partial class Form1 : Form
	{
		DataTable questionsTable = new DataTable();
		int currentQuestionIndex = 0;
		int score = 0;

		string connectionString = "Server=.\\SQLEXPRESS;Database=QuizMaster;Trusted_Connection=True;";

		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			lblScore.Text = "";
			lblScore.Visible = true;
			lblScore.Text = $"Your Score: {score}/{questionsTable.Rows.Count}";
			btnNext.Enabled = false;
		}

		private void btnStartQuiz_Click_1(object sender, EventArgs e)
		{
			try
			{
				questionsTable.Clear();
				SqlConnection conn = new SqlConnection(connectionString);
				SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Questions", conn);
				questionsTable.Clear();
				da.Fill(questionsTable);

				currentQuestionIndex = 0;
				score = 0;
				lblUsername.Text = "";
				btnNext.Enabled = true;

				LoadQuestion();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Database connection failed: " + ex.Message);
			}
		}

		private void LoadQuestion()
		{
			if (currentQuestionIndex < questionsTable.Rows.Count)
			{
				DataRow row = questionsTable.Rows[currentQuestionIndex];
				lblQuestion.Text = row["QuestionText"].ToString();
				radioButton1.Text = row["OptionA"].ToString();
				radioButton2.Text = row["OptionB"].ToString();
				radioButton3.Text = row["OptionC"].ToString();
				radioButton4.Text = row["OptionD"].ToString();

				radioButton1.Checked = false;
				radioButton2.Checked = false;
				radioButton3.Checked = false;
				radioButton4.Checked = false;
			}
			else
			{
				lblQuestion.Text = "Quiz Completed!";
				btnNext.Enabled = false;
			}
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			string selected = "";

			if (radioButton1.Checked) selected = "A";
			else if (radioButton2.Checked) selected = "B";
			else if (radioButton3.Checked) selected = "C";
			else if (radioButton4.Checked) selected = "D";
			string correct = questionsTable.Rows[currentQuestionIndex]["CorrectOption"].ToString();

			if (string.IsNullOrEmpty(selected))
			{
				MessageBox.Show("Please select an answer before continuing.");
				return;
			}




			if (selected == correct)
				score++;

			currentQuestionIndex++;
			LoadQuestion();
			lblScore.Text = $"Your Score: {score}/{questionsTable.Rows.Count}";
		}

		private void btnExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void InitializeComponent()
		{
			this.lblUsername = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.lblQuestion = new System.Windows.Forms.Label();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton4 = new System.Windows.Forms.RadioButton();
			this.btnNext = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblScore = new System.Windows.Forms.Label();
			this.button3 = new System.Windows.Forms.Button();
			this.btnStartQuiz = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblUsername
			// 
			this.lblUsername.AutoSize = true;
			this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblUsername.Location = new System.Drawing.Point(152, 9);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(626, 54);
			this.lblUsername.TabIndex = 0;
			this.lblUsername.Text = "ENTER YOUR USERNAME";
			this.lblUsername.Click += new System.EventHandler(this.lblScore_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(12, 80);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(0, 22);
			this.textBox1.TabIndex = 1;
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(12, 80);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(905, 22);
			this.textBox2.TabIndex = 2;
			// 
			// lblQuestion
			// 
			this.lblQuestion.AutoSize = true;
			this.lblQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblQuestion.Location = new System.Drawing.Point(5, 175);
			this.lblQuestion.Name = "lblQuestion";
			this.lblQuestion.Size = new System.Drawing.Size(422, 38);
			this.lblQuestion.TabIndex = 3;
			this.lblQuestion.Text = "The question will come here";
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.radioButton1.Location = new System.Drawing.Point(41, 244);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(64, 46);
			this.radioButton1.TabIndex = 4;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "A";
			this.radioButton1.UseVisualStyleBackColor = true;
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.radioButton2.Location = new System.Drawing.Point(41, 296);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(64, 46);
			this.radioButton2.TabIndex = 5;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "B";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.radioButton3.Location = new System.Drawing.Point(41, 358);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(62, 42);
			this.radioButton3.TabIndex = 6;
			this.radioButton3.TabStop = true;
			this.radioButton3.Text = "C";
			this.radioButton3.UseVisualStyleBackColor = true;
			// 
			// radioButton4
			// 
			this.radioButton4.AutoSize = true;
			this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.radioButton4.Location = new System.Drawing.Point(43, 417);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(62, 42);
			this.radioButton4.TabIndex = 7;
			this.radioButton4.TabStop = true;
			this.radioButton4.Text = "D";
			this.radioButton4.UseVisualStyleBackColor = true;
			// 
			// btnNext
			// 
			this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.btnNext.Location = new System.Drawing.Point(43, 511);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(378, 92);
			this.btnNext.TabIndex = 8;
			this.btnNext.Text = "Next";
			this.btnNext.UseVisualStyleBackColor = true;
			this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.label1.Location = new System.Drawing.Point(614, 175);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(260, 38);
			this.label1.TabIndex = 9;
			this.label1.Text = "Quiz Completed!";
			// 
			// lblScore
			// 
			this.lblScore.AutoSize = true;
			this.lblScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.lblScore.Location = new System.Drawing.Point(604, 346);
			this.lblScore.Name = "lblScore";
			this.lblScore.Size = new System.Drawing.Size(270, 54);
			this.lblScore.TabIndex = 10;
			this.lblScore.Text = "Your Score:";
			this.lblScore.Click += new System.EventHandler(this.lblScore_Click);
			// 
			// button3
			// 
			this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.button3.Location = new System.Drawing.Point(507, 510);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(378, 95);
			this.button3.TabIndex = 11;
			this.button3.Text = "Exit";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// btnStartQuiz
			// 
			this.btnStartQuiz.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
			this.btnStartQuiz.Location = new System.Drawing.Point(308, 108);
			this.btnStartQuiz.Name = "btnStartQuiz";
			this.btnStartQuiz.Size = new System.Drawing.Size(298, 64);
			this.btnStartQuiz.TabIndex = 12;
			this.btnStartQuiz.Text = "Start Quiz";
			this.btnStartQuiz.UseVisualStyleBackColor = true;
			this.btnStartQuiz.Click += new System.EventHandler(this.btnStartQuiz_Click_1);
			// 
			// Form1
			// 
			this.ClientSize = new System.Drawing.Size(1043, 634);
			this.Controls.Add(this.btnStartQuiz);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.lblScore);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnNext);
			this.Controls.Add(this.radioButton4);
			this.Controls.Add(this.radioButton3);
			this.Controls.Add(this.radioButton2);
			this.Controls.Add(this.radioButton1);
			this.Controls.Add(this.lblQuestion);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.lblUsername);
			this.Name = "Form1";
			this.Text = "Quiz Master";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void Form1_Load_1(object sender, EventArgs e)
		{

		}

		private void radioButton4_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void radioButton2_CheckedChanged(object sender, EventArgs e)
		{

		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void lblScore_Click(object sender, EventArgs e)
		{
			string username = textBox2.Text.Trim();

			if (string.IsNullOrEmpty(username))
			{
				MessageBox.Show("Username is empty!");
				return;
			}
		}
	}
}