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
            progressBarPeopleLoyalty = new ProgressBar();
            labelPeopleLoyalty = new Label();
            textCurrentCrownTask = new TextBox();
            labelWorker = new Label();
            labelCrownLoyalty = new Label();
            progressBarCrownLoyalty = new ProgressBar();
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
            btnTick.Location = new Point(32, 227);
            btnTick.Margin = new Padding(2, 2, 2, 2);
            btnTick.Name = "btnTick";
            btnTick.Size = new Size(229, 146);
            btnTick.TabIndex = 0;
            btnTick.Text = "Tick";
            btnTick.UseVisualStyleBackColor = true;
            btnTick.Click += btnTick_Click;
            // 
            // btnCreateWorld
            // 
            btnCreateWorld.Location = new Point(32, 133);
            btnCreateWorld.Margin = new Padding(2, 2, 2, 2);
            btnCreateWorld.Name = "btnCreateWorld";
            btnCreateWorld.Size = new Size(229, 74);
            btnCreateWorld.TabIndex = 1;
            btnCreateWorld.Text = "CreateWorld";
            btnCreateWorld.UseVisualStyleBackColor = true;
            btnCreateWorld.Click += btnCreateWorld_Click;
            // 
            // textWorkersState
            // 
            textWorkersState.Location = new Point(754, 148);
            textWorkersState.Margin = new Padding(2, 2, 2, 2);
            textWorkersState.Multiline = true;
            textWorkersState.Name = "textWorkersState";
            textWorkersState.Size = new Size(252, 638);
            textWorkersState.TabIndex = 2;
            // 
            // textBuildingsState
            // 
            textBuildingsState.Location = new Point(1007, 148);
            textBuildingsState.Margin = new Padding(2, 2, 2, 2);
            textBuildingsState.Multiline = true;
            textBuildingsState.Name = "textBuildingsState";
            textBuildingsState.Size = new Size(252, 638);
            textBuildingsState.TabIndex = 3;
            // 
            // btnCreateBuilding
            // 
            btnCreateBuilding.Location = new Point(32, 461);
            btnCreateBuilding.Margin = new Padding(2, 2, 2, 2);
            btnCreateBuilding.Name = "btnCreateBuilding";
            btnCreateBuilding.Size = new Size(229, 52);
            btnCreateBuilding.TabIndex = 4;
            btnCreateBuilding.Text = "CreateBuilding";
            btnCreateBuilding.UseVisualStyleBackColor = true;
            btnCreateBuilding.Click += btnCreateBuilding_Click;
            // 
            // comboBoxBuildingType
            // 
            comboBoxBuildingType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBuildingType.FormattingEnabled = true;
            comboBoxBuildingType.Location = new Point(32, 523);
            comboBoxBuildingType.Margin = new Padding(2, 2, 2, 2);
            comboBoxBuildingType.Name = "comboBoxBuildingType";
            comboBoxBuildingType.Size = new Size(229, 45);
            comboBoxBuildingType.TabIndex = 5;
            comboBoxBuildingType.Visible = false;
            comboBoxBuildingType.SelectedIndexChanged += comboBoxBuildingType_SelectedIndexChanged;
            // 
            // btnDestroyBuilding
            // 
            btnDestroyBuilding.Location = new Point(300, 461);
            btnDestroyBuilding.Margin = new Padding(2, 2, 2, 2);
            btnDestroyBuilding.Name = "btnDestroyBuilding";
            btnDestroyBuilding.Size = new Size(229, 52);
            btnDestroyBuilding.TabIndex = 6;
            btnDestroyBuilding.Text = "DestroyBuilding";
            btnDestroyBuilding.UseVisualStyleBackColor = true;
            btnDestroyBuilding.Click += btnDestroyBuilding_Click;
            // 
            // comboBoxDestroyType
            // 
            comboBoxDestroyType.FormattingEnabled = true;
            comboBoxDestroyType.Location = new Point(302, 523);
            comboBoxDestroyType.Margin = new Padding(2, 2, 2, 2);
            comboBoxDestroyType.Name = "comboBoxDestroyType";
            comboBoxDestroyType.Size = new Size(351, 45);
            comboBoxDestroyType.TabIndex = 7;
            comboBoxDestroyType.Visible = false;
            comboBoxDestroyType.SelectedIndexChanged += comboBoxDestroyType_SelectedIndexChanged;
            // 
            // btnHireWorker
            // 
            btnHireWorker.Location = new Point(32, 587);
            btnHireWorker.Margin = new Padding(2, 2, 2, 2);
            btnHireWorker.Name = "btnHireWorker";
            btnHireWorker.Size = new Size(229, 52);
            btnHireWorker.TabIndex = 8;
            btnHireWorker.Text = "HireWorker";
            btnHireWorker.UseVisualStyleBackColor = true;
            btnHireWorker.Click += btnHireWorker_Click;
            // 
            // comboBoxChooseWorker
            // 
            comboBoxChooseWorker.FormattingEnabled = true;
            comboBoxChooseWorker.Location = new Point(161, 644);
            comboBoxChooseWorker.Margin = new Padding(2, 2, 2, 2);
            comboBoxChooseWorker.Name = "comboBoxChooseWorker";
            comboBoxChooseWorker.Size = new Size(364, 45);
            comboBoxChooseWorker.TabIndex = 9;
            comboBoxChooseWorker.Visible = false;
            // 
            // comboBoxChooseBuildingForWorker
            // 
            comboBoxChooseBuildingForWorker.FormattingEnabled = true;
            comboBoxChooseBuildingForWorker.Location = new Point(161, 696);
            comboBoxChooseBuildingForWorker.Margin = new Padding(2, 2, 2, 2);
            comboBoxChooseBuildingForWorker.Name = "comboBoxChooseBuildingForWorker";
            comboBoxChooseBuildingForWorker.Size = new Size(364, 45);
            comboBoxChooseBuildingForWorker.TabIndex = 10;
            comboBoxChooseBuildingForWorker.Visible = false;
            // 
            // btnFireWorker
            // 
            btnFireWorker.Location = new Point(302, 587);
            btnFireWorker.Margin = new Padding(2, 2, 2, 2);
            btnFireWorker.Name = "btnFireWorker";
            btnFireWorker.Size = new Size(229, 52);
            btnFireWorker.TabIndex = 11;
            btnFireWorker.Text = "FireWorker";
            btnFireWorker.UseVisualStyleBackColor = true;
            btnFireWorker.Click += btnFireWorker_Click;
            // 
            // textSettlementResourcesState
            // 
            textSettlementResourcesState.Location = new Point(1260, 148);
            textSettlementResourcesState.Margin = new Padding(2, 2, 2, 2);
            textSettlementResourcesState.Multiline = true;
            textSettlementResourcesState.Name = "textSettlementResourcesState";
            textSettlementResourcesState.Size = new Size(252, 638);
            textSettlementResourcesState.TabIndex = 12;
            // 
            // LabelWorkers
            // 
            LabelWorkers.AutoSize = true;
            LabelWorkers.Location = new Point(754, 96);
            LabelWorkers.Margin = new Padding(2, 0, 2, 0);
            LabelWorkers.Name = "LabelWorkers";
            LabelWorkers.Size = new Size(113, 37);
            LabelWorkers.TabIndex = 13;
            LabelWorkers.Text = "Workers";
            // 
            // LabelBuildings
            // 
            LabelBuildings.AutoSize = true;
            LabelBuildings.Location = new Point(1007, 96);
            LabelBuildings.Margin = new Padding(2, 0, 2, 0);
            LabelBuildings.Name = "LabelBuildings";
            LabelBuildings.Size = new Size(126, 37);
            LabelBuildings.TabIndex = 14;
            LabelBuildings.Text = "Buildings";
            // 
            // LabelSettlementresources
            // 
            LabelSettlementresources.AutoSize = true;
            LabelSettlementresources.Location = new Point(1260, 96);
            LabelSettlementresources.Margin = new Padding(2, 0, 2, 0);
            LabelSettlementresources.Name = "LabelSettlementresources";
            LabelSettlementresources.Size = new Size(263, 37);
            LabelSettlementresources.TabIndex = 15;
            LabelSettlementresources.Text = "Settlement resources";
            // 
            // progressBarPeopleLoyalty
            // 
            progressBarPeopleLoyalty.Location = new Point(1247, 32);
            progressBarPeopleLoyalty.Margin = new Padding(2, 2, 2, 2);
            progressBarPeopleLoyalty.Name = "progressBarPeopleLoyalty";
            progressBarPeopleLoyalty.Size = new Size(197, 52);
            progressBarPeopleLoyalty.TabIndex = 16;
            // 
            // labelPeopleLoyalty
            // 
            labelPeopleLoyalty.AutoSize = true;
            labelPeopleLoyalty.Location = new Point(1044, 32);
            labelPeopleLoyalty.Margin = new Padding(2, 0, 2, 0);
            labelPeopleLoyalty.Name = "labelPeopleLoyalty";
            labelPeopleLoyalty.Size = new Size(190, 37);
            labelPeopleLoyalty.TabIndex = 17;
            labelPeopleLoyalty.Text = "People Loyalty";
            // 
            // textCurrentCrownTask
            // 
            textCurrentCrownTask.Location = new Point(212, 32);
            textCurrentCrownTask.Margin = new Padding(2, 2, 2, 2);
            textCurrentCrownTask.Multiline = true;
            textCurrentCrownTask.Name = "textCurrentCrownTask";
            textCurrentCrownTask.Size = new Size(424, 51);
            textCurrentCrownTask.TabIndex = 18;
            // 
            // labelWorker
            // 
            labelWorker.AutoSize = true;
            labelWorker.Location = new Point(32, 651);
            labelWorker.Margin = new Padding(2, 0, 2, 0);
            labelWorker.Name = "labelWorker";
            labelWorker.Size = new Size(102, 37);
            labelWorker.TabIndex = 19;
            labelWorker.Text = "Worker";
            // 
            // labelCrownLoyalty
            // 
            labelCrownLoyalty.AutoSize = true;
            labelCrownLoyalty.Location = new Point(643, 35);
            labelCrownLoyalty.Margin = new Padding(2, 0, 2, 0);
            labelCrownLoyalty.Name = "labelCrownLoyalty";
            labelCrownLoyalty.Size = new Size(186, 37);
            labelCrownLoyalty.TabIndex = 20;
            labelCrownLoyalty.Text = "Crown Loyalty";
            // 
            // progressBarCrownLoyalty
            // 
            progressBarCrownLoyalty.Location = new Point(840, 32);
            progressBarCrownLoyalty.Margin = new Padding(2, 2, 2, 2);
            progressBarCrownLoyalty.Name = "progressBarCrownLoyalty";
            progressBarCrownLoyalty.Size = new Size(197, 52);
            progressBarCrownLoyalty.TabIndex = 21;
            // 
            // labelGoal
            // 
            labelGoal.AutoSize = true;
            labelGoal.Location = new Point(32, 35);
            labelGoal.Margin = new Padding(2, 0, 2, 0);
            labelGoal.Name = "labelGoal";
            labelGoal.Size = new Size(161, 37);
            labelGoal.TabIndex = 22;
            labelGoal.Text = "CurrentGoal";
            // 
            // labelBuilding
            // 
            labelBuilding.AutoSize = true;
            labelBuilding.Location = new Point(32, 698);
            labelBuilding.Margin = new Padding(2, 0, 2, 0);
            labelBuilding.Name = "labelBuilding";
            labelBuilding.Size = new Size(115, 37);
            labelBuilding.TabIndex = 23;
            labelBuilding.Text = "Building";
            // 
            // btnAskForNewWorkers
            // 
            btnAskForNewWorkers.Location = new Point(302, 165);
            btnAskForNewWorkers.Margin = new Padding(2, 2, 2, 2);
            btnAskForNewWorkers.Name = "btnAskForNewWorkers";
            btnAskForNewWorkers.Size = new Size(257, 121);
            btnAskForNewWorkers.TabIndex = 24;
            btnAskForNewWorkers.Text = "AskForNewWorkers for 30 money each one";
            btnAskForNewWorkers.UseVisualStyleBackColor = true;
            btnAskForNewWorkers.Click += btnAskForNewWorkers_Click;
            // 
            // comboBoxAmountOfWorkers
            // 
            comboBoxAmountOfWorkers.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxAmountOfWorkers.FormattingEnabled = true;
            comboBoxAmountOfWorkers.Location = new Point(302, 291);
            comboBoxAmountOfWorkers.Margin = new Padding(2, 2, 2, 2);
            comboBoxAmountOfWorkers.Name = "comboBoxAmountOfWorkers";
            comboBoxAmountOfWorkers.Size = new Size(253, 45);
            comboBoxAmountOfWorkers.TabIndex = 25;
            comboBoxAmountOfWorkers.Visible = false;
            // 
            // textTicksTillNewWorkers
            // 
            textTicksTillNewWorkers.Location = new Point(566, 212);
            textTicksTillNewWorkers.Margin = new Padding(2, 2, 2, 2);
            textTicksTillNewWorkers.Name = "textTicksTillNewWorkers";
            textTicksTillNewWorkers.Size = new Size(180, 43);
            textTicksTillNewWorkers.TabIndex = 26;
            // 
            // labelTicksTillComeNewWorkers
            // 
            labelTicksTillComeNewWorkers.AutoSize = true;
            labelTicksTillComeNewWorkers.Location = new Point(566, 170);
            labelTicksTillComeNewWorkers.Margin = new Padding(2, 0, 2, 0);
            labelTicksTillComeNewWorkers.Name = "labelTicksTillComeNewWorkers";
            labelTicksTillComeNewWorkers.Size = new Size(179, 37);
            labelTicksTillComeNewWorkers.TabIndex = 27;
            labelTicksTillComeNewWorkers.Text = "TicksTillCome";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1605, 841);
            Controls.Add(labelTicksTillComeNewWorkers);
            Controls.Add(textTicksTillNewWorkers);
            Controls.Add(comboBoxAmountOfWorkers);
            Controls.Add(btnAskForNewWorkers);
            Controls.Add(labelBuilding);
            Controls.Add(labelGoal);
            Controls.Add(progressBarCrownLoyalty);
            Controls.Add(labelCrownLoyalty);
            Controls.Add(labelWorker);
            Controls.Add(textCurrentCrownTask);
            Controls.Add(labelPeopleLoyalty);
            Controls.Add(progressBarPeopleLoyalty);
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
            Margin = new Padding(6, 7, 6, 7);
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
        private ProgressBar progressBarPeopleLoyalty;
        private Label labelPeopleLoyalty;
        private TextBox textCurrentCrownTask;
        private Label labelWorker;
        private Label labelCrownLoyalty;
        private ProgressBar progressBarCrownLoyalty;
        private Label labelGoal;
        private Label labelBuilding;
        private Button btnAskForNewWorkers;
        private ComboBox comboBoxAmountOfWorkers;
        private TextBox textTicksTillNewWorkers;
        private Label labelTicksTillComeNewWorkers;
    }
}
