using SettlementGame.Domain;

namespace WinFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnTick = new Button();
            btnCreateWorld = new Button();
            textWorkersState = new TextBox();
            textBuildingsState = new TextBox();
            btnCreateBuilding = new Button();
            comboBoxBuildingType = new ComboBox();
            buildingCatalogBindingSource = new BindingSource(components);
            btnDestroyBuilding = new Button();
            comboBoxDestroyType = new ComboBox();
            btnHireWorker = new Button();
            comboBoxChooseWorker = new ComboBox();
            comboBoxChooseBuildingForWorker = new ComboBox();
            btnFireWorker = new Button();
            textSettlementResourcesState = new TextBox();
            LabelWorkers = new Label();
            LabelBuildings = new Label();
            LabelSettlementresources = new Label();
            progressBarPeopleLoyality = new ProgressBar();
            labelPeopleLoyality = new Label();
            textCurrentCrownTask = new TextBox();
            labelWorker = new Label();
            labelCrownLoyality = new Label();
            progressBarCrownLoyality = new ProgressBar();
            labelGoal = new Label();
            labelBuilding = new Label();
            btnAskForNewWorkers = new Button();
            comboBoxAmountOfWorkers = new ComboBox();
            textTicksTillNewWorkers = new TextBox();
            labelTicksTillComeNewWorkers = new Label();
            ((System.ComponentModel.ISupportInitialize)buildingCatalogBindingSource).BeginInit();
            SuspendLayout();
            // 
            // btnTick
            // 
            btnTick.Location = new Point(15, 92);
            btnTick.Margin = new Padding(1, 1, 1, 1);
            btnTick.Name = "btnTick";
            btnTick.Size = new Size(107, 59);
            btnTick.TabIndex = 0;
            btnTick.Text = "Tick";
            btnTick.UseVisualStyleBackColor = true;
            btnTick.Click += btnTick_Click;
            // 
            // btnCreateWorld
            // 
            btnCreateWorld.Location = new Point(15, 54);
            btnCreateWorld.Margin = new Padding(1, 1, 1, 1);
            btnCreateWorld.Name = "btnCreateWorld";
            btnCreateWorld.Size = new Size(107, 30);
            btnCreateWorld.TabIndex = 1;
            btnCreateWorld.Text = "CreateWorld";
            btnCreateWorld.UseVisualStyleBackColor = true;
            btnCreateWorld.Click += btnCreateWorld_Click;
            // 
            // textWorkersState
            // 
            textWorkersState.Location = new Point(352, 60);
            textWorkersState.Margin = new Padding(1, 1, 1, 1);
            textWorkersState.Multiline = true;
            textWorkersState.Name = "textWorkersState";
            textWorkersState.Size = new Size(111, 261);
            textWorkersState.TabIndex = 2;
            // 
            // textBuildingsState
            // 
            textBuildingsState.Location = new Point(470, 60);
            textBuildingsState.Margin = new Padding(1, 1, 1, 1);
            textBuildingsState.Multiline = true;
            textBuildingsState.Name = "textBuildingsState";
            textBuildingsState.Size = new Size(111, 261);
            textBuildingsState.TabIndex = 3;
            // 
            // btnCreateBuilding
            // 
            btnCreateBuilding.Location = new Point(15, 187);
            btnCreateBuilding.Margin = new Padding(1, 1, 1, 1);
            btnCreateBuilding.Name = "btnCreateBuilding";
            btnCreateBuilding.Size = new Size(107, 21);
            btnCreateBuilding.TabIndex = 4;
            btnCreateBuilding.Text = "CreateBuilding";
            btnCreateBuilding.UseVisualStyleBackColor = true;
            btnCreateBuilding.Click += btnCreateBuilding_Click;
            // 
            // comboBoxBuildingType
            // 
            comboBoxBuildingType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBuildingType.FormattingEnabled = true;
            comboBoxBuildingType.Location = new Point(15, 212);
            comboBoxBuildingType.Margin = new Padding(1, 1, 1, 1);
            comboBoxBuildingType.Name = "comboBoxBuildingType";
            comboBoxBuildingType.Size = new Size(109, 23);
            comboBoxBuildingType.TabIndex = 5;
            comboBoxBuildingType.Visible = false;
            comboBoxBuildingType.SelectedIndexChanged += comboBoxBuildingType_SelectedIndexChanged;
            // 
            // btnDestroyBuilding
            // 
            btnDestroyBuilding.Location = new Point(140, 187);
            btnDestroyBuilding.Margin = new Padding(1, 1, 1, 1);
            btnDestroyBuilding.Name = "btnDestroyBuilding";
            btnDestroyBuilding.Size = new Size(107, 21);
            btnDestroyBuilding.TabIndex = 6;
            btnDestroyBuilding.Text = "DestroyBuilding";
            btnDestroyBuilding.UseVisualStyleBackColor = true;
            btnDestroyBuilding.Click += btnDestroyBuilding_Click;
            // 
            // comboBoxDestroyType
            // 
            comboBoxDestroyType.FormattingEnabled = true;
            comboBoxDestroyType.Location = new Point(141, 212);
            comboBoxDestroyType.Margin = new Padding(1, 1, 1, 1);
            comboBoxDestroyType.Name = "comboBoxDestroyType";
            comboBoxDestroyType.Size = new Size(166, 23);
            comboBoxDestroyType.TabIndex = 7;
            comboBoxDestroyType.Visible = false;
            comboBoxDestroyType.SelectedIndexChanged += comboBoxDestroyType_SelectedIndexChanged;
            // 
            // btnHireWorker
            // 
            btnHireWorker.Location = new Point(15, 238);
            btnHireWorker.Margin = new Padding(1, 1, 1, 1);
            btnHireWorker.Name = "btnHireWorker";
            btnHireWorker.Size = new Size(107, 21);
            btnHireWorker.TabIndex = 8;
            btnHireWorker.Text = "HireWorker";
            btnHireWorker.UseVisualStyleBackColor = true;
            btnHireWorker.Click += btnHireWorker_Click;
            // 
            // comboBoxChooseWorker
            // 
            comboBoxChooseWorker.FormattingEnabled = true;
            comboBoxChooseWorker.Location = new Point(75, 261);
            comboBoxChooseWorker.Margin = new Padding(1, 1, 1, 1);
            comboBoxChooseWorker.Name = "comboBoxChooseWorker";
            comboBoxChooseWorker.Size = new Size(172, 23);
            comboBoxChooseWorker.TabIndex = 9;
            comboBoxChooseWorker.Visible = false;
            // 
            // comboBoxChooseBuildingForWorker
            // 
            comboBoxChooseBuildingForWorker.FormattingEnabled = true;
            comboBoxChooseBuildingForWorker.Location = new Point(75, 282);
            comboBoxChooseBuildingForWorker.Margin = new Padding(1, 1, 1, 1);
            comboBoxChooseBuildingForWorker.Name = "comboBoxChooseBuildingForWorker";
            comboBoxChooseBuildingForWorker.Size = new Size(172, 23);
            comboBoxChooseBuildingForWorker.TabIndex = 10;
            comboBoxChooseBuildingForWorker.Visible = false;
            // 
            // btnFireWorker
            // 
            btnFireWorker.Location = new Point(141, 238);
            btnFireWorker.Margin = new Padding(1, 1, 1, 1);
            btnFireWorker.Name = "btnFireWorker";
            btnFireWorker.Size = new Size(107, 21);
            btnFireWorker.TabIndex = 11;
            btnFireWorker.Text = "FireWorker";
            btnFireWorker.UseVisualStyleBackColor = true;
            btnFireWorker.Click += btnFireWorker_Click;
            // 
            // textSettlementResourcesState
            // 
            textSettlementResourcesState.Location = new Point(588, 60);
            textSettlementResourcesState.Margin = new Padding(1, 1, 1, 1);
            textSettlementResourcesState.Multiline = true;
            textSettlementResourcesState.Name = "textSettlementResourcesState";
            textSettlementResourcesState.Size = new Size(111, 261);
            textSettlementResourcesState.TabIndex = 12;
            // 
            // LabelWorkers
            // 
            LabelWorkers.AutoSize = true;
            LabelWorkers.Location = new Point(352, 39);
            LabelWorkers.Margin = new Padding(1, 0, 1, 0);
            LabelWorkers.Name = "LabelWorkers";
            LabelWorkers.Size = new Size(50, 15);
            LabelWorkers.TabIndex = 13;
            LabelWorkers.Text = "Workers";
            // 
            // LabelBuildings
            // 
            LabelBuildings.AutoSize = true;
            LabelBuildings.Location = new Point(470, 39);
            LabelBuildings.Margin = new Padding(1, 0, 1, 0);
            LabelBuildings.Name = "LabelBuildings";
            LabelBuildings.Size = new Size(56, 15);
            LabelBuildings.TabIndex = 14;
            LabelBuildings.Text = "Buildings";
            // 
            // LabelSettlementresources
            // 
            LabelSettlementresources.AutoSize = true;
            LabelSettlementresources.Location = new Point(588, 39);
            LabelSettlementresources.Margin = new Padding(1, 0, 1, 0);
            LabelSettlementresources.Name = "LabelSettlementresources";
            LabelSettlementresources.Size = new Size(117, 15);
            LabelSettlementresources.TabIndex = 15;
            LabelSettlementresources.Text = "Settlement resources";
            // 
            // progressBarPeopleLoyality
            // 
            progressBarPeopleLoyality.Location = new Point(582, 13);
            progressBarPeopleLoyality.Margin = new Padding(1, 1, 1, 1);
            progressBarPeopleLoyality.Name = "progressBarPeopleLoyality";
            progressBarPeopleLoyality.Size = new Size(92, 21);
            progressBarPeopleLoyality.TabIndex = 16;
            // 
            // labelPeopleLoyality
            // 
            labelPeopleLoyality.AutoSize = true;
            labelPeopleLoyality.Location = new Point(487, 13);
            labelPeopleLoyality.Margin = new Padding(1, 0, 1, 0);
            labelPeopleLoyality.Name = "labelPeopleLoyality";
            labelPeopleLoyality.Size = new Size(87, 15);
            labelPeopleLoyality.TabIndex = 17;
            labelPeopleLoyality.Text = "People Loyality";
            // 
            // textCurrentCrownTask
            // 
            textCurrentCrownTask.Location = new Point(99, 13);
            textCurrentCrownTask.Margin = new Padding(1, 1, 1, 1);
            textCurrentCrownTask.Multiline = true;
            textCurrentCrownTask.Name = "textCurrentCrownTask";
            textCurrentCrownTask.Size = new Size(200, 23);
            textCurrentCrownTask.TabIndex = 18;
            // 
            // labelWorker
            // 
            labelWorker.AutoSize = true;
            labelWorker.Location = new Point(15, 264);
            labelWorker.Margin = new Padding(1, 0, 1, 0);
            labelWorker.Name = "labelWorker";
            labelWorker.Size = new Size(45, 15);
            labelWorker.TabIndex = 19;
            labelWorker.Text = "Worker";
            // 
            // labelCrownLoyality
            // 
            labelCrownLoyality.AutoSize = true;
            labelCrownLoyality.Location = new Point(300, 14);
            labelCrownLoyality.Margin = new Padding(1, 0, 1, 0);
            labelCrownLoyality.Name = "labelCrownLoyality";
            labelCrownLoyality.Size = new Size(86, 15);
            labelCrownLoyality.TabIndex = 20;
            labelCrownLoyality.Text = "Crown Loyality";
            // 
            // progressBarCrownLoyality
            // 
            progressBarCrownLoyality.Location = new Point(392, 13);
            progressBarCrownLoyality.Margin = new Padding(1, 1, 1, 1);
            progressBarCrownLoyality.Name = "progressBarCrownLoyality";
            progressBarCrownLoyality.Size = new Size(92, 21);
            progressBarCrownLoyality.TabIndex = 21;
            // 
            // labelGoal
            // 
            labelGoal.AutoSize = true;
            labelGoal.Location = new Point(15, 14);
            labelGoal.Margin = new Padding(1, 0, 1, 0);
            labelGoal.Name = "labelGoal";
            labelGoal.Size = new Size(71, 15);
            labelGoal.TabIndex = 22;
            labelGoal.Text = "CurrentGoal";
            // 
            // labelBuilding
            // 
            labelBuilding.AutoSize = true;
            labelBuilding.Location = new Point(15, 283);
            labelBuilding.Margin = new Padding(1, 0, 1, 0);
            labelBuilding.Name = "labelBuilding";
            labelBuilding.Size = new Size(51, 15);
            labelBuilding.TabIndex = 23;
            labelBuilding.Text = "Building";
            // 
            // btnAskForNewWorkers
            // 
            btnAskForNewWorkers.Location = new Point(141, 67);
            btnAskForNewWorkers.Margin = new Padding(1, 1, 1, 1);
            btnAskForNewWorkers.Name = "btnAskForNewWorkers";
            btnAskForNewWorkers.Size = new Size(120, 49);
            btnAskForNewWorkers.TabIndex = 24;
            btnAskForNewWorkers.Text = "AskForNewWorkers for 30 money each one";
            btnAskForNewWorkers.UseVisualStyleBackColor = true;
            btnAskForNewWorkers.Click += btnAskForNewWorkers_Click;
            // 
            // comboBoxAmountOfWorkers
            // 
            comboBoxAmountOfWorkers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAmountOfWorkers.FormattingEnabled = true;
            comboBoxAmountOfWorkers.Location = new Point(141, 118);
            comboBoxAmountOfWorkers.Margin = new Padding(1, 1, 1, 1);
            comboBoxAmountOfWorkers.Name = "comboBoxAmountOfWorkers";
            comboBoxAmountOfWorkers.Size = new Size(120, 23);
            comboBoxAmountOfWorkers.TabIndex = 25;
            comboBoxAmountOfWorkers.Visible = false;
            // 
            // textTicksTillNewWorkers
            // 
            textTicksTillNewWorkers.Location = new Point(264, 86);
            textTicksTillNewWorkers.Margin = new Padding(1, 1, 1, 1);
            textTicksTillNewWorkers.Name = "textTicksTillNewWorkers";
            textTicksTillNewWorkers.Size = new Size(86, 23);
            textTicksTillNewWorkers.TabIndex = 26;
            // 
            // labelTicksTillComeNewWorkers
            // 
            labelTicksTillComeNewWorkers.AutoSize = true;
            labelTicksTillComeNewWorkers.Location = new Point(264, 69);
            labelTicksTillComeNewWorkers.Margin = new Padding(1, 0, 1, 0);
            labelTicksTillComeNewWorkers.Name = "labelTicksTillComeNewWorkers";
            labelTicksTillComeNewWorkers.Size = new Size(80, 15);
            labelTicksTillComeNewWorkers.TabIndex = 27;
            labelTicksTillComeNewWorkers.Text = "TicksTillCome";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(749, 341);
            Controls.Add(labelTicksTillComeNewWorkers);
            Controls.Add(textTicksTillNewWorkers);
            Controls.Add(comboBoxAmountOfWorkers);
            Controls.Add(btnAskForNewWorkers);
            Controls.Add(labelBuilding);
            Controls.Add(labelGoal);
            Controls.Add(progressBarCrownLoyality);
            Controls.Add(labelCrownLoyality);
            Controls.Add(labelWorker);
            Controls.Add(textCurrentCrownTask);
            Controls.Add(labelPeopleLoyality);
            Controls.Add(progressBarPeopleLoyality);
            Controls.Add(LabelSettlementresources);
            Controls.Add(LabelBuildings);
            Controls.Add(LabelWorkers);
            Controls.Add(textSettlementResourcesState);
            Controls.Add(btnFireWorker);
            Controls.Add(comboBoxChooseBuildingForWorker);
            Controls.Add(comboBoxChooseWorker);
            Controls.Add(btnHireWorker);
            Controls.Add(comboBoxDestroyType);
            Controls.Add(btnDestroyBuilding);
            Controls.Add(comboBoxBuildingType);
            Controls.Add(btnCreateBuilding);
            Controls.Add(textBuildingsState);
            Controls.Add(textWorkersState);
            Controls.Add(btnCreateWorld);
            Controls.Add(btnTick);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)buildingCatalogBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTick;
        private Button btnCreateWorld;
        private TextBox textWorkersState;
        private TextBox textBuildingsState;
        private Button btnCreateBuilding;
        private ComboBox comboBoxBuildingType;
        private BindingSource buildingCatalogBindingSource;
        private Button btnDestroyBuilding;
        private ComboBox comboBoxDestroyType;
        private Button btnHireWorker;
        private ComboBox comboBoxChooseWorker;
        private ComboBox comboBoxChooseBuildingForWorker;
        private Button btnFireWorker;
        private TextBox textSettlementResourcesState;
        private Label LabelWorkers;
        private Label LabelBuildings;
        private Label LabelSettlementresources;
        private ProgressBar progressBarPeopleLoyality;
        private Label labelPeopleLoyality;
        private TextBox textCurrentCrownTask;
        private Label labelWorker;
        private Label labelCrownLoyality;
        private ProgressBar progressBarCrownLoyality;
        private Label labelGoal;
        private Label labelBuilding;
        private Button btnAskForNewWorkers;
        private ComboBox comboBoxAmountOfWorkers;
        private TextBox textTicksTillNewWorkers;
        private Label labelTicksTillComeNewWorkers;
    }
}
