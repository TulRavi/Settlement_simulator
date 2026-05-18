using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SettlementGame.Domain;
using System.Windows.Forms;


namespace Windows_Forms_App
{
    public partial class Form1 : Form
    {
        private WorldService _worldService;
        private WorldCreator _worldCreator;

        public Form1()
        {
            InitializeComponent();// создаёт кнопки,поля эт цэтра
            _worldCreator.CreateWorld();


            private void btnTick_Click(object sender, EventArgs e)
        {
            _worldService.Tick(world);
            MessageBox.Show("Tick done");
        }
        var options = new DbContextOptionsBuilder<GameDbContext>()
                .UseSqlite("Data Source=game.db")
                .Options;

            var db = new GameDbContext(options);

            _worldService = new WorldService(
                new DataWorld(),
                new WorldCreator(),
                db,
                new WorkerEmploymentService(db)
            );
        }
    }
}
