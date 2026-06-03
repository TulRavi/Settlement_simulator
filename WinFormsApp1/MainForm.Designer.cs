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
            ((System.ComponentModel.ISupportInitialize)buildingCatalogBindingSource).BeginInit();
            SuspendLayout();
            // 
            // btnTick
            // 
            btnTick.Location = new Point(39, 125);
            btnTick.Name = "btnTick";
            btnTick.Size = new Size(229, 145);
            btnTick.TabIndex = 0;
            btnTick.Text = "Tick";
            btnTick.UseVisualStyleBackColor = true;
            btnTick.Click += btnTick_Click;
            // 
            // btnCreateWorld
            // 
            btnCreateWorld.Location = new Point(39, 31);
            btnCreateWorld.Name = "btnCreateWorld";
            btnCreateWorld.Size = new Size(229, 75);
            btnCreateWorld.TabIndex = 1;
            btnCreateWorld.Text = "CreateWorld";
            btnCreateWorld.UseVisualStyleBackColor = true;
            btnCreateWorld.Click += btnCreateWorld_Click;
            // 
            // textWorkersState
            // 
            textWorkersState.Location = new Point(657, 31);
            textWorkersState.Multiline = true;
            textWorkersState.Name = "textWorkersState";
            textWorkersState.Size = new Size(234, 600);
            textWorkersState.TabIndex = 2;
            // 
            // textBuildingsState
            // 
            textBuildingsState.Location = new Point(917, 31);
            textBuildingsState.Multiline = true;
            textBuildingsState.Name = "textBuildingsState";
            textBuildingsState.Size = new Size(234, 600);
            textBuildingsState.TabIndex = 3;
            // 
            // btnCreateBuilding
            // 
            btnCreateBuilding.Location = new Point(39, 297);
            btnCreateBuilding.Name = "btnCreateBuilding";
            btnCreateBuilding.Size = new Size(229, 107);
            btnCreateBuilding.TabIndex = 4;
            btnCreateBuilding.Text = "CreateBuilding";
            btnCreateBuilding.UseVisualStyleBackColor = true;
            btnCreateBuilding.Click += btnCreateBuilding_Click;
            // 
            // comboBoxBuildingType
            // 
            comboBoxBuildingType.DataSource = new BuildingType[]
    {
    BuildingType.WoodMakery,
    BuildingType.StoneMakery,
    BuildingType.GoldMakery,
    BuildingType.Tavern,
    BuildingType.BeerMakery
    };
            comboBoxBuildingType.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxBuildingType.FormattingEnabled = true;
            //comboBoxBuildingType.Items.AddRange(new object[] { BuildingType.WoodMakery, BuildingType.StoneMakery, BuildingType.GoldMakery, BuildingType.Tavern, BuildingType.BeerMakery });
            //todo: ошибка System.ArgumentException: "Изменение коллекции элементов невозможно, если задано свойство DataSource."
            comboBoxBuildingType.Location = new Point(39, 422);
            comboBoxBuildingType.Name = "comboBoxBuildingType";
            comboBoxBuildingType.Size = new Size(229, 45);
            comboBoxBuildingType.TabIndex = 5;
            comboBoxBuildingType.Visible = false;
            comboBoxBuildingType.SelectedIndexChanged += comboBoxBuildingType_SelectedIndexChanged;
            // 
            // buildingCatalogBindingSource
            // 
            buildingCatalogBindingSource.DataSource = typeof(BuildingCatalog);
            // 
            // btnDestroyBuilding
            // 
            btnDestroyBuilding.Location = new Point(289, 300);
            btnDestroyBuilding.Name = "btnDestroyBuilding";
            btnDestroyBuilding.Size = new Size(231, 104);
            btnDestroyBuilding.TabIndex = 6;
            btnDestroyBuilding.Text = "DestroyBuilding";
            btnDestroyBuilding.UseVisualStyleBackColor = true;
            btnDestroyBuilding.Click += btnDestroyBuilding_Click;
            // 
            // comboBoxDestroyType
            // 
            comboBoxDestroyType.FormattingEnabled = true;
            comboBoxDestroyType.Location = new Point(291, 422);
            comboBoxDestroyType.Name = "comboBoxDestroyType";
            comboBoxDestroyType.Size = new Size(229, 45);
            comboBoxDestroyType.TabIndex = 7;
            comboBoxDestroyType.Visible = false;
            comboBoxDestroyType.SelectedIndexChanged += comboBoxDestroyType_SelectedIndexChanged;
            // 
            // btnHireWorker
            // 
            btnHireWorker.Location = new Point(39, 484);
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
            comboBoxChooseWorker.Location = new Point(166, 542);
            comboBoxChooseWorker.Name = "comboBoxChooseWorker";
            comboBoxChooseWorker.Size = new Size(228, 45);
            comboBoxChooseWorker.TabIndex = 9;
            comboBoxChooseWorker.Visible = false;
            // 
            // comboBoxChooseBuildingForWorker
            // 
            comboBoxChooseBuildingForWorker.FormattingEnabled = true;
            comboBoxChooseBuildingForWorker.Location = new Point(39, 593);
            comboBoxChooseBuildingForWorker.Name = "comboBoxChooseBuildingForWorker";
            comboBoxChooseBuildingForWorker.Size = new Size(228, 45);
            comboBoxChooseBuildingForWorker.TabIndex = 10;
            comboBoxChooseBuildingForWorker.Visible = false;
            // 
            // btnFireWorker
            // 
            btnFireWorker.Location = new Point(291, 484);
            btnFireWorker.Name = "btnFireWorker";
            btnFireWorker.Size = new Size(229, 52);
            btnFireWorker.TabIndex = 11;
            btnFireWorker.Text = "FireWorker";
            btnFireWorker.UseVisualStyleBackColor = true;
            btnFireWorker.Click += btnFireWorker_Click;
            
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(15F, 37F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1311, 739);
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
    }
}
