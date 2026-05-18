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

        public Form1()
        {
            InitializeComponent();

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
